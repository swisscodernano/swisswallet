# macOS Code Signing for SwissWallet

## Problem: Gatekeeper Warning

Without code signing, macOS users see "SwissWallet.app is damaged" or need to run:
```bash
xattr -cr /Applications/SwissWallet.app
```

This is because macOS Gatekeeper blocks unsigned apps downloaded from the internet.

---

## Solution: Apple Developer Certificate

To eliminate this warning, SwissWallet needs to be **code signed** and **notarized** by Apple.

### Requirements

1. **Apple Developer Account** ($99/year)
   - https://developer.apple.com/programs/
   - Requires Apple ID with 2FA enabled

2. **macOS Machine**
   - Needed for signing and notarization
   - Xcode or Xcode Command Line Tools required

3. **Developer ID Certificate**
   - Type: "Developer ID Application"
   - Used to sign apps distributed outside Mac App Store

---

## Step-by-Step Process

### 1. Enroll in Apple Developer Program

1. Go to https://developer.apple.com/programs/
2. Sign in with your Apple ID
3. Complete enrollment ($99/year)
4. Enable Two-Factor Authentication if not already enabled

### 2. Create Developer ID Certificate

1. Go to https://developer.apple.com/account/resources/certificates/list
2. Click "+" to create new certificate
3. Select **"Developer ID Application"**
4. Follow prompts to create Certificate Signing Request (CSR)
5. Download the `.cer` file

### 3. Export Certificate as .p12

1. Open **Keychain Access** on macOS
2. Find your "Developer ID Application" certificate
3. Right-click → Export
4. Save as `.p12` (Personal Information Exchange)
5. Set a strong password (save it securely!)

### 4. Get App-Specific Password for Notarization

1. Go to https://appleid.apple.com/account/manage
2. Sign in with your Apple ID
3. Under "Security" → "App-Specific Passwords"
4. Generate new password with label "SwissWallet Notarization"
5. **Save this password** - you'll need it for notarization

---

## GitHub Actions Integration

### Option 1: Manual Signing (Current)

**Pros:**
- No costs
- Full control

**Cons:**
- Users need to bypass Gatekeeper manually
- Less professional appearance

**User workaround:**
```bash
# Method 1: Remove quarantine attribute
xattr -cr /Applications/SwissWallet.app

# Method 2: Right-click → Open (first time only)
# This adds exception to Gatekeeper
```

### Option 2: GitHub Actions with Apple Certificates (Recommended)

Store certificates as GitHub Secrets and sign during build.

**Setup:**

1. **Add GitHub Secrets:**
   ```
   MACOS_CERTIFICATE_P12: Base64-encoded .p12 file
   MACOS_CERTIFICATE_PASSWORD: Password for .p12
   APPLE_ID: Your Apple Developer email
   APPLE_APP_PASSWORD: App-specific password
   APPLE_TEAM_ID: Your team ID (from developer.apple.com)
   ```

2. **Update GitHub Actions workflow:**
   ```yaml
   - name: Import Code Signing Certificate
     run: |
       echo "${{ secrets.MACOS_CERTIFICATE_P12 }}" | base64 --decode > certificate.p12
       security create-keychain -p actions build.keychain
       security default-keychain -s build.keychain
       security unlock-keychain -p actions build.keychain
       security import certificate.p12 -k build.keychain -P "${{ secrets.MACOS_CERTIFICATE_PASSWORD }}" -T /usr/bin/codesign
       security set-key-partition-list -S apple-tool:,apple:,codesign: -s -k actions build.keychain

   - name: Sign Application
     run: |
       codesign --deep --force --verify --verbose --sign "Developer ID Application: Your Name (TEAMID)" \
         --options runtime --entitlements Contrib/entitlements.plist \
         SwissWallet.app

   - name: Notarize Application
     run: |
       # Create ZIP for notarization
       ditto -c -k --keepParent SwissWallet.app SwissWallet.zip

       # Submit for notarization
       xcrun notarytool submit SwissWallet.zip \
         --apple-id "${{ secrets.APPLE_ID }}" \
         --password "${{ secrets.APPLE_APP_PASSWORD }}" \
         --team-id "${{ secrets.APPLE_TEAM_ID }}" \
         --wait

       # Staple the ticket
       xcrun stapler staple SwissWallet.app
   ```

### Option 3: Self-Signing (NOT Recommended)

Self-signed certificates don't bypass Gatekeeper - users still need workarounds.

---

## Cost Comparison

| Option | Cost | User Experience | Security |
|--------|------|-----------------|----------|
| No Signing (Current) | **$0** | Users need `xattr -cr` or right-click → Open | ⚠️ Users must bypass security |
| Apple Code Signing | **$99/year** | Double-click to install - no warnings | ✅ Full Gatekeeper approval |
| Enterprise Signing | **$299/year** | Only for internal distribution | ✅ Enterprise-level |

---

## Recommended Approach for SwissWallet

### Short Term (Current - v3.0.0)

**Document the workaround clearly:**

Update README and documentation to show users:

1. **Method 1 (Easiest):**
   ```
   Right-click SwissWallet.app → Open
   Click "Open" in the dialog
   ```
   This creates a permanent exception.

2. **Method 2 (Command Line):**
   ```bash
   xattr -cr /Applications/SwissWallet.app
   ```

### Long Term (Future Releases)

**Invest in Apple Developer Account:**

1. Enroll in Apple Developer Program ($99/year)
2. Create Developer ID certificate
3. Set up GitHub Actions with signing
4. All future releases will be signed and notarized
5. Users can simply double-click to install

**Benefits:**
- Professional appearance
- No security warnings
- Builds trust with users
- Better user experience
- No manual workarounds needed

---

## Implementation Checklist

- [ ] Decide on signing strategy (Apple Developer vs manual workaround)
- [ ] If using Apple signing:
  - [ ] Enroll in Apple Developer Program
  - [ ] Create Developer ID certificate
  - [ ] Export as .p12
  - [ ] Generate app-specific password
  - [ ] Add secrets to GitHub
  - [ ] Update `.github/workflows/swisswallet-release.yml`
  - [ ] Create entitlements.plist file
  - [ ] Test signing on local Mac
  - [ ] Test notarization workflow
- [ ] Update documentation:
  - [ ] README installation instructions
  - [ ] Release notes
  - [ ] User guides

---

## Additional Resources

- **Apple Developer Program:** https://developer.apple.com/programs/
- **Notarization Guide:** https://developer.apple.com/documentation/security/notarizing_macos_software_before_distribution
- **Code Signing Guide:** https://developer.apple.com/library/archive/technotes/tn2206/_index.html
- **Wasabi Wallet Implementation:** [MacOsSigning.md](../WalletWasabi.Documentation/Guides/MacOsSigning.md)

---

## Questions?

- **GitHub Discussions:** https://github.com/swisscodernano/swisswallet/discussions
- **Security Issues:** See [SECURITY.md](../SECURITY.md)

---

🇨🇭 **SwissWallet** — Built with Swiss precision for Bitcoin privacy
