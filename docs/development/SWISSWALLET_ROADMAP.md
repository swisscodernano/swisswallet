# 🇨🇭 SwissWallet Development Roadmap

> **Mission:** Transform SwissWallet into a premium, Swiss-branded Bitcoin privacy wallet with hardcoded security features and professional visual identity.

**Base:** Wasabi Wallet fork with enhanced Swiss security standards
**Target:** Independent, Swiss-branded wallet with locked-down privacy features
**Timeline:** 4 weeks to v3.0.0 "Swiss Edition"

---

## 📊 Progress Overview

- **Phase 1 (Critical Fixes):** 0/6 completed
- **Phase 2 (Swiss Identity):** 0/8 completed
- **Phase 3 (UI Redesign):** 0/10 completed
- **Phase 4 (Documentation):** 0/6 completed
- **Phase 5 (Release):** 0/4 completed

**Overall Progress:** 0/34 tasks (0%)

---

## 🚨 PHASE 1: CRITICAL FIXES (Week 1 - Days 1-3)

**Goal:** Make SwissWallet fully independent from Wasabi Wallet

### Day 1 - Data Directory & Update Manager

- [ ] **1.1** Change data directory path
  - [ ] Modify `EnvironmentHelpers.cs` to use `~/.swisswallet/` instead of `~/.walletwasabi/`
  - [ ] Update all references to data directory
  - [ ] Test wallet creation in new directory
  - [ ] Verify no conflicts with Wasabi Wallet when both running
  - **Files:** `WalletWasabi/Helpers/EnvironmentHelpers.cs`
  - **Testing:** Run both Wasabi and SwissWallet simultaneously

- [ ] **1.2** Disable Wasabi update manager
  - [ ] Remove/disable UpdateManager service in `Global.cs`
  - [ ] Remove update notifications from UI
  - [ ] Create SwissWallet-specific update mechanism (GitHub releases)
  - **Files:** `WalletWasabi.Daemon/Global.cs`, `WalletWasabi.Fluent/ViewModels/`
  - **Testing:** Launch app, verify no update prompts appear

### Day 2 - Coordinator Lock-down

- [ ] **1.3** Make coordinator read-only in Settings UI
  - [ ] Modify `CoordinatorTabSettingsViewModel.cs` to disable editing
  - [ ] Add Swiss branding message: "🇨🇭 Swiss Coordinators - Hardcoded for Security"
  - [ ] Display both coordinators (Tor + HTTPS) with status
  - [ ] Show lock icon and explanatory text
  - **Files:** `WalletWasabi.Fluent/ViewModels/Settings/CoordinatorTabSettingsViewModel.cs`
  - **Testing:** Open Settings → Coordinator tab, verify cannot edit

- [ ] **1.4** Verify coordinator enforcement in code
  - [ ] Audit `Config.cs` to ensure TryGetCoordinatorUri() cannot be bypassed
  - [ ] Remove any config file overrides for coordinator
  - [ ] Add validation on app startup
  - **Files:** `WalletWasabi.Daemon/Config/Config.cs`
  - **Testing:** Try editing config.json manually, verify app ignores changes

### Day 3 - Branding Cleanup

- [ ] **1.5** Update application metadata
  - [ ] Change app name from "Wasabi Wallet" to "SwissWallet"
  - [ ] Update window titles throughout app
  - [ ] Update process name and identifiers
  - [ ] Update plist files for macOS
  - **Files:** All `.csproj` files, `Info.plist`, `Constants.cs`
  - **Testing:** Launch app, check window title, process name in Activity Monitor

- [ ] **1.6** Create v2.1.0 release with critical fixes
  - [ ] Tag release: `v2.1.0-critical-fixes`
  - [ ] Build all platforms (macOS, Windows, Linux)
  - [ ] Test on macOS that both wallets run independently
  - [ ] Publish GitHub release with changelog
  - **Testing:** Full multi-platform test

---

## 🎨 PHASE 2: SWISS VISUAL IDENTITY (Week 1-2 - Days 4-7)

**Goal:** Create professional Swiss branding assets and design system

### Day 4 - Logo Design

- [ ] **2.1** Design professional SwissWallet logo
  - [ ] Concept 1: Swiss cross + Bitcoin shield
  - [ ] Concept 2: Mountain silhouette + Swiss cross
  - [ ] Concept 3: Vault/safe with Swiss cross inlay
  - [ ] Create multiple sizes: 16x16, 32x32, 64x64, 128x128, 256x256, 512x512, 1024x1024
  - [ ] Export formats: PNG (transparent), SVG (vector), ICO, ICNS
  - **Deliverables:** Logo files in `Contrib/Assets/Swiss/`
  - **Tools:** Design in Figma/Illustrator or commission professional designer

- [ ] **2.2** Create app icons for all platforms
  - [ ] macOS: .icns file with all sizes
  - [ ] Windows: .ico file with all sizes
  - [ ] Linux: PNG files in hicolor spec
  - [ ] Update build scripts to use new icons
  - **Files:** `Contrib/create-swiss-icon.sh`, `WalletWasabi.Fluent.Desktop.csproj`
  - **Testing:** Build and verify icon appears in dock/taskbar

### Day 5 - Color System Implementation

- [ ] **2.3** Define Swiss color palette constants
  - [ ] Create `SwissColors.cs` with color definitions
  - [ ] Primary: Swiss Red (#DC143C), Pure White (#FFFFFF)
  - [ ] Accents: Swiss Gold (#FFD700), Alpine Silver (#C0C0C0), Dark Charcoal (#2C2C2C)
  - [ ] Semantic: Success (#00A651), Warning (#FFA500), Error (#FF3B30)
  - **File:** `WalletWasabi.Fluent/Helpers/SwissColors.cs`

- [ ] **2.4** Create Swiss theme resource dictionary
  - [ ] Define Avalonia Brushes for all colors
  - [ ] Define gradients (Swiss Premium, Alpine Frost)
  - [ ] Define text styles (headers, body, captions)
  - [ ] Define spacing constants (Swiss precision grid: 4/8/16/24/32/48)
  - **File:** `WalletWasabi.Fluent/Themes/SwissTheme.axaml`

### Day 6 - Icon Set Creation

- [ ] **2.5** Design Swiss-themed UI icons
  - [ ] Security: Swiss cross shield, lock, encrypted
  - [ ] Privacy: Tor onion + Swiss cross, anonymous mask, shield
  - [ ] Status: Swiss checkmark, warning triangle, error cross
  - [ ] Actions: Send/receive with Swiss styling
  - [ ] Export as SVG and integrate into Avalonia resources
  - **Files:** `WalletWasabi.Fluent/Assets/Icons/Swiss/`

- [ ] **2.6** Create Swiss pattern graphics
  - [ ] Subtle Swiss cross pattern for backgrounds
  - [ ] Alpine mountain silhouette for headers
  - [ ] Gradient overlays for cards
  - **Files:** `WalletWasabi.Fluent/Assets/Patterns/`

### Day 7 - Typography & Components

- [ ] **2.7** Define Swiss typography system
  - [ ] Select Swiss-appropriate fonts (Inter for body, Swiss721 alternative for headers)
  - [ ] Define font sizes: H1 (32), H2 (24), H3 (20), Body (14), Caption (12)
  - [ ] Define font weights: Light (300), Regular (400), Medium (500), Bold (700)
  - [ ] Update ResourceDictionary
  - **File:** `WalletWasabi.Fluent/Themes/SwissFonts.axaml`

- [ ] **2.8** Create Swiss component library
  - [ ] SwissButton (Primary/Secondary/Outline variants)
  - [ ] SwissCard (with Swiss Red border)
  - [ ] SwissBadge (status indicators)
  - [ ] SwissTooltip (with Swiss styling)
  - **Files:** `WalletWasabi.Fluent/Controls/Swiss/`

---

## 💎 PHASE 3: UI REDESIGN (Week 2-3 - Days 8-14)

**Goal:** Redesign all major UI screens with Swiss branding

### Day 8 - Navigation & Layout

- [ ] **3.1** Redesign main navigation bar
  - [ ] Background: Swiss Red (#DC143C)
  - [ ] Text: White with subtle shadow
  - [ ] Icons: White outline style
  - [ ] Active state: Gold underline
  - [ ] Add Swiss cross logo in top-left
  - **Files:** `WalletWasabi.Fluent/Views/NavBar/NavBar.axaml`
  - **Testing:** Visual regression test, screenshot comparison

- [ ] **3.2** Redesign status bar
  - [ ] Swiss coordinator status (Tor/HTTPS indicators)
  - [ ] Privacy level badge (Swiss cross + shield count)
  - [ ] Tor connection indicator (onion + Swiss flag)
  - [ ] Bitcoin network badge
  - **Files:** `WalletWasabi.Fluent/Views/StatusBar/`

### Day 9 - Welcome & Onboarding

- [ ] **3.3** Redesign welcome/splash screen
  - [ ] Swiss mountain background (subtle, blurred)
  - [ ] Large SwissWallet logo
  - [ ] Tagline: "Swiss Security • Maximum Privacy"
  - [ ] Feature highlights with Swiss icons
  - **File:** `WalletWasabi.Fluent/Views/Home/WelcomePageView.axaml`

- [ ] **3.4** Redesign wallet creation flow
  - [ ] Swiss-themed wizard steps
  - [ ] Security messaging at each step
  - [ ] Swiss cross checkmarks for completed steps
  - [ ] Professional backup instructions with Swiss precision language
  - **Files:** `WalletWasabi.Fluent/Views/Wallets/Add/Create/`

### Day 10 - Wallet View

- [ ] **3.5** Redesign wallet card components
  - [ ] Swiss Red 2px border
  - [ ] White background with subtle gradient
  - [ ] Balance display with Swiss typography
  - [ ] Privacy score (1-5 Swiss shields)
  - [ ] Status badges (Tor-routed, Coinjoined, etc.)
  - **Files:** `WalletWasabi.Fluent/Views/Wallets/Home/WalletView.axaml`

- [ ] **3.6** Redesign transaction list
  - [ ] Header: Swiss Red background, white text
  - [ ] Rows: Alternating white/light gray
  - [ ] Status icons: Swiss cross (confirmed), shield (private), clock (pending)
  - [ ] Amount styling: Bold for outgoing (red), incoming (green)
  - [ ] Privacy indicators per transaction
  - **Files:** `WalletWasabi.Fluent/Views/Wallets/Home/History/`

### Day 11 - Settings & Coordinator Panel

- [ ] **3.7** Redesign Settings page layout
  - [ ] Swiss-themed section headers
  - [ ] Card-based layout with Swiss borders
  - [ ] Icons for each settings category
  - **File:** `WalletWasabi.Fluent/Views/Settings/SettingsPageView.axaml`

- [ ] **3.8** Create Swiss Coordinator Status Dashboard
  - [ ] Dedicated panel showing both coordinators
  - [ ] Real-time status (connected/disconnected)
  - [ ] Latency metrics
  - [ ] "🔒 Hardcoded for Security" badge
  - [ ] Lock icon + explanation text
  - **File:** `WalletWasabi.Fluent/Views/Settings/SwissCoordinatorStatusView.axaml`

### Day 12 - Send & Receive

- [ ] **3.9** Redesign Send dialog
  - [ ] Swiss-themed form fields
  - [ ] Privacy slider with Swiss shield indicators
  - [ ] Fee selector with Swiss precision (exact sat/vB)
  - [ ] Confirmation screen with Swiss security checklist
  - **Files:** `WalletWasabi.Fluent/Views/Wallets/Send/`

- [ ] **3.10** Redesign Receive dialog
  - [ ] QR code with Swiss cross overlay
  - [ ] Address display with Swiss monospace font
  - [ ] Label/amount fields with Swiss styling
  - [ ] Privacy tips in Swiss security language
  - **Files:** `WalletWasabi.Fluent/Views/Wallets/Receive/`

### Day 13-14 - Privacy Features UI

- [ ] **3.11** Redesign CoinJoin interface
  - [ ] Swiss-themed progress indicators
  - [ ] Anonymity set visualization with Swiss shields
  - [ ] Round status with Swiss precision metrics
  - [ ] "Swiss Privacy Guarantee" messaging
  - **Files:** `WalletWasabi.Fluent/Views/Wallets/CoinJoin/`

- [ ] **3.12** Create Privacy Analytics Dashboard
  - [ ] Wallet privacy score (overall)
  - [ ] UTXO privacy levels (per-coin shields)
  - [ ] Coinjoin history and stats
  - [ ] Tor usage metrics
  - [ ] Swiss-themed charts and graphs
  - **File:** `WalletWasabi.Fluent/Views/Wallets/Privacy/PrivacyDashboardView.axaml` (NEW)

---

## 📚 PHASE 4: DOCUMENTATION & MESSAGING (Week 3-4 - Days 15-18)

**Goal:** Professional documentation with Swiss security positioning

### Day 15 - Core Documentation

- [ ] **4.1** Rewrite README.md with Swiss identity
  - [ ] Hero section: "🇨🇭 SwissWallet - Swiss Privacy Bitcoin Wallet"
  - [ ] Clear positioning as Wasabi fork with Swiss enhancements
  - [ ] Feature comparison table (Wasabi vs SwissWallet)
  - [ ] Installation instructions for all platforms
  - [ ] Credits to Wasabi Wallet
  - **File:** `README.md`

- [ ] **4.2** Create Swiss Security Whitepaper
  - [ ] Technical explanation of hardcoded coordinators
  - [ ] Tor-first architecture benefits
  - [ ] Privacy guarantees and threat model
  - [ ] Comparison with standard Wasabi
  - [ ] Swiss security philosophy
  - **File:** `SWISS_SECURITY_WHITEPAPER.md`

### Day 16 - User Documentation

- [ ] **4.3** Create user guides
  - [ ] Getting Started Guide (Swiss-themed)
  - [ ] Privacy Best Practices (Swiss precision approach)
  - [ ] CoinJoin Tutorial (with Swiss coordinator context)
  - [ ] Troubleshooting Guide
  - **Files:** `docs/guides/`

- [ ] **4.4** Create FAQ
  - [ ] Why SwissWallet vs Wasabi?
  - [ ] Can I change the coordinator? (No, by design)
  - [ ] Is my data secure?
  - [ ] Can I run both Wasabi and SwissWallet?
  - [ ] What makes it "Swiss"?
  - **File:** `docs/FAQ.md`

### Day 17 - Developer Documentation

- [ ] **4.5** Update CONTRIBUTING.md
  - [ ] Swiss project philosophy
  - [ ] Code style guide (Swiss precision)
  - [ ] How to propose features
  - [ ] Build instructions
  - **File:** `CONTRIBUTING.md`

- [ ] **4.6** Create ARCHITECTURE.md
  - [ ] System overview
  - [ ] Swiss enhancements vs base Wasabi
  - [ ] Coordinator enforcement mechanism
  - [ ] Data directory structure
  - **File:** `ARCHITECTURE.md`

### Day 18 - Marketing Assets

- [ ] **4.7** Create marketing screenshots
  - [ ] Screenshot of main wallet view (Swiss-themed)
  - [ ] Screenshot of Swiss coordinator panel
  - [ ] Screenshot of privacy dashboard
  - [ ] Screenshot of settings (showing locked coordinator)
  - [ ] Annotate with callouts highlighting Swiss features
  - **Files:** `docs/screenshots/`

- [ ] **4.8** Write release announcement template
  - [ ] Professional tone
  - [ ] Swiss security emphasis
  - [ ] Feature highlights
  - [ ] Download links
  - [ ] Credits to Wasabi
  - **File:** `RELEASE_TEMPLATE.md`

---

## 🚀 PHASE 5: TESTING & RELEASE (Week 4 - Days 19-21)

**Goal:** Professional v3.0.0 "Swiss Edition" release

### Day 19 - Testing

- [ ] **5.1** Multi-platform testing
  - [ ] macOS (ARM64 + x64): Full UI/UX test
  - [ ] Windows (x64): Full UI/UX test
  - [ ] Linux (x64 + .deb): Full UI/UX test
  - [ ] Verify Swiss branding on all platforms
  - [ ] Test alongside Wasabi Wallet (no conflicts)
  - **Checklist:** Create testing matrix document

- [ ] **5.2** Security audit
  - [ ] Verify coordinators cannot be changed (UI + config + code)
  - [ ] Verify Tor-first connection priority
  - [ ] Verify data directory isolation
  - [ ] Verify no Wasabi update prompts
  - [ ] Code review of all Swiss modifications
  - **Deliverable:** Security audit report

### Day 20 - Release Preparation

- [ ] **5.3** Prepare release notes
  - [ ] Changelog from v2.0.9 → v3.0.0
  - [ ] Swiss features summary
  - [ ] Breaking changes (new data directory)
  - [ ] Migration instructions (optional Wasabi → Swiss)
  - [ ] Credits and acknowledgments
  - **File:** `RELEASE_NOTES_v3.0.0.md`

- [ ] **5.4** Build final release packages
  - [ ] Clean build environment
  - [ ] Build all platforms with v3.0.0 tag
  - [ ] Sign binaries (if applicable)
  - [ ] Create checksums (SHA256)
  - [ ] Test installation on fresh systems
  - **Deliverables:** All platform packages in `packages/`

### Day 21 - Release & Launch

- [ ] **5.5** Create GitHub Release
  - [ ] Tag: `v3.0.0-swiss-edition`
  - [ ] Upload all platform packages
  - [ ] Upload checksums
  - [ ] Attach release notes
  - [ ] Mark as "Latest Release"
  - **URL:** https://github.com/swisscodernano/swisswallet/releases/tag/v3.0.0

- [ ] **5.6** Post-release verification
  - [ ] Verify download links work
  - [ ] Test fresh installation from GitHub releases
  - [ ] Monitor for bug reports
  - [ ] Update README.md badges (version, downloads)
  - **Monitoring:** First 48 hours critical

---

## 📅 DAILY WORKFLOW

Each day, follow this process:

1. **Start of Day:**
   - Review this roadmap
   - Check off yesterday's completed tasks
   - Identify today's tasks
   - Create Git branch for day's work (e.g., `day-01-data-directory`)

2. **During Work:**
   - Check off subtasks as completed
   - Commit frequently with descriptive messages
   - Update roadmap with any blockers or changes
   - Test each change before moving on

3. **End of Day:**
   - Merge day's branch to master
   - Create Git tag (e.g., `day-01-checkpoint`)
   - Push to GitHub
   - Update this roadmap with progress
   - Note any items moved to next day

---

## 🎯 SUCCESS CRITERIA

**v3.0.0 "Swiss Edition" is ready when:**

✅ SwissWallet runs independently from Wasabi (separate data dir)
✅ Swiss branding is complete (logo, colors, UI)
✅ Coordinators are locked and cannot be changed
✅ No Wasabi update prompts appear
✅ Documentation clearly explains Swiss enhancements
✅ All platforms build and install successfully
✅ Professional screenshots and marketing materials ready
✅ Security audit passed

---

## 📊 METRICS TO TRACK

- **Code Changes:** Lines of code modified
- **UI Components:** Number of components redesigned
- **Test Coverage:** % of Swiss features tested
- **Documentation:** Pages of documentation written
- **Visual Assets:** Number of icons/graphics created
- **Build Success Rate:** % of successful builds across platforms

---

## 🔄 ITERATION NOTES

After v3.0.0, future versions will focus on:

- **v3.1:** Advanced Swiss privacy analytics
- **v3.2:** Swiss coordinator health monitoring
- **v3.3:** Multi-language support (DE, FR, IT, EN)
- **v3.4:** Swiss-themed notifications and alerts
- **v4.0:** Major feature additions based on user feedback

---

**Last Updated:** 2025-10-05
**Current Version:** v2.0.9 (macOS launch fixed)
**Target Version:** v3.0.0 "Swiss Edition"
**Status:** Phase 1 starting - Day 1
