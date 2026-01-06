#!/usr/bin/env bash
set -euo pipefail

PACKAGE_DIR="${1:-}"
KEY_ID="${2:-}"

if [ -z "$PACKAGE_DIR" ] || [ -z "$KEY_ID" ]; then
  echo "Usage: ./sign-package.sh <package-folder> <gpg-key-id>"
  exit 1
fi

if [ ! -d "$PACKAGE_DIR" ]; then
  echo "Error: directory '$PACKAGE_DIR' does not exist"
  exit 1
fi

ARCHIVE_NAME="${PACKAGE_DIR%/}.zip"

echo "Creating archive: $ARCHIVE_NAME"
rm -f "$ARCHIVE_NAME"
zip -r -q "$ARCHIVE_NAME" "$PACKAGE_DIR"

echo "Computing SHA-256 digest"
if command -v sha256sum >/dev/null 2>&1; then
  sha256sum "$ARCHIVE_NAME" >"$ARCHIVE_NAME.sha256"
else
  shasum -a 256 "$ARCHIVE_NAME" >"$ARCHIVE_NAME.sha256"
fi

echo "Signing digest with GPG key: $KEY_ID"
gpg --output "$ARCHIVE_NAME.sig" \
  --local-user "$KEY_ID" \
  --detach-sign \
  "$ARCHIVE_NAME.sha256"

echo "✅ Done!"
echo "Generated:"
echo "  - $ARCHIVE_NAME"
echo "  - $ARCHIVE_NAME.sha256"
echo "  - $ARCHIVE_NAME.sig"
