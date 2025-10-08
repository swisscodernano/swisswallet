using System.Collections.Generic;
using System.Linq;
using NBitcoin;
using ReactiveUI;
using WalletWasabi.Blockchain.TransactionBroadcasting;
using WalletWasabi.FeeRateEstimation;
using WalletWasabi.Fluent.Infrastructure;
using WalletWasabi.Fluent.Models.UI;
using WalletWasabi.Fluent.Validation;
using WalletWasabi.Fluent.ViewModels.Navigation;
using WalletWasabi.Models;
using WalletWasabi.Wallets.Exchange;

namespace WalletWasabi.Fluent.ViewModels.Settings;

[AppLifetime]
[NavigationMetaData(
	Title = "Connections",
	Caption = "🇨🇭 Swiss Infrastructure - Privacy First",
	Order = 3,
	Category = "Settings",
	Keywords = new[]
	{
			"Settings", "Connections", "Backend", "URI", "Exchange", "Rate", "Provider", "Fee", "Estimation", "Network", "Anonymization",
			"Tor", "Terminate", "Swiss", "Indexer"
	},
	IconName = "settings_general_regular")]
public partial class ConnectionsSettingsTabViewModel : RoutableViewModel
{
	[AutoNotify] private string _indexerUri;
	[AutoNotify] private string _swissIndexerInfo;

	public ConnectionsSettingsTabViewModel(IApplicationSettings settings)
	{
		Settings = settings;

		// SwissWallet: Always display Swiss indexer Tor address
		_indexerUri = WalletWasabi.Helpers.Constants.SwissIndexerOnion.Replace("http://", "");
		_swissIndexerInfo = "🔒 SwissWallet uses hardcoded Swiss indexer for maximum privacy.\n" +
		                    "Indexer (Tor): 2jaslypvb6pyeret7zextmvbvvs4mqzvwsodihisozys7ecy6aqp4bid.onion\n\n" +
		                    "This provides block filters and blockchain data through Swiss infrastructure.";

		if (settings.Network == Network.Main)
		{
			ExternalBroadcastProviders = ExternalTransactionBroadcaster.Providers.Select(x => x.Name);
		}
		else
		{
			ExternalBroadcastProviders = ExternalTransactionBroadcaster.TestNet4Providers.Select(x => x.Name);
		}

		// SwissWallet: Indexer URI validation disabled as it's hardcoded
		// this.ValidateProperty(x => x.IndexerUri, ValidateIndexerUri);

		this.WhenAnyValue(x => x.Settings.IndexerUri)
			.Subscribe(x => IndexerUri = WalletWasabi.Helpers.Constants.SwissIndexerOnion.Replace("http://", ""));
	}

	public bool IsReadOnly => Settings.IsOverridden;

	// SwissWallet: Indexer is always read-only (hardcoded for privacy)
	public bool IsIndexerLocked => true;

	public IApplicationSettings Settings { get; }

	public IEnumerable<string> ExchangeRateProviders => WalletWasabi.Wallets.Exchange.ExchangeRateProviders.Providers;
	public IEnumerable<string> FeeRateEstimationProviders => FeeRateProviders.Providers;
	public IEnumerable<string> ExternalBroadcastProviders { get; }

	public IEnumerable<TorMode> TorModes =>
		Enum.GetValues(typeof(TorMode)).Cast<TorMode>();

	private void ValidateIndexerUri(IValidationErrors errors)
	{
		var indexerUri = IndexerUri;

		if (string.IsNullOrEmpty(indexerUri))
		{
			return;
		}

		if (!Uri.TryCreate(indexerUri, UriKind.Absolute, out _))
		{
			errors.Add(ErrorSeverity.Error, "Invalid URI.");
			return;
		}

		Settings.IndexerUri = indexerUri;
	}
}
