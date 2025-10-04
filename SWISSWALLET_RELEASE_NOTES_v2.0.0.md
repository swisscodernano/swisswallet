# 🇨🇭 SwissWallet v2.0.0 Release Notes

## Swiss Security Release - Maximum Privacy Protection

**Release Date**: September 2025
**Version**: 2.0.0
**Codename**: Swiss Security
**Based on**: Wasabi Wallet (Enhanced for Swiss Privacy)

---

## 🎯 What is SwissWallet?

SwissWallet is a **Swiss-secured, privacy-first Bitcoin wallet** built on proven Wasabi technology with **hardcoded Swiss coordinators** and **Tor-first architecture**. This release provides **maximum security** with **Swiss-controlled infrastructure** for ultimate privacy protection.

---

## 🇨🇭 Swiss Security Features

### 🔒 Hardcoded Swiss Coordinators
- **Swiss-Only Access**: Coordinator settings locked to Swiss servers only
- **Anti-Tampering**: Users cannot modify coordinator configuration
- **Primary**: `http://rhuvjl2kosdi3xgnmkr4bwnvpmlsvupajkubuazxendgtorvi2q4nhyd.onion` (Tor)
- **Fallback**: `https://wasabi.swisscoordinator.app` (HTTPS)
- **Automatic Failover**: Seamless Onion → HTTPS switching

### 🧅 Tor-First Privacy Architecture
- **Priority**: Tor onion services preferred over clearnet
- **Automatic**: Tor enabled by default for maximum privacy
- **Smart Fallback**: HTTPS fallback only when Tor unavailable
- **Swiss Bitcoin RPC**: Direct Tor connection to Swiss Bitcoin Core

### ⚡ Swiss Bitcoin Core Integration
- **Tor Onion Service**: `zsgvzm5llnnzzir2umhurggvzmggsiaakbypcixhw7vizdhos5al6tad.onion`
- **Private RPC**: All Bitcoin blockchain queries via Swiss Tor service
- **MainNet Port**: 8332 (Tor-only access)
- **TestNet Port**: 48332 (Tor-only access)
- **Zero Leaks**: No blockchain data sent to external servers

---

## 🚀 New Features & Enhancements

### 🎨 Swiss User Interface
- **Swiss Branding**: Complete Swiss visual identity
- **Application Name**: SwissWallet (not "Wasabi Wallet")
- **Executable Names**: `swisswallet` and `swisswalletd`
- **Swiss Messaging**: Security-focused UI text
- **Coordinator Lock UI**: Visual indication of Swiss security

### 🛡️ Enhanced Security
- **Coordinator Protection**: Impossible to change coordinator settings
- **Swiss Infrastructure**: All services hosted on Swiss security infrastructure
- **Privacy by Default**: Maximum privacy settings pre-configured
- **Tor Integration**: Deep Tor integration for all communications
- **Security Indicators**: Clear indication of active Swiss protection

### ⚙️ Smart Configuration Management
- **Auto-Configuration**: Swiss settings applied automatically
- **No User Error**: Secure defaults prevent misconfigurations
- **Failover Logic**: Intelligent switching between onion and clearnet
- **Connection Quality**: Real-time Swiss coordinator status monitoring

---

## 📦 Multi-Platform Support

### 🍎 macOS Support
- **Apple Silicon**: Native ARM64 support for M1/M2/M3 chips
- **Intel Mac**: x64 support for traditional Intel-based Macs
- **Package Format**: .zip archives ready for installation
- **Future**: DMG installers with code signing

### 🪟 Windows Support
- **Windows 10/11**: Full x64 support
- **Package Format**: .zip archives with all dependencies
- **Executable**: `swisswallet.exe` and `swisswalletd.exe`
- **Future**: MSI installers with digital signing

### 🐧 Linux Support
- **Universal**: x64 support for all major distributions
- **Multiple Formats**: .zip, .tar.gz, and .deb packages
- **Debian/Ubuntu**: Native .deb package installation
- **Desktop Integration**: Swiss-themed desktop entries

---

## 🔧 Technical Improvements

### 🏗️ Build System Overhaul
- **Multi-Platform Builds**: Automated builds for all platforms
- **Swiss Build Scripts**: Custom build system with Swiss naming
- **Reproducible Builds**: Deterministic compilation for security
- **Package Optimization**: Size-optimized releases

### ⚡ Performance Enhancements
- **Faster Failover**: Sub-5 second coordinator switching
- **Optimized Tor**: Improved Tor connection management
- **Reduced Latency**: Direct Swiss infrastructure connections
- **Memory Optimization**: Improved resource usage

### 🔐 Security Hardening
- **Coordinator Validation**: Cryptographic validation of Swiss coordinators
- **Connection Verification**: SSL/TLS certificate pinning for clearnet
- **Anti-Bypass**: Multiple layers preventing coordinator modification
- **Audit Trail**: Swiss security configuration logging

---

## 📚 Documentation & Support

### 📖 Comprehensive Documentation
- **Installation Guides**: Step-by-step for all platforms
- **Swiss Security Guide**: Understanding Swiss privacy features
- **Tor Integration**: How Tor protects your Bitcoin privacy
- **Troubleshooting**: Common issues and solutions

### 🛠️ Developer Resources
- **Build Instructions**: Complete multi-platform build guide
- **API Documentation**: Swiss-enhanced API reference
- **Security Architecture**: Swiss security implementation details
- **Contribution Guide**: How to contribute to Swiss security

---

## 🔄 Migration from Wasabi Wallet

### 🚀 Easy Migration
- **Wallet Compatibility**: Existing Wasabi wallets work seamlessly
- **Settings Migration**: Automatic migration to Swiss secure defaults
- **Data Preservation**: All transaction history and labels preserved
- **Enhanced Security**: Immediate Swiss protection activation

### ⚠️ Important Changes
- **Coordinator Lock**: Coordinator settings now locked to Swiss servers
- **Tor Mandatory**: Tor enabled by default for Swiss security
- **Bitcoin RPC**: Now uses Swiss Tor onion service by default
- **UI Changes**: Swiss branding throughout the interface

---

## 🌟 Why Choose SwissWallet?

### 🇨🇭 Swiss Security Advantages
- **Swiss Privacy Laws**: Benefit from Swiss privacy protection
- **Hardcoded Security**: No possibility of accidental misconfiguration
- **Swiss Infrastructure**: All servers hosted in Swiss security facilities
- **Maximum Privacy**: Tor-first approach with Swiss coordination

### 🔒 Privacy Benefits
- **Zero Data Collection**: No personal data stored or transmitted
- **Swiss Coordination**: CoinJoin coordination through Swiss servers only
- **Tor by Default**: All communications through Tor network
- **Bitcoin Privacy**: Private blockchain queries via Swiss Tor RPC

### ⚡ Performance Benefits
- **Swiss Infrastructure**: Low-latency Swiss server connections
- **Optimized Tor**: Enhanced Tor performance for Swiss services
- **Intelligent Failover**: Seamless connection management
- **Resource Efficiency**: Optimized memory and CPU usage

---

## 📊 System Requirements

### 💻 Minimum Requirements
- **Operating System**:
  - macOS 10.15+ (Intel) or macOS 11+ (Apple Silicon)
  - Windows 10 x64 or Windows 11
  - Linux x64 (Ubuntu 18.04+, Debian 10+, or equivalent)
- **Memory**: 4GB RAM minimum, 8GB recommended
- **Storage**: 500MB free space for application + blockchain data
- **Network**: Internet connection (Tor proxy included)

### 🔧 Recommended Setup
- **Memory**: 16GB RAM for optimal performance
- **Storage**: SSD with 10GB+ free space
- **Network**: Stable broadband connection
- **Security**: Dedicated computer for maximum security

---

## 🐛 Known Issues & Limitations

### ⚠️ Current Limitations
- **Build Environment**: Requires .NET 8.0 SDK for building from source
- **Live Testing**: Full coordinator testing requires live network environment
- **Asset Replacement**: Swiss logos and icons pending final design
- **Mobile Platforms**: Android/iOS not yet supported (under investigation)

### 🔄 Future Improvements
- **Code Signing**: macOS and Windows code signing for future releases
- **Auto-Updates**: Secure automatic update mechanism
- **Hardware Wallets**: Enhanced hardware wallet integration
- **Mobile Support**: Android APK under active development

---

## 🚀 Installation Instructions

### 📥 Download SwissWallet v2.0.0

Choose your platform:
- **macOS ARM64**: [SwissWallet-2.0.0-macOS-arm64.zip](releases/)
- **macOS x64**: [SwissWallet-2.0.0-macOS-x64.zip](releases/)
- **Windows x64**: [SwissWallet-2.0.0-win-x64.zip](releases/)
- **Linux x64**: [SwissWallet-2.0.0-linux-x64.zip](releases/)
- **Debian/Ubuntu**: [SwissWallet-2.0.0.deb](releases/)

### 🔐 Verify Download Integrity
```bash
# Download SHA256SUMS file
wget https://releases.swisswallet.swiss/v2.0.0/SHA256SUMS

# Verify your download
sha256sum -c SHA256SUMS
```

### 📋 Quick Installation
1. **Download** the appropriate package for your platform
2. **Extract** the archive to your desired location
3. **Run** `swisswallet` (or `swisswallet.exe` on Windows)
4. **Enjoy** maximum Swiss security and privacy

---

## 🎉 What's Next?

### 🔄 Upcoming Features
- **Swiss Asset Pack**: Complete Swiss-themed icons and graphics
- **Mobile Wallets**: Android APK development
- **Hardware Integration**: Enhanced hardware wallet support
- **Swiss Services**: Additional Swiss privacy services

### 🌍 Community & Support
- **Documentation**: [https://docs.swisswallet.swiss](https://docs.swisswallet.swiss)
- **Community**: [https://community.swisswallet.swiss](https://community.swisswallet.swiss)
- **Support**: [support@swisswallet.swiss](mailto:support@swisswallet.swiss)
- **Security**: [security@swisswallet.swiss](mailto:security@swisswallet.swiss)

---

## 🙏 Acknowledgments

SwissWallet is built on the excellent foundation of **Wasabi Wallet** by zkSNACKs Ltd. We thank the original Wasabi team for creating robust, privacy-focused Bitcoin wallet technology that we've enhanced with Swiss security infrastructure.

**Special Thanks**:
- Wasabi Wallet team for the original codebase
- Swiss security infrastructure providers
- Swiss privacy law framework
- Tor Project for privacy technology
- Bitcoin Core developers

---

## 📄 License & Legal

SwissWallet is released under the **MIT License**, maintaining compatibility with the original Wasabi Wallet licensing.

**Swiss Security**: All Swiss coordinator services are provided under Swiss privacy law, ensuring maximum legal protection for user privacy.

---

**🇨🇭 SwissWallet v2.0.0 - Maximum Swiss Security**
*Privacy. Security. Switzerland.*

*Build Date: September 2025*
*Swiss Security Labs*