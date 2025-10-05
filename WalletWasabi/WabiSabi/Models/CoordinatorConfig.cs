using System.Text.Json.Serialization;

namespace WalletWasabi.WabiSabi.Models;

/// <summary>
/// Configuration parameters provided by the Swiss Coordinator.
/// Fetched from /api/config endpoint.
/// </summary>
public record CoordinatorConfig
{
	/// <summary>
	/// Coordinator fee rate (as decimal, e.g., 0.003 = 0.3%)
	/// </summary>
	[JsonPropertyName("coordinatorFeeRate")]
	public decimal CoordinatorFeeRate { get; init; }

	/// <summary>
	/// Recommended mining fee rate in sat/vB.
	/// This is the Swiss Coordinator's recommendation based on current mempool conditions.
	/// </summary>
	[JsonPropertyName("recommendedMiningFeeRate")]
	public decimal RecommendedMiningFeeRate { get; init; }

	/// <summary>
	/// Minimum suggested mining fee rate in sat/vB.
	/// </summary>
	[JsonPropertyName("minMiningFeeRate")]
	public decimal MinMiningFeeRate { get; init; }

	/// <summary>
	/// Maximum suggested mining fee rate in sat/vB.
	/// </summary>
	[JsonPropertyName("maxMiningFeeRate")]
	public decimal MaxMiningFeeRate { get; init; }

	/// <summary>
	/// Returns true if the recommended fee rate is within valid bounds.
	/// </summary>
	public bool IsValid =>
		RecommendedMiningFeeRate >= MinMiningFeeRate &&
		RecommendedMiningFeeRate <= MaxMiningFeeRate &&
		CoordinatorFeeRate >= 0 && CoordinatorFeeRate <= 1;
}
