#!/usr/bin/env bash

#------------------------------------------------------------------------------------#
#  build-windows.sh                                                                  #
#                                                                                    #
#  Helper script to build SwissWallet for Windows x64                               #
#------------------------------------------------------------------------------------#

echo "🪟 Building SwissWallet for Windows x64..."
echo ""

# Change to the project root directory
cd "$(dirname "$0")/.."

# Run the Swiss build script for Windows
./Contrib/swiss-release.sh windows

echo ""
echo "🪟 Windows build complete!"
echo "📦 Check the packages/ directory for:"
echo "   - SwissWallet-*-win-x64.zip"