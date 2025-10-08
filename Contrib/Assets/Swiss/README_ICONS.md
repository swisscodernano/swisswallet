# SwissWallet macOS 2025 Icon Generation

## Modern Icon Design

The SwissWallet macOS 2025 icon features:
- **Rounded corners** (226px radius for 1024x1024) matching Apple's current design language
- **3D gradient effect** with layered red tones (#FF1A1A → #DC0000 → #B00000)
- **Subtle inner shadows** for depth
- **Soft glow** on the white Swiss cross
- **Top highlight** for dimensional appearance

## Files Generated

### Source
- `SwissWallet_macOS2025.svg` - Vector source with modern styling

### PNG Iconset (for .icns creation)
- `SwissWallet_macOS2025.iconset/` - Complete iconset directory with all required sizes:
  - icon_16x16.png, icon_16x16@2x.png (32x32)
  - icon_32x32.png, icon_32x32@2x.png (64x64)
  - icon_64x64.png (no @2x needed)
  - icon_128x128.png, icon_128x128@2x.png (256x256)
  - icon_256x256.png, icon_256x256@2x.png (512x512)
  - icon_512x512.png, icon_512x512@2x.png (1024x1024)
  - icon_1024x1024.png

### Windows
- `SwissWallet_macOS2025.ico` - Multi-resolution .ico file (16, 32, 64, 128, 256)

## Creating .icns on macOS

To create the .icns file on a Mac, run:

```bash
iconutil -c icns SwissWallet_macOS2025.iconset -o SwissWallet_macOS2025.icns
```

Then copy to app assets:

```bash
cp SwissWallet_macOS2025.icns ../../../WalletWasabi.Fluent.Desktop/Assets/SwissWallet.icns
```

## Creating .icns on Linux

If you need to create .icns on Linux (note: may have compatibility issues):

```bash
png2icns SwissWallet_macOS2025.icns SwissWallet_macOS2025.iconset/*.png
```

**Recommendation:** Always generate .icns on macOS using `iconutil` for best compatibility.

## Updating Icons in Build

The icons are automatically included during build:
- **macOS**: `WalletWasabi.Fluent.Desktop/Assets/SwissWallet.icns`
- **Windows**: `WalletWasabi.Fluent.Desktop/Assets/SwissWallet.ico`

After updating icons, rebuild the app to see changes.
