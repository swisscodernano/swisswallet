using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using WalletWasabi.Fluent.Models.UI;
using WalletWasabi.WebClients.Wasabi;

namespace WalletWasabi.Fluent.ViewModels.StatusBar;

/// <summary>
/// ViewModel for displaying Swiss Coordinator connection status in the status bar.
/// Shows current coordinator type (Onion/Clearnet) and connection quality.
/// </summary>
public partial class SwissCoordinatorStatusViewModel : ViewModelBase
{
	[AutoNotify] private string _statusText = "🇨🇭 Connecting...";
	[AutoNotify] private string _coordinatorType = "Unknown";
	[AutoNotify] private string _connectionQuality = "Unknown";
	[AutoNotify] private bool _isOnion = false;
	[AutoNotify] private bool _isConnected = false;

	private readonly SwissCoordinatorHttpClientFactory? _swissCoordinatorFactory;

	public SwissCoordinatorStatusViewModel(UiContext uiContext, SwissCoordinatorHttpClientFactory? swissCoordinatorFactory = null)
	{
		UiContext = uiContext;
		_swissCoordinatorFactory = swissCoordinatorFactory;

		// Monitor coordinator status every 30 seconds
		Observable.Interval(TimeSpan.FromSeconds(30))
			.StartWith(0) // Immediate first check
			.Subscribe(_ => UpdateCoordinatorStatus())
			.DisposeWith(Disposables);
	}

	public UiContext UiContext { get; }

	private void UpdateCoordinatorStatus()
	{
		if (_swissCoordinatorFactory == null)
		{
			StatusText = "🇨🇭 Swiss Coordinator Offline";
			CoordinatorType = "Unavailable";
			ConnectionQuality = "No Connection";
			IsConnected = false;
			IsOnion = false;
			return;
		}

		var currentCoordinator = _swissCoordinatorFactory.CurrentCoordinator;
		var isOnionConnection = currentCoordinator.Type == CoordinatorType.OnionService;

		// Update status based on coordinator type
		IsOnion = isOnionConnection;
		IsConnected = true;

		CoordinatorType = isOnionConnection ? "Tor Onion Service" : "HTTPS Clearnet";

		// Calculate connection quality based on how recent the last switch was
		var timeSinceSwitch = DateTime.UtcNow - currentCoordinator.LastSwitchTime;
		ConnectionQuality = timeSinceSwitch.TotalMinutes switch
		{
			< 1 => "Recently Switched",
			< 10 => "Stable",
			< 60 => "Very Stable",
			_ => "Excellent"
		};

		// Create status text with Swiss flag and appropriate icons
		var icon = isOnionConnection ? "🧅" : "🌐";
		var qualityIcon = ConnectionQuality switch
		{
			"Recently Switched" => "🔄",
			"Stable" => "🟡",
			"Very Stable" => "🟢",
			"Excellent" => "💚",
			_ => "⚪"
		};

		StatusText = $"🇨🇭 Swiss {icon} {qualityIcon}";
	}

	public string GetDetailedStatus()
	{
		if (_swissCoordinatorFactory == null)
		{
			return "Swiss Coordinator service is not available";
		}

		var coordinator = _swissCoordinatorFactory.CurrentCoordinator;
		return $"""
		       🇨🇭 Swiss Coordinator Status:

		       Type: {CoordinatorType}
		       Quality: {ConnectionQuality}
		       Endpoint: {coordinator.Uri.Host}
		       Last Switch: {(coordinator.LastSwitchTime == DateTime.MinValue ? "Never" : coordinator.LastSwitchTime.ToString("HH:mm:ss"))}

		       Priority: {(IsOnion ? "Onion service (preferred)" : "Clearnet fallback")}
		       Security: Maximum Swiss protection enabled
		       """;
	}
}