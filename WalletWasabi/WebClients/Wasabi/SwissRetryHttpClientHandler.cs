using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using WalletWasabi.Logging;

namespace WalletWasabi.WebClients.Wasabi;

/// <summary>
/// Swiss-enhanced HTTP client handler with coordinator failover capabilities.
/// Extends the standard retry mechanism with Swiss coordinator switching logic.
/// </summary>
public class SwissRetryHttpClientHandler : NotifyHttpClientHandler
{
	private readonly HttpClientHandlerConfiguration _config;
	private readonly SwissCoordinatorHttpClientFactory _coordinatorFactory;

	public SwissRetryHttpClientHandler(
		string name,
		Action<string> disposedCallback,
		HttpClientHandlerConfiguration config,
		SwissCoordinatorHttpClientFactory coordinatorFactory)
		: base(name, disposedCallback)
	{
		_config = config;
		_coordinatorFactory = coordinatorFactory;
	}

	protected override async Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken)
	{
		var attempt = 0;
		var originalRequestUri = request.RequestUri;

		while (attempt < _config.MaxAttempts)
		{
			try
			{
				// Update request URI to current coordinator if needed
				if (request.RequestUri?.Host != _coordinatorFactory.CurrentCoordinator.Uri.Host)
				{
					request.RequestUri = new Uri(_coordinatorFactory.CurrentCoordinator.Uri, originalRequestUri?.PathAndQuery ?? "/");
				}

				var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

				switch (response.StatusCode)
				{
					case HttpStatusCode.RequestTimeout:
					case HttpStatusCode.BadGateway:
					case HttpStatusCode.ServiceUnavailable:
					case HttpStatusCode.InternalServerError:
						Logger.LogTrace($"🇨🇭 Swiss Coordinator retry {request.RequestUri} due to {response.ReasonPhrase}");

						// Try failover if this is the last attempt for this coordinator
						if (attempt >= _config.MaxAttempts - 1)
						{
							var exception = new HttpRequestException($"HTTP {response.StatusCode}: {response.ReasonPhrase}");
							if (_coordinatorFactory.TryFailover(exception))
							{
								attempt = 0; // Reset attempts for new coordinator
								continue;
							}
						}

						await Task.Delay(_config.TimeBeforeRetringAfterServerError, cancellationToken).ConfigureAwait(false);
						continue;

					case HttpStatusCode.TooManyRequests:
						Logger.LogTrace($"🇨🇭 Swiss Coordinator retry {request.RequestUri} due to rate limiting");
						await Task.Delay(_config.TimeBeforeRetringAfterTooManyRequests, cancellationToken).ConfigureAwait(false);
						continue;

					default:
						// Success or unretryable error
						if (response.IsSuccessStatusCode)
						{
							Logger.LogTrace($"🇨🇭 Swiss Coordinator request successful via {_coordinatorFactory.CurrentCoordinator.Type}");
						}
						return response;
				}
			}
			catch (Exception ex) when (ShouldRetryOrFailover(ex))
			{
				Logger.LogTrace($"🇨🇭 Swiss Coordinator retry {request.RequestUri} due to {ex.GetType().Name}: {ex.Message}");

				// Try failover on connection errors if this is the last attempt for this coordinator
				if (attempt >= _config.MaxAttempts - 1 && IsConnectionError(ex))
				{
					if (_coordinatorFactory.TryFailover(ex))
					{
						attempt = 0; // Reset attempts for new coordinator
						await Task.Delay(_config.TimeBeforeRetringAfterNetworkError, cancellationToken).ConfigureAwait(false);
						continue;
					}
				}

				await Task.Delay(_config.TimeBeforeRetringAfterNetworkError, cancellationToken).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				// Non-retryable exception, but still try failover once
				if (attempt == 0 && IsConnectionError(ex) && _coordinatorFactory.TryFailover(ex))
				{
					attempt = 0;
					continue;
				}
				throw;
			}
			finally
			{
				attempt++;
			}
		}

		throw new HttpRequestException($"🇨🇭 Swiss Coordinator: Failed to make request '{originalRequestUri}' after {_config.MaxAttempts} attempts on {_coordinatorFactory.CurrentCoordinator.Type}");
	}

	private static bool ShouldRetryOrFailover(Exception ex) =>
		ex switch
		{
			SocketException => true,
			HttpRequestException httpEx => IsRetryableHttpException(httpEx),
			TaskCanceledException => false, // Don't retry timeouts
			{ InnerException: var inner } when inner != null => ShouldRetryOrFailover(inner),
			_ => false
		};

	private static bool IsRetryableHttpException(HttpRequestException httpEx) =>
		httpEx.HttpRequestError switch
		{
			HttpRequestError.ConnectionError or
			HttpRequestError.ProxyTunnelError or
			HttpRequestError.SecureConnectionError or
			HttpRequestError.NameResolutionError or
			HttpRequestError.InvalidResponse or
			HttpRequestError.ResponseEnded => true,
			_ => false
		};

	private static bool IsConnectionError(Exception ex) =>
		ex switch
		{
			SocketException => true,
			HttpRequestException httpEx => IsRetryableHttpException(httpEx),
			{ InnerException: var inner } when inner != null => IsConnectionError(inner),
			_ => false
		};
}