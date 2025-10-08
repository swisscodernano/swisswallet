# 🇨🇭 SwissWallet Multi-Platform Build System

## 📋 Overview

SwissWallet is configured for multi-platform distribution across:
- **macOS ARM64** (Apple Silicon M1/M2/M3)
- **macOS x64** (Intel)
- **Windows x64**
- **Linux x64** (with .deb package)

## 🛠️ Build Requirements

### Prerequisites
- **.NET 8.0 SDK** (latest)
- **Git** for version management
- **Platform-specific tools**:
  - macOS: Xcode Command Line Tools
  - Windows: Visual Studio 2022 or Build Tools
  - Linux: Standard build-essential

### Runtime Identifiers (RIDs)
The project uses these target frameworks:
```xml
<RuntimeIdentifiers>win-x64;linux-x64;linux-arm64;osx-x64;osx-arm64</RuntimeIdentifiers>
```

## 🚀 Build Scripts

### Main Build Scripts
Located in `Contrib/` directory:

1. **`swiss-release.sh`** - Master build script
2. **`build-all.sh`** - Build all platforms at once
3. **`build-macos.sh`** - macOS-only builds
4. **`build-windows.sh`** - Windows-only builds
5. **`build-linux.sh`** - Linux-only builds

### Usage Examples

```bash
# Build all platforms
./Contrib/build-all.sh

# Build specific platforms
./Contrib/build-macos.sh    # Both ARM64 and x64
./Contrib/build-windows.sh  # Windows x64
./Contrib/build-linux.sh    # Linux x64 + .deb

# Advanced: Use master script directly
./Contrib/swiss-release.sh          # All platforms
./Contrib/swiss-release.sh test     # Quick Linux test
./Contrib/swiss-release.sh macos    # macOS only
./Contrib/swiss-release.sh windows  # Windows only
./Contrib/swiss-release.sh linux    # Linux only
```

## 📦 Output Structure

### Package Naming Convention
```
SwissWallet-{VERSION}-{PLATFORM}.{FORMAT}
```

### Examples:
```
packages/
├── SwissWallet-3.2.1-macOS-arm64.zip
├── SwissWallet-3.2.1-macOS-x64.zip
├── SwissWallet-3.2.1-win-x64.zip
├── SwissWallet-3.2.1-linux-x64.zip
├── SwissWallet-3.2.1-linux-x64.tar.gz
└── SwissWallet-3.2.1.deb
```

### Executable Names
- **Desktop GUI**: `swisswallet` / `swisswallet.exe`
- **Daemon**: `swisswalletd` / `swisswalletd.exe`

## 🎯 Platform-Specific Details

### macOS Builds
- **ARM64**: For Apple Silicon (M1/M2/M3 chips)
- **x64**: For Intel-based Macs
- **Output**: .zip archives ready for distribution
- **Future**: .dmg installer (requires signing certificates)

### Windows Builds
- **x64**: 64-bit Windows 10/11
- **Output**: .zip archives
- **Future**: .msi installer (requires WiX toolset and signing)

### Linux Builds
- **x64**: 64-bit Linux distributions
- **Outputs**:
  - `.zip` archive (portable)
  - `.tar.gz` archive (portable)
  - `.deb` package (Ubuntu/Debian installation)

## ⚙️ Build Configuration

### Project Metadata Updates
All projects updated with Swiss branding:

```xml
<Authors>Swiss Security Team</Authors>
<Company>Swiss Security Labs</Company>
<AssemblyTitle>SwissWallet</AssemblyTitle>
<Product>SwissWallet</Product>
<PackageProjectUrl>https://swisswallet.swiss/</PackageProjectUrl>
<ApplicationIcon>Assets\SwissLogo.ico</ApplicationIcon>
```

### Runtime Configuration
- **Framework**: .NET 8.0
- **Self-contained**: Yes (includes .NET runtime)
- **Trimming**: Optimized for size
- **Deterministic**: Yes (reproducible builds)

## 🔒 Security Features

### Swiss Security Hardcoding
- **Coordinators**: Hardcoded Swiss coordinators only
- **Tor Priority**: Onion service preferred, clearnet fallback
- **Bitcoin RPC**: Tor onion service connection
- **No User Modifications**: Coordinator settings locked

### Build Security
- **Reproducible Builds**: Deterministic compilation
- **SHA512 Cleanup**: Removes path-dependent hashes
- **Code Signing Ready**: Infrastructure for platform signing

## 🧪 Testing Build Process

### Quick Test Build
```bash
# Test Linux build without packages
./Contrib/swiss-release.sh test
```

### Full Platform Test
```bash
# Build all platforms
./Contrib/build-all.sh

# Verify packages created
ls -la packages/SwissWallet-*
```

## 📊 Performance Optimizations

### Build Settings
```xml
<property:SelfContained=true />
<property:DebugType=none />
<property:DebugSymbols=false />
<property:Deterministic=true />
```

### Size Optimization
- Remove unused microservices binaries per platform
- Trim unnecessary dependencies
- Optimize for target architecture

## 🔄 CI/CD Integration

### GitHub Actions Ready
The build system is designed for:
- **Automated builds** on git tags
- **Multi-platform runners** (Windows, macOS, Linux)
- **Artifact upload** to releases
- **Security scanning** integration

### Environment Variables
```bash
VERSION=3.2.1              # SwissWallet version
PLATFORMS=("win-x64" "linux-x64" "osx-x64" "osx-arm64")
EXECUTABLE_NAME=swisswallet
PACKAGE_PREFIX=SwissWallet
```

## 🎯 Next Steps

### Phase 5: Testing & QA
- Test builds on actual target platforms
- Verify Swiss coordinator connections
- Validate UI branding consistency
- Security audit of hardcoded configurations

### Phase 6: Distribution
- Set up release infrastructure
- Create installation guides
- Implement update mechanism
- Swiss community distribution

## 📋 Build Status

| Platform | Status | Package Format | Notes |
|----------|--------|----------------|-------|
| macOS ARM64 | ✅ Ready | .zip | Apple Silicon support |
| macOS x64 | ✅ Ready | .zip | Intel Mac support |
| Windows x64 | ✅ Ready | .zip | Windows 10/11 support |
| Linux x64 | ✅ Ready | .zip, .tar.gz, .deb | Full Linux support |

**🇨🇭 Swiss Security**: All platforms configured with maximum Swiss protection
**🧅 Tor Integration**: Privacy-first architecture across all platforms
**🚀 Distribution Ready**: Complete multi-platform build system

---

*Generated by SwissWallet Build System v3.2.1*