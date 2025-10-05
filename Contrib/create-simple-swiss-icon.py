#!/usr/bin/env python3
"""
Simple Swiss Icon Generator - Clean Swiss Flag Design
Just a white cross on Swiss red background. Period.
"""

import os
import sys
from pathlib import Path

def check_requirements():
    """Check if required libraries are installed."""
    try:
        from PIL import Image, ImageDraw
        print("✓ Pillow installed")
        return True
    except ImportError:
        print("ERROR: Pillow not found. Install with: pip3 install Pillow")
        return False

def create_swiss_flag_icon(size):
    """
    Create simple Swiss flag icon.
    Swiss red background + white cross. Clean and minimal.
    """
    from PIL import Image, ImageDraw

    # Swiss colors (official)
    SWISS_RED = "#FF0000"
    WHITE = "#FFFFFF"

    # Create red square background
    img = Image.new('RGB', (size, size), SWISS_RED)
    draw = ImageDraw.Draw(img)

    # Swiss cross dimensions (official proportions: 7:6 for arms)
    # Cross width is 1/5 of flag size
    cross_width = size // 5
    # Cross length is 7/6 of width
    cross_length = int(cross_width * 7 / 6)

    center = size // 2

    # Draw vertical bar of cross
    draw.rectangle([
        center - cross_width // 2,
        center - cross_length // 2,
        center + cross_width // 2,
        center + cross_length // 2
    ], fill=WHITE)

    # Draw horizontal bar of cross
    draw.rectangle([
        center - cross_length // 2,
        center - cross_width // 2,
        center + cross_length // 2,
        center + cross_width // 2
    ], fill=WHITE)

    return img

def main():
    """Generate all icon sizes."""
    print("🇨🇭 Simple Swiss Flag Icon Generator")
    print("=" * 50)

    if not check_requirements():
        sys.exit(1)

    from PIL import Image
    import struct
    from io import BytesIO

    # Paths
    script_dir = Path(__file__).parent
    assets_dir = script_dir / "Assets" / "Swiss"
    assets_dir.mkdir(parents=True, exist_ok=True)

    print(f"\n📁 Output directory: {assets_dir}")

    # Generate all sizes
    sizes = [16, 32, 64, 128, 256, 512, 1024]

    print("\n🎨 Generating PNG icons...")
    for size in sizes:
        img = create_swiss_flag_icon(size)
        png_path = assets_dir / f"icon_{size}x{size}.png"
        img.save(png_path, format='PNG')
        print(f"  ✓ Created icon_{size}x{size}.png")

    # Create SVG (simple geometric)
    print("\n📐 Creating SVG icon...")
    svg_content = '''<?xml version="1.0" encoding="UTF-8"?>
<svg width="512" height="512" viewBox="0 0 512 512" xmlns="http://www.w3.org/2000/svg">
  <!-- Swiss Red Background -->
  <rect width="512" height="512" fill="#FF0000"/>

  <!-- White Swiss Cross -->
  <!-- Vertical bar -->
  <rect x="205" y="154" width="102" height="204" fill="#FFFFFF"/>
  <!-- Horizontal bar -->
  <rect x="154" y="205" width="204" height="102" fill="#FFFFFF"/>
</svg>'''

    svg_path = assets_dir / "SwissWallet.svg"
    with open(svg_path, 'w') as f:
        f.write(svg_content)
    print(f"  ✓ Created SwissWallet.svg")

    # Create Windows .ico
    print("\n📦 Creating Windows .ico...")
    ico_sizes = [16, 32, 48, 64, 128, 256]
    images = []
    for size in ico_sizes:
        if size == 48:
            # Generate 48x48 from 64x64
            img = create_swiss_flag_icon(64).resize((48, 48), Image.Resampling.LANCZOS)
        else:
            img = create_swiss_flag_icon(size)
        images.append(img)

    ico_path = assets_dir / "SwissWallet.ico"
    images[0].save(
        ico_path,
        format='ICO',
        sizes=[(img.width, img.height) for img in images]
    )
    print(f"  ✓ Created SwissWallet.ico ({ico_path.stat().st_size:,} bytes)")

    # Create macOS .icns
    print("\n📦 Creating macOS .icns...")

    # ICNS format
    icns_types = {
        'ic07': 128,
        'ic08': 256,
        'ic09': 512,
        'ic10': 1024,
        'ic11': 32,
        'ic12': 64,
        'ic13': 256,
        'ic14': 512,
    }

    icns_data = b'icns'
    icns_content = b''

    for icon_type, size in icns_types.items():
        img = create_swiss_flag_icon(size)

        # Convert to PNG bytes
        png_bytes = BytesIO()
        img.save(png_bytes, format='PNG')
        png_data = png_bytes.getvalue()

        # ICNS entry
        entry_size = 8 + len(png_data)
        entry = icon_type.encode('ascii') + struct.pack('>I', entry_size) + png_data
        icns_content += entry

    total_size = 8 + len(icns_content)

    icns_path = assets_dir / "SwissWallet.icns"
    with open(icns_path, 'wb') as f:
        f.write(icns_data)
        f.write(struct.pack('>I', total_size))
        f.write(icns_content)

    print(f"  ✓ Created SwissWallet.icns ({icns_path.stat().st_size:,} bytes)")

    print("\n" + "=" * 50)
    print("✅ Simple Swiss flag icons generated!")
    print(f"\n📂 Files created in: {assets_dir}")
    print("   - SwissWallet.svg")
    print("   - SwissWallet.ico")
    print("   - SwissWallet.icns")
    print("   - icon_*.png (7 sizes)")

if __name__ == "__main__":
    main()
