# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## SwissWallet (Wasabi Wallet Fork)

SwissWallet is a privacy-focused Bitcoin wallet forked from Wasabi Wallet. It features hardcoded Swiss coordinators, Tor-first connectivity, and multi-platform distribution (macOS ARM64/x64, Windows x64, Linux x64).

## IMPORTANT: Repository Structure

**Working Directory:** `/home/swisswallet/swisswallet/`

This is the single repository directory for all SwissWallet development:
- `/home/swisswallet/swisswallet/` - Main repository directory
- Always work in this directory for all development tasks
- No symlinks, no duplicates - clean and simple structure

**Git Remote:** `git@github.com-swisswallet-bitcoin:swisscodernano/swisswallet.git`
**GitHub Repository:** `github.com/swisscodernano/swisswallet`

All commits should be made to `/home/swisswallet/swisswallet/`.

**Key Coordinators:**
- Primary (Tor): `rhuvjl2kosdi3xgnmkr4bwnvpmlsvupajkubuazxendgtorvi2q4nhyd.onion`
- Fallback (HTTPS): `https://wasabi.swisscoordinator.app`

## Essential Commands

### Building

```bash
# Quick development build (desktop GUI)
cd WalletWasabi.Fluent.Desktop
dotnet build

# Run desktop app (from project directory)
dotnet run

# Build all platforms (macOS ARM64/x64, Windows, Linux)
./Contrib/build-all.sh

# Build specific platform
./Contrib/build-macos.sh      # macOS ARM64 + x64
./Contrib/build-windows.sh    # Windows x64
./Contrib/build-linux.sh      # Linux x64 + .deb
```

**Build outputs:** `packages/SwissWallet-*`

### Testing

```bash
# Run all tests
dotnet test WalletWasabi.Tests/WalletWasabi.Tests.csproj

# Run specific test class
dotnet test --filter "FullyQualifiedName~WalletWasabi.Tests.UnitTests.Crypto.CryptoTests"

# Run tests with specific category (if tagged)
dotnet test --filter "Category=Unit"
```

**Test structure:**
- `WalletWasabi.Tests/UnitTests/` - Fast, isolated unit tests
- `WalletWasabi.Tests/IntegrationTests/` - Multi-component tests (may use regtest)
- `WalletWasabi.Tests/RegressionTests/` - Regression test suite
- Uses xUnit, Moq for mocking, Avalonia.Headless for UI tests

### Running Components

```bash
# Desktop GUI
cd WalletWasabi.Fluent.Desktop && dotnet run

# Headless daemon
cd WalletWasabi.Daemon && dotnet run

# Backend indexer service
cd WalletWasabi.Backend && dotnet run

# CoinJoin coordinator
cd WalletWasabi.Coordinator && dotnet run
```

## Architecture Overview

### Project Structure

**Core Projects:**
- **WalletWasabi** - Core library (Bitcoin ops, WabiSabi protocol, services, no UI)
- **WalletWasabi.Daemon** - Headless mode & service orchestration via `Global` class
- **WalletWasabi.Fluent** - Avalonia UI framework (MVVM with ReactiveUI)
- **WalletWasabi.Fluent.Desktop** - Desktop app entry point
- **WalletWasabi.Backend** - Indexer service (block filters, blockchain data)
- **WalletWasabi.Coordinator** - CoinJoin coordinator (WabiSabi protocol)
- **WalletWasabi.Tests** - Comprehensive test suite

### Key Architectural Patterns

1. **Service Composition (not DI container)**
   - `WalletWasabi.Daemon/Global.cs` - Manual service composition root
   - `HostedServices` - Background service lifecycle management
   - `EventBus` - Type-safe pub/sub for cross-service communication

2. **MVVM (Fluent UI)**
   - ViewModels inherit `ViewModelBase` (ReactiveUI's `ReactiveObject`)
   - Models bridge core `Wallet` logic to UI (e.g., `WalletModel`, `WalletCoinsModel`)
   - Heavy use of `IObservable<T>` for reactive data flows

3. **Repository Pattern**
   - `BitcoinStore` - Central data repository
     - `IndexStore` - Block filters (SQLite)
     - `AllTransactionStore` - Transactions (SQLite)
     - `SmartHeaderChain` - In-memory blockchain headers
     - `MempoolService` - Current mempool state

4. **Worker/Service Pattern**
   - Background services: `Synchronizer`, `FeeRateUpdater`, `CoinJoinManager`
   - Functional composition using `Periodically()`, `Continuously()`, `EventDriven()`

### Critical Subsystems

#### CoinJoin/Privacy (WabiSabi)
- **Client:** `WalletWasabi/WabiSabi/Client/CoinJoin/Manager/CoinJoinManager.cs` - Orchestrates CoinJoins
- **Coordinator:** `WalletWasabi/WabiSabi/Coordinator/Arena.cs` - Manages rounds
- **Protocol:** Anonymous credential-based CoinJoins using WabiSabi cryptography

#### Tor Integration
- `TorProcessManager` - Manages Tor daemon lifecycle
- All coordinator/indexer communication can route through Tor
- `OnionHttpClientFactory` - HTTP clients with SOCKS5 Tor routing
- **Swiss Priority:** Tor onion service preferred, HTTPS fallback

#### Bitcoin Core RPC (Optional)
- `RpcClientBase` - Wrapper around NBitcoin's RPCClient
- Used for: Block fetching, filter verification, broadcasting, fee estimation
- `RpcMonitor` - Connection health monitoring

#### Wallet Management
- `WalletWasabi/Wallets/Wallet.cs` - Core wallet (HD keys, UTXO tracking, filters)
- `WalletManager` - Multi-wallet lifecycle management
- States: `Uninitialized → Loading → Starting → Started → Stopping → Stopped`

#### Configuration System
- **Daemon:** `WalletWasabi.Daemon/Config/Config.cs` - JSON config with env var overrides
- **Swiss Hardcoding:** `Config.TryGetCoordinatorUri()` forces Swiss coordinators
- **UI:** `WalletWasabi.Fluent/UiConfig/UiConfig.cs` - UI-specific settings

### Important Files for Context

- `/WalletWasabi.Daemon/Global.cs` - Service composition root
- `/WalletWasabi/Wallets/Wallet.cs` - Core wallet implementation
- `/WalletWasabi/WabiSabi/Client/CoinJoin/Manager/CoinJoinManager.cs` - CoinJoin orchestration
- `/WalletWasabi.Fluent/ViewModels/ViewModelBase.cs` - Base for all ViewModels
- `/WalletWasabi/Services/EventBus.cs` - Event communication backbone
- `/WalletWasabi/Stores/BitcoinStore.cs` - Data access layer
- `/WalletWasabi/Helpers/Constants.cs` - Swiss coordinators & app constants

## SwissWallet Customizations

### Hardcoded Coordinators
The following files enforce Swiss coordinator usage:

1. **Constants** (`WalletWasabi/Helpers/Constants.cs`)
   - Defines Swiss onion and clearnet coordinator URIs

2. **Config Layer** (`WalletWasabi.Daemon/Config/Config.cs`)
   - `TryGetCoordinatorUri()` method enforces Tor-first with clearnet fallback
   - Blocks user modifications to coordinator settings

3. **UI Lock** (`WalletWasabi.Fluent/ViewModels/Settings/CoordinatorTabSettingsViewModel.cs`)
   - Coordinator field is read-only with Swiss branding
   - Displays security messaging

### Build System
- `Contrib/swiss-release.sh` - Main multi-platform build script
- Platform-specific: `build-macos.sh`, `build-windows.sh`, `build-linux.sh`
- Executables renamed: `swisswallet`, `swisswalletd`
- Outputs: ZIP/TAR.GZ archives + Debian .deb package

## Development Notes

### Tech Stack
- **.NET 8.0 SDK** required
- **Avalonia** - Cross-platform UI framework
- **ReactiveUI** - Reactive MVVM
- **NBitcoin** - Bitcoin library
- **SQLite** - Local storage for filters/transactions
- **xUnit** - Testing framework

### Code Style
- C# 10+ features (records, init properties)
- Async/await throughout with `CancellationToken` support
- Immutable data patterns preferred
- Reactive programming via `IObservable<T>`

### Testing Approach
- Unit tests mock dependencies (Moq)
- Integration tests may spin up Bitcoin regtest nodes
- UI tests use Avalonia.Headless
- Test data in `WalletWasabi.Tests/UnitTests/Data/`

### Git Workflow
- Main branch: `master`
- Feature branches for new work
- Recent commits show active development areas

## Network & External Services

- **Bitcoin Network:** Configurable (mainnet/testnet/regtest) via `Config.Network`
- **Indexer:** Provides block filters (default or configurable via `BackendUri`)
- **Coordinator:** WabiSabi CoinJoin coordinator (hardcoded Swiss coordinators)
- **Tor:** Managed Tor daemon for privacy (configurable SOCKS/control ports)
- **Bitcoin Core RPC:** Optional local node integration for block data

## Configuration Files

- `~/.walletwasabi/client/Config.json` - User config (daemon settings)
- `~/.walletwasabi/client/UiConfig.json` - UI preferences
- Both support environment variable overrides (prefix: env vars in `Config.cs`)

## Requirements

- .NET 8.0 SDK
- Git
- ~15GB free disk space for full build
- Tor (bundled, auto-managed)
- Optional: Bitcoin Core for RPC features
