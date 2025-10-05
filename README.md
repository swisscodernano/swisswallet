# 🇨🇭 SwissWallet — Bitcoin Privacy with Swiss Precision

<p align="center">
  <img src="Contrib/Assets/Swiss/SwissWallet.svg" width="200" alt="SwissWallet Logo">
</p>

<p align="center">
  <strong>Your Bitcoin. Your Privacy. Your Sovereignty.</strong>
</p>

<p align="center">
  <a href="#-why-swiss">Why Swiss?</a> •
  <a href="#-features">Features</a> •
  <a href="#-download">Download</a> •
  <a href="#-quick-start">Quick Start</a> •
  <a href="#-documentation">Documentation</a>
</p>

---

## 🇨🇭 Why Swiss?

**SwissWallet is Bitcoin privacy done the Swiss way** — with unwavering commitment to neutrality, sovereignty, and precision.

We forked Wasabi Wallet to provide Swiss citizens (and privacy-conscious global users) with a Bitcoin wallet that embodies **Swiss values**:

- 🏔️ **Swiss Coordinators** — Your CoinJoin privacy processed on Swiss infrastructure, under Swiss law
- 🔒 **No Foreign Dependencies** — Hardcoded Swiss servers, no trust in external entities
- 🛡️ **Privacy by Default** — Tor-first architecture, zero telemetry
- ⚙️ **Manual Control** — No auto-updates, you decide when to upgrade
- 🔍 **Complete Transparency** — Open-source, reproducible builds, auditable code

**→ Read the full manifesto:** [WHY_SWISS.md](WHY_SWISS.md)

*Switzerland defended its independence for 500 years. SwissWallet extends this to your digital sovereignty.*

---

## ✨ Features

### 🔐 Swiss Privacy Infrastructure
- **Swiss CoinJoin Coordinators** hardcoded (Tor + HTTPS)
  - Primary: `.onion` hidden service (Tor-only)
  - Fallback: `wasabi.swisscoordinator.app` (HTTPS)
- **Non-configurable** — Prevents misconfiguration attacks
- **Swiss jurisdiction** — Protected by Swiss privacy laws

### 🧅 Tor-First Architecture
- Built-in Tor daemon (managed automatically)
- All coordinator communication via Tor by default
- Optional clearnet fallback with health monitoring
- No IP address exposure to coordinators

### 🎨 Swiss Visual Identity
- Clean Swiss flag icon (red + white cross)
- Swiss Red (#DC143C) UI theme throughout
- Professional, security-focused design
- Instantly recognizable Swiss branding

### 🛡️ Security & Sovereignty
- **No auto-updates** — Manual GitHub releases only
- **Independent data directory** (`~/.swisswallet/`)
- **Zero telemetry** — No tracking, no phone-home
- **Reproducible builds** — Verify binaries match source code

### 🌍 Multi-Platform Support
- **macOS**: ARM64 (Apple Silicon) and x64 (Intel)
- **Windows**: x64
- **Linux**: x64 (+ Debian .deb packages)

---

## 📥 Download

**Latest Release:** [v3.0.0](https://github.com/swisscodernano/swisswallet/releases/latest)

| Platform | Download |
|----------|----------|
| 🍎 **macOS ARM64** (M1/M2/M3) | [SwissWallet-macOS-arm64.dmg](https://github.com/swisscodernano/swisswallet/releases/latest) |
| 🍎 **macOS x64** (Intel) | [SwissWallet-macOS-x64.dmg](https://github.com/swisscodernano/swisswallet/releases/latest) |
| 🪟 **Windows x64** | [SwissWallet-win-x64.zip](https://github.com/swisscodernano/swisswallet/releases/latest) |
| 🐧 **Linux x64** | [SwissWallet-linux-x64.tar.gz](https://github.com/swisscodernano/swisswallet/releases/latest) |

**All releases include:**
- Cryptographic signatures for verification
- SHA-256 checksums
- Full release notes

---

## 🚀 Quick Start

### macOS
```bash
# Download and install
open SwissWallet-macOS-arm64.dmg

# Drag SwissWallet.app to Applications
# Launch from Applications folder
```

### Windows
```bash
# Extract ZIP and run
swisswallet.exe
```

### Linux
```bash
# Extract archive
tar -xzf SwissWallet-linux-x64.tar.gz
cd SwissWallet-linux-x64
./swisswallet
```

**First Launch:**
1. SwissWallet creates `~/.swisswallet/` data directory
2. Tor daemon starts automatically (may take 10-30 seconds)
3. Create or recover your wallet
4. Begin private Bitcoin transactions

---

## 📚 Documentation

### User Guides
- **[Why Swiss?](WHY_SWISS.md)** — Read the full manifesto on Swiss digital sovereignty
- **[Installation Guide](docs/build/MACOS_INSTALLATION.md)** — Detailed setup instructions
- **[Security Best Practices](SECURITY.md)** — Protect your Bitcoin

### Developer Documentation
- **[Build Instructions](docs/build/BUILD_QUICK_START.md)** — Compile from source
- **[Build System](docs/build/SWISSWALLET_BUILD_SYSTEM.md)** — Multi-platform build process
- **[Contributing Guide](CONTRIBUTING.md)** — How to contribute

### Technical Specifications
- **[Distribution Packages](docs/build/DISTRIBUTION_PACKAGE_SPEC.md)** — Package formats
- **[GitHub Release Setup](docs/build/GITHUB_RELEASE_SETUP.md)** — CI/CD pipeline
- **[Project Roadmap](docs/development/SWISSWALLET_PROJECT_ROADMAP.md)** — Development plan

---

## 🏗️ Building from Source

**Requirements:**
- .NET 8.0 SDK
- Git

**Quick Build:**
```bash
git clone https://github.com/swisscodernano/swisswallet.git
cd swisswallet
cd WalletWasabi.Fluent.Desktop
dotnet build
dotnet run
```

**Multi-Platform Build:**
```bash
./Contrib/build-all.sh
# Packages created in: packages/
```

**See:** [Build Documentation](docs/build/BUILD_QUICK_START.md)

---

## 🤝 SwissWallet vs Wasabi Wallet

SwissWallet is a **fork** of Wasabi Wallet. We built on their excellent work with deep respect.

| Feature | Wasabi Wallet | SwissWallet |
|---------|---------------|-------------|
| **Coordinators** | Configurable (zkSNACKs default) | Hardcoded Swiss only |
| **Infrastructure** | Global | Swiss-controlled |
| **Auto-Updates** | Enabled | Disabled (manual) |
| **Data Directory** | `~/.walletwasabi/` | `~/.swisswallet/` |
| **Branding** | Wasabi | Swiss Red theme |
| **Jurisdiction** | Various | Switzerland |
| **Philosophy** | Global tool | **Swiss sovereignty** |

**Credits:** Built on [Wasabi Wallet](https://github.com/WalletWasabi/WalletWasabi) by zkSNACKs Ltd.

---

## 🔒 Security

- **Swiss Coordinators**: Onion + HTTPS, hardcoded, non-configurable
- **Open-source**: Fully auditable code
- **No telemetry**: Zero tracking
- **Manual updates**: No auto-update vulnerabilities

See [SECURITY.md](SECURITY.md) for vulnerability reporting.

---

## 🌐 Community

- **Issues**: [Report bugs](https://github.com/swisscodernano/swisswallet/issues)
- **Discussions**: [Ask questions](https://github.com/swisscodernano/swisswallet/discussions)
- **Wiki**: [Documentation](https://github.com/swisscodernano/swisswallet/wiki)

---

## 📜 License

**MIT License** — See [LICENSE.md](LICENSE.md)

---

<p align="center">
  <strong>🇨🇭 Your Bitcoin. Your Privacy. Your Sovereignty. 🇨🇭</strong>
</p>

<p align="center">
  <em>SwissWallet — Bitcoin Privacy with Swiss Precision</em>
</p>
