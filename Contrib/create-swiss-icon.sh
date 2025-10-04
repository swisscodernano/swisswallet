#!/usr/bin/env bash

#------------------------------------------------------------------------------------#
#  create-swiss-icon.sh                                                              #
#                                                                                    #
#  Creates a Swiss-themed icon for SwissWallet using native macOS tools             #
#------------------------------------------------------------------------------------#

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
RESOURCES_DIR="$1"

if [ -z "$RESOURCES_DIR" ]; then
  echo "Usage: $0 <resources-dir>"
  exit 1
fi

echo "🎨 Creating Swiss-themed icon..."

ICON_NAME="SwissWallet"
ICONSET_DIR="$RESOURCES_DIR/${ICON_NAME}.iconset"
ICNS_FILE="$RESOURCES_DIR/${ICON_NAME}.icns"

# Create iconset directory
mkdir -p "$ICONSET_DIR"

# Use ImageMagick if available, otherwise create simple colored squares
if command -v convert &> /dev/null; then
    echo "   🖼️  Creating Swiss cross icon with ImageMagick..."

    # Create Swiss flag icon with ImageMagick
    for size in 16 32 64 128 256 512 1024; do
        convert -size ${size}x${size} xc:'#DC143C' \
                -fill white \
                -draw "rectangle $((size*2/5)),$((size/5)) $((size*3/5)),$((size*4/5))" \
                -draw "rectangle $((size/5)),$((size*2/5)) $((size*4/5)),$((size*3/5))" \
                "$ICONSET_DIR/icon_${size}x${size}.png"

        # Create @2x versions
        if [ $size -le 512 ]; then
            cp "$ICONSET_DIR/icon_${size}x${size}.png" "$ICONSET_DIR/icon_$((size/2))x$((size/2))@2x.png"
        fi
    done

elif command -v sips &> /dev/null; then
    echo "   🖼️  Creating Swiss icon with sips..."

    # Fallback: create simple red squares with sips
    python3 << 'EOF'
from PIL import Image, ImageDraw
import os

def create_swiss_icon(size):
    # Swiss red background
    img = Image.new('RGB', (size, size), '#DC143C')
    draw = ImageDraw.Draw(img)

    # White Swiss cross (simplified, elegant)
    cross_width = size // 5
    cross_length = size * 3 // 5
    center = size // 2

    # Vertical bar
    draw.rectangle([
        center - cross_width // 2,
        center - cross_length // 2,
        center + cross_width // 2,
        center + cross_length // 2
    ], fill='white')

    # Horizontal bar
    draw.rectangle([
        center - cross_length // 2,
        center - cross_width // 2,
        center + cross_length // 2,
        center + cross_width // 2
    ], fill='white')

    # Add rounded corners for modern look
    mask = Image.new('L', (size, size), 0)
    mask_draw = ImageDraw.Draw(mask)
    mask_draw.rounded_rectangle([0, 0, size, size], radius=size//8, fill=255)

    # Apply mask
    output = Image.new('RGBA', (size, size), (0, 0, 0, 0))
    output.paste(img, (0, 0))
    output.putalpha(mask)

    return output

# Generate all required sizes
sizes = {
    16: ['icon_16x16.png'],
    32: ['icon_16x16@2x.png', 'icon_32x32.png'],
    64: ['icon_32x32@2x.png'],
    128: ['icon_128x128.png'],
    256: ['icon_128x128@2x.png', 'icon_256x256.png'],
    512: ['icon_256x256@2x.png', 'icon_512x512.png'],
    1024: ['icon_512x512@2x.png']
}

iconset_dir = os.environ.get('ICONSET_DIR')
for size, filenames in sizes.items():
    img = create_swiss_icon(size)
    for filename in filenames:
        img.save(f"{iconset_dir}/{filename}")
        print(f"   ✅ Created {filename}")

print("   🎨 Swiss icon created successfully!")
EOF

else
    echo "   ⚠️  Python PIL not available, using fallback icon..."
    # Create simple colored squares as fallback
    for size in 16 32 64 128 256 512 1024; do
        # Create red square (Swiss flag color)
        if command -v convert &> /dev/null; then
            convert -size ${size}x${size} xc:'#DC143C' "$ICONSET_DIR/icon_${size}x${size}.png"
        fi
    done
fi

# Convert iconset to icns
if command -v iconutil &> /dev/null; then
    iconutil -c icns "$ICONSET_DIR" -o "$ICNS_FILE"
    rm -rf "$ICONSET_DIR"
    echo "   ✅ Icon created: $ICNS_FILE"
else
    echo "   ⚠️  iconutil not available (not on macOS), skipping .icns creation"
fi

export ICONSET_DIR
