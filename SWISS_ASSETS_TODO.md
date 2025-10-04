# 🇨🇭 SwissWallet Assets Replacement Guide

## 🎨 Assets che necessitano di rebranding Swiss

### 📁 **Main Assets to Replace:**

1. **Main Logo & Icon**
   - `ui-ww.png` → `ui-swiss.png` (main UI preview)
   - `WalletWasabi.Fluent/Assets/WasabiLogo.ico` → `SwissWalletLogo.ico`

2. **Desktop Application Icons**
   - `WalletWasabi.Fluent.Desktop/Assets/WasabiLogo.ico` → `SwissWalletLogo.ico`
   - `WalletWasabi.Fluent.Desktop/Assets/WasabiLogo16.png` → `SwissWalletLogo16.png`
   - `WalletWasabi.Fluent.Desktop/Assets/WasabiLogo24.png` → `SwissWalletLogo24.png`
   - `WalletWasabi.Fluent.Desktop/Assets/WasabiLogo32.png` → `SwissWalletLogo32.png`
   - `WalletWasabi.Fluent.Desktop/Assets/WasabiLogo48.png` → `SwissWalletLogo48.png`
   - `WalletWasabi.Fluent.Desktop/Assets/WasabiLogo256.png` → `SwissWalletLogo256.png`

3. **Contrib Assets**
   - `Contrib/Assets/WasabiLogo16.png` → `SwissWalletLogo16.png`
   - `Contrib/Assets/WasabiLogo24.png` → `SwissWalletLogo24.png`
   - `Contrib/Assets/WasabiLogo32.png` → `SwissWalletLogo32.png`
   - `Contrib/Assets/WasabiLogo48.png` → `SwissWalletLogo48.png`
   - `Contrib/Assets/WasabiLogo256.png` → `SwissWalletLogo256.png`

## 🎯 **Swiss Design Requirements:**

### **Color Scheme:**
- **Primary**: Swiss Red (#FF0000)
- **Secondary**: White (#FFFFFF)
- **Accent**: Dark Red (#CC0000)
- **Background**: Light Gray (#F5F5F5)

### **Design Elements:**
- **Swiss Cross**: White cross on red background
- **Typography**: Clean, modern, security-focused
- **Style**: Minimalist, professional, trustworthy

### **Icon Specifications:**
- **16x16**: System tray icon
- **24x24**: Small UI elements
- **32x32**: Standard desktop icon
- **48x48**: Large desktop icon
- **256x256**: High-resolution icon
- **ICO format**: Multi-resolution Windows icon

## 🛠️ **Implementation Steps:**

### Phase 2.4a: Create Swiss Logo Design
- [ ] Design main SwissWallet logo with Swiss cross
- [ ] Create variations in all required sizes
- [ ] Export in PNG and ICO formats

### Phase 2.4b: Replace Asset Files
- [ ] Replace all WasabiLogo files with SwissWallet equivalents
- [ ] Update file references in project files
- [ ] Test icon display across all platforms

### Phase 2.4c: Update Asset References
- [ ] Update TrayIcon source path in App.axaml
- [ ] Update project file references
- [ ] Update installer scripts

## 🔗 **File References to Update:**

```xml
<!-- App.axaml -->
<TrayIcon Icon="/Assets/SwissWalletLogo.ico" />
```

```csharp
// Any hardcoded asset paths in C# files
```

## 📝 **Notes:**
- This is currently marked as TODO for Phase 2.4
- Assets can be created by graphic designer
- Placeholder text-based solution implemented for now
- Final graphics should maintain professional appearance
- Icons must work well at all sizes (scalable design)

**Status**: 🟡 Pending graphic design work
**Priority**: Medium (functionality works without graphics)
**Timeline**: Can be completed independently after core functionality