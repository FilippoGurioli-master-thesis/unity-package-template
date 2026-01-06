#!/usr/bin/env node

/**
 * Generates a single SDK-style .csproj for a Unity package.
 * Works both locally and in CI.
 */

const fs = require("fs");
const path = require("path");
const os = require("os");

const ROOT = path.resolve(__dirname, "..");
const PACKAGE_ROOT = path.join(ROOT, "__NAMESPACE__");
const UNITY_PROJECT_ROOT = path.join(ROOT, "Sandbox.__NAMESPACE__");
const UNITY_VERSION = getUnityVersion(UNITY_PROJECT_ROOT);
const SCRIPT_ASSEMBLIES = path.join(UNITY_PROJECT_ROOT, "Library/ScriptAssemblies");
const PACKAGE_NAME = path.basename(PACKAGE_ROOT);
const OUTPUT_CSPROJ = path.join(PACKAGE_ROOT, `${PACKAGE_NAME}.csproj`);

// ------------------------------------------------------------
// Helpers
// ------------------------------------------------------------

function getUnityVersion(projectPath) {
  const versionFile = path.join(
    projectPath,
    "ProjectSettings",
    "ProjectVersion.txt"
  );
  if (!fs.existsSync(versionFile)) {
    throw new Error(`ProjectVersion.txt not found at ${versionFile}`);
  }
  const content = fs.readFileSync(versionFile, "utf8");
  const match = content.match(/^m_EditorVersion:\s*(.+)$/m);
  if (!match) {
    throw new Error("Unable to parse m_EditorVersion from ProjectVersion.txt");
  }
  return match[1].trim();
}

function exists(p) {
  return fs.existsSync(p);
}

function findAllDlls(root) {
  if (!exists(root)) return [];
  return fs
    .readdirSync(root)
    .filter(f => f.endsWith(".dll"))
    .map(f => path.join(root, f));
}

function detectUnityInstall() {
  if (process.env.UNITY_EDITOR_PATH && exists(process.env.UNITY_EDITOR_PATH)) {
    return process.env.UNITY_EDITOR_PATH;
  }

  const editorLog = path.join(UNITY_PROJECT_ROOT, "Editor.log");
  if (exists(editorLog)) {
    const log = fs.readFileSync(editorLog, "utf8");
    const match = log.match(/Launching Unity from: (.*)/);
    if (match && exists(match[1])) {
      return match[1];
    }
  }

  const platform = os.platform();

  if (platform === "darwin") {
    const hub = `/Application/Unity/Hub/Editor/${UNITY_VERSION}`;
    if (exists(hub)) {
      const versions = fs.readdirSync(hub);
      if (versions.length > 0) {
        return path.join(hub, versions[0], "Unity.app", "Contents");
      }
    }
  }

  if (platform === "win32") {
    const base = `C:\\Program Files\\Unity\\Hub\\Editor\\${UNITY_VERSION}\\Editor`;
    if (exists(base)) {
      const versions = fs.readdirSync(base);
      if (versions.length > 0) {
        return path.join(base, versions[0], "Editor");
      }
    }
  }

  if (platform === "linux") {
    const linuxPath = `${os.homedir()}/Unity/Hub/Editor/${UNITY_VERSION}/Editor`;
    if (exists(linuxPath)) {
      return linuxPath;
    }
  }

  console.error("ERROR: Could not detect Unity installation path.");
  process.exit(1);
}

function findUnityEngineDlls(unityRoot) {
  const managed = path.join(unityRoot, "Data", "Managed");
  const engine = path.join(managed, "UnityEngine");

  return [
    ...findAllDlls(managed),
    ...findAllDlls(engine)
  ];
}

// ------------------------------------------------------------
// Validate environment
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
// Collect DLLs
// ------------------------------------------------------------

const unityInstall = detectUnityInstall();
const unityDlls = findUnityEngineDlls(unityInstall);
const scriptDlls = findAllDlls(SCRIPT_ASSEMBLIES);

if (unityDlls.length === 0) {
  console.error("ERROR: No UnityEngine DLLs found. Cannot resolve MonoBehaviour.");
  process.exit(1);
}

const allDlls = [...unityDlls, ...scriptDlls];

// Convert absolute → relative paths
const relativeDlls = allDlls.map(f => path.relative(PACKAGE_ROOT, f).replace(/\\/g, "/"));

// ------------------------------------------------------------
// Generate .csproj
// ------------------------------------------------------------

function generateCsproj() {
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
${referenceItems}
  </ItemGroup>

</Project>
`;
}

// ------------------------------------------------------------
// Write output
// ------------------------------------------------------------

fs.writeFileSync(OUTPUT_CSPROJ, generateCsproj(), "utf8");
console.log(`✔ Generated ${OUTPUT_CSPROJ}`);
