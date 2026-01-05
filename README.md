# 📦 Unity Package Template

![Build Status](https://github.com/FilippoGurioli-master-thesis/unity-package-template/actions/workflows/ci.yml/badge.svg)
![Release](https://img.shields.io/github/v/release/FilippoGurioli-master-thesis/unity-package-template)
![License](https://img.shields.io/github/license/FilippoGurioli-master-thesis/unity-package-template)

A robust Unity Package boilerplate featuring GPG signing, automated semantic releases, and a Sandbox environment.

---

## 🛠 Compatibility

| Unity Version | URP | HDRP | Built-in |
| :--- | :---: | :---: | :---: |
| 2022.3 LTS+ | ✅ | ✅ | ✅ |

---

## 🏗 Project Structure

- **`.github/`**: CI/CD pipelines (Automatic GPG signing, SonarQube, and semantic-release).
- **`.template`**: Presence of this file triggers "Template Mode" in CI. Remove it to start a real package.
- **`Sandbox.__NAMESPACE__/`**: A local Unity project to test your package in isolation.
- **`Assets/`**: The actual package source code.

---

## Usage

Run `init` script to start your unity package development.

```bash
bash ./init.sh
```

