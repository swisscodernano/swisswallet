#!/usr/bin/env bash

#------------------------------------------------------------------------------------#
#  build-macos.sh                                                                    #
#                                                                                    #
#  Helper script to build SwissWallet for macOS platforms only                      #
#  Builds both ARM64 (Apple Silicon) and x64 (Intel) versions                      #
#------------------------------------------------------------------------------------#

echo "🍎 Building SwissWallet for macOS platforms..."
echo "   📱 macOS ARM64 (Apple Silicon M1/M2/M3)"
echo "   💻 macOS x64 (Intel)"
echo ""

# Change to the project root directory
cd "$(dirname "$0")/.."

# Run the Swiss build script for macOS
./Contrib/swiss-release.sh macos

echo ""
echo "🍎 macOS build complete!"
echo "📦 Check the packages/ directory for:"
echo "   - SwissWallet-*-macOS-arm64.zip"
echo "   - SwissWallet-*-macOS-x64.zip"