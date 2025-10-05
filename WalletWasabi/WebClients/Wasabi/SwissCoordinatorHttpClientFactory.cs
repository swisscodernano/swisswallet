using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WalletWasabi.Helpers;
using WalletWasabi.Logging;

namespace WalletWasabi.WebClients.Wasabi;

/// <summary>
/// Swiss Coordinator HttpClient Factory with automatic failover between Onion and Clearnet coordinators.
/// Implements Swiss security standards with automatic priority-based coordinator switching.
/// </summary>
public class SwissCoordinatorHttpClientFactory : IHttpClientFactory
{
	private readonly HttpClientFactory _internalHttpClientFactory;
	private readonly object _lock = new();
	private CoordinatorInfo _currentCoordinator;
	private DateTime _lastFailoverCheck = DateTime.MinValue;
	private readonly TimeSpan _healthCheckInterval = TimeSpan.FromMinutes(2);
	private readonly TimeSpan _failoverCooldown = TimeSpan.FromSeconds(30);

	// Swiss Coordinator Configuration
	private readonly CoordinatorInfo[] _swissCoordinators = new[]
	{
		new CoordinatorInfo(new Uri(Constants.SwissCoordinatorOnion), CoordinatorType.OnionService, Priority: 1),
		new CoordinatorInfo(new Uri(Constants.SwissCoordinatorClearnet), CoordinatorType.Clearnet, Priority: 2)
	};

	public SwissCoordinatorHttpClientFactory(HttpClientFactory internalHttpClientFactory)
	{
		_internalHttpClientFactory = internalHttpClientFactory;
		_internalHttpClientFactory.AddLifetimeResolver(ResolveLifetimeByIdentity);

		// Start with highest priority coordinator (Onion)
		_currentCoordinator = _swissCoordinators.OrderBy(x => x.Priority).First();
		Logger.LogInfo($"🇨🇭 Swiss Coordinator initialized with {_currentCoordinator.Type}: {_currentCoordinator.Uri}");
	}

	public HttpClient CreateClient(string name)
	{
		// Check if we need to perform health check and potential failover
		CheckCoordinatorHealth();

		var httpClient = _internalHttpClientFactory.CreateClient(name);
		httpClient.BaseAddress = _currentCoordinator.Uri;
		httpClient.DefaultRequestVersion = HttpVersion.Version11;
		httpClient.DefaultRequestHeaders.UserAgent.Clear();

		// Add Swiss security headers
		httpClient.DefaultRequestHeaders.Add("X-Swiss-Security", "enabled");
		httpClient.DefaultRequestHeaders.Add("X-Coordinator-Type", _currentCoordinator.Type.ToString());

		return httpClient;
	}

	public CoordinatorInfo CurrentCoordinator => _currentCoordinator;

	private void CheckCoordinatorHealth()
	{
		if (DateTime.UtcNow - _lastFailoverCheck < _healthCheckInterval)
		{
			return;
		}

		lock (_lock)
		{
			// Double-check pattern
			if (DateTime.UtcNow - _lastFailoverCheck < _healthCheckInterval)
			{
				return;
			}

			_lastFailoverCheck = DateTime.UtcNow;

			// Always prefer Onion service if available
			var preferredCoordinator = _swissCoordinators.OrderBy(x => x.Priority).First();

			if (_currentCoordinator.Uri != preferredCoordinator.Uri)
			{
				// Check if we can switch back to preferred coordinator
				Task.Run(async () => await TryFailoverToPreferred());
			}
		}
	}

	private async Task TryFailoverToPreferred()
	{
		var preferredCoordinator = _swissCoordinators.OrderBy(x => x.Priority).First();

		if (_currentCoordinator.Uri == preferredCoordinator.Uri)
		{
			return; // Already using preferred
		}

		try
		{
			// Test connection to preferred coordinator
			if (await TestCoordinatorConnection(preferredCoordinator))
			{
				SwitchCoordinator(preferredCoordinator, "Preferred coordinator is available");
			}
		}
		catch (Exception ex)
		{
			Logger.LogDebug($"🇨🇭 Swiss Coordinator health check failed for {preferredCoordinator.Type}: {ex.Message}");
		}
	}

	private async Task<bool> TestCoordinatorConnection(CoordinatorInfo coordinator)
	{
		try
		{
			using var httpClient = _internalHttpClientFactory.CreateClient("health-check");
			httpClient.BaseAddress = coordinator.Uri;
			httpClient.Timeout = TimeSpan.FromSeconds(10);

			// Simple health check - try to connect to coordinator status endpoint
			var response = await httpClient.GetAsync("/api/v4/status", CancellationToken.None);
			return response.IsSuccessStatusCode;
		}
		catch
		{
			return false;
		}
	}

	public bool TryFailover(Exception lastException)
	{
		// Don't failover too frequently
		if (DateTime.UtcNow - _currentCoordinator.LastSwitchTime < _failoverCooldown)
		{
			return false;
		}

		lock (_lock)
		{
			// Find next available coordinator
			var availableCoordinators = _swissCoordinators
				.Where(c => c.Uri != _currentCoordinator.Uri)
				.OrderBy(c => c.Priority)
				.ToArray();

			if (!availableCoordinators.Any())
			{
				Logger.LogWarning("🇨🇭 Swiss Coordinator failover: No alternative coordinators available");
				return false;
			}

			var nextCoordinator = availableCoordinators.First();
			SwitchCoordinator(nextCoordinator, $"Failover due to: {lastException.Message}");
			return true;
		}
	}

	private void SwitchCoordinator(CoordinatorInfo newCoordinator, string reason)
	{
		var previousCoordinator = _currentCoordinator;
		_currentCoordinator = newCoordinator with { LastSwitchTime = DateTime.UtcNow };

		Logger.LogWarning($"🇨🇭 Swiss Coordinator switched: {previousCoordinator.Type} → {newCoordinator.Type}");
		Logger.LogInfo($"🇨🇭 Reason: {reason}");
		Logger.LogInfo($"🇨🇭 New coordinator: {newCoordinator.Uri}");
	}

	private DateTime ResolveLifetimeByIdentity(string name)
	{
		var identity = name.Split('-', StringSplitOptions.RemoveEmptyEntries).First();
		var lifetime = TimeSpan.FromSeconds(identity switch
		{
			"bob" => 40,
			"alice" => 1.5 * 3_600,
			"satoshi" => int.MaxValue,
			"health-check" => 30, // Short-lived for health checks
			_ => int.MaxValue,
		});
		return DateTime.UtcNow.Add(lifetime);
	}
}

public record CoordinatorInfo(Uri Uri, CoordinatorType Type, int Priority)
{
	public DateTime LastSwitchTime { get; init; } = DateTime.MinValue;
}

public enum CoordinatorType
{
	OnionService,
	Clearnet
}