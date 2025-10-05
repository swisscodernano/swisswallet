using System;
using System.Globalization;
using System.Threading.Tasks;
using ReactiveUI;
using WalletWasabi.Fluent.Extensions;
using WalletWasabi.Fluent.Infrastructure;
using WalletWasabi.Fluent.Models.UI;
using WalletWasabi.Fluent.Validation;
using WalletWasabi.Fluent.ViewModels.Navigation;
using WalletWasabi.Helpers;
using WalletWasabi.Models;
using WalletWasabi.WabiSabi.Client;

namespace WalletWasabi.Fluent.ViewModels.Settings;

[AppLifetime]
[NavigationMetaData(
	Title = "Coordinator",
	Caption = "🇨🇭 Swiss Coordinator - Hardcoded for Security",
	Order = 2,
	Category = "Settings",
	Keywords =
	[
		"Settings", "Coordinator", "URI", "Max", "Coinjoin", "Mining", "Fee", "Rate", "Min", "Input", "Count", "Swiss", "Security"
	],
	IconName = "settings_bitcoin_regular")]
public partial class CoordinatorTabSettingsViewModel : RoutableViewModel
{
	[AutoNotify] private string _coordinatorUri;
	[AutoNotify] private string _maxCoinJoinMiningFeeRate;
	[AutoNotify] private string _absoluteMinInputCount;
	[AutoNotify] private string _swissCoordinatorInfo;
	[AutoNotify] private string _recommendedFeeRateInfo;

	public CoordinatorTabSettingsViewModel(IApplicationSettings settings)
	{
		Settings = settings;

		// SwissWallet: Coordinator is hardcoded and cannot be changed
		_swissCoordinatorInfo = "🔒 SwissWallet uses hardcoded Swiss coordinators for maximum security.\n" +
		                        "Primary (Tor): rhuvjl2kosdi3xgnmkr4bwnvpmlsvupajkubuazxendgtorvi2q4nhyd.onion\n" +
		                        "Fallback (HTTPS): wasabi.swisscoordinator.app\n\n" +
		                        "This setting cannot be changed to ensure Swiss privacy standards.";

		_recommendedFeeRateInfo = "💡 Loading recommended values from Swiss Coordinator...";

		// Note: Coordinator URI validation disabled for SwissWallet as it's hardcoded
		// this.ValidateProperty(x => x.CoordinatorUri, ValidateCoordinatorUri);
		this.ValidateProperty(x => x.MaxCoinJoinMiningFeeRate, ValidateMaxCoinJoinMiningFeeRate);
		this.ValidateProperty(x => x.AbsoluteMinInputCount, ValidateAbsoluteMinInputCount);

		_coordinatorUri = settings.CoordinatorUri;
		_maxCoinJoinMiningFeeRate = settings.MaxCoinJoinMiningFeeRate;
		_absoluteMinInputCount = settings.AbsoluteMinInputCount;

		this.WhenAnyValue(
				x => x.Settings.CoordinatorUri,
				x => x.Settings.Network)
			.ToSignal()
			.Subscribe(x => CoordinatorUri = Settings.CoordinatorUri);

		this.WhenAnyValue(x => x.Settings.MaxCoinJoinMiningFeeRate)
			.ToSignal()
			.Subscribe(x => MaxCoinJoinMiningFeeRate = Settings.MaxCoinJoinMiningFeeRate);

		this.WhenAnyValue(x => x.Settings.AbsoluteMinInputCount)
			.ToSignal()
			.Subscribe(x => AbsoluteMinInputCount = Settings.AbsoluteMinInputCount);

		// Fetch recommended values from coordinator
		Task.Run(async () => await LoadRecommendedValues());
	}

	private async Task LoadRecommendedValues()
	{
		try
		{
			var configService = UiContext.Default?.CoordinatorConfigService;
			if (configService is not null)
			{
				var config = await configService.GetConfigAsync();
				if (config is not null && config.IsValid)
				{
					RecommendedFeeRateInfo = $"💡 Recommended: {config.RecommendedMiningFeeRate} sat/vB (from Swiss Coordinator)\n" +
					                         $"   Valid range: {config.MinMiningFeeRate} - {config.MaxMiningFeeRate} sat/vB";
					return;
				}
			}

			// Fallback if service not available or config fetch failed
			RecommendedFeeRateInfo = $"💡 Default: {Constants.DefaultMaxCoinJoinMiningFeeRate} sat/vB\n" +
			                         $"   Range: 10 - 200 sat/vB";
		}
		catch
		{
			RecommendedFeeRateInfo = $"💡 Default: {Constants.DefaultMaxCoinJoinMiningFeeRate} sat/vB";
		}
	}

	// SwissWallet: Coordinator is always read-only (hardcoded for security)
	public bool IsReadOnly => true;

	public bool IsCoordinatorLocked => true; // Swiss feature: coordinator cannot be changed

	public IApplicationSettings Settings { get; }

	private void ValidateCoordinatorUri(IValidationErrors errors)
	{
		var coordinatorUri = CoordinatorUri;

		if (string.IsNullOrEmpty(coordinatorUri))
		{
			return;
		}

		if (!Uri.TryCreate(coordinatorUri, UriKind.Absolute, out _))
		{
			errors.Add(ErrorSeverity.Error, "Invalid URI.");
			return;
		}

		Settings.CoordinatorUri = coordinatorUri;
	}

	private void ValidateMaxCoinJoinMiningFeeRate(IValidationErrors errors)
	{
		var maxCoinJoinMiningFeeRate = MaxCoinJoinMiningFeeRate;

		if (string.IsNullOrEmpty(maxCoinJoinMiningFeeRate))
		{
			return;
		}

		if (!decimal.TryParse(maxCoinJoinMiningFeeRate, out var maxCoinJoinMiningFeeRateDecimal))
		{
			errors.Add(ErrorSeverity.Error, "Invalid number.");
			return;
		}

		if (maxCoinJoinMiningFeeRateDecimal < 1)
		{
			errors.Add(ErrorSeverity.Error, "Mining fee rate must be at least 1 sat/vb");
			return;
		}

		Settings.MaxCoinJoinMiningFeeRate = maxCoinJoinMiningFeeRateDecimal.ToString(CultureInfo.InvariantCulture);
	}

	private void ValidateAbsoluteMinInputCount(IValidationErrors errors)
	{
		var absoluteMinInputCount = AbsoluteMinInputCount;

		if (string.IsNullOrEmpty(absoluteMinInputCount))
		{
			return;
		}

		if (!int.TryParse(absoluteMinInputCount, out var absoluteMinInputCountInt))
		{
			errors.Add(ErrorSeverity.Error, "Invalid number.");
			return;
		}

		if (absoluteMinInputCountInt < Constants.AbsoluteMinInputCount)
		{
			errors.Add(ErrorSeverity.Error, $"Absolute min input count should be at least {Constants.AbsoluteMinInputCount}");
			return;
		}

		Settings.AbsoluteMinInputCount = absoluteMinInputCountInt.ToString(CultureInfo.InvariantCulture);
	}
}
