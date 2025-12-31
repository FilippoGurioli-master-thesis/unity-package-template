#!/usr/bin/env node

/**
 * Generates a single SDK-style .csproj for a Unity package.
 * Works both locally and in CI.
 *
 * Assumptions:
 * - Script is located in root/Tools/
 * - Package is located in root/__NAMESPACE__/
 * - Unity project is located in root/Sandbox.__NAMESPACE__/
 * - Library/ScriptAssemblies is populated
 */

const fs = require("fs");
const path = require("path");

const ROOT = path.resolve(__dirname, "..");
const PACKAGE_ROOT = path.join(ROOT, "__NAMESPACE__");
const UNITY_PROJECT_ROOT = path.join(ROOT, "Sandbox.__NAMESPACE__");
const SCRIPT_ASSEMBLIES = path.join(UNITY_PROJECT_ROOT, "Library/ScriptAssemblies");
const PACKAGE_NAME = path.basename(PACKAGE_ROOT);
const OUTPUT_CSPROJ = path.join(PACKAGE_ROOT, `${PACKAGE_NAME}.csproj`);

// ------------------------------------------------------------
// Helpers
// ------------------------------------------------------------

function exists(p) {
  return fs.existsSync(p);
}

function findAllCsFiles(root) {
  const results = [];

  function walk(dir) {
    for (const entry of fs.readdirSync(dir, { withFileTypes: true })) {
      const full = path.join(dir, entry.name);
      if (entry.isDirectory()) {
        walk(full);
      } else if (entry.isFile() && entry.name.endsWith(".cs")) {
        results.push(full);
      }
    }
  }

  walk(root);
  return results;
}

function findAllDlls(root) {
  if (!exists(root)) return [];
  return fs
    .readdirSync(root)
    .filter(f => f.endsWith(".dll"))
    .map(f => path.join(root, f));
}

// ------------------------------------------------------------
// Step 1 — Validate environment
// ------------------------------------------------------------

if (!exists(PACKAGE_ROOT)) {
  console.error("ERROR: Package folder not found:", PACKAGE_ROOT);
  process.exit(1);
}

if (!exists(UNITY_PROJECT_ROOT)) {
  console.error("ERROR: Unity project folder not found:", UNITY_PROJECT_ROOT);
  process.exit(1);
}

if (!exists(SCRIPT_ASSEMBLIES)) {
  console.error("ERROR: ScriptAssemblies folder not found:", SCRIPT_ASSEMBLIES);
  console.error("Unity must be opened at least once to generate assemblies.");
  process.exit(1);
}

// ------------------------------------------------------------
// Step 2 — Collect source files
// ------------------------------------------------------------

const csFiles = findAllCsFiles(PACKAGE_ROOT);
if (csFiles.length === 0) {
  console.error("WARNING: No .cs files found in package:", PACKAGE_ROOT);
}

// Convert absolute paths → relative paths for csproj
const relativeCsFiles = csFiles.map(f => path.relative(PACKAGE_ROOT, f).replace(/\\/g, "/"));

// ------------------------------------------------------------
// Step 3 — Collect DLL references
// ------------------------------------------------------------

const dlls = findAllDlls(SCRIPT_ASSEMBLIES);

if (dlls.length === 0) {
  console.error("ERROR: No DLLs found in ScriptAssemblies:", SCRIPT_ASSEMBLIES);
  process.exit(1);
}

// Convert absolute → relative paths for csproj
const relativeDlls = dlls.map(f => path.relative(PACKAGE_ROOT, f).replace(/\\/g, "/"));

// ------------------------------------------------------------
// Step 4 — Generate .csproj XML
// ------------------------------------------------------------

function generateCsproj() {
  const compileItems = relativeCsFiles
    .map(f => `    <Compile Include="${f}" />`)
    .join("\n");

  const referenceItems = relativeDlls
    .map(f => {
      const name = path.basename(f, ".dll");
      return `    <Reference Include="${name}">\n      <HintPath>${f}</HintPath>\n    </Reference>`;
    })
    .join("\n");

  return `<?xml version="1.0" encoding="utf-8"?>
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>1591</NoWarn>
  </PropertyGroup>

  <ItemGroup>
${compileItems}
  </ItemGroup>

  <ItemGroup>
${referenceItems}
  </ItemGroup>

</Project>
`;
}

// ------------------------------------------------------------
// Step 5 — Write output
// ------------------------------------------------------------

const xml = generateCsproj();
fs.writeFileSync(OUTPUT_CSPROJ, xml, "utf8");

console.log(`✔ Generated ${OUTPUT_CSPROJ}`);
