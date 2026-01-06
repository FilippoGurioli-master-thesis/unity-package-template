#!/bin/bash

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd -- "$SCRIPT_DIR/.." && pwd)"

if [ -f "$ROOT_DIR/.template" ]; then
  dotnet tool run docfx "$ROOT_DIR/docs/docfx.json"
else
  dotnet tool run docfx "$ROOT_DIR/__NAMESPACE__/Documentation~/docfx.json"
fi
