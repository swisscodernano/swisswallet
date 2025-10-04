# 🍎 SwissWallet - macOS Installation Guide

## Normal Installation (Recommended)

1. **Download** `SwissWallet-X.X.X-macOS-arm64.dmg` (M1/M2/M3) or `macOS-x64.dmg` (Intel)
2. **Open** the downloaded `.dmg` file
3. **Drag** `SwissWallet.app` to the **Applications** folder
4. **Eject** the disk image
5. **First launch**: Right-click `SwissWallet.app` → **Open**

## If You See "SwissWallet.app is damaged" Error

This happens because the app is not code-signed by Apple. Here's how to fix it:

### Method 1: Remove Quarantine Attribute (Fastest)

Open **Terminal** and run:

```bash
xattr -cr /Applications/SwissWallet.app
```

Then open SwissWallet normally.

### Method 2: Security Settings

1. Try to open SwissWallet.app (it will show the error)
2. Go to **System Preferences** → **Security & Privacy**
3. Click **"Open Anyway"** next to the SwissWallet message
4. Confirm by clicking **Open**

### Method 3: Disable Gatekeeper Temporarily (Advanced)

**⚠️ Only if methods above don't work:**

```bash
# Disable Gatekeeper
sudo spctl --master-disable

# Open SwissWallet
open /Applications/SwissWallet.app

# Re-enable Gatekeeper (recommended)
sudo spctl --master-enable
```

## Why This Happens

SwissWallet is open-source and not enrolled in Apple's Developer Program ($99/year). This means:
- ✅ **Code is transparent** - You can verify what it does
- ✅ **No tracking** - No Apple telemetry
- ⚠️ **Not code-signed** - Requires the workaround above

## Verify the Build

Since the app isn't code-signed, you can verify it yourself:

```bash
# Check the build was created by GitHub Actions
gh release view v2.0.0

# Or build it yourself from source
git clone https://github.com/swisscodernano/swisswallet.git
cd swisswallet
./Contrib/build-macos.sh
```

## Need Help?

- **GitHub Issues**: https://github.com/swisscodernano/swisswallet/issues
- **Source Code**: https://github.com/swisscodernano/swisswallet

---

🇨🇭 **SwissWallet - Swiss Privacy for Bitcoin**
