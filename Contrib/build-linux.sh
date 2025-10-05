#!/usr/bin/env bash

#------------------------------------------------------------------------------------#
#  build-linux.sh                                                                    #
#                                                                                    #
#  Helper script to build SwissWallet for Linux x64                                 #
#  Creates both zip and deb packages                                                #
#------------------------------------------------------------------------------------#

echo "🐧 Building SwissWallet for Linux x64..."
echo "   📦 Creating .zip archive"
echo "   📦 Creating .tar.gz archive"
echo "   📦 Creating .deb package"
echo ""

# Change to the project root directory
cd "$(dirname "$0")/.."

# Run the Swiss build script for Linux
./Contrib/swiss-release.sh linux

echo ""
echo "🐧 Linux build complete!"
echo "📦 Check the packages/ directory for:"
echo "   - SwissWallet-*-linux-x64.zip"
echo "   - SwissWallet-*-linux-x64.tar.gz"
echo "   - SwissWallet-*.deb"