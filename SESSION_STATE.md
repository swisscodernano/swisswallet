# SwissWallet Debug Session - State Document

**Last Updated**: 2025-10-11 02:00 UTC
**Current Version**: v3.4.4
**Status**: Waiting for user testing

---

## 🎯 Current Problem

**Issue**: CoinJoin shows "Awaiting coinjoin" but never starts mixing coins.

**Root Cause (Identified)**: The Swiss coordinator appears to have no suitable CoinJoin rounds available for the wallet to join.

---

## 🔍 Investigation Summary

### What We Know:

1. ✅ **Wallet is working correctly**
   - Starts successfully
   - Tor connection works (127.0.0.1:37150)
   - Coordinator is reachable via onion service
   - Wallet synchronizes to latest block

2. ✅ **CoinJoin process works**
   - Client initiates CoinJoin
   - Sanity checks pass (coins are eligible)
   - Waits for coordinator round

3. ❌ **Round availability issue**
   - After 10 minutes: timeout (no suitable round found)
   - Client cancelled and auto-restarts
   - Coordinator admin reports: 0 input registrations received

### Evidence from Logs:

```
00:25:11 - Wallet 'BitDen3' started
00:37:22 - CoinJoinClient was cancelled (12 min later)
00:37:22 - CoinJoinClient restart automatically
```

**Pattern**: 10-12 minute wait → timeout → restart loop

---

## 🛠️ Diagnostic Builds

### Version History:

| Version | Purpose | Status |
|---------|---------|--------|
| v3.4.0 | AutoCoinJoin default ON for new wallets | ✅ Released |
| v3.4.1 | Enhanced error logging (🎉, ❌) | ✅ Released |
| v3.4.2 | PlebStop threshold logging (⛔) | ✅ Released |
| v3.4.3 | Round wait timeout logging (⏳, ⏱️, ✅) | ⚠️ Build issue |
| v3.4.4 | **Clean rebuild with correct versioning** | ✅ **CURRENT** |

### v3.4.4 New Diagnostics:

**Round Wait Logging** (in `CoinJoinClient.cs`):
```csharp
⏳ Waiting for suitable CoinJoin round from coordinator (max wait: 10 minutes)...
✅ Found suitable round {roundId} in phase {phase}
⏱️ Timeout: No suitable CoinJoin round found after 10 minutes
```

**Enhanced Error Logging** (in `CoinJoinManager.cs`):
```csharp
🎉 All coins are already private! Privacy percentage: 100%
⚠️ No coins eligible to mix - all candidates are already private (target anonymity score: XX)
⛔ PlebStop threshold active - Confirmed balance X.XX BTC below threshold Y.YY BTC
```

---

## 📊 Current Wallet State

**Wallet**: BitDen3
**Balance**: 40,000 sats (0.0004 BTC)
**Privacy**: 64%
**Stop Threshold**: 10,000 sats
**AutoCoinJoin**: Unknown (needs verification in v3.4.4)

**Transaction History**:
- 1 receive: 46,931 sats (Aug 2, 2025)
- 3 CoinJoins: Completed successfully (Aug 2-3, 2025)
- All confirmed: 10,000+ confirmations

---

## 🔧 Technical Details

### Round Requirements (Why Wallet Might Not Join):

A round must meet ALL these criteria:

1. **Phase**: `InputRegistration`
2. **Time Remaining**: > 1 minute before input registration closes
3. **Not Blame Round**: Must be normal mixing round
4. **Output Amount**: `AllowedOutputAmounts.Min < 1 BTC` (sanity check)
5. **Mining Fee**: `MiningFeeRate ≤ 50 sat/vB` (configurable max)
6. **Min Inputs**: `MinInputCountByRound ≥ 10` (Swiss default)

### Coordinator Info:

**Onion Service**: `http://rhuvjl2kosdi3xgnmkr4bwnvpmlsvupajkubuazxendgtorvi2q4nhyd.onion`
**Clearnet Fallback**: `https://wasabi.swisscoordinator.app`

**Admin Reports**:
- ✅ Rounds created every hour
- ✅ Nostr announcements every 15 minutes
- ✅ `/wabisabi/status` requests from various IPs
- ❌ ZERO input registration requests
- ❌ 0 actual inputs in all rounds

---

## ⚠️ Misdiagnosis to Avoid

### Bitcoin RPC is NOT the Problem!

**Sistemista incorrectly claims**: "401 Unauthorized RPC error is blocking CoinJoin"

**Reality**:
- Bitcoin RPC is **OPTIONAL** for CoinJoin
- RPC is only used for: local block verification, fee estimation, alternative broadcast
- CoinJoin uses the **Swiss Indexer** for filters, not RPC
- The 401 error is just noise in logs
- **DO NOT waste time configuring RPC credentials**

**To silence the noise**: Settings → Bitcoin → Bitcoin Core → Disable "Use Bitcoin Core"

---

## 📝 Next Session Tasks

### 1. User Testing (Priority 1)

**User needs to**:
1. Download v3.4.4: https://github.com/swisscodernano/swisswallet/releases/tag/v3.4.4
2. Install `SwissWallet-3.4.4-macOS-arm64.zip`
3. Verify version shows **3.4.4 "Swiss Round Detection"**
4. Open BitDen3 wallet
5. Wait **minimum 12 minutes**
6. Copy logs from `~/.swisswallet/client/Logs.txt`
7. Share logs here

### 2. Log Analysis (Priority 1)

Look for these patterns in new logs:

**Pattern A: No Rounds** (Most Likely)
```
⏳ Waiting for suitable CoinJoin round from coordinator (max wait: 10 minutes)...
[10 minutes of silence]
⏱️ Timeout: No suitable CoinJoin round found after 10 minutes
CoinJoinClient was cancelled.
CoinJoinClient restart automatically.
```
→ **Action**: Coordinator needs to fix round parameters or scheduling

**Pattern B: All Coins Private**
```
🎉 All coins are already private! Privacy percentage: 100%
```
→ **Action**: No action needed, coins already sufficiently mixed

**Pattern C: PlebStop**
```
⛔ PlebStop threshold active - Balance 0.00040000 BTC below threshold 0.00010000 BTC
```
→ **Action**: Verify threshold setting (should not trigger with 40k sats balance)

**Pattern D: Success!**
```
⏳ Waiting for suitable CoinJoin round...
✅ Found suitable round abc123... in phase InputRegistration
[CoinJoin proceeds normally]
```
→ **Action**: Monitor if inputs actually register

### 3. Coordinator Investigation (Priority 2)

**Questions for sistemista** (after confirming Pattern A):

```
Quando viene creato un round, verifica:

1. Durata fase InputRegistration?
   → Deve essere > 2 minuti

2. Valore di AllowedOutputAmounts.Min?
   → Deve essere < 100000000 satoshi (1 BTC)

3. MiningFeeRate del round?
   → Deve essere ≤ 50 sat/vB

4. MinInputCountByRound?
   → Deve essere ≥ 10

5. Tipo di round?
   → I wallet saltano i "blame rounds"

Controlla i log del coordinator per vedere se:
- I round esistono effettivamente
- I parametri sono nei range corretti
- Il wallet si connette e fa query /wabisabi/status
```

---

## 🗂️ Important Files

### Repository Structure:
```
/home/swisswallet/swisswallet/
├── WalletWasabi/
│   ├── Helpers/Constants.cs (version number)
│   ├── WabiSabi/Client/CoinJoin/
│   │   ├── Manager/CoinJoinManager.cs (main orchestration)
│   │   └── Client/CoinJoinClient.cs (round wait logic)
├── packages/ (build outputs)
├── COINJOIN_DIAGNOSIS_v3.4.3.md (troubleshooting guide)
└── SESSION_STATE.md (this file)
```

### Key Code Locations:

**Round Wait Logic** (`CoinJoinClient.cs:74-103`):
- Waits max 10 minutes for suitable round
- Logs when waiting starts (⏳)
- Logs when round found (✅) or timeout (⏱️)

**Sanity Checks** (`CoinJoinManager.cs:200-228`):
- Checks PlebStop threshold (⛔)
- Checks if all coins private (🎉)
- Checks if no eligible coins (⚠️)

**Version Definition** (`Constants.cs:103-104`):
```csharp
public static readonly Version ClientVersion = new(3, 4, 4);
public static readonly string VersionName = "Swiss Round Detection";
```

---

## 🔄 Build Process

### To rebuild if needed:

```bash
cd /home/swisswallet/swisswallet

# Clean everything
rm -rf packages/* build/* obj bin

# Set dotnet path
export DOTNET_ROOT="/home/swisswallet/.dotnet"
export PATH="$PATH:$DOTNET_ROOT"

# Clean build
dotnet clean
./Contrib/build-all.sh

# Create release
gh release create v3.4.X \
  --title "SwissWallet v3.4.X - Description" \
  --notes "Release notes..." \
  packages/SwissWallet-3.4.X-*.zip \
  packages/SwissWallet-3.4.X-*.tar.gz
```

---

## 📈 Token Optimization Strategy

### For Next Session:

1. **Start with this file** - Contains all context
2. **Reference specific sections** - Use grep to find relevant parts
3. **Read only needed files** - Don't re-read entire codebase
4. **Use targeted searches** - Grep specific patterns instead of broad reads

### Key Context to Preserve:

- Round wait timeout = 10 minutes (`_maxWaitingTimeForRound`)
- Round criteria in `WaitForRoundAsync()` predicate
- User's wallet: BitDen3, 40k sats, 64% privacy
- Coordinator admin sees: 0 inputs, rounds created hourly
- RPC errors are NOT the problem

---

## 🎯 Success Criteria

**Session Complete When**:

1. ✅ User confirms v3.4.4 installed correctly
2. ✅ New diagnostic logs show exact failure reason
3. ✅ Coordinator issue identified and communicated to admin
4. ✅ Fix implemented and CoinJoin starts working

**OR**

1. ✅ Logs confirm coins are already sufficiently private
2. ✅ User understands no further action needed

---

## 💡 Quick Reference

**User's Mac Log Path**: `~/.swisswallet/client/Logs.txt`

**GitHub Release**: https://github.com/swisscodernano/swisswallet/releases/tag/v3.4.4

**Diagnosis Doc**: `/home/swisswallet/swisswallet/COINJOIN_DIAGNOSIS_v3.4.3.md`

**Current Working Directory**: `/home/swisswallet/swisswallet`

---

**Next Session Start**: Read this file first, then ask user for test results.
