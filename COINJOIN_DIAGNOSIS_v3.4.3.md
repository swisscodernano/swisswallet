# CoinJoin Diagnosis Report - SwissWallet v3.4.3

## Problem Summary

**Issue**: CoinJoin shows "Awaiting coinjoin" but never starts mixing coins. Coordinator sees 0 input registrations from the wallet.

**Root Cause Identified**: The Swiss coordinator appears to have **no active CoinJoin rounds** available. The wallet is working correctly but times out waiting for rounds that don't exist.

## What's Happening (Technical Flow)

1. ✅ **Wallet Starts**: BitDen3 wallet loads successfully
2. ✅ **Tor Connection**: Successfully connects via `127.0.0.1:37150`
3. ✅ **Coordinator Registration**: Connects to Swiss coordinator onion service
4. ✅ **Synchronization**: Wallet syncs to latest block (918488+)
5. ✅ **CoinJoin Initiated**: Client starts CoinJoin process
6. ✅ **Sanity Checks Pass**: Coins are eligible for mixing
7. ⏳ **Waiting for Round**: Client waits for suitable coordinator round
8. ⏱️ **Timeout (10 min)**: No suitable round found
9. ❌ **Cancellation**: `CoinJoinClient was cancelled`
10. 🔄 **Auto Restart**: Process repeats every ~10 minutes

## Diagnostic Logging Added in v3.4.3

### Round Wait Detection

The new version adds these log messages to identify the problem:

**When CoinJoin starts waiting:**
```
⏳ Waiting for suitable CoinJoin round from coordinator (max wait: 10 minutes)...
```

**If a suitable round is found:**
```
✅ Found suitable round {roundId} in phase {phase}
```

**If timeout occurs (no rounds):**
```
⏱️ Timeout: No suitable CoinJoin round found after 10 minutes
```

### Additional Diagnostics (from v3.4.2)

**All coins already private:**
```
🎉 All coins are already private! Privacy percentage: 100%
```

**No eligible coins:**
```
⚠️ No coins eligible to mix - all candidates are already private (target anonymity score: XX)
```

**PlebStop threshold:**
```
⛔ PlebStop threshold active - Confirmed balance X.XXXXXXXX BTC below threshold Y.YYYYYYYY BTC
```

## Testing Instructions

### Step 1: Verify Version

Install from: `packages/SwissWallet-3.4.3-macOS-arm64.zip`

Check version in the app:
- Should show **v3.4.3** "Swiss Round Detection"

### Step 2: Run Test

1. **Open SwissWallet**
2. **Load BitDen3 wallet**
3. **Wait 12 minutes minimum** (10 min timeout + 2 min buffer)
4. **Check logs** at: `~/.swisswallet/client/Logs.txt`

### Step 3: Analyze Logs

Look for these patterns:

**Pattern A: No Rounds Available (Most Likely)**
```
⏳ Waiting for suitable CoinJoin round from coordinator (max wait: 10 minutes)...
[10 minutes pass with no activity]
⏱️ Timeout: No suitable CoinJoin round found after 10 minutes
CoinJoinClient was cancelled.
CoinJoinClient restart automatically.
```

**Pattern B: Coins Already Private**
```
🎉 All coins are already private! Privacy percentage: 100%
CoinJoinClient restart automatically.
```

**Pattern C: PlebStop Triggered**
```
⛔ PlebStop threshold active - Balance 0.00040000 BTC below threshold 0.00010000 BTC
```

**Pattern D: Round Found (Success!)**
```
⏳ Waiting for suitable CoinJoin round from coordinator...
✅ Found suitable round abc123... in phase InputRegistration
[CoinJoin proceeds]
```

## Expected Result

Based on the symptoms, you should see **Pattern A** (No Rounds Available), which indicates:

**The coordinator is not running active CoinJoin rounds.**

## Coordinator Round Requirements

For a round to be suitable, it must meet ALL these criteria:

1. **Phase**: Must be in `InputRegistration` phase
2. **Time Remaining**: At least 1 minute before input registration closes
3. **Not a Blame Round**: Must be a normal mixing round
4. **Reasonable Parameters**:
   - Minimum output amount < 1 BTC (sanity check)
   - Minimum input count ≥ 10 participants (Swiss default)
   - Mining fee rate ≤ 50 sat/vB (configurable max)

## Next Steps

### If Pattern A (No Rounds):

**Contact Coordinator Administrator:**

```
Coordinator: http://rhuvjl2kosdi3xgnmkr4bwnvpmlsvupajkubuazxendgtorvi2q4nhyd.onion
```

Ask:
1. Is the coordinator actively running rounds?
2. What is the round schedule? (e.g., every 2 hours, on-demand, etc.)
3. Are there minimum participant requirements before rounds start?
4. Can they verify coordinator logs show the wallet connecting?

### If Pattern B (Coins Already Private):

**Your coins are already sufficiently mixed!**

- Current privacy: 64% means most coins have good anonymity scores
- The 3 completed CoinJoins in your transaction history already mixed your coins
- Privacy target might already be achieved for these specific coins
- You can:
  - Receive new "red" coins that need mixing
  - Lower the anonymity score target in settings (not recommended)
  - Accept that your coins are already private

### If Pattern C (PlebStop):

**Balance below threshold:**

- Current balance: ~0.0004 BTC (40,000 sats)
- PlebStop threshold: 0.0001 BTC (10,000 sats) configured
- The balance IS above threshold, so this shouldn't trigger
- If it does, check coin confirmation status

## Current Wallet Status

From screenshots and logs:

```
Wallet: BitDen3
Balance: 40,000 sats (0.0004 BTC)
Privacy: 64%
Transactions:
  - 1 receive (46,931 sats) - Aug 2, 2025
  - 3 CoinJoins (completed successfully) - Aug 2-3, 2025
  - All confirmed with 10,000+ confirmations
Stop CoinJoin Threshold: 10,000 sats
AutoCoinJoin: Needs verification (check Wallet Settings)
```

## Bitcoin RPC Note

The logs show repeated RPC "Unauthorized" warnings:
```
WARNING: Bitcoin RPC interface failed to fetch block filters
401 (Unauthorized)
```

**This is NOT the problem.** Bitcoin RPC is:
- ✅ Optional for CoinJoin (not required)
- ✅ Only used for local block verification
- ⚠️ Misconfigured credentials cause noise in logs
- 💡 Recommended: Disable RPC in Settings → Bitcoin → Bitcoin Core

## File Locations

**Logs**: `~/.swisswallet/client/Logs.txt`
**Config**: `~/.swisswallet/client/Config.json`
**UI Config**: `~/.swisswallet/client/UiConfig.json`
**Wallet Data**: `~/.swisswallet/client/Wallets/BitDen3.json`

## Version History

- **v3.4.0**: Changed AutoCoinJoin default to ON (new wallets only)
- **v3.4.1**: Added enhanced error logging (🎉, ❌)
- **v3.4.2**: Added PlebStop threshold logging (⛔)
- **v3.4.3**: Added round wait timeout logging (⏳, ✅, ⏱️) ← **Current**

## Summary

The wallet is functioning correctly. The issue is that the Swiss coordinator has no active CoinJoin rounds for the wallet to join. The v3.4.3 logs will confirm this diagnosis.

Once the coordinator starts running rounds, CoinJoin should begin automatically (assuming AutoCoinJoin is enabled in Wallet Settings).

---

**Generated**: 2025-10-11
**Build**: SwissWallet v3.4.3 "Swiss Round Detection"
**Debug Session**: CoinJoin diagnosis with enhanced coordinator logging
