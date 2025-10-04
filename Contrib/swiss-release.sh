#!/usr/bin/env bash

#------------------------------------------------------------------------------------#
#  swiss-release.sh                                                                  #
#                                                                                    #
#  This script builds SwissWallet (WalletWasabi.Fluent.Desktop) for all supported   #
#  platforms, creates the zips and tar.gz files for all of them and creates the     #
#  .deb package for Debian linux.                                                   #
#                                                                                    #
#  Multi-platform targets: macOS ARM64, macOS x64, Windows x64, Linux x64          #
#------------------------------------------------------------------------------------#
set -xe

STASH_MESSAGE="Stashed changes for SwissWallet build execution"
# Check if there are any uncommitted changes
if [[ -n $(git status --porcelain) ]]; then
  # Stash the changes
  git stash push -m "$STASH_MESSAGE" --quiet
fi

# Get the latest Git tag or use default version for SwissWallet
if git describe --tags --abbrev=0 >/dev/null 2>&1; then
  LATEST_TAG=$(git describe --tags --abbrev=0)
  VERSION=${LATEST_TAG:1}
else
  # Default SwissWallet version if no tags exist
  VERSION="2.0.0"
fi
SHORT_VERSION=${VERSION:0:${#VERSION}-2}

# Define project names (keeping original structure for compatibility)
DESKTOP="WalletWasabi.Fluent.Desktop"
BACKEND="WalletWasabi.Backend"
COORDINATOR="WalletWasabi.Coordinator"
DAEMON="WalletWasabi.Daemon"
DESKTOP_PROJECT="./$DESKTOP/$DESKTOP.csproj"
BACKEND_PROJECT="./$BACKEND/$BACKEND.csproj"
COORDINATOR_PROJECT="./$COORDINATOR/$COORDINATOR.csproj"

# Build directory
ROOT_DIR=$(pwd)
BUILD_DIR="$ROOT_DIR/build"

# Swiss executable names
EXECUTABLE_NAME="swisswallet"
DAEMON_EXECUTABLE_NAME="swisswalletd"
BACKEND_EXECUTABLE_NAME="swissbackend"
COORDINATOR_EXECUTABLE_NAME="swisscoordinator"

# Directory where to save the generated packages
PACKAGES_DIR="$ROOT_DIR/packages"

# Swiss package naming
PACKAGE_FILE_NAME_PREFIX="SwissWallet-$VERSION"

if [[ "$RUNNER_OS" == "Windows" ]]; then
  ZIP="7z.exe a"
else
  ZIP="zip -r"
fi

# Default to building all platforms
if [ -z "$1" ]; then
  # All supported platforms for SwissWallet
  PLATFORMS=("win-x64" "linux-x64" "osx-x64" "osx-arm64")
  CREATE_WINDOWS_INSTALLER="no"
  CREATE_DEBIAN_PACKAGE="yes"
  RELEASE_NOTE="no"
  SIGN_PGP="no"
  CREATE_OSX_DMG="no"
  PACKAGE_COORDINATOR="no"
elif [ "$1" = "windows" ]; then
  PLATFORMS=("win-x64")
  CREATE_WINDOWS_INSTALLER="no"  # Disabled for now
  CREATE_DEBIAN_PACKAGE="no"
  RELEASE_NOTE="no"
  SIGN_PGP="no"
  CREATE_OSX_DMG="no"
  PACKAGE_COORDINATOR="no"
elif [ "$1" = "linux" ]; then
  PLATFORMS=("linux-x64")
  CREATE_WINDOWS_INSTALLER="no"
  CREATE_DEBIAN_PACKAGE="yes"
  RELEASE_NOTE="no"
  SIGN_PGP="no"
  CREATE_OSX_DMG="no"
  PACKAGE_COORDINATOR="no"
elif [ "$1" = "macos" ]; then
  PLATFORMS=("osx-x64" "osx-arm64")
  CREATE_WINDOWS_INSTALLER="no"
  CREATE_DEBIAN_PACKAGE="no"
  RELEASE_NOTE="no"
  SIGN_PGP="no"
  CREATE_OSX_DMG="no"  # Disabled for now
  PACKAGE_COORDINATOR="no"
elif [ "$1" = "test" ]; then
  # Quick test build for local testing
  PLATFORMS=("linux-x64")
  CREATE_WINDOWS_INSTALLER="no"
  CREATE_DEBIAN_PACKAGE="no"
  RELEASE_NOTE="no"
  SIGN_PGP="no"
  CREATE_OSX_DMG="no"
  PACKAGE_COORDINATOR="no"
fi

# Remove the build directory if it exists and recreate it
rm -rf "$BUILD_DIR"
mkdir -p "$BUILD_DIR"

# Create packages directory (where all final packages are saved)
rm -rf "$PACKAGES_DIR"
mkdir -p "$PACKAGES_DIR"

echo "🇨🇭 Building SwissWallet $VERSION for platforms: ${PLATFORMS[*]}"

#------------------------------------------------------------------------------------#
# BUILD DESKTOP FOR ALL PLATFORMS                                                    #
#------------------------------------------------------------------------------------#
# Loop through each platform and build the project
for PLATFORM in "${PLATFORMS[@]}"; do
  echo "🔨 Building for $PLATFORM..."

  # Define output directory for the platform
  OUTPUT_DIR=$BUILD_DIR/$PLATFORM

  # Only build Desktop project for SwissWallet
  PROJECTS_TO_BUILD=("$DESKTOP_PROJECT")

  for PROJECT in "${PROJECTS_TO_BUILD[@]}"; do
    echo "   📦 Building project: $PROJECT"

    # Build dotnet application
    dotnet restore $PROJECT --locked-mode
    dotnet publish $PROJECT \
            --configuration Release \
            --runtime $PLATFORM \
            --force \
            --output $OUTPUT_DIR \
            --self-contained true \
            --disable-parallel \
            --no-cache \
            --no-restore \
            --property:SelfContained=true \
            --property:VersionPrefix=$VERSION \
            --property:DebugType=none \
            --property:DebugSymbols=false \
            --property:ErrorReport=none \
            --property:DocumentationFile='' \
            --property:Deterministic=true \
            /clp:ErrorsOnly
  done

  # Determine executable file extension based on platform
  EXE_FILE_EXTENSION=''
  PLATFORM_PREFIX="${PLATFORM:0:3}"
  if [[ "$PLATFORM_PREFIX" == "win" ]]; then
    EXE_FILE_EXTENSION=".exe"
  fi

  # Rename executables to Swiss names
  mv $OUTPUT_DIR/{$DESKTOP,${EXECUTABLE_NAME}}$EXE_FILE_EXTENSION
  mv $OUTPUT_DIR/{$DAEMON,${DAEMON_EXECUTABLE_NAME}}$EXE_FILE_EXTENSION

  # Remove microservices binaries for other platforms
  MICRO_SERVICES_DIR="$OUTPUT_DIR/Microservices/Binaries"
  if [ -d "$MICRO_SERVICES_DIR" ]; then
    export PLATFORM_MICRO_SERVICES="${PLATFORM:0:3}${PLATFORM: -2}"
    find $MICRO_SERVICES_DIR -mindepth 1 -maxdepth 1 -type d ! -name "$PLATFORM_MICRO_SERVICES" -exec rm -rf {} + 2>/dev/null || true
  fi

  # Hack! *.deps.json files contains this SHA516 that depends on the absolute path of
  # the nuget packages. This means that these files are different in different computers
  # and for different users. (End goal: reproducibility)
  if [[ "${PLATFORM_PREFIX}" == "osx" ]]; then
    sed -i '' 's/"sha512": "sha512-[^"]*"/"sha512": ""/g' "$OUTPUT_DIR/$DESKTOP.deps.json" 2>/dev/null || true
  else
    sed -i 's/"sha512": "sha512-[^"]*"/"sha512": ""/g' "$OUTPUT_DIR/$DESKTOP.deps.json" 2>/dev/null || true
  fi

  # Adjust platform name for macOS
  ALTER_PLATFORM=$PLATFORM
  if [[ "${PLATFORM_PREFIX}" == "osx" ]]; then
    ALTER_PLATFORM="macOS${PLATFORM:3}"
  fi

  # Create compressed package files (.zip and .tar.gz)
  PACKAGE_FILE_NAME=$PACKAGE_FILE_NAME_PREFIX-$ALTER_PLATFORM
  echo "   📁 Creating package: $PACKAGE_FILE_NAME"

  if [[ "${PLATFORM_PREFIX}" == "lin" ]]; then
    tar -pczvf $PACKAGES_DIR/$PACKAGE_FILE_NAME.tar.gz -C "$BUILD_DIR" $(basename "$OUTPUT_DIR")
  fi

  pushd "$OUTPUT_DIR" || exit
  $ZIP "$PACKAGES_DIR/$PACKAGE_FILE_NAME.zip" .
  popd || exit

  echo "   ✅ Completed $PLATFORM build"
done

#------------------------------------------------------------------------------------#
# CREATE DEBIAN PACKAGE                                                              #
#------------------------------------------------------------------------------------#
if [ "$CREATE_DEBIAN_PACKAGE" = "yes" ]; then
echo "🐧 Creating Debian package..."

# Create .deb package
DEBIAN_PACKAGE_DIR=$BUILD_DIR/deb
DEBIAN=$DEBIAN_PACKAGE_DIR/DEBIAN
DEBIAN_USR=$DEBIAN_PACKAGE_DIR/usr
DEBIAN_BIN=$DEBIAN_USR/local/bin

# Create necessary directories
mkdir -p $DEBIAN
mkdir -p $DEBIAN_BIN
mkdir -p $DEBIAN_USR/share/{applications,icons/hicolor}

# Copy icon files (using Swiss logo when available, fallback to Wasabi for now)
ICON_SOURCE_DIR="./Contrib/Assets"
if [ -f "$ICON_SOURCE_DIR/SwissLogo.png" ]; then
  ICON_PREFIX="SwissLogo"
else
  ICON_PREFIX="WasabiLogo"
fi

for ICON_FILE in $ICON_SOURCE_DIR/${ICON_PREFIX}*.png; do
  if [ -f "$ICON_FILE" ]; then
    SIZE=$(echo "$ICON_FILE" | grep -oP '\d+')
    ICON_DIR="$DEBIAN_USR/share/icons/hicolor/${SIZE}x${SIZE}/apps"
    mkdir -p "$ICON_DIR"
    cp "$ICON_FILE" "$ICON_DIR/$EXECUTABLE_NAME.png"
  fi
done

# Calculate package size (in kilobytes)
DEBIAN_PACKAGE_SIZE=$(du -s $BUILD_DIR/linux-x64 | cut -f1)

# Create the control file content
DEBIAN_CONTROL_FILE_CONTENT="Package: ${EXECUTABLE_NAME}
Priority: optional
Section: utils
Maintainer: Swiss Security Labs
Version: ${VERSION}
Homepage: https://swisswallet.swiss
Vcs-Git: git://github.com/swisssecurity/swisswallet.git
Vcs-Browser: https://github.com/swisssecurity/swisswallet
Architecture: amd64
License: Open Source (MIT)
Installed-Size: ${DEBIAN_PACKAGE_SIZE}
Recommends: policykit-1
Description: Swiss-secured, non-custodial, privacy-first Bitcoin wallet
  Built-in Tor, Swiss coordinators, coinjoin, payjoin and coin control features."

echo "${DEBIAN_CONTROL_FILE_CONTENT}" > $DEBIAN/control

# Post-installation script content
USR_LOCAL_BIN_DIR="/usr/local/bin"
INSTALL_DIR="${USR_LOCAL_BIN_DIR}/swisswallet"
DEBIAN_POST_INST_SCRIPT_CONTENT="#!/usr/bin/env sh
${INSTALL_DIR}/Microservices/Binaries/lin64/hwi installudevrules
exit 0"
echo "${DEBIAN_POST_INST_SCRIPT_CONTENT}" > $DEBIAN/postinst
chmod 0775 ${DEBIAN}/postinst

# Create the desktop file content
DEBIAN_DESKTOP_CONTENT="[Desktop Entry]
Type=Application
Name=SwissWallet
StartupWMClass=SwissWallet
GenericName=Swiss Bitcoin Wallet
Comment=Swiss-secured, privacy-first Bitcoin wallet.
Icon=${EXECUTABLE_NAME}
Terminal=false
Exec=${EXECUTABLE_NAME}
Categories=Office;Finance;
Keywords=bitcoin;wallet;crypto;blockchain;swiss;privacy;security;swisswallet;"

# Write the content to the file
DEBIAN_DESKTOP="${DEBIAN_USR}/share/applications/${EXECUTABLE_NAME}.desktop"
echo "${DEBIAN_DESKTOP_CONTENT}" > $DEBIAN_DESKTOP
chmod 0644 $DEBIAN_DESKTOP

# Copy the build to into the debian package structure
cp -a $BUILD_DIR/linux-x64 $DEBIAN_BIN/swisswallet

# Create wrapper scripts
echo "#!/usr/bin/env sh
${INSTALL_DIR}/${EXECUTABLE_NAME} \$@" > ${DEBIAN_BIN}/${EXECUTABLE_NAME}

echo "#!/usr/bin/env sh
${INSTALL_DIR}/${DAEMON_EXECUTABLE_NAME} \$@" > ${DEBIAN_BIN}/${DAEMON_EXECUTABLE_NAME}

# Set permissions
chmod 0755 ${DEBIAN_BIN}/swisswallet
find ${DEBIAN_BIN}/swisswallet -type f -exec chmod 655 {} \;
find ${DEBIAN_BIN}/swisswallet -type d -not -path ${DEBIAN_BIN}/swisswallet -exec chmod 755 {} \;
chmod 0755 ${DEBIAN_BIN}/swisswallet/${EXECUTABLE_NAME}
chmod 0755 ${DEBIAN_BIN}/swisswallet/${DAEMON_EXECUTABLE_NAME}
chmod 0755 ${DEBIAN_BIN}/${EXECUTABLE_NAME}
chmod 0755 ${DEBIAN_BIN}/${DAEMON_EXECUTABLE_NAME}

# Build the .deb package
dpkg-deb -Zxz --build "${DEBIAN_PACKAGE_DIR}" "$PACKAGES_DIR/${PACKAGE_FILE_NAME_PREFIX}.deb"

echo "   ✅ Debian package created"
fi

#------------------------------------------------------------------------------------#
# SUMMARY                                                                            #
#------------------------------------------------------------------------------------#
echo ""
echo "🇨🇭 SwissWallet $VERSION Build Complete!"
echo "📦 Packages created in: $PACKAGES_DIR"
echo ""
echo "📋 Platform Summary:"
for PLATFORM in "${PLATFORMS[@]}"; do
  ALTER_PLATFORM=$PLATFORM
  if [[ "${PLATFORM:0:3}" == "osx" ]]; then
    ALTER_PLATFORM="macOS${PLATFORM:3}"
  fi
  PACKAGE_FILE_NAME=$PACKAGE_FILE_NAME_PREFIX-$ALTER_PLATFORM

  echo "   ✅ $ALTER_PLATFORM: $PACKAGE_FILE_NAME.zip"
  if [[ "${PLATFORM:0:3}" == "lin" ]]; then
    echo "      + $PACKAGE_FILE_NAME.tar.gz"
  fi
done

if [ "$CREATE_DEBIAN_PACKAGE" = "yes" ]; then
  echo "   ✅ Debian: $PACKAGE_FILE_NAME_PREFIX.deb"
fi

echo ""
echo "🔐 Swiss Security: Maximum protection enabled"
echo "🧅 Tor Integration: Privacy-first architecture"
echo "🚀 Ready for distribution across all platforms"

# Unstash changes if there were any
if git stash list | head -1 | grep -q "$STASH_MESSAGE"; then
  git stash pop
  echo "Changes unstashed."
fi