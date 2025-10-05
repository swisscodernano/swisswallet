#!/usr/bin/env python3
"""
SwissWallet Logo Generator - "The Swiss Sovereign Key"

Generates professional Swiss-themed logo combining:
- Pentagon shield (5 pillars of sovereignty)
- Swiss cross (neutrality, independence)
- Key shape (your keys, your sovereignty)
- Bitcoin symbol (economic freedom)

Color palette:
- Swiss Red: #DC143C
- Swiss Gold: #FFD700
- Pure White: #FFFFFF
- Dark Charcoal: #2C2C2C
"""

import os
import sys

try:
    from PIL import Image, ImageDraw, ImageFont
    import cairosvg
except ImportError:
    print("ERROR: Required libraries not found.")
    print("Install with: pip3 install Pillow cairosvg")
    sys.exit(1)

# Swiss color palette
SWISS_RED = "#DC143C"
SWISS_GOLD = "#FFD700"
PURE_WHITE = "#FFFFFF"
DARK_CHARCOAL = "#2C2C2C"

def create_svg_logo(size=512):
    """Create SVG version of Swiss Sovereign Key logo"""

    # Calculate proportions
    shield_width = size * 0.8
    shield_height = size * 0.85
    shield_x = (size - shield_width) / 2
    shield_y = size * 0.08

    # Key proportions
    key_width = shield_width * 0.5
    key_height = shield_height * 0.7
    key_x = (size - key_width) / 2
    key_y = shield_y + shield_height * 0.15

    # Swiss cross dimensions
    cross_size = key_width * 0.3
    cross_x = size / 2 - cross_size / 2
    cross_y = key_y + key_height * 0.1

    # Bitcoin symbol
    btc_size = key_width * 0.25
    btc_x = size / 2 - btc_size / 2
    btc_y = key_y + key_height * 0.65

    svg = f'''<?xml version="1.0" encoding="UTF-8" standalone="no"?>
<svg width="{size}" height="{size}" viewBox="0 0 {size} {size}"
     xmlns="http://www.w3.org/2000/svg" version="1.1">

  <!-- Background circle -->
  <circle cx="{size/2}" cy="{size/2}" r="{size/2}" fill="{SWISS_RED}"/>

  <!-- Pentagon Shield (5 pillars of sovereignty) -->
  <defs>
    <linearGradient id="shieldGradient" x1="0%" y1="0%" x2="0%" y2="100%">
      <stop offset="0%" style="stop-color:{SWISS_GOLD};stop-opacity:1" />
      <stop offset="100%" style="stop-color:#B8860B;stop-opacity:1" />
    </linearGradient>

    <filter id="shadow">
      <feDropShadow dx="0" dy="4" stdDeviation="4" flood-opacity="0.3"/>
    </filter>
  </defs>

  <!-- Shield outline (pentagon shape) -->
  <path d="M {size/2} {shield_y}
           L {shield_x + shield_width} {shield_y + shield_height * 0.3}
           L {shield_x + shield_width * 0.85} {shield_y + shield_height}
           L {shield_x + shield_width * 0.15} {shield_y + shield_height}
           L {shield_x} {shield_y + shield_height * 0.3}
           Z"
        fill="url(#shieldGradient)"
        stroke="{PURE_WHITE}"
        stroke-width="3"
        filter="url(#shadow)"/>

  <!-- Inner shield (darker, for depth) -->
  <path d="M {size/2} {shield_y + 15}
           L {shield_x + shield_width - 15} {shield_y + shield_height * 0.3 + 10}
           L {shield_x + shield_width * 0.85 - 10} {shield_y + shield_height - 20}
           L {shield_x + shield_width * 0.15 + 10} {shield_y + shield_height - 20}
           L {shield_x + 15} {shield_y + shield_height * 0.3 + 10}
           Z"
        fill="{DARK_CHARCOAL}"
        opacity="0.9"/>

  <!-- Swiss Cross (key handle) -->
  <g id="swissCross">
    <!-- Vertical bar -->
    <rect x="{cross_x + cross_size * 0.35}" y="{cross_y}"
          width="{cross_size * 0.3}" height="{cross_size}"
          fill="{PURE_WHITE}" rx="2"/>
    <!-- Horizontal bar -->
    <rect x="{cross_x}" y="{cross_y + cross_size * 0.35}"
          width="{cross_size}" height="{cross_size * 0.3}"
          fill="{PURE_WHITE}" rx="2"/>
  </g>

  <!-- Key shaft -->
  <rect x="{key_x + key_width * 0.42}" y="{cross_y + cross_size}"
        width="{key_width * 0.16}" height="{key_height * 0.35}"
        fill="{PURE_WHITE}" rx="4"/>

  <!-- Key teeth (Bitcoin integrated) -->
  <g id="keyTeeth">
    <!-- Bitcoin circle background -->
    <circle cx="{btc_x + btc_size/2}" cy="{btc_y + btc_size/2}"
            r="{btc_size * 0.6}" fill="{SWISS_GOLD}"
            stroke="{PURE_WHITE}" stroke-width="3"/>

    <!-- Bitcoin B symbol -->
    <text x="{btc_x + btc_size/2}" y="{btc_y + btc_size * 0.7}"
          font-family="Arial, sans-serif" font-weight="bold"
          font-size="{btc_size * 0.8}" fill="{DARK_CHARCOAL}"
          text-anchor="middle">₿</text>

    <!-- Key teeth details -->
    <rect x="{key_x + key_width * 0.2}" y="{btc_y + btc_size * 1.1}"
          width="{key_width * 0.1}" height="{key_height * 0.08}" fill="{PURE_WHITE}"/>
    <rect x="{key_x + key_width * 0.35}" y="{btc_y + btc_size * 1.1}"
          width="{key_width * 0.1}" height="{key_height * 0.12}" fill="{PURE_WHITE}"/>
    <rect x="{key_x + key_width * 0.55}" y="{btc_y + btc_size * 1.1}"
          width="{key_width * 0.1}" height="{key_height * 0.08}" fill="{PURE_WHITE}"/>
    <rect x="{key_x + key_width * 0.7}" y="{btc_y + btc_size * 1.1}"
          width="{key_width * 0.1}" height="{key_height * 0.12}" fill="{PURE_WHITE}"/>
  </g>

  <!-- Tagline (optional, for larger sizes) -->
  <text x="{size/2}" y="{size * 0.95}"
        font-family="Arial, sans-serif" font-weight="bold"
        font-size="{size * 0.04}" fill="{PURE_WHITE}"
        text-anchor="middle" opacity="0.9">SWISS SOVEREIGNTY</text>
</svg>'''

    return svg


def create_png_from_svg(svg_content, output_path, size):
    """Convert SVG to PNG"""
    try:
        cairosvg.svg2png(
            bytestring=svg_content.encode('utf-8'),
            write_to=output_path,
            output_width=size,
            output_height=size
        )
        print(f"   ✅ Created: {output_path}")
        return True
    except Exception as e:
        print(f"   ❌ Failed to create {output_path}: {e}")
        return False


def create_simple_fallback_logo(size, output_path):
    """Create simple fallback logo if SVG fails"""
    img = Image.new('RGBA', (size, size), (220, 20, 60, 255))  # Swiss Red
    draw = ImageDraw.Draw(img)

    # Draw circle background
    draw.ellipse([0, 0, size, size], fill=(220, 20, 60, 255))

    # Draw white shield outline
    shield_points = [
        (size//2, size*0.1),
        (size*0.85, size*0.35),
        (size*0.75, size*0.9),
        (size*0.25, size*0.9),
        (size*0.15, size*0.35)
    ]
    draw.polygon(shield_points, fill=(255, 215, 0, 255), outline=(255, 255, 255, 255), width=3)

    # Draw Swiss cross
    cross_size = size * 0.15
    cx, cy = size//2, size*0.25
    draw.rectangle([cx - cross_size*0.15, cy - cross_size*0.5,
                   cx + cross_size*0.15, cy + cross_size*0.5], fill=(255, 255, 255, 255))
    draw.rectangle([cx - cross_size*0.5, cy - cross_size*0.15,
                   cx + cross_size*0.5, cy + cross_size*0.15], fill=(255, 255, 255, 255))

    # Draw Bitcoin symbol
    try:
        font_size = int(size * 0.3)
        font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf", font_size)
    except:
        font = ImageFont.load_default()

    btc_text = "₿"
    bbox = draw.textbbox((0, 0), btc_text, font=font)
    text_width = bbox[2] - bbox[0]
    text_height = bbox[3] - bbox[1]
    text_x = (size - text_width) // 2
    text_y = size * 0.5

    # Draw gold circle for Bitcoin
    btc_circle_r = size * 0.15
    draw.ellipse([size//2 - btc_circle_r, size*0.5 - btc_circle_r,
                 size//2 + btc_circle_r, size*0.5 + btc_circle_r],
                fill=(255, 215, 0, 255), outline=(255, 255, 255, 255), width=3)

    draw.text((text_x, text_y), btc_text, fill=(44, 44, 44, 255), font=font)

    img.save(output_path)
    print(f"   ✅ Created (fallback): {output_path}")


def main():
    """Generate Swiss Sovereign Key logo in all required sizes"""

    print("🇨🇭 SwissWallet Logo Generator")
    print("=" * 50)
    print("Generating: The Swiss Sovereign Key")
    print()

    # Determine output directory
    script_dir = os.path.dirname(os.path.abspath(__file__))
    assets_dir = os.path.join(script_dir, "Assets", "Swiss")
    os.makedirs(assets_dir, exist_ok=True)

    print(f"📁 Output directory: {assets_dir}")
    print()

    # Standard icon sizes
    sizes = {
        16: "icon_16x16.png",
        32: "icon_32x32.png",
        64: "icon_64x64.png",
        128: "icon_128x128.png",
        256: "icon_256x256.png",
        512: "icon_512x512.png",
        1024: "icon_1024x1024.png"
    }

    # Generate SVG (master file)
    print("🎨 Creating master SVG...")
    svg_content = create_svg_logo(512)
    svg_path = os.path.join(assets_dir, "SwissWallet.svg")

    with open(svg_path, 'w') as f:
        f.write(svg_content)
    print(f"   ✅ Created: {svg_path}")
    print()

    # Generate PNGs from SVG
    print("🖼️  Creating PNG files...")
    success_count = 0

    for size, filename in sizes.items():
        output_path = os.path.join(assets_dir, filename)

        # Try SVG to PNG conversion
        if create_png_from_svg(svg_content, output_path, size):
            success_count += 1
        else:
            # Fallback to simple logo
            print(f"   ⚠️  Using fallback for {size}px...")
            create_simple_fallback_logo(size, output_path)
            success_count += 1

    print()
    print("=" * 50)
    print(f"✅ Logo generation complete!")
    print(f"   {success_count}/{len(sizes)} PNG files created")
    print(f"   1 SVG master file created")
    print()
    print("📋 Next steps:")
    print("   1. Review logos in: Contrib/Assets/Swiss/")
    print("   2. Update build scripts to use new icons")
    print("   3. Replace WasabiLogo.ico with SwissWallet icons")
    print()
    print("🇨🇭 Swiss Sovereignty • Your Keys • Your Wealth")


if __name__ == "__main__":
    main()
