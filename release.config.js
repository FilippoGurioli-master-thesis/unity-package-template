const fs = require("fs");
const path = require("path");
const isTemplate = fs.existsSync(path.join(__dirname, ".template"));
console.log(`[semantic-release] Mode: ${isTemplate ? "TEMPLATE" : "PACKAGE"}`);
let config = require("semantic-release-preconfigured-conventional-commits");

if (isTemplate) {
  // --- TEMPLATE MODE ---
  config.plugins.push(
    ["@semantic-release/npm", { npmPublish: false }],
    "@semantic-release/github",
    [
      "@semantic-release/git",
      {
        assets: ['package.json', 'package-lock.json', 'CHANGELOG.md'],
        message: 'chore(release): ${nextRelease.version} [skip ci]'
      }
    ]
  );
} else {
  // --- PACKAGE MODE (Unity) ---
  config.plugins.push(
    ["@semantic-release/exec", {
      prepareCmd: "node Tools/update-unity-package-version.js ${nextRelease.version}"
    }],
    ["@semantic-release/changelog", {
      changelogFile: "__NAMESPACE__/CHANGELOG.md"
    }],
    "@semantic-release/github",
    [
      "@semantic-release/git",
      {
        assets: ['__NAMESPACE__/package.json', '__NAMESPACE__/package-lock.json', '__NAMESPACE__/CHANGELOG.md'],
        message: 'chore(release): ${nextRelease.version} [skip ci]'
      }
    ]
  );
}

module.exports = config;
