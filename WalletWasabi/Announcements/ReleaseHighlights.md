## Release Highlights
#### 🟠 Enhanced Bitcoin Node Integration
#### 🎨 Refreshed UI with Icons & Animations
#### ⚙️ Dedicated Config Files Per Network
#### 🤯 Stronger & Smarter Coordinators
#### 🛠️ Refinements & Fixes

## Release Summary
SwissWallet v3.0.1 brings Swiss sovereignty to Bitcoin privacy with hardcoded Swiss coordinators, professional documentation, and complete Swiss visual identity.

### 🇨🇭 Swiss Coordinators
All CoinJoin privacy operations route exclusively through Swiss infrastructure. Primary Tor onion service with HTTPS fallback ensures maximum privacy under Swiss law.

### 🎨 Swiss Visual Identity
Complete UI refresh with Swiss Red (#DC143C) color scheme, Swiss flag iconography, and professional Swiss branding throughout the application.

### 📁 Professional Repository
Comprehensive documentation with WHY_SWISS.md manifesto explaining the 5 pillars of Swiss digital sovereignty: Neutrality, Privacy, Precision, Democracy, and Economic Sovereignty.

### ⚙️ Dedicated Config Files Per Network
Each network  Mainnet, Testnet4, and Regtest — now has its own independent configuration file. Switching to test networks is easier and your preferences are always preserved.

### 🤯 Stronger & Smarter Coordinators
Coordinators are automatically published as onion services right out of the box: no manual Tor setup needed. Coordinators can now also run on pruned nodes in blocksonly mode.

Plus, fallback fee rate providers were implemented (mempool.space and blockstream.info), ensuring accurate fee estimates, even if your node can’t provide them.

### 🛠️ Refinements & Fixes
- **Full-RBF by default** – All transactions are treated as replaceable.
- **Resilient HTTP communication** – Smarter retry handling makes connections sturdier.
- **Seed recovery fixes** – Annoying typing issues are resolved.
- **Sharper fee estimations** – Precise decimal calculations with no rounding loss.
- **NBitcoin updated to 8.0.14** – Latest Bitcoin protocol improvements included.
- **Clearer terminology** – “Backend” is now called “Indexer.”
- **Lean codebase** – Legacy components like TurboSync and BlockNotifier removed.
- **Safer Coinjoin handling** – Excluded Coins can only be changed when Coinjoin is paused.
- **Donation Button removed from Main Screen** – The button is gone, but donations are still possible via the search bar.
- **Conflux by default** - Better Tor configuration for improved connectivity.
