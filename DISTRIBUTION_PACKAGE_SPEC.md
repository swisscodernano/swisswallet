# 📦 SwissWallet Distribution Package Specifications

## v2.0.0 Swiss Security Release Package Definitions

**Target Platforms**: macOS ARM64, macOS x64, Windows x64, Linux x64
**Package Format**: Optimized for each platform with Swiss branding

---

## 📋 Package Naming Convention

### 🏷️ Standard Format
```
SwissWallet-{VERSION}-{PLATFORM}.{FORMAT}
```

### 📦 Complete Package List
```
SwissWallet-2.0.0-macOS-arm64.zip      # Apple Silicon Mac
SwissWallet-2.0.0-macOS-x64.zip        # Intel Mac
SwissWallet-2.0.0-win-x64.zip          # Windows 10/11 x64
SwissWallet-2.0.0-linux-x64.zip        # Linux universal portable
SwissWallet-2.0.0-linux-x64.tar.gz     # Linux archive format
SwissWallet-2.0.0.deb                  # Debian/Ubuntu package
```

---

## 🍎 macOS Package Specifications

### macOS ARM64 (Apple Silicon)
**File**: `SwissWallet-2.0.0-macOS-arm64.zip`

**Contents**:
```
SwissWallet-2.0.0-macOS-arm64/
├── swisswallet                    # Main GUI executable
├── swisswalletd                   # Daemon executable
├── *.dll                         # .NET runtime libraries
├── *.json                        # Configuration files
├── *.pdb                         # Debug symbols (removed)
├── Microservices/
│   └── Binaries/
│       └── osxarm64/             # macOS ARM64 specific binaries
│           ├── tor               # Tor executable
│           ├── hwi               # Hardware wallet interface
│           └── ...
└── Tor/                          # Tor configuration
    ├── torrc                     # Tor configuration
    └── ...
```

**Specifications**:
- **Architecture**: ARM64 (Apple Silicon M1/M2/M3)
- **Target OS**: macOS 11.0+ (Big Sur and later)
- **Runtime**: Self-contained .NET 8.0 ARM64
- **Size**: ~150-200 MB (estimated)
- **Executable**: `swisswallet` (no .exe extension)

### macOS x64 (Intel)
**File**: `SwissWallet-2.0.0-macOS-x64.zip`

**Contents**: Similar to ARM64 but with Intel binaries
```
├── Microservices/Binaries/osx64/    # Intel macOS binaries
```

**Specifications**:
- **Architecture**: x64 (Intel)
- **Target OS**: macOS 10.15+ (Catalina and later)
- **Runtime**: Self-contained .NET 8.0 x64
- **Size**: ~150-200 MB (estimated)

---

## 🪟 Windows Package Specifications

### Windows x64
**File**: `SwissWallet-2.0.0-win-x64.zip`

**Contents**:
```
SwissWallet-2.0.0-win-x64/
├── swisswallet.exe               # Main GUI executable
├── swisswalletd.exe              # Daemon executable
├── *.dll                        # .NET runtime libraries
├── *.json                       # Configuration files
├── Microservices/
│   └── Binaries/
│       └── win64/               # Windows x64 specific binaries
│           ├── tor.exe          # Tor executable
│           ├── hwi.exe          # Hardware wallet interface
│           └── ...
├── Tor/                         # Tor configuration
│   ├── torrc                    # Tor configuration
│   └── ...
└── SwissWallet.ico              # Application icon
```

**Specifications**:
- **Architecture**: x64
- **Target OS**: Windows 10 x64, Windows 11
- **Runtime**: Self-contained .NET 8.0 Windows x64
- **Size**: ~180-220 MB (estimated)
- **Executable**: `swisswallet.exe`

**Additional Windows Features**:
- **Windows Defender Compatibility**: Signed binaries (future)
- **UAC Compliance**: No administrator privileges required
- **Windows Firewall**: Automatic exception request
- **Registry Clean**: No registry modifications

---

## 🐧 Linux Package Specifications

### Linux x64 Portable (ZIP)
**File**: `SwissWallet-2.0.0-linux-x64.zip`

**Contents**:
```
SwissWallet-2.0.0-linux-x64/
├── swisswallet                   # Main GUI executable
├── swisswalletd                  # Daemon executable
├── *.dll                        # .NET runtime libraries
├── *.so                         # Linux shared libraries
├── *.json                       # Configuration files
├── Microservices/
│   └── Binaries/
│       └── lin64/               # Linux x64 specific binaries
│           ├── tor              # Tor executable
│           ├── hwi              # Hardware wallet interface
│           └── ...
├── Tor/                         # Tor configuration
│   ├── torrc                    # Tor configuration
│   └── ...
└── swisswallet.desktop          # Desktop entry file
```

**Specifications**:
- **Architecture**: x64
- **Target OS**: Linux x64 (Ubuntu 18.04+, Debian 10+, CentOS 8+)
- **Runtime**: Self-contained .NET 8.0 Linux x64
- **Size**: ~170-210 MB (estimated)
- **Dependencies**: None (self-contained)

### Linux x64 Archive (TAR.GZ)
**File**: `SwissWallet-2.0.0-linux-x64.tar.gz`

**Contents**: Identical to ZIP format but compressed with gzip
**Specifications**:
- **Compression**: gzip compression for better compression ratio
- **Permissions**: Executable permissions preserved
- **Symlinks**: Symbolic links preserved if any

### Debian Package (DEB)
**File**: `SwissWallet-2.0.0.deb`

**Package Structure**:
```
SwissWallet-2.0.0.deb
├── DEBIAN/
│   ├── control                   # Package metadata
│   ├── postinst                  # Post-installation script
│   └── prerm                     # Pre-removal script
├── usr/
│   ├── local/
│   │   └── bin/
│   │       ├── swisswallet       # Wrapper script
│   │       ├── swisswalletd      # Daemon wrapper
│   │       └── swisswallet/      # Main application directory
│   │           ├── swisswallet   # Actual executable
│   │           ├── swisswalletd  # Actual daemon
│   │           └── ... (all app files)
│   └── share/
│       ├── applications/
│       │   └── swisswallet.desktop  # Desktop entry
│       └── icons/
│           └── hicolor/
│               ├── 16x16/apps/swisswallet.png
│               ├── 32x32/apps/swisswallet.png
│               ├── 48x48/apps/swisswallet.png
│               └── 256x256/apps/swisswallet.png
```

**Package Metadata** (`DEBIAN/control`):
```
Package: swisswallet
Priority: optional
Section: utils
Maintainer: Swiss Security Labs
Version: 2.0.0
Homepage: https://swisswallet.swiss
Architecture: amd64
License: Open Source (MIT)
Installed-Size: 200000
Recommends: policykit-1
Description: Swiss-secured, privacy-first Bitcoin wallet
 Built-in Tor, Swiss coordinators, coinjoin, payjoin and coin control features.
 Maximum privacy protection with Swiss infrastructure.
```

---

## 🔐 Security & Integrity

### 📋 Package Verification

**SHA256 Checksums** (`SHA256SUMS`):
```
a1b2c3d4... SwissWallet-2.0.0-macOS-arm64.zip
e5f6g7h8... SwissWallet-2.0.0-macOS-x64.zip
i9j0k1l2... SwissWallet-2.0.0-win-x64.zip
m3n4o5p6... SwissWallet-2.0.0-linux-x64.zip
q7r8s9t0... SwissWallet-2.0.0-linux-x64.tar.gz
u1v2w3x4... SwissWallet-2.0.0.deb
```

**GPG Signatures** (Future):
```
SwissWallet-2.0.0-macOS-arm64.zip.asc
SwissWallet-2.0.0-macOS-x64.zip.asc
SwissWallet-2.0.0-win-x64.zip.asc
SwissWallet-2.0.0-linux-x64.zip.asc
SwissWallet-2.0.0-linux-x64.tar.gz.asc
SwissWallet-2.0.0.deb.asc
```

### 🛡️ Security Features Verification

**Each package contains**:
- ✅ **Hardcoded Swiss coordinators**: Verified in Constants.cs
- ✅ **Swiss branding**: SwissWallet executable names
- ✅ **Tor integration**: Tor binaries included for each platform
- ✅ **Anti-tampering**: Config.cs hardcoding active
- ✅ **Bitcoin RPC**: Swiss Tor onion service configured

---

## 📊 Package Size Estimates

| Package | Compressed Size | Extracted Size | Platform |
|---------|----------------|----------------|----------|
| macOS ARM64 | ~80 MB | ~180 MB | Apple Silicon |
| macOS x64 | ~85 MB | ~190 MB | Intel Mac |
| Windows x64 | ~90 MB | ~200 MB | Windows 10/11 |
| Linux ZIP | ~85 MB | ~185 MB | Universal Linux |
| Linux TAR.GZ | ~75 MB | ~185 MB | Universal Linux |
| Debian DEB | ~80 MB | ~185 MB | Debian/Ubuntu |

### 💾 Storage Requirements

**Per Platform**:
- **Download**: 75-90 MB per package
- **Installation**: 180-200 MB per platform
- **Runtime**: Additional 50-100 MB for blockchain data directory

---

## 🚀 Build Process Specifications

### 🔨 Build Configuration

**Common Build Parameters**:
```bash
dotnet publish \
  --configuration Release \
  --runtime {PLATFORM_RID} \
  --self-contained true \
  --property:VersionPrefix=2.0.0 \
  --property:DebugType=none \
  --property:DebugSymbols=false \
  --property:Deterministic=true
```

**Platform RIDs**:
- macOS ARM64: `osx-arm64`
- macOS x64: `osx-x64`
- Windows x64: `win-x64`
- Linux x64: `linux-x64`

### 🧹 Post-Build Cleanup

**Removed from packages**:
- ✅ Debug symbols (.pdb files)
- ✅ Unnecessary microservice binaries (other platforms)
- ✅ Development dependencies
- ✅ Unused language packs
- ✅ Build artifacts

**Optimizations applied**:
- ✅ SHA512 hashes normalized for reproducible builds
- ✅ Executable permissions set correctly
- ✅ Swiss branding applied consistently
- ✅ Package size optimized

---

## 📦 Distribution Channels

### 🌐 Primary Distribution

**GitHub Releases**:
- **URL**: `https://github.com/swisssecurity/swisswallet/releases/v2.0.0`
- **Assets**: All platform packages + checksums
- **Verification**: SHA256SUMS file provided

**Swiss Website**:
- **URL**: `https://releases.swisswallet.swiss/v2.0.0/`
- **Mirror**: Redundant download source
- **Verification**: GPG signatures and checksums

### 🔄 Package Managers (Future)

**Planned Distribution**:
- **Homebrew** (macOS): `brew install --cask swisswallet`
- **Chocolatey** (Windows): `choco install swisswallet`
- **APT Repository** (Debian/Ubuntu): `apt install swisswallet`
- **Flatpak** (Linux): `flatpak install swisswallet`

---

## ✅ Package Quality Checklist

### 🔍 Pre-Release Verification

**Each package must pass**:
- [ ] **Extract Test**: Package extracts without errors
- [ ] **Execute Test**: SwissWallet launches successfully
- [ ] **Swiss Features**: Coordinator hardcoding verified
- [ ] **Tor Test**: Tor integration working
- [ ] **UI Test**: Swiss branding visible
- [ ] **Size Check**: Package size within expected range
- [ ] **Checksum**: SHA256 matches calculated value

### 🛡️ Security Verification

**Security requirements**:
- [ ] **No Backdoors**: Code review confirms no malicious additions
- [ ] **Swiss Hardcoding**: Constants.cs contains Swiss coordinators
- [ ] **Config Lock**: Config.cs prevents coordinator changes
- [ ] **Tor Integration**: Tor binaries included and functional
- [ ] **Clean Build**: Reproducible build from source

---

## 🎯 Release Deployment Process

### 📋 Deployment Steps

1. **Build Packages**: Use `swiss-release.sh` for all platforms
2. **Verify Packages**: Run quality checklist for each package
3. **Generate Checksums**: Create SHA256SUMS file
4. **Upload to GitHub**: Release to GitHub with documentation
5. **Update Website**: Sync to Swiss website mirror
6. **Announce Release**: Community announcement

### 🔄 Post-Release Monitoring

**Track metrics**:
- **Download counts** per platform
- **Installation success rate**
- **User feedback** and issues
- **Security incident** monitoring

---

**🇨🇭 SwissWallet Distribution Packages - Ready for Swiss Security**

*Maximum Privacy • Swiss Infrastructure • Multi-Platform Support*

---

*Generated by SwissWallet Distribution System v2.0.0*