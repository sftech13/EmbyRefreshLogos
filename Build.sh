#!/bin/bash

# Define variables
PROJECT_PATH="$HOME/git/EmbyRefreshLogos"
BUILD_PATH="$PROJECT_PATH/Build"
BIN_PATH="$PROJECT_PATH/bin/Release/net8.0"
ZIP_TOOL="7z"

# Ensure Build directory exists
mkdir -p "$BUILD_PATH"

# Publish for Windows (win-x64)
dotnet publish "$PROJECT_PATH" -r win-x64 -c Release \
    -p:PublishReadyToRun=true \
    -p:PublishSingleFile=true \
    --self-contained true \
    -p:IncludeNativeLibrariesForSelfExtract=true

# Publish for macOS (osx-x64)
dotnet publish "$PROJECT_PATH" -r osx-x64 -c Release \
    -p:PublishSingleFile=true \
    -p:IncludeNativeLibrariesForSelfExtract=true \
    --self-contained true

# Publish for Linux ARM (linux-arm)
dotnet publish "$PROJECT_PATH" -r linux-arm -c Release \
    -p:PublishSingleFile=true \
    -p:IncludeNativeLibrariesForSelfExtract=true \
    --self-contained true

# Publish for Linux x64 (linux-x64)
dotnet publish "$PROJECT_PATH" -r linux-x64 -c Release \
    -p:PublishSingleFile=true \
    -p:IncludeNativeLibrariesForSelfExtract=true \
    --self-contained true

# Package the builds

# Windows (win-x64)
cp "$BIN_PATH/win-x64/publish/EmbyRefreshLogos.exe" "$BUILD_PATH/EmbyRefreshLogos.exe"
$ZIP_TOOL a -tzip "$BUILD_PATH/EmbyRefreshLogos-WIN.zip" "$BUILD_PATH/EmbyRefreshLogos.exe" "$PROJECT_PATH/EmbyRefreshLogos.txt"

# macOS (osx-x64)
cp "$BIN_PATH/osx-x64/publish/EmbyRefreshLogos" "$BUILD_PATH/EmbyRefreshLogos"
$ZIP_TOOL a -t7z "$BUILD_PATH/EmbyRefreshLogos-MAC.7z" "$BUILD_PATH/EmbyRefreshLogos" "$PROJECT_PATH/EmbyRefreshLogos.txt"

# Linux ARM (linux-arm)
cp "$BIN_PATH/linux-arm/publish/EmbyRefreshLogos" "$BUILD_PATH/EmbyRefreshLogos"
$ZIP_TOOL a -t7z "$BUILD_PATH/EmbyRefreshLogos-RasPi.7z" "$BUILD_PATH/EmbyRefreshLogos" "$PROJECT_PATH/EmbyRefreshLogos.txt"

# Linux x64 (linux-x64)
cp "$BIN_PATH/linux-x64/publish/EmbyRefreshLogos" "$BUILD_PATH/EmbyRefreshLogos"
$ZIP_TOOL a -t7z "$BUILD_PATH/EmbyRefreshLogos-LIN64.7z" "$BUILD_PATH/EmbyRefreshLogos" "$PROJECT_PATH/EmbyRefreshLogos.txt"

echo "Build and packaging complete. Files are located in $BUILD_PATH."


