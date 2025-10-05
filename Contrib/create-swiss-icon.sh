#!/usr/bin/env bash

#------------------------------------------------------------------------------------#
#  create-swiss-icon.sh                                                              #
#                                                                                    #
#  Installs pre-generated Swiss icons for SwissWallet into macOS .app bundle        #
#------------------------------------------------------------------------------------#

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
RESOURCES_DIR="$1"

if [ -z "$RESOURCES_DIR" ]; then
  echo "Usage: $0 <resources-dir>"
  exit 1
fi

echo "🎨 Installing Swiss-themed icon..."

ICON_NAME="SwissWallet"
ICNS_FILE="$RESOURCES_DIR/${ICON_NAME}.icns"
SWISS_ASSETS_DIR="$SCRIPT_DIR/Assets/Swiss"

# Check if pre-generated .icns exists
if [ -f "$SWISS_ASSETS_DIR/SwissWallet.icns" ]; then
    echo "   ✅ Using pre-generated Swiss icon..."
    cp "$SWISS_ASSETS_DIR/SwissWallet.icns" "$ICNS_FILE"
    echo "   ✅ Icon installed: $ICNS_FILE"
    exit 0
fi

# Fallback: create from PNGs if .icns doesn't exist
echo "   🔨 Pre-generated .icns not found, creating from PNGs..."

ICONSET_DIR="$RESOURCES_DIR/${ICON_NAME}.iconset"
mkdir -p "$ICONSET_DIR"

# Copy pre-generated PNGs with correct naming for macOS iconset
if [ -f "$SWISS_ASSETS_DIR/icon_16x16.png" ]; then
    cp "$SWISS_ASSETS_DIR/icon_16x16.png" "$ICONSET_DIR/icon_16x16.png"
    cp "$SWISS_ASSETS_DIR/icon_32x32.png" "$ICONSET_DIR/icon_16x16@2x.png"
    cp "$SWISS_ASSETS_DIR/icon_32x32.png" "$ICONSET_DIR/icon_32x32.png"
    cp "$SWISS_ASSETS_DIR/icon_64x64.png" "$ICONSET_DIR/icon_32x32@2x.png"
    cp "$SWISS_ASSETS_DIR/icon_128x128.png" "$ICONSET_DIR/icon_128x128.png"
    cp "$SWISS_ASSETS_DIR/icon_256x256.png" "$ICONSET_DIR/icon_128x128@2x.png"
    cp "$SWISS_ASSETS_DIR/icon_256x256.png" "$ICONSET_DIR/icon_256x256.png"
    cp "$SWISS_ASSETS_DIR/icon_512x512.png" "$ICONSET_DIR/icon_256x256@2x.png"
    cp "$SWISS_ASSETS_DIR/icon_512x512.png" "$ICONSET_DIR/icon_512x512.png"
    cp "$SWISS_ASSETS_DIR/icon_1024x1024.png" "$ICONSET_DIR/icon_512x512@2x.png"

    echo "   ✅ Copied Swiss PNGs to iconset"

    # Convert iconset to icns (macOS only)
    if command -v iconutil &> /dev/null; then
        iconutil -c icns "$ICONSET_DIR" -o "$ICNS_FILE"
        rm -rf "$ICONSET_DIR"
        echo "   ✅ Icon created: $ICNS_FILE"
    else
        echo "   ⚠️  iconutil not available (not on macOS)"
        echo "   ℹ️  Iconset directory available at: $ICONSET_DIR"
    fi
else
    echo "   ❌ ERROR: Swiss icon PNGs not found in $SWISS_ASSETS_DIR"
    exit 1
fi
