# 🇨🇭 SwissWallet - Swiss Privacy Bitcoin Wallet

<p align="center">
  <img src="https://raw.githubusercontent.com/swisscodernano/swisswallet/master/WalletWasabi.Fluent.Desktop/Assets/WasabiLogo.ico" width="200">
</p>

<h3 align="center">
    Bitcoin Privacy with Swiss Precision
</h3>

<p align="center">
  <strong>Hardcoded Swiss Coordinators • Tor-First Connection • Maximum Privacy</strong>
</p>

<p align="center">
  <a href="#-downloads">Downloads</a> •
  <a href="#-why-swisswallet">Why SwissWallet</a> •
  <a href="#-build-from-source">Build</a> •
  <a href="#-credits">Credits</a>
</p>

---

## 🎯 Why SwissWallet?

SwissWallet is a **privacy-focused Bitcoin wallet** built on the proven [Wasabi Wallet](https://wasabiwallet.io) architecture, enhanced with **Swiss security standards** and **hardcoded privacy features**.

### ✅ What Makes SwissWallet Different

| Feature | Wasabi Wallet | SwissWallet |
|---------|---------------|-------------|
| **Coordinators** | User-configurable | 🔒 **Hardcoded Swiss coordinators** |
| **Connection Priority** | Clearnet first | 🧅 **Tor-first (onion priority)** |
| **Data Directory** | `~/.walletwasabi/` | 📁 **Separate `~/.swisswallet/`** |
| **Updates** | Auto-update to Wasabi | ⚙️ **Manual via GitHub releases** |
| **Branding** | Wasabi green | 🇨🇭 **Swiss red (#DC143C)** |
| **Use Case** | General privacy | 🛡️ **Maximum privacy guarantee** |

### 🔒 Swiss Security Features

#### **1. Hardcoded Coordinators** *(Cannot be changed)*
```
Primary (Tor):    rhuvjl2kosdi3xgnmkr4bwnvpmlsvupajkubuazxendgtorvi2q4nhyd.onion
Fallback (HTTPS): wasabi.swisscoordinator.app
```
- ✅ **Prevents phishing attacks** - users cannot be tricked into using malicious coordinators
- ✅ **Swiss infrastructure** - coordinators hosted with Swiss privacy standards
- ✅ **Trust by design** - no configuration needed, works out of the box

#### **2. Tor-First Architecture**
- SwissWallet always **tries Tor onion service first** before falling back to HTTPS
- Wasabi tries clearnet first, then Tor
- **Maximum anonymity** by default, not by configuration

#### **3. Independent Data Directory**
- SwissWallet uses `~/.swisswallet/` (Linux/macOS) or `%APPDATA%\SwissWallet\` (Windows)
- **Run both Wasabi and SwissWallet** simultaneously without conflicts
- Separate wallets, config, and privacy sets

---

## 📥 Downloads

### Latest Release: [v2.1.0](https://github.com/swisscodernano/swisswallet/releases/latest)

#### macOS
- **ARM64 (Apple Silicon):** `SwissWallet-X.X.X-macOS-arm64.dmg` 🍎 *Recommended for M1/M2/M3 Macs*
- **x64 (Intel Mac):** `SwissWallet-X.X.X-macOS-x64.dmg`

#### Windows
- **x64:** `SwissWallet-X.X.X-win-x64.zip`

#### Linux
- **x64 ZIP:** `SwissWallet-X.X.X-linux-x64.zip`
- **x64 TAR.GZ:** `SwissWallet-X.X.X-linux-x64.tar.gz`
- **Debian Package:** `SwissWallet-X.X.X.deb`

### Installation

#### macOS
1. Download the `.dmg` file for your Mac (ARM64 for Apple Silicon, x64 for Intel)
2. Open the `.dmg` file
3. Drag `SwissWallet.app` to the Applications folder
4. **First launch:** Right-click → Open (to bypass Gatekeeper)

#### Windows
1. Download `SwissWallet-X.X.X-win-x64.zip`
2. Extract to desired location
3. Run `swisswallet.exe`

#### Linux (Debian/Ubuntu)
```bash
sudo dpkg -i SwissWallet-X.X.X.deb
```

#### Linux (Other Distros)
```bash
# Extract and run
tar -xzf SwissWallet-X.X.X-linux-x64.tar.gz
cd linux-x64
./swisswallet
```

---

## 🛠️ Build from Source

### Requirements

1. **Git:** https://git-scm.com/downloads
2. **.NET 8.0 SDK:** https://dotnet.microsoft.com/download
3. **(Optional)** Disable .NET telemetry:
   - Linux/macOS: `export DOTNET_CLI_TELEMETRY_OPTOUT=1`
   - Windows: `setx DOTNET_CLI_TELEMETRY_OPTOUT 1`

### Quick Build (Desktop GUI)

```bash
# Clone repository
git clone https://github.com/swisscodernano/swisswallet.git
cd swisswallet

# Build and run
cd WalletWasabi.Fluent.Desktop
dotnet build
dotnet run
```

### Build All Platforms

```bash
# Build for all platforms (macOS, Windows, Linux)
./Contrib/build-all.sh

# Build specific platform
./Contrib/build-macos.sh      # macOS ARM64 + x64
./Contrib/build-windows.sh    # Windows x64
./Contrib/build-linux.sh      # Linux x64 + .deb

# Packages will be in: ./packages/
```

### Run Tests

```bash
dotnet test WalletWasabi.Tests/WalletWasabi.Tests.csproj
```

---

## 🇨🇭 What is "Swiss" About SwissWallet?

### 🔐 **Security by Default**
- Like Swiss bank vaults, SwissWallet **locks down critical security settings**
- Hardcoded coordinators eliminate user error and phishing attacks
- Tor-first approach ensures privacy without configuration

### 🎯 **Precision Engineering**
- Swiss precision in code: **deterministic builds**, strict testing
- Clean architecture based on Wasabi's battle-tested codebase
- No compromises on privacy or security

### 🛡️ **Privacy Guarantee**
- Swiss coordinators operated with **Swiss privacy standards**
- No telemetry, no tracking, no data collection
- Your funds, your privacy, **your sovereignty**

---

## 📖 Documentation

- **[SwissWallet Roadmap](SWISSWALLET_ROADMAP.md)** - Development plans and timeline
- **[Swiss Security Whitepaper](SWISS_SECURITY_WHITEPAPER.md)** *(Coming soon)* - Technical security details
- **[Wasabi Documentation](https://docs.wasabiwallet.io/)** - General wallet usage (applies to SwissWallet)

---

## 🤝 Credits

### Built on Wasabi Wallet

SwissWallet is a **fork** of [Wasabi Wallet](https://wasabiwallet.io), created by zkSNACKs Ltd.

**We stand on the shoulders of giants:**
- ✅ **WabiSabi Protocol** - Privacy-preserving CoinJoin implementation
- ✅ **Battle-tested codebase** - Years of production use and audits
- ✅ **Open source** - Transparent, auditable, community-driven

**Original Wasabi Wallet:**
- Website: https://wasabiwallet.io
- GitHub: https://github.com/WalletWasabi/WalletWasabi
- Documentation: https://docs.wasabiwallet.io
- License: MIT

**SwissWallet Enhancements:**
- © 2025 Swiss Security Labs
- Hardcoded coordinators, Tor-first architecture, Swiss branding
- License: MIT (same as Wasabi)

### Why Fork Wasabi?

We **love** Wasabi Wallet! It's the best privacy wallet available. SwissWallet exists because:

1. **Hardcoded security** - Some users want **zero configuration** and **maximum trust**
2. **Swiss focus** - Dedicated Swiss infrastructure and privacy standards
3. **Tor-first** - Privacy by default, not by choice
4. **Independence** - Run alongside Wasabi without conflicts

**We encourage you to also try [Wasabi Wallet](https://wasabiwallet.io)** if you prefer configurability!

---

## 🔧 Technical Architecture

### Core Components

- **WalletWasabi** - Core library (Bitcoin operations, WabiSabi protocol)
- **WalletWasabi.Daemon** - Headless mode & service orchestration
- **WalletWasabi.Fluent** - Avalonia UI framework (MVVM + ReactiveUI)
- **WalletWasabi.Fluent.Desktop** - Desktop app entry point

### Swiss Modifications

1. **`WalletWasabi.Daemon/Config/Config.cs`**
   - Data directory: `SwissWallet` instead of `WalletWasabi`
   - Coordinator enforcement via `TryGetCoordinatorUri()`

2. **`WalletWasabi.Daemon/Global.cs`**
   - Update Manager disabled (no Wasabi version checks)

3. **`WalletWasabi/Helpers/Constants.cs`**
   - Swiss coordinator URIs (onion + clearnet)
   - Swiss branding constants

4. **`WalletWasabi.Fluent/ViewModels/Settings/CoordinatorTabSettingsViewModel.cs`**
   - Coordinator field read-only
   - Swiss security messaging

5. **`Directory.Build.props`**
   - `InvariantGlobalization` disabled (fixes macOS launch)

---

## 🌐 Network & Privacy

### Bitcoin Network
- **Mainnet** by default
- Testnet/Regtest support for development

### Tor Integration
- **Bundled Tor daemon** - no external Tor installation needed
- **SOCKS5 proxy** on port 9050 (default)
- **Onion-first connection** to Swiss coordinators

### CoinJoin Coordinators
```
🇨🇭 Primary (Tor):
http://rhuvjl2kosdi3xgnmkr4bwnvpmlsvupajkubuazxendgtorvi2q4nhyd.onion

🇨🇭 Fallback (HTTPS):
https://wasabi.swisscoordinator.app
```

---

## 🐛 Bug Reports & Support

### Found a Bug?
- **SwissWallet-specific issues:** [Open an issue](https://github.com/swisscodernano/swisswallet/issues)
- **Wasabi core issues:** Report to [Wasabi Wallet](https://github.com/WalletWasabi/WalletWasabi/issues)

### Need Help?
- **Wasabi Documentation:** https://docs.wasabiwallet.io/ (most features identical)
- **Discussions:** [GitHub Discussions](https://github.com/swisscodernano/swisswallet/discussions)

---

## 📜 License

SwissWallet is open source software licensed under the **MIT License**, same as Wasabi Wallet.

```
MIT License

Copyright (c) 2025 Swiss Security Labs (SwissWallet enhancements)
Copyright (c) 2018-2024 zkSNACKs Ltd (Original Wasabi Wallet)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

---

## 🚀 Roadmap

See [SWISSWALLET_ROADMAP.md](SWISSWALLET_ROADMAP.md) for detailed development plans.

**Current Status:** v2.1.0 - Swiss Independence
- ✅ macOS launch fixed
- ✅ Separate data directory (`~/.swisswallet/`)
- ✅ Update manager disabled
- ✅ Coordinator read-only UI

**Next Milestones:**
- 🔄 v3.0.0 "Swiss Edition" - Full Swiss branding and UI redesign
- 🔄 Swiss logo and visual identity
- 🔄 Swiss-themed UI components
- 🔄 Swiss security documentation

---

<p align="center">
  <strong>🇨🇭 Built with Swiss Precision for Bitcoin Privacy 🇨🇭</strong>
</p>

<p align="center">
  <sub>SwissWallet • Hardcoded Security • Tor-First Privacy • Maximum Anonymity</sub>
</p>
