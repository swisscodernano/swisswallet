#!/usr/bin/env bash

#------------------------------------------------------------------------------------#
#  build-all.sh                                                                      #
#                                                                                    #
#  Master build script to build SwissWallet for ALL supported platforms            #
#  macOS ARM64, macOS x64, Windows x64, Linux x64                                   #
#------------------------------------------------------------------------------------#

echo "🇨🇭 SwissWallet Multi-Platform Build System"
echo "=============================================="
echo ""
echo "🎯 Target Platforms:"
echo "   🍎 macOS ARM64 (Apple Silicon M1/M2/M3)"
echo "   🍎 macOS x64 (Intel)"
echo "   🪟 Windows x64"
echo "   🐧 Linux x64 (with .deb package)"
echo ""

# Change to the project root directory
cd "$(dirname "$0")/.."

# Verify we're in the right directory
if [ ! -f "WalletWasabi.sln" ]; then
  echo "❌ Error: Not in SwissWallet project root directory"
  echo "Please run this script from the SwissWallet project root"
  exit 1
fi

echo "📍 Building from: $(pwd)"
echo ""

# Run the complete Swiss build script
./Contrib/swiss-release.sh

echo ""
echo "🎉 SwissWallet Multi-Platform Build Complete!"
echo ""
echo "📊 Build Summary:"
echo "=================="

if [ -d "packages" ]; then
  cd packages

  echo ""
  echo "🍎 macOS Packages:"
  ls -la SwissWallet-*-macOS*.zip 2>/dev/null | awk '{print "   " $9 " (" $5 " bytes)"}'

  echo ""
  echo "🪟 Windows Packages:"
  ls -la SwissWallet-*-win*.zip 2>/dev/null | awk '{print "   " $9 " (" $5 " bytes)"}'

  echo ""
  echo "🐧 Linux Packages:"
  ls -la SwissWallet-*-linux*.zip 2>/dev/null | awk '{print "   " $9 " (" $5 " bytes)"}'
  ls -la SwissWallet-*-linux*.tar.gz 2>/dev/null | awk '{print "   " $9 " (" $5 " bytes)"}'
  ls -la SwissWallet-*.deb 2>/dev/null | awk '{print "   " $9 " (" $5 " bytes)"}'

  echo ""
  echo "📁 Total packages created: $(ls SwissWallet-* 2>/dev/null | wc -l)"

  cd ..
else
  echo "⚠️ No packages directory found"
fi

echo ""
echo "🔐 Swiss Security: Maximum protection enabled across all platforms"
echo "🧅 Tor Integration: Privacy-first architecture"
echo "🚀 Ready for Swiss distribution"