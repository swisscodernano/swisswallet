# 🇨🇭 SwissWallet GitHub Release Setup

## Release Infrastructure Configuration

**Target**: Automated GitHub releases for SwissWallet multi-platform distribution
**Version**: v3.2.1 Swiss Security Release

---

## 📋 Repository Structure

### 🗂️ Required Repository Setup
```
swisswallet/
├── .github/
│   └── workflows/
│       ├── release.yml          # Main release workflow
│       ├── build-test.yml       # Build testing
│       └── security-scan.yml    # Security scanning
├── Contrib/
│   ├── swiss-release.sh         # Multi-platform build script
│   ├── build-all.sh            # All platforms builder
│   └── release-assets/          # Release asset templates
├── docs/
│   ├── INSTALLATION_GUIDE.md    # Installation instructions
│   ├── SWISS_SECURITY.md       # Security documentation
│   └── TROUBLESHOOTING.md      # Support guide
└── releases/                    # Release documentation
    ├── v2.0.0/
    │   ├── RELEASE_NOTES.md     # Version release notes
    │   └── CHANGELOG.md         # Detailed changes
    └── templates/               # Release templates
```

---

## 🚀 GitHub Actions Release Workflow

### 📄 `.github/workflows/release.yml`

```yaml
name: 🇨🇭 SwissWallet Release

on:
  push:
    tags:
      - 'v*.*.*'  # Triggers on version tags like v2.0.0
  workflow_dispatch:
    inputs:
      version:
        description: 'Release version (e.g., v3.2.1)'
        required: true
        default: 'v3.2.1'

env:
  DOTNET_VERSION: '8.0.x'
  RELEASE_VERSION: ${{ github.event.inputs.version || github.ref_name }}

jobs:
  # Build for all platforms
  build-multiplatform:
    name: 🔨 Build Swiss Packages
    runs-on: ubuntu-latest
    strategy:
      matrix:
        platform: [win-x64, linux-x64, osx-x64, osx-arm64]

    steps:
    - name: 📥 Checkout SwissWallet
      uses: actions/checkout@v4

    - name: 🔧 Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: ${{ env.DOTNET_VERSION }}

    - name: 🇨🇭 Build Swiss Platform (${{ matrix.platform }})
      run: |
        chmod +x ./Contrib/swiss-release.sh
        ./Contrib/swiss-release.sh ${{ matrix.platform }}

    - name: 📦 Upload Platform Artifacts
      uses: actions/upload-artifact@v4
      with:
        name: swisswallet-${{ matrix.platform }}
        path: packages/
        retention-days: 7

  # Create Debian package
  build-debian:
    name: 🐧 Build Debian Package
    runs-on: ubuntu-latest

    steps:
    - name: 📥 Checkout SwissWallet
      uses: actions/checkout@v4

    - name: 🔧 Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: ${{ env.DOTNET_VERSION }}

    - name: 🐧 Build Debian Package
      run: |
        chmod +x ./Contrib/swiss-release.sh
        ./Contrib/swiss-release.sh linux

    - name: 📦 Upload Debian Artifact
      uses: actions/upload-artifact@v4
      with:
        name: swisswallet-debian
        path: packages/*.deb
        retention-days: 7

  # Security scanning
  security-scan:
    name: 🔒 Swiss Security Scan
    runs-on: ubuntu-latest
    needs: [build-multiplatform]

    steps:
    - name: 📥 Checkout SwissWallet
      uses: actions/checkout@v4

    - name: 🛡️ Run Security Scan
      run: |
        # Verify Swiss coordinator hardcoding
        grep -r "SwissCoordinatorOnion" WalletWasabi/Helpers/Constants.cs
        grep -r "swisscoordinator.app" WalletWasabi/Helpers/Constants.cs

        # Verify anti-tampering
        grep -r "Swiss Security: Force Swiss coordinators" WalletWasabi.Daemon/Config/Config.cs

        echo "✅ Swiss security verification passed"

  # Create GitHub Release
  create-release:
    name: 🚀 Create Swiss Release
    runs-on: ubuntu-latest
    needs: [build-multiplatform, build-debian, security-scan]

    steps:
    - name: 📥 Checkout SwissWallet
      uses: actions/checkout@v4

    - name: 📦 Download All Artifacts
      uses: actions/download-artifact@v4
      with:
        path: release-assets/

    - name: 🔍 List Release Assets
      run: |
        find release-assets/ -name "*.zip" -o -name "*.tar.gz" -o -name "*.deb"

    - name: 📝 Generate Release Notes
      run: |
        # Create comprehensive release notes
        cat > RELEASE_BODY.md << 'EOF'
        # 🇨🇭 SwissWallet ${{ env.RELEASE_VERSION }} - Swiss Security Release

        ## Maximum Privacy & Security with Swiss Infrastructure

        ### 🔒 Swiss Security Features
        - **Hardcoded Swiss Coordinators**: Anti-tampering protection
        - **Tor-First Architecture**: Maximum privacy by default
        - **Swiss Bitcoin RPC**: Private blockchain access via Tor
        - **Multi-Platform Support**: macOS, Windows, Linux

        ### 📦 Download Packages
        Choose your platform below:

        ### 🍎 macOS
        - **Apple Silicon (M1/M2/M3)**: SwissWallet-${{ env.RELEASE_VERSION }}-macOS-arm64.zip
        - **Intel Mac**: SwissWallet-${{ env.RELEASE_VERSION }}-macOS-x64.zip

        ### 🪟 Windows
        - **Windows 10/11 x64**: SwissWallet-${{ env.RELEASE_VERSION }}-win-x64.zip

        ### 🐧 Linux
        - **Universal x64**: SwissWallet-${{ env.RELEASE_VERSION }}-linux-x64.zip
        - **Archive**: SwissWallet-${{ env.RELEASE_VERSION }}-linux-x64.tar.gz
        - **Debian/Ubuntu**: SwissWallet-${{ env.RELEASE_VERSION }}.deb

        ### 🔐 Security Verification
        ```bash
        # Verify download integrity
        sha256sum -c SHA256SUMS
        ```

        ### 📚 Documentation
        - [Installation Guide](docs/INSTALLATION_GUIDE.md)
        - [Swiss Security Features](docs/SWISS_SECURITY.md)
        - [Troubleshooting](docs/TROUBLESHOOTING.md)

        **🇨🇭 Swiss Security: Maximum Protection Enabled**

        *Privacy. Security. Switzerland.*
        EOF

    - name: 🔐 Generate Checksums
      run: |
        cd release-assets/
        find . -name "*.zip" -o -name "*.tar.gz" -o -name "*.deb" | \
        xargs sha256sum > SHA256SUMS

    - name: 🚀 Create GitHub Release
      uses: softprops/action-gh-release@v1
      with:
        tag_name: ${{ env.RELEASE_VERSION }}
        name: 🇨🇭 SwissWallet ${{ env.RELEASE_VERSION }} - Swiss Security
        body_path: RELEASE_BODY.md
        draft: false
        prerelease: false
        generate_release_notes: true
        files: |
          release-assets/**/*.zip
          release-assets/**/*.tar.gz
          release-assets/**/*.deb
          release-assets/SHA256SUMS
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}

    - name: 📢 Post-Release Actions
      run: |
        echo "🎉 SwissWallet ${{ env.RELEASE_VERSION }} released successfully!"
        echo "📦 Packages: $(find release-assets/ -name "*.zip" -o -name "*.tar.gz" -o -name "*.deb" | wc -l)"
        echo "🔐 Swiss Security: Verified and hardcoded"
        echo "🌍 Distribution: Multi-platform ready"
```

---

## 🔧 Release Automation Scripts

### 📄 `Contrib/release-automation.sh`

```bash
#!/usr/bin/env bash
#------------------------------------------------------------------------------------#
# SwissWallet Release Automation                                                    #
# Automates the complete release process for all platforms                          #
#------------------------------------------------------------------------------------#

set -e

VERSION=${1:-"3.2.1"}
RELEASE_TAG="v$VERSION"
RELEASE_NAME="🇨🇭 SwissWallet $RELEASE_TAG - Swiss Security"

echo "🇨🇭 Starting SwissWallet Release Process"
echo "Version: $VERSION"
echo "Tag: $RELEASE_TAG"

# 1. Verify Git status
if [[ -n $(git status --porcelain) ]]; then
    echo "⚠️ Warning: Uncommitted changes detected"
    echo "Please commit or stash changes before release"
    exit 1
fi

# 2. Create and push release tag
echo "🏷️ Creating release tag: $RELEASE_TAG"
git tag -a "$RELEASE_TAG" -m "SwissWallet $VERSION - Swiss Security Release"
git push origin "$RELEASE_TAG"

# 3. Trigger GitHub Actions
echo "🚀 Release will be built automatically by GitHub Actions"
echo "📍 Check progress: https://github.com/swisssecurity/swisswallet/actions"

# 4. Wait for completion (optional)
echo ""
echo "🎯 Release Process Initiated Successfully!"
echo ""
echo "📋 Next Steps:"
echo "1. Monitor GitHub Actions build progress"
echo "2. Verify all platform packages are created"
echo "3. Test download and installation"
echo "4. Announce release to community"
echo ""
echo "🇨🇭 Swiss Security: Maximum Protection Enabled"
```

### 📄 `Contrib/verify-release.sh`

```bash
#!/usr/bin/env bash
#------------------------------------------------------------------------------------#
# SwissWallet Release Verification                                                  #
# Verifies Swiss security features in release packages                              #
#------------------------------------------------------------------------------------#

RELEASE_TAG=${1:-"v3.2.1"}
DOWNLOAD_URL="https://github.com/swisssecurity/swisswallet/releases/download/$RELEASE_TAG"

echo "🔍 Verifying SwissWallet $RELEASE_TAG Release"

# Download verification files
curl -L "$DOWNLOAD_URL/SHA256SUMS" -o SHA256SUMS
curl -L "$DOWNLOAD_URL/SHA256SUMS.asc" -o SHA256SUMS.asc

# Verify checksums
echo "🔐 Verifying package integrity..."
if sha256sum -c SHA256SUMS; then
    echo "✅ All packages verified successfully"
else
    echo "❌ Package verification failed"
    exit 1
fi

# Swiss security verification
echo "🇨🇭 Verifying Swiss security features..."

# Download and check one package for Swiss hardcoding
curl -L "$DOWNLOAD_URL/SwissWallet-$RELEASE_TAG-linux-x64.zip" -o test-package.zip
unzip -q test-package.zip -d test-extract/

# Check for Swiss coordinators
if grep -r "swisscoordinator.app" test-extract/ > /dev/null; then
    echo "✅ Swiss coordinators verified"
else
    echo "❌ Swiss coordinators not found"
    exit 1
fi

# Check for Swiss branding
if grep -r "SwissWallet" test-extract/ > /dev/null; then
    echo "✅ Swiss branding verified"
else
    echo "❌ Swiss branding not found"
    exit 1
fi

# Cleanup
rm -rf test-package.zip test-extract/

echo ""
echo "🎉 SwissWallet $RELEASE_TAG Release Verification Complete!"
echo "🔒 Swiss Security: Verified and Active"
echo "📦 All Packages: Integrity Confirmed"
echo "🇨🇭 Ready for Swiss Distribution"
```

---

## 🔐 Security Configuration

### 🔑 Required GitHub Secrets

```yaml
# Repository Secrets (Settings → Secrets and variables → Actions)
GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}  # Automatic
GPG_PRIVATE_KEY: ${{ secrets.GPG_PRIVATE_KEY }}  # For signing
GPG_PASSPHRASE: ${{ secrets.GPG_PASSPHRASE }}   # GPG key passphrase
```

### 🛡️ Repository Security Settings

```yaml
# Branch Protection Rules
main:
  required_status_checks:
    - "Swiss Security Scan"
    - "Multi-Platform Build"
  required_pull_request_reviews: 1
  dismiss_stale_reviews: true
  require_code_owner_reviews: true

# Security Features
vulnerability_alerts: enabled
dependency_security_updates: enabled
secret_scanning: enabled
```

---

## 📊 Release Analytics

### 📈 GitHub Release Metrics

Track release success with:
- **Download counts** per platform
- **Geographic distribution** of downloads
- **Platform popularity** (macOS vs Windows vs Linux)
- **Release adoption rate** over time

### 🔍 Monitoring Setup

```yaml
# Release monitoring workflow
name: 📊 Release Metrics
on:
  schedule:
    - cron: '0 12 * * *'  # Daily at noon

jobs:
  metrics:
    runs-on: ubuntu-latest
    steps:
    - name: 📈 Collect Download Stats
      run: |
        # GitHub API to get release download counts
        curl -H "Authorization: token ${{ secrets.GITHUB_TOKEN }}" \
        "https://api.github.com/repos/swisssecurity/swisswallet/releases" | \
        jq '.[] | {tag_name, assets: [.assets[] | {name, download_count}]}'
```

---

## 🚀 Release Workflow Commands

### 🎯 Manual Release Process

```bash
# 1. Prepare release
git checkout main
git pull origin main

# 2. Update version and create tag
./Contrib/release-automation.sh 3.2.1

# 3. Monitor GitHub Actions
# Visit: https://github.com/swisssecurity/swisswallet/actions

# 4. Verify release
./Contrib/verify-release.sh v3.2.1

# 5. Announce release
echo "🎉 SwissWallet v3.2.1 released with Swiss security!"
```

### ⚡ Emergency Release Process

```bash
# For urgent security updates
git checkout main
git cherry-pick <security-fix-commit>
./Contrib/release-automation.sh 3.2.2
```

---

## 📚 Release Documentation Templates

### 📄 Release Announcement Template

```markdown
# 🇨🇭 SwissWallet v{VERSION} Released - Swiss Security Enhanced

We're excited to announce SwissWallet v{VERSION} with enhanced Swiss security features!

## 🔒 Swiss Security Features
- Hardcoded Swiss coordinators for maximum security
- Tor-first privacy architecture
- Swiss Bitcoin RPC integration

## 📦 Download Now
- macOS: [Download](link)
- Windows: [Download](link)
- Linux: [Download](link)

## 🛡️ Security Verification
```bash
sha256sum -c SHA256SUMS
```

🇨🇭 **Swiss Security: Maximum Protection Enabled**
```

---

## ✅ Release Readiness Checklist

- [ ] **GitHub Repository**: Configured with proper structure
- [ ] **GitHub Actions**: Release workflow tested and working
- [ ] **Security Scanning**: Swiss coordinator verification active
- [ ] **Multi-Platform Builds**: All platforms building successfully
- [ ] **Documentation**: Installation guides and security docs ready
- [ ] **Checksums**: SHA256 verification implemented
- [ ] **GPG Signing**: Package signing configured (future)
- [ ] **Release Notes**: Comprehensive documentation prepared
- [ ] **Community**: Announcement channels ready

**🚀 SwissWallet GitHub Release Infrastructure: Ready for Deployment!**

---

**🇨🇭 SwissWallet Release Infrastructure**
*Automated • Secure • Swiss*