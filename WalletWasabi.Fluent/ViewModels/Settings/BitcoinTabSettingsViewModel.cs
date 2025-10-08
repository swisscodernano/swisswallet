using System.Collections.Generic;
using System.Reactive.Linq;
using NBitcoin;
using NBitcoin.RPC;
using ReactiveUI;
using WalletWasabi.Fluent.Infrastructure;
using WalletWasabi.Fluent.Models.UI;
using WalletWasabi.Fluent.Validation;
using WalletWasabi.Fluent.ViewModels.Navigation;
using WalletWasabi.Helpers;
using WalletWasabi.Models;

namespace WalletWasabi.Fluent.ViewModels.Settings;

[AppLifetime]
[NavigationMetaData(
	Title = "Bitcoin",
	Caption = "Manage Bitcoin settings",
	Order = 1,
	Category = "Settings",
	Keywords =
	[
		"Settings", "Bitcoin", "Network", "Main", "TestNet", "TestNet4", "RegTest", "Run", "Node", "Core", "Knots", "Version", "Startup",
		"Stop", "Shutdown", "Rpc", "Endpoint", "Dust", "Attack", "Limit"
	],
	IconName = "settings_bitcoin_regular")]
public partial class BitcoinTabSettingsViewModel : RoutableViewModel
{
	[AutoNotify] private string _bitcoinRpcUri;
	[AutoNotify] private string _bitcoinRpcCredentialString;
	[AutoNotify] private string _dustThreshold;

	public BitcoinTabSettingsViewModel(IApplicationSettings settings)
	{
		Settings = settings;

		this.ValidateProperty(x => x.BitcoinRpcUri, ValidateBitcoinRpcUri);
		this.ValidateProperty(x => x.BitcoinRpcCredentialString, ValidateBitcoinRpcCredentialString);
		this.ValidateProperty(x => x.DustThreshold, ValidateDustThreshold);

		_bitcoinRpcUri = settings.BitcoinRpcUri;
		// SwissWallet: NEVER show saved RPC credentials in UI for security
		_bitcoinRpcCredentialString = string.Empty;
		_dustThreshold = settings.DustThreshold;

		this.WhenAnyValue(x => x.Settings.BitcoinRpcUri)
			.Subscribe(x => BitcoinRpcUri = x);

		// SwissWallet: Never update UI with saved credentials - force re-entry for security
		// this.WhenAnyValue(x => x.Settings.BitcoinRpcCredentialString)
		//	.Subscribe(x => BitcoinRpcCredentialString = x);

		// SwissWallet: Save credentials immediately when user types valid format
		this.WhenAnyValue(x => x.BitcoinRpcCredentialString)
			.Where(x => !string.IsNullOrWhiteSpace(x))
			.Where(x => RPCCredentialString.TryParse(x, out _))
			.Subscribe(x => Settings.BitcoinRpcCredentialString = x);

		this.WhenAnyValue(x => x.Settings.DustThreshold)
			.Subscribe(x => DustThreshold = x);
	}

	public bool IsReadOnly => Settings.IsOverridden;

	public IApplicationSettings Settings { get; }

	public IEnumerable<Network> Networks { get; } = new[] { Network.Main, Network.TestNet, Network.RegTest };

	private void ValidateBitcoinRpcUri(IValidationErrors errors)
	{
		if (!string.IsNullOrWhiteSpace(BitcoinRpcUri))
		{
			if (!Uri.TryCreate(BitcoinRpcUri, UriKind.Absolute, out _))
			{
				errors.Add(ErrorSeverity.Error, "Invalid bitcoin rpc uri.");
			}
			else
			{
				Settings.BitcoinRpcUri = BitcoinRpcUri;
			}
		}
	}

	private void ValidateBitcoinRpcCredentialString(IValidationErrors errors)
	{
		// SwissWallet: Only save if user entered new credentials (field is not empty)
		if (string.IsNullOrWhiteSpace(BitcoinRpcCredentialString))
		{
			// Empty field = user hasn't entered anything, don't overwrite existing credentials
			// DO NOT clear existing saved credentials
			return;
		}

		// User entered something, validate and save it
		if (RPCCredentialString.TryParse(BitcoinRpcCredentialString, out _))
		{
			// Valid credentials format, save them
			Settings.BitcoinRpcCredentialString = BitcoinRpcCredentialString;
		}
		else
		{
			errors.Add(ErrorSeverity.Error, "Invalid bitcoin rpc credential string.");
		}
	}

	private void ValidateDustThreshold(IValidationErrors errors)
	{
		var dustThreshold = DustThreshold;
		if (!string.IsNullOrWhiteSpace(dustThreshold))
		{
			bool error = false;

			if (!string.IsNullOrEmpty(dustThreshold) && dustThreshold.Contains(
				',',
				StringComparison.InvariantCultureIgnoreCase))
			{
				error = true;
				errors.Add(ErrorSeverity.Error, "Use decimal point instead of comma.");
			}

			if (!decimal.TryParse(dustThreshold, out var dust) || dust < 0)
			{
				error = true;
				errors.Add(ErrorSeverity.Error, "Invalid dust attack limit.");
			}

			if (!error)
			{
				Settings.DustThreshold = dustThreshold;
			}
		}
	}
}
