#!/usr/bin/env bash

#------------------------------------------------------------------------------------#
#  create-macos-app.sh                                                               #
#                                                                                    #
#  Creates macOS .app bundle and .dmg installer from build output                   #
#  Usage: ./create-macos-app.sh <platform> <version> <build-dir> <packages-dir>     #
#------------------------------------------------------------------------------------#

set -e

PLATFORM=$1           # osx-x64 or osx-arm64
VERSION=$2            # e.g., 2.0.0
BUILD_DIR=$3          # Path to build output directory
PACKAGES_DIR=$4       # Path to packages directory

if [ -z "$PLATFORM" ] || [ -z "$VERSION" ] || [ -z "$BUILD_DIR" ] || [ -z "$PACKAGES_DIR" ]; then
  echo "Usage: $0 <platform> <version> <build-dir> <packages-dir>"
  exit 1
fi

echo "🍎 Creating macOS .app bundle for $PLATFORM..."

# Determine architecture name for display
if [[ "$PLATFORM" == "osx-arm64" ]]; then
  ARCH_NAME="ARM64"
  ALTER_PLATFORM="macOS-arm64"
else
  ARCH_NAME="x64"
  ALTER_PLATFORM="macOS-x64"
fi

APP_NAME="SwissWallet"
APP_BUNDLE="${APP_NAME}.app"
APP_DIR="$BUILD_DIR/$APP_BUNDLE"
CONTENTS_DIR="$APP_DIR/Contents"
MACOS_DIR="$CONTENTS_DIR/MacOS"
RESOURCES_DIR="$CONTENTS_DIR/Resources"

# Create .app bundle structure
echo "   📁 Creating bundle structure..."
rm -rf "$APP_DIR"
mkdir -p "$MACOS_DIR"
mkdir -p "$RESOURCES_DIR"

# Copy all build files to MacOS directory
echo "   📦 Copying binaries..."
cp -R "$BUILD_DIR/$PLATFORM/"* "$MACOS_DIR/"

# Rename the actual binary
mv "$MACOS_DIR/swisswallet" "$MACOS_DIR/swisswallet-bin"

# Create launcher script
echo "   📝 Creating launcher script..."
cat > "$MACOS_DIR/swisswallet" << 'LAUNCHER_EOF'
#!/bin/bash
DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$DIR"

# Set environment for Avalonia on macOS
export DYLD_LIBRARY_PATH="$DIR:$DYLD_LIBRARY_PATH"
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Launch the actual binary
exec "$DIR/swisswallet-bin" "$@"
LAUNCHER_EOF

chmod +x "$MACOS_DIR/swisswallet"
chmod +x "$MACOS_DIR/swisswallet-bin"
chmod +x "$MACOS_DIR/swisswalletd"

# Create Info.plist
echo "   📝 Creating Info.plist..."
cat > "$CONTENTS_DIR/Info.plist" << EOF
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>CFBundleDevelopmentRegion</key>
    <string>en</string>
    <key>CFBundleDisplayName</key>
    <string>SwissWallet</string>
    <key>CFBundleExecutable</key>
    <string>swisswallet</string>
    <key>CFBundleIconFile</key>
    <string>SwissWallet.icns</string>
    <key>CFBundleIdentifier</key>
    <string>com.swisswallet.app</string>
    <key>CFBundleInfoDictionaryVersion</key>
    <string>6.0</string>
    <key>CFBundleName</key>
    <string>SwissWallet</string>
    <key>CFBundlePackageType</key>
    <string>APPL</string>
    <key>CFBundleShortVersionString</key>
    <string>$VERSION</string>
    <key>CFBundleVersion</key>
    <string>$VERSION</string>
    <key>LSMinimumSystemVersion</key>
    <string>11.0</string>
    <key>NSHighResolutionCapable</key>
    <true/>
    <key>NSSupportsAutomaticGraphicsSwitching</key>
    <true/>
    <key>CFBundleDocumentTypes</key>
    <array>
        <dict>
            <key>CFBundleTypeExtensions</key>
            <array>
                <string>*</string>
            </array>
            <key>CFBundleTypeName</key>
            <string>All</string>
            <key>CFBundleTypeRole</key>
            <string>Viewer</string>
        </dict>
    </array>
</dict>
</plist>
EOF

# Create Swiss icon
echo "   🎨 Creating Swiss icon..."
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
chmod +x "$SCRIPT_DIR/create-swiss-icon.sh"
export ICONSET_DIR
"$SCRIPT_DIR/create-swiss-icon.sh" "$RESOURCES_DIR" || echo "   ⚠️  Icon creation skipped (will use default)"

# Create PkgInfo
echo "APPL????" > "$CONTENTS_DIR/PkgInfo"

echo "   ✅ .app bundle created: $APP_DIR"

# Create DMG installer
echo "🍎 Creating .dmg installer..."

DMG_NAME="SwissWallet-$VERSION-$ALTER_PLATFORM"
DMG_PATH="$PACKAGES_DIR/${DMG_NAME}.dmg"
TEMP_DMG_DIR="$BUILD_DIR/dmg-temp"

# Create temporary directory for DMG contents
rm -rf "$TEMP_DMG_DIR"
mkdir -p "$TEMP_DMG_DIR"

# Copy .app bundle to temp directory
cp -R "$APP_DIR" "$TEMP_DMG_DIR/"

# Create Applications symlink
ln -s /Applications "$TEMP_DMG_DIR/Applications"

# Create DMG
echo "   📀 Creating disk image..."
hdiutil create -volname "$APP_NAME" \
    -srcfolder "$TEMP_DMG_DIR" \
    -ov -format UDZO \
    "$DMG_PATH"

# Remove quarantine attributes to avoid "damaged" error
echo "   🔓 Removing quarantine attributes..."
xattr -cr "$TEMP_DMG_DIR/$APP_BUNDLE" 2>/dev/null || true
if [ -f "$DMG_PATH" ]; then
    xattr -cr "$DMG_PATH" 2>/dev/null || true
fi

# Clean up
rm -rf "$TEMP_DMG_DIR"

echo "   ✅ DMG created: $DMG_PATH"

# Also create ZIP of the .app for alternate distribution
ZIP_PATH="$PACKAGES_DIR/${DMG_NAME}.zip"
echo "   📦 Creating .zip archive..."
cd "$BUILD_DIR"
zip -r -q "$ZIP_PATH" "$APP_BUNDLE"
cd - > /dev/null

echo "   ✅ ZIP created: $ZIP_PATH"

echo ""
echo "🎉 macOS packaging complete for $PLATFORM!"
echo "   📦 Files created:"
echo "      - $DMG_NAME.dmg (drag-to-install)"
echo "      - $DMG_NAME.zip (direct .app)"
