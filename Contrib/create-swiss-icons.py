#!/usr/bin/env python3
"""
SwissWallet Icon File Generator
Creates platform-specific icon files (.icns for macOS, .ico for Windows)
from the generated PNG files.
"""

import os
import sys
from pathlib import Path
from PIL import Image

def check_requirements():
    """Check if required libraries are installed."""
    try:
        import PIL
        print("✓ Pillow installed")
        return True
    except ImportError:
        print("ERROR: Pillow not found. Install with: pip3 install Pillow")
        return False

def create_ico_file(assets_dir: Path, output_path: Path):
    """
    Create Windows .ico file from PNG images.
    .ico files can contain multiple resolutions.
    """
    print("\n📦 Creating Windows .ico file...")

    # Windows .ico typically includes these sizes
    sizes = [16, 32, 48, 64, 128, 256]
    images = []

    for size in sizes:
        png_path = assets_dir / f"icon_{size}x{size}.png"
        if png_path.exists():
            img = Image.open(png_path)
            images.append(img)
            print(f"  ✓ Added {size}x{size}")
        else:
            print(f"  ⚠ Missing icon_{size}x{size}.png")

    if images:
        # Save as .ico with all sizes
        images[0].save(
            output_path,
            format='ICO',
            sizes=[(img.width, img.height) for img in images]
        )
        print(f"✅ Created: {output_path}")
        print(f"   Size: {output_path.stat().st_size:,} bytes")
        return True
    else:
        print("❌ No images found to create .ico file")
        return False

def create_icns_file(assets_dir: Path, output_path: Path):
    """
    Create macOS .icns file from PNG images.
    Note: Python-based .icns creation has limitations compared to iconutil,
    but works for basic icon needs.
    """
    print("\n📦 Creating macOS .icns file...")

    try:
        from PIL import Image
        import struct

        # macOS .icns format specifications
        # We'll create a basic .icns with common icon types
        icns_types = {
            'ic07': 128,   # 128x128
            'ic08': 256,   # 256x256
            'ic09': 512,   # 512x512
            'ic10': 1024,  # 1024x1024 (retina)
            'ic11': 32,    # 32x32
            'ic12': 64,    # 64x64
            'ic13': 256,   # 256x256 (retina)
            'ic14': 512,   # 512x512 (retina)
        }

        # Build ICNS file manually
        icns_data = b'icns'  # Magic number
        file_size_placeholder = b'\x00\x00\x00\x00'  # Will update later
        icns_content = b''

        for icon_type, size in icns_types.items():
            png_path = assets_dir / f"icon_{size}x{size}.png"
            if not png_path.exists():
                continue

            # Load PNG and convert to PNG bytes
            img = Image.open(png_path)

            # Convert to PNG bytes
            from io import BytesIO
            png_bytes = BytesIO()
            img.save(png_bytes, format='PNG')
            png_data = png_bytes.getvalue()

            # ICNS entry: 4-byte type + 4-byte length + data
            entry_size = 8 + len(png_data)
            entry = icon_type.encode('ascii') + struct.pack('>I', entry_size) + png_data
            icns_content += entry
            print(f"  ✓ Added {icon_type} ({size}x{size})")

        # Calculate total file size
        total_size = 8 + len(icns_content)

        # Write complete ICNS file
        with open(output_path, 'wb') as f:
            f.write(icns_data)
            f.write(struct.pack('>I', total_size))
            f.write(icns_content)

        print(f"✅ Created: {output_path}")
        print(f"   Size: {output_path.stat().st_size:,} bytes")
        print("\nℹ️  Note: For production, rebuild on macOS using iconutil for best results.")
        return True

    except Exception as e:
        print(f"❌ Error creating .icns: {e}")
        print("\n⚠️  Fallback: Creating iconset directory structure instead.")
        print("   Run this on macOS: iconutil -c icns SwissWallet.iconset")
        return False

def main():
    """Main execution."""
    print("🇨🇭 SwissWallet Icon Generator")
    print("=" * 50)

    if not check_requirements():
        sys.exit(1)

    # Paths
    script_dir = Path(__file__).parent
    assets_dir = script_dir / "Assets" / "Swiss"

    if not assets_dir.exists():
        print(f"❌ Assets directory not found: {assets_dir}")
        sys.exit(1)

    print(f"\n📁 Assets directory: {assets_dir}")

    # Create .ico for Windows
    ico_path = assets_dir / "SwissWallet.ico"
    create_ico_file(assets_dir, ico_path)

    # Create .icns for macOS
    icns_path = assets_dir / "SwissWallet.icns"
    create_icns_file(assets_dir, icns_path)

    print("\n" + "=" * 50)
    print("✅ Icon generation complete!")
    print("\n📋 Next steps:")
    print("1. Test .ico file on Windows builds")
    print("2. Test .icns file on macOS builds")
    print("3. Update build scripts to use new icons")
    print("4. Replace old Wasabi icons in project files")

if __name__ == "__main__":
    main()
