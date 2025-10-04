# 🇨🇭 SwissWallet Installation Guide

## Quick Start - Get Swiss Security in 5 Minutes

**SwissWallet v2.0.0** - Maximum Swiss Privacy & Security

---

## 📥 Download SwissWallet

### 🔗 Official Downloads
Visit: **[https://releases.swisswallet.swiss](https://releases.swisswallet.swiss)**

**Available Packages**:
- 🍎 **macOS ARM64**: `SwissWallet-2.0.0-macOS-arm64.zip` (Apple Silicon M1/M2/M3)
- 🍎 **macOS x64**: `SwissWallet-2.0.0-macOS-x64.zip` (Intel Mac)
- 🪟 **Windows x64**: `SwissWallet-2.0.0-win-x64.zip` (Windows 10/11)
- 🐧 **Linux x64**: `SwissWallet-2.0.0-linux-x64.zip` (Universal)
- 🐧 **Debian/Ubuntu**: `SwissWallet-2.0.0.deb` (Package installer)

---

## 🔐 Verify Download Integrity

**Always verify your download before installation:**

```bash
# Download verification files
wget https://releases.swisswallet.swiss/v2.0.0/SHA256SUMS
wget https://releases.swisswallet.swiss/v2.0.0/SHA256SUMS.asc

# Verify checksums
sha256sum -c SHA256SUMS

# Verify GPG signature (optional)
gpg --verify SHA256SUMS.asc SHA256SUMS
```

---

## 🍎 macOS Installation

### For Apple Silicon (M1/M2/M3 Macs)

1. **Download**: `SwissWallet-2.0.0-macOS-arm64.zip`
2. **Extract**: Double-click the zip file to extract
3. **Move**: Drag `SwissWallet.app` to Applications folder
4. **Security**: Right-click → Open (first time only)
5. **Launch**: SwissWallet from Applications or Launchpad

### For Intel Macs

1. **Download**: `SwissWallet-2.0.0-macOS-x64.zip`
2. **Extract**: Double-click the zip file to extract
3. **Move**: Drag `SwissWallet.app` to Applications folder
4. **Security**: Right-click → Open (first time only)
5. **Launch**: SwissWallet from Applications or Launchpad

### 🛡️ macOS Security Note
macOS may warn about "unidentified developer". This is expected as we're not yet code-signed.
- **Solution**: Right-click the app → "Open" → "Open" to bypass Gatekeeper

### 📁 macOS File Locations
- **Application**: `/Applications/SwissWallet.app`
- **Data Directory**: `~/Library/Application Support/SwissWallet/`
- **Logs**: `~/Library/Application Support/SwissWallet/Logs/`

---

## 🪟 Windows Installation

### Windows 10/11 x64

1. **Download**: `SwissWallet-2.0.0-win-x64.zip`
2. **Extract**: Right-click → "Extract All..." or use 7-Zip
3. **Choose Location**: Extract to `C:\SwissWallet\` or your preferred location
4. **Run**: Double-click `swisswallet.exe`
5. **Firewall**: Allow through Windows Firewall if prompted

### 🛡️ Windows Security Note
Windows Defender may warn about "unknown publisher":
- **Solution**: Click "More info" → "Run anyway" to proceed

### 📁 Windows File Locations
- **Application**: `C:\SwissWallet\` (or your chosen location)
- **Data Directory**: `%APPDATA%\SwissWallet\`
- **Logs**: `%APPDATA%\SwissWallet\Logs\`

### 🔧 Windows Advanced Options

**Create Desktop Shortcut**:
```batch
# Right-click swisswallet.exe → "Create shortcut"
# Move shortcut to Desktop
```

**Add to Start Menu**:
```batch
# Copy shortcut to: %APPDATA%\Microsoft\Windows\Start Menu\Programs\
```

---

## 🐧 Linux Installation

### Option 1: Debian/Ubuntu Package (.deb)

```bash
# Download the .deb package
wget https://releases.swisswallet.swiss/v2.0.0/SwissWallet-2.0.0.deb

# Install using dpkg
sudo dpkg -i SwissWallet-2.0.0.deb

# Install dependencies if needed
sudo apt-get install -f

# Launch from terminal
swisswallet

# Or launch from Applications menu
```

### Option 2: Universal Linux (.zip)

```bash
# Download and extract
wget https://releases.swisswallet.swiss/v2.0.0/SwissWallet-2.0.0-linux-x64.zip
unzip SwissWallet-2.0.0-linux-x64.zip -d ~/SwissWallet/

# Make executable
chmod +x ~/SwissWallet/swisswallet

# Run SwissWallet
~/SwissWallet/swisswallet
```

### Option 3: Tar.gz Archive

```bash
# Download and extract
wget https://releases.swisswallet.swiss/v2.0.0/SwissWallet-2.0.0-linux-x64.tar.gz
tar -xzf SwissWallet-2.0.0-linux-x64.tar.gz -C ~/

# Make executable
chmod +x ~/SwissWallet/swisswallet

# Run SwissWallet
~/SwissWallet/swisswallet
```

### 🔧 Linux Advanced Setup

**Create System-wide Installation**:
```bash
# Extract to /opt
sudo tar -xzf SwissWallet-2.0.0-linux-x64.tar.gz -C /opt/

# Create symlink
sudo ln -s /opt/SwissWallet/swisswallet /usr/local/bin/swisswallet

# Run from anywhere
swisswallet
```

**Create Desktop Entry**:
```bash
# Create desktop file
cat > ~/.local/share/applications/swisswallet.desktop << EOF
[Desktop Entry]
Type=Application
Name=SwissWallet
GenericName=Swiss Bitcoin Wallet
Comment=Swiss-secured, privacy-first Bitcoin wallet
Icon=swisswallet
Exec=/home/$USER/SwissWallet/swisswallet
Terminal=false
Categories=Office;Finance;
Keywords=bitcoin;wallet;crypto;swiss;privacy;security;
EOF

# Make executable
chmod +x ~/.local/share/applications/swisswallet.desktop
```

### 📁 Linux File Locations
- **Application**: `~/SwissWallet/` or `/opt/SwissWallet/`
- **Data Directory**: `~/.local/share/SwissWallet/`
- **Configuration**: `~/.config/SwissWallet/`
- **Logs**: `~/.local/share/SwissWallet/Logs/`

---

## 🚀 First Launch Setup

### 1. Welcome to Swiss Security
- SwissWallet automatically connects to Swiss coordinators
- Tor is enabled by default for maximum privacy
- All settings are pre-configured for Swiss security

### 2. Initial Configuration
- **Network**: MainNet (default) or TestNet
- **Tor**: Enabled (recommended, default)
- **Swiss Coordinators**: Hardcoded and locked
- **Bitcoin RPC**: Swiss Tor onion service (if available)

### 3. Wallet Creation/Recovery
- **New Wallet**: Generate new Swiss-secured wallet
- **Recover Wallet**: Import existing wallet (compatible with Wasabi)
- **Hardware Wallet**: Connect supported hardware wallets

---

## 🔧 Advanced Configuration

### 🧅 Tor Configuration
SwissWallet includes Tor by default:
- **Tor Path**: Automatically detected or bundled
- **SOCKS Port**: 9050 (default)
- **Control Port**: 9051 (default)
- **Swiss Onion**: `rhuvjl2kosdi3xgnmkr4bwnvpmlsvupajkubuazxendgtorvi2q4nhyd.onion`

### 🔗 Bitcoin Core Integration
Optional Bitcoin Core connection via Swiss Tor:
- **Onion Service**: `zsgvzm5llnnzzir2umhurggvzmggsiaakbypcixhw7vizdhos5al6tad.onion`
- **MainNet Port**: 8332
- **TestNet Port**: 48332
- **Authentication**: Pre-configured credentials

### 📁 Data Directory Locations

| Platform | Default Data Directory |
|----------|------------------------|
| macOS | `~/Library/Application Support/SwissWallet/` |
| Windows | `%APPDATA%\SwissWallet\` |
| Linux | `~/.local/share/SwissWallet/` |

---

## 🛟 Troubleshooting

### ❓ Common Issues

**"Application can't be opened" (macOS)**:
- Right-click app → Open → Open (bypass Gatekeeper)

**"Windows protected your PC" (Windows)**:
- Click "More info" → "Run anyway"

**"Permission denied" (Linux)**:
- Check executable permissions: `chmod +x swisswallet`

**Tor connection issues**:
- Check firewall settings
- Verify Tor is not blocked by ISP
- Try connecting to clearnet fallback

**SwissWallet won't start**:
- Check system requirements (.NET 8.0 runtime)
- Review log files in data directory
- Ensure sufficient disk space

### 🔍 Getting Help

**Log Files**: Check logs in data directory for error details
**Support**: [support@swisswallet.swiss](mailto:support@swisswallet.swiss)
**Community**: [https://community.swisswallet.swiss](https://community.swisswallet.swiss)
**Documentation**: [https://docs.swisswallet.swiss](https://docs.swisswallet.swiss)

---

## 🔄 Updating SwissWallet

### Manual Updates
1. **Download** new version
2. **Stop** SwissWallet completely
3. **Backup** wallet files (recommended)
4. **Extract** new version over old installation
5. **Launch** SwissWallet

### Future Automatic Updates
- Automatic update mechanism planned for future releases
- Secure update verification with Swiss signatures
- Optional update notifications

---

## 🗑️ Uninstalling SwissWallet

### macOS
```bash
# Remove application
rm -rf /Applications/SwissWallet.app

# Remove data (optional - this deletes your wallets!)
rm -rf ~/Library/Application\ Support/SwissWallet/
```

### Windows
```batch
# Remove application folder
# Delete data folder (optional - this deletes your wallets!)
rmdir /s "%APPDATA%\SwissWallet"
```

### Linux
```bash
# Remove application
rm -rf ~/SwissWallet/

# Remove desktop entry
rm ~/.local/share/applications/swisswallet.desktop

# Remove data (optional - this deletes your wallets!)
rm -rf ~/.local/share/SwissWallet/
```

⚠️ **Warning**: Only remove data directories if you have backed up your wallet files!

---

## 🎉 You're Ready!

**Congratulations!** SwissWallet is now installed with maximum Swiss security:

✅ **Swiss Coordinators**: Hardcoded and secure
✅ **Tor Privacy**: Maximum anonymity enabled
✅ **Swiss Infrastructure**: All connections to Swiss servers
✅ **Bitcoin Privacy**: Optional Swiss Bitcoin Core integration

**Start using SwissWallet for maximum Bitcoin privacy and security!**

---

**🇨🇭 SwissWallet - Privacy. Security. Switzerland.**

*For support and documentation, visit: [https://swisswallet.swiss](https://swisswallet.swiss)*