# SwissWallet Code Signing Guide

Complete guide for signing SwissWallet on all platforms to eliminate security warnings.

---

## 📋 Overview

**Current Status (v3.5.0):**
- ❌ **No code signing** - users see security warnings
- ✅ **App works perfectly** - just needs manual approval
- 🎯 **Goal**: Eliminate warnings for professional distribution

---

## 🍎 macOS Code Signing

**See detailed guide:** [MACOS_CODE_SIGNING.md](MACOS_CODE_SIGNING.md)

### Quick Summary

**Problem:**
```
"SwissWallet.app" cannot be opened because the developer cannot be verified
```

**Solutions:**

#### Option 1: User Workaround (Current - Free)
```bash
# Method 1: Right-click → Open (creates permanent exception)
# Method 2: Remove quarantine
xattr -cr /Applications/SwissWallet.app
```

#### Option 2: Apple Developer Certificate ($99/year)
1. Enroll in Apple Developer Program
2. Create "Developer ID Application" certificate
3. Sign with `codesign`
4. Notarize with `notarytool`
5. Staple notarization ticket

**Result**: Users can double-click to install - no warnings!

---

## 🪟 Windows Code Signing

### Problem

Windows SmartScreen shows:
```
Windows protected your PC
Microsoft Defender SmartScreen prevented an unrecognized app from starting
```

### Solutions

#### Option 1: User Workaround (Current - Free)
```
1. Click "More info"
2. Click "Run anyway"
```
This is safe - Windows is just being cautious about unsigned apps.

#### Option 2: Code Signing Certificate ($200-400/year)

**Types of Certificates:**

1. **Standard Code Signing** ($200-300/year)
   - Requires business verification
   - From: DigiCert, Sectigo, SSL.com
   - Process: 1-3 days verification

2. **EV Code Signing** ($300-400/year)
   - Enhanced Validation
   - Better SmartScreen reputation
   - Requires hardware token (USB)
   - From: DigiCert, Sectigo

**Best Option for SwissWallet:**
- **Sectigo EV Code Signing** (~$350/year)
- Comes with USB token
- Best reputation with SmartScreen
- Professional appearance

### How to Get Windows Code Signing Certificate

1. **Purchase Certificate**
   - Go to: https://www.sectigo.com/ssl-certificates-tls/code-signing
   - Choose "EV Code Signing Certificate"
   - Price: ~$350/year

2. **Business Verification**
   - Provide business documents
   - Verification takes 1-3 business days
   - May need:
     - Business registration
     - Tax ID
     - Address verification

3. **Receive USB Token**
   - Certificate stored on hardware token
   - Plug in USB before signing

4. **Sign the Application**
   ```powershell
   # Using SignTool (part of Windows SDK)
   signtool sign /fd SHA256 /tr http://timestamp.sectigo.com /td SHA256 /a SwissWallet.exe
   ```

5. **Verify Signature**
   ```powershell
   signtool verify /pa SwissWallet.exe
   ```

### GitHub Actions Integration (Windows)

**Add to `.github/workflows/swisswallet-release.yml`:**

```yaml
- name: Sign Windows Executable
  if: runner.os == 'Windows'
  env:
    CERTIFICATE_PASSWORD: ${{ secrets.WINDOWS_CERT_PASSWORD }}
  run: |
    # Decode certificate from base64
    echo "${{ secrets.WINDOWS_CERTIFICATE }}" | base64 --decode > certificate.pfx

    # Sign the executable
    signtool sign /f certificate.pfx /p $env:CERTIFICATE_PASSWORD /fd SHA256 /tr http://timestamp.sectigo.com /td SHA256 SwissWallet.exe

    # Verify signature
    signtool verify /pa SwissWallet.exe

    # Cleanup
    Remove-Item certificate.pfx
```

**Required GitHub Secrets:**
- `WINDOWS_CERTIFICATE`: Base64-encoded .pfx file
- `WINDOWS_CERT_PASSWORD`: Certificate password

---

## 🐧 Linux Code Signing

**Good news:** Linux doesn't require code signing!

Users can verify integrity with:
```bash
# Check SHA256 checksums (provided in release)
sha256sum SwissWallet-3.5.0-linux-x64.tar.gz

# Verify GPG signature (if provided)
gpg --verify SwissWallet-3.5.0-linux-x64.tar.gz.sig
```

**Optional: GPG Signatures**
```bash
# Sign release
gpg --detach-sign --armor SwissWallet-3.5.0-linux-x64.tar.gz

# Creates: SwissWallet-3.5.0-linux-x64.tar.gz.asc
```

---

## 💰 Cost Summary

| Platform | Option | Cost/Year | User Experience |
|----------|--------|-----------|-----------------|
| **macOS** | No signing | $0 | Users right-click → Open |
| **macOS** | Apple Developer | $99 | Double-click install ✅ |
| **Windows** | No signing | $0 | Users click "Run anyway" |
| **Windows** | Standard Cert | $200-300 | Still shows warning initially |
| **Windows** | EV Cert | $350-400 | No warnings ✅ |
| **Linux** | Checksums | $0 | Standard verification ✅ |

**Total for Full Signing:** ~$450-500/year

---

## 🎯 Recommended Approach

### Phase 1: Current (v3.5.0)
✅ **Document workarounds clearly**
- Update README with installation steps
- Clear instructions for bypassing warnings
- Explain that warnings are normal for open-source apps

### Phase 2: macOS Signing (Priority)
🍎 **Apple Developer Certificate** ($99/year)
- Most professional impact
- macOS users are security-conscious
- Relatively affordable

### Phase 3: Windows EV Signing
🪟 **Sectigo EV Certificate** ($350/year)
- Better than standard cert
- Builds SmartScreen reputation over time
- Professional appearance

### Phase 4: Linux GPG
🐧 **GPG Signatures** (Free)
- Generate GPG key
- Sign all releases
- Publish public key

---

## 🚀 Implementation Steps

### For macOS Signing

1. ✅ Enroll in Apple Developer Program
2. ✅ Create Developer ID certificate
3. ✅ Export as .p12 file
4. ✅ Generate app-specific password
5. ✅ Add GitHub Secrets:
   - `MACOS_CERTIFICATE_P12`
   - `MACOS_CERTIFICATE_PASSWORD`
   - `APPLE_ID`
   - `APPLE_APP_PASSWORD`
   - `APPLE_TEAM_ID`
6. ✅ Update workflow to sign + notarize
7. ✅ Test on clean Mac

### For Windows Signing

1. ✅ Purchase EV Code Signing Certificate
2. ✅ Complete business verification
3. ✅ Receive USB token
4. ✅ Export certificate as .pfx
5. ✅ Add GitHub Secrets:
   - `WINDOWS_CERTIFICATE`
   - `WINDOWS_CERT_PASSWORD`
6. ✅ Update workflow to sign
7. ✅ Test on Windows VM

### For Linux GPG

1. ✅ Generate GPG key for SwissWallet
2. ✅ Publish public key on keyservers
3. ✅ Add to GitHub secrets:
   - `GPG_PRIVATE_KEY`
   - `GPG_PASSPHRASE`
4. ✅ Sign all releases automatically
5. ✅ Include .asc signature files

---

## 📝 Testing Code Signing

### macOS
```bash
# Check if app is signed
codesign -dv --verbose=4 SwissWallet.app

# Check notarization
spctl -a -vv SwissWallet.app

# Should show: accepted
```

### Windows
```powershell
# Verify signature
signtool verify /pa SwissWallet.exe

# Check certificate details
signtool verify /v /pa SwissWallet.exe
```

### Linux
```bash
# Verify GPG signature
gpg --verify SwissWallet-3.5.0-linux-x64.tar.gz.asc

# Check SHA256
sha256sum -c SwissWallet-3.5.0-linux-x64.tar.gz.sha256
```

---

## 🔐 Security Best Practices

1. **Never commit certificates to git**
   - Use GitHub Secrets
   - Rotate regularly

2. **Use strong passwords**
   - Certificate passwords
   - Apple ID passwords

3. **Backup certificates securely**
   - Encrypted storage
   - Multiple backups

4. **Timestamp signatures**
   - Ensures validity after cert expires
   - Use reliable TSA servers

5. **Monitor certificate expiration**
   - Set calendar reminders
   - Renew 30 days before expiry

---

## ❓ FAQ

### Q: Why don't you sign SwissWallet?
A: Code signing costs $450-500/year. We're open-source and budget-conscious. The workarounds are safe and simple.

### Q: Is it safe to bypass the warnings?
A: Yes! The warnings just mean we haven't paid for certificates. Verify the download from our official GitHub releases.

### Q: Will you sign in the future?
A: Possibly, if we get sponsorship or donations to cover the costs.

### Q: Can users verify integrity without signatures?
A: Yes! We provide SHA256 checksums for all releases.

### Q: What about reproducible builds?
A: See [Reproducible Builds Guide](build/REPRODUCIBLE_BUILDS.md)

---

## 📚 Resources

- **Apple Notarization:** https://developer.apple.com/documentation/security/notarizing_macos_software_before_distribution
- **Windows Code Signing:** https://docs.microsoft.com/en-us/windows/win32/seccrypto/cryptography-tools
- **Sectigo EV Cert:** https://www.sectigo.com/ssl-certificates-tls/code-signing
- **DigiCert:** https://www.digicert.com/signing/code-signing-certificates

---

🇨🇭 **SwissWallet** — Built with Swiss precision for Bitcoin privacy
