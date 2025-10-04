# 🇨🇭 SwissWallet Build Quick Start

## ⚡ Quick Commands

```bash
# Build all platforms
./Contrib/build-all.sh

# Build specific platform
./Contrib/build-macos.sh     # macOS ARM64 + x64
./Contrib/build-windows.sh   # Windows x64
./Contrib/build-linux.sh     # Linux x64 + .deb

# Test build (Linux only)
./Contrib/swiss-release.sh test
```

## 📦 Output Location

All builds go to: `packages/SwissWallet-*`

## 🔧 Requirements

- .NET 8.0 SDK
- Git
- 15GB+ free space

## 🎯 Target Platforms

✅ **macOS ARM64** - Apple Silicon
✅ **macOS x64** - Intel Mac
✅ **Windows x64** - Windows 10/11
✅ **Linux x64** - Ubuntu/Debian + generic

## 🇨🇭 Swiss Features

- Hardcoded Swiss coordinators
- Tor-first connection priority
- Bitcoin Core integration via Tor
- Swiss security branding

---

*Ready to build Swiss-secured Bitcoin wallet*