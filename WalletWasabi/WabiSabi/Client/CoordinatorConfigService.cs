using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WalletWasabi.Helpers;
using WalletWasabi.Logging;
using WalletWasabi.WabiSabi.Models;

namespace WalletWasabi.WabiSabi.Client;

/// <summary>
/// Service for fetching dynamic configuration from Swiss Coordinator.
/// Provides recommended fee rates and other parameters with local fallback.
/// </summary>
public class CoordinatorConfigService
{
	private readonly IHttpClientFactory _httpClientFactory;
	private CoordinatorConfig? _cachedConfig;
	private DateTime _lastFetch = DateTime.MinValue;
	private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);
	private readonly SemaphoreSlim _fetchLock = new(1, 1);

	public CoordinatorConfigService(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	/// <summary>
	/// Gets the recommended mining fee rate from coordinator, with local fallback.
	/// </summary>
	public async Task<decimal> GetRecommendedMiningFeeRateAsync(CancellationToken cancellationToken = default)
	{
		var config = await GetConfigAsync(cancellationToken);
		return config?.RecommendedMiningFeeRate ?? Constants.DefaultMaxCoinJoinMiningFeeRate;
	}

	/// <summary>
	/// Gets the full coordinator configuration, with caching.
	/// </summary>
	public async Task<CoordinatorConfig?> GetConfigAsync(CancellationToken cancellationToken = default)
	{
		// Check cache
		if (_cachedConfig is not null && DateTime.UtcNow - _lastFetch < _cacheDuration)
		{
			return _cachedConfig;
		}

		// Prevent concurrent fetches
		await _fetchLock.WaitAsync(cancellationToken);
		try
		{
			// Double-check cache (another thread might have fetched while we waited)
			if (_cachedConfig is not null && DateTime.UtcNow - _lastFetch < _cacheDuration)
			{
				return _cachedConfig;
			}

			return await FetchConfigFromCoordinatorAsync(cancellationToken);
		}
		finally
		{
			_fetchLock.Release();
		}
	}

	private async Task<CoordinatorConfig?> FetchConfigFromCoordinatorAsync(CancellationToken cancellationToken)
	{
		try
		{
			var httpClient = _httpClientFactory.CreateClient("config-fetch");
			httpClient.Timeout = TimeSpan.FromSeconds(10);

			Logger.LogDebug("🇨🇭 Fetching coordinator config from /api/config...");

			var response = await httpClient.GetAsync("/api/config", cancellationToken);

			if (!response.IsSuccessStatusCode)
			{
				Logger.LogWarning($"🇨🇭 Failed to fetch coordinator config: HTTP {response.StatusCode}");
				return null;
			}

			var json = await response.Content.ReadAsStringAsync(cancellationToken);
			var config = JsonSerializer.Deserialize<CoordinatorConfig>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			if (config is null || !config.IsValid)
			{
				Logger.LogWarning($"🇨🇭 Invalid coordinator config received");
				return null;
			}

			_cachedConfig = config;
			_lastFetch = DateTime.UtcNow;

			Logger.LogInfo($"🇨🇭 Coordinator config fetched successfully: " +
			              $"Recommended fee: {config.RecommendedMiningFeeRate} sat/vB, " +
			              $"Coordinator fee: {config.CoordinatorFeeRate * 100:F2}%");

			return config;
		}
		catch (Exception ex)
		{
			Logger.LogWarning($"🇨🇭 Failed to fetch coordinator config: {ex.Message}");
			return null;
		}
	}

	/// <summary>
	/// Clears the cached configuration, forcing a fresh fetch on next request.
	/// </summary>
	public void InvalidateCache()
	{
		_cachedConfig = null;
		_lastFetch = DateTime.MinValue;
	}
}
