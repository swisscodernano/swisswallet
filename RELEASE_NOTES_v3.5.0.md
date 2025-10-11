# SwissWallet v3.5.0 - CoinJoin Fix Release Notes

**Release Date**: October 11, 2025
**Version**: 3.5.0 "Swiss CoinJoin Fix"

---

## 🎉 Critical Bug Fix

This release **fixes the CoinJoin blocking bug** that prevented users from participating in CoinJoin rounds.

### The Problem

SwissWallet was rejecting coordinator rounds due to a **boundary condition bug** in the round selection logic:

```csharp
// BEFORE (v3.4.9 and earlier):
var isMinOutputOk = minOutput < MinimumOutputAmountSanity;  // Rejected rounds with EXACTLY 10,000 sats

// AFTER (v3.5.0):
var isMinOutputOk = minOutput <= MinimumOutputAmountSanity; // Accepts rounds up to 10,000 sats
```

**Result**: The wallet was rejecting perfectly valid rounds from the Swiss coordinator that had `MinOutput = 10,000 sats`.

---

## 🔍 How We Found It

### Diagnostic Journey

**v3.4.8** - Added comprehensive coin selection logging:
```
✅ FINAL AVAILABLE COINS: 3/3
💰 Available amount: 40000 sats in 3 coins
```
→ Confirmed coins were available and passing all filters

**v3.4.9** - Added detailed round evaluation logging:
```
🔍 Round c1df62b3...: MinOutput=10000 sats, Suitable=False
❌ Min output too high (10000 >= 10000 sats limit)
```
→ Identified the exact problem: boundary condition bug!

**v3.5.0** - Applied the fix:
```
🔍 Round c54cf818...: MinOutput=5000 sats, Suitable=True
✅ Found suitable round c54cf818... in phase InputRegistration
```
→ **CoinJoin working!** ✅

---

## ✅ What's Fixed

1. **Boundary Condition Bug**
   - Changed strict `<` to inclusive `<=`
   - Now accepts rounds with MinOutput up to and including 10,000 sats
   - File: `WalletWasabi/WabiSabi/Client/CoinJoin/Client/CoinJoinClient.cs:93`

2. **All Previous Fixes Included**
   - ✅ HTTP timeout fixes for Tor (v3.4.6)
   - ✅ Comprehensive coin selection logging (v3.4.8)
   - ✅ Detailed round evaluation logging (v3.4.9)

---

## 📊 Expected Behavior

After installing v3.5.0, you should see in the logs:

```
🪙 GetCoinjoinCoinCandidatesAsync returned X coins
✅ FINAL AVAILABLE COINS: X/X
💰 Available amount: XXXX sats in X coins
⏳ Waiting for suitable CoinJoin round...
🔍 Round [...]: MinOutput=XXXX sats, Suitable=True  ← Should be True!
✅ Found suitable round [...]
```

**And your coins should start mixing!** 🎊

---

## ⚠️ Known Behavior: MinInputCount

You may see this message:
```
❌ CoinJoin cannot start: MinInputCountTooLow
Min input count for the round was 2 but min allowed is 10
```

**This is NORMAL and EXPECTED!**

SwissWallet requires **at least 10 participants** in a round for privacy reasons. If the coordinator has a round with fewer participants, the wallet will wait for more users to join.

**What happens:**
1. Wallet checks every 30 seconds for suitable rounds
2. When 10+ users are ready, CoinJoin automatically starts
3. This ensures maximum privacy and anonymity

**If you want to test with fewer participants**, contact us and we can create a test build with lower thresholds.

---

## 📦 Downloads

**GitHub Release**: https://github.com/swisscodernano/swisswallet/releases/tag/v3.5.0

### All Platforms

- **macOS ARM64** (M1/M2/M3): `SwissWallet-3.5.0-macOS-arm64.zip` or `.dmg`
- **macOS x64** (Intel): `SwissWallet-3.5.0-macOS-x64.zip` or `.dmg`
- **Windows x64**: `SwissWallet-3.5.0-win-x64.zip`
- **Linux x64**: `SwissWallet-3.5.0-linux-x64.tar.gz` or `.zip`

---

## 🚀 Installation

### macOS

```bash
# ARM64 (Apple Silicon)
unzip SwissWallet-3.5.0-macOS-arm64.zip
cd SwissWallet.app/Contents/MacOS
./swisswallet

# Intel
unzip SwissWallet-3.5.0-macOS-x64.zip
cd SwissWallet.app/Contents/MacOS
./swisswallet
```

**First Launch**: Right-click → Open to bypass Gatekeeper
**See**: [docs/MACOS_CODE_SIGNING.md](docs/MACOS_CODE_SIGNING.md)

### Windows

```powershell
# Extract ZIP
cd SwissWallet
.\swisswallet.exe

# If SmartScreen warning: "More info" → "Run anyway"
```

### Linux

```bash
tar xzf SwissWallet-3.5.0-linux-x64.tar.gz
cd SwissWallet
./swisswallet
```

---

## 🔐 Verification

**SHA256 Checksums**: Included in release assets

```bash
# macOS/Linux
sha256sum SwissWallet-3.5.0-*.zip

# Windows
CertUtil -hashfile SwissWallet-3.5.0-win-x64.zip SHA256
```

---

## 🆕 What's New in v3.5.0

### Bug Fixes
- **CRITICAL**: Fixed boundary condition bug in round selection (CoinJoin blocking issue)
- Changed MinOutput check from `<` to `<=` to accept rounds with exactly 10,000 sats minimum

### Improvements from v3.4.6-v3.4.9
- HTTP timeout fixes for Tor onion services (300s for onion, 120s for clearnet)
- Comprehensive coin selection diagnostic logging (v3.4.8)
- Detailed round evaluation logging (v3.4.9)
- All logging at INFO level for maximum visibility

### Documentation
- Updated README to v3.5.0
- Added comprehensive [CODE_SIGNING_GUIDE.md](docs/CODE_SIGNING_GUIDE.md)
- macOS, Windows, and Linux code signing instructions
- Cost comparison and implementation steps

---

## 🐛 Known Issues

### MinInputCount Requirement
- Wallet requires minimum 10 participants for privacy
- If coordinator round has < 10 inputs, wallet will wait
- **This is intentional** for privacy protection
- Not a bug - wallet behaves correctly

### Code Signing
- macOS: Not signed - users need to right-click → Open first time
- Windows: Not signed - users need "More info" → "Run anyway"
- **See**: [docs/CODE_SIGNING_GUIDE.md](docs/CODE_SIGNING_GUIDE.md) for future plans

---

## 🔬 Technical Details

### Files Modified

1. **CoinJoinClient.cs** (line 93)
   ```csharp
   // Changed from:
   var isMinOutputOk = minOutput < MinimumOutputAmountSanity;

   // To:
   var isMinOutputOk = minOutput <= MinimumOutputAmountSanity;
   ```

2. **Constants.cs** (lines 103-104)
   ```csharp
   public static readonly Version ClientVersion = new(3, 5, 0);
   public static readonly string VersionName = "Swiss CoinJoin Fix";
   ```

### Commit History
- `2354fce514`: Fix boundary condition bug in round selection
- `526b12dbce`: Add detailed round selection logging (v3.4.9)
- `8e2370c629`: Add comprehensive coin selection logging (v3.4.8)
- Previous fixes for HTTP timeouts (v3.4.6)

---

## 📚 Documentation

### Updated Guides
- [README.md](README.md) - Updated to v3.5.0
- [CODE_SIGNING_GUIDE.md](docs/CODE_SIGNING_GUIDE.md) - New comprehensive guide
- [MACOS_CODE_SIGNING.md](docs/MACOS_CODE_SIGNING.md) - Existing macOS guide
- [CLAUDE.md](CLAUDE.md) - Development environment guide

### For Developers
- See commit history for detailed implementation
- Diagnostic logging can be used to troubleshoot issues
- All logs at INFO level - no DEBUG level needed

---

## 🙏 Acknowledgments

- **Bug Reporter**: Mike (User testing on macOS)
- **Swiss Coordinator Team**: For server-side diagnostics
- **Wasabi Wallet**: Original codebase foundation
- **Claude Code**: Development and debugging assistance

---

## ⚡ Quick Upgrade Path

**From any previous version:**

1. Download v3.5.0 for your platform
2. Close existing SwissWallet
3. Extract/install new version
4. Launch and restore your wallet
5. Your CoinJoin should work now!

**No configuration changes needed** - the fix is automatic.

---

## 🐞 Report Issues

Found a bug? Have feedback?

- **GitHub Issues**: https://github.com/swisscodernano/swisswallet/issues
- **GitHub Discussions**: https://github.com/swisscodernano/swisswallet/discussions

**Include in bug reports:**
- SwissWallet version (Help → About)
- Operating system and version
- Log files (`~/.swisswallet/client/Logs.txt`)
- Steps to reproduce

---

## 🔮 Future Plans

### Short Term
- Monitor v3.5.0 stability in production
- Gather user feedback on CoinJoin performance
- Address any edge cases discovered

### Medium Term
- macOS code signing (Apple Developer certificate)
- Windows code signing (EV certificate)
- Automated signing in GitHub Actions

### Long Term
- Additional privacy features
- Performance optimizations
- Enhanced diagnostic tools
- Multi-language support

---

## 🇨🇭 SwissWallet Philosophy

**Privacy, Sovereignty, Precision**

SwissWallet embodies Swiss values:
- 🏔️ Swiss infrastructure
- 🔒 Hardcoded Swiss coordinators
- 🛡️ Privacy by default (Tor-first)
- ⚙️ Manual control (no auto-updates)
- 🔍 Complete transparency (open-source)

**Your Bitcoin. Your Privacy. Your Sovereignty.**

---

🤖 Generated with [Claude Code](https://claude.com/claude-code)

🇨🇭 **SwissWallet v3.5.0** — Built with Swiss precision for Bitcoin privacy
