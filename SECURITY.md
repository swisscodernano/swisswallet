# Security Policy

## Reporting Security Vulnerabilities

SwissWallet takes security seriously. We appreciate responsible disclosure of security vulnerabilities.

### Non-Critical Issues

If a vulnerability does **not** compromise users' privacy or security, please open a [regular GitHub issue](https://github.com/swisscodernano/swisswallet/issues/new/choose).

### Critical Security Issues

If the vulnerability **does** compromise privacy or security, please use **responsible disclosure**:

**Preferred Method:** Report a [private security advisory on GitHub](https://github.com/swisscodernano/swisswallet/security/advisories/new)

**Alternative:** Send an encrypted email to **security@swisscoordinator.app**

**PGP Encryption:**
```
Fingerprint: 718D 6019 9ED3 505B 893C C69D 958B 0AB4 0003 E569
```

Download our PGP public key: [swisscoordinator-pubkey.asc](https://swisscoordinator.app/static/swisscoordinator-pubkey.asc)

---

## Security Principles

SwissWallet is built with **Swiss security standards**:

### 🔒 Hardcoded Swiss Coordinators
- Coordinators **cannot be changed** by users
- Prevents misconfiguration attacks
- Ensures all traffic routes through Swiss infrastructure

### 🇨🇭 Swiss Jurisdiction
- All infrastructure under Swiss privacy law
- Protected by Swiss Federal Constitution Article 13 (right to privacy)
- No foreign jurisdiction exposure

### 🧅 Tor-First Architecture
- Primary: Tor onion service (`.onion`)
- Fallback: HTTPS over Tor
- No IP address exposure to coordinators

### 🛡️ No Auto-Updates
- Manual updates only via GitHub releases
- Eliminates auto-update attack vectors
- Users control when to upgrade

### 🔍 Open Source & Reproducible
- All code publicly auditable
- Reproducible builds
- Community security review

### 🚫 Zero Telemetry
- No tracking
- No phone-home functionality
- No data collection

---

## Security Best Practices for Users

### Wallet Security
- **Backup your seed phrase** - Write it on paper, store securely
- **Use strong passwords** - For wallet encryption
- **Verify downloads** - Check SHA-256 checksums and PGP signatures
- **Keep software updated** - Install security updates promptly

### Network Security
- **Use Tor** - SwissWallet uses Tor by default
- **Verify onion address** - `rhuvjl2kosdi3xgnmkr4bwnvpmlsvupajkubuazxendgtorvi2q4nhyd.onion`
- **Avoid public WiFi** - Use trusted networks when managing funds

### System Security
- **Keep OS updated** - Install security patches
- **Use antivirus** - On Windows systems
- **Full disk encryption** - Protect wallet data at rest
- **Secure your device** - Lock screen when unattended

### Privacy Best Practices
- **Use CoinJoin** - Enhance transaction privacy
- **Label addresses** - Track where funds come from
- **Avoid address reuse** - Use fresh addresses for each transaction
- **Run your own node** - Optional, for maximum sovereignty

---

## Known Security Considerations

### Bitcoin RPC (Optional Feature)
- **Disabled by default** - Must be manually enabled
- **Credentials required** - Set via environment variables only
- **Local node only** - Do not expose RPC to internet
- **See documentation** - [docs/build/BITCOIN_CORE_SETUP.md](docs/build/BITCOIN_CORE_SETUP.md)

### Tor Dependency
- SwissWallet bundles Tor daemon
- Tor connectivity required for Swiss coordinators
- Fallback to HTTPS if Tor fails (over Tor if possible)

### Data Directory
- Located at `~/.swisswallet/`
- Contains wallet files and configuration
- **Backup regularly**
- **Encrypt if possible**

---

## Security Audit Status

SwissWallet is a fork of [Wasabi Wallet](https://github.com/WalletWasabi/WalletWasabi), which has undergone independent security audits.

**Swiss-Specific Changes:**
- Hardcoded Swiss coordinators
- Disabled auto-updates
- Modified configuration system
- Swiss UI branding

**Audit Recommendations:**
We welcome independent security audits of Swiss-specific changes. Contact us via security@swisscoordinator.app.

---

## Responsible Disclosure Timeline

1. **Report received** - We acknowledge within 48 hours
2. **Initial assessment** - Within 7 days
3. **Fix developed** - Timeframe depends on severity
4. **Coordinated disclosure** - With reporter agreement
5. **Public disclosure** - After fix is released

We appreciate security researchers who follow responsible disclosure practices.

---

## Security Hall of Fame

We will recognize security researchers who responsibly disclose vulnerabilities (with their permission).

*No entries yet - be the first!*

---

## Contact

- **Security Email:** security@swisscoordinator.app (PGP encrypted)
- **GitHub Security Advisories:** [Report privately](https://github.com/swisscodernano/swisswallet/security/advisories/new)
- **General Issues:** [GitHub Issues](https://github.com/swisscodernano/swisswallet/issues)

---

🇨🇭 **Swiss Security. Swiss Privacy. Swiss Sovereignty.**
