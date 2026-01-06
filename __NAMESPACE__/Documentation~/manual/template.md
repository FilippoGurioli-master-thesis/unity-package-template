# DevOps

This files contains all devops that have been adopted in this template.

## Features

### Project Identity & Scope

1. Asset-First Orientation
  Explicit classification as a Unity asset/library, not an executable product
  Clear separation between:
    - Runtime code
    - Editor-only tooling
    - Sample / demo content
  No assumptions about a playable build or entry point
  Packaging aligned with Unity’s asset distribution expectations
2. Template Intent
  Designed to be forked or instantiated, not directly extended
  Zero manual setup required after cloning (or minimal guided bootstrap)
  Opinionated defaults with documented rationale
  Safe to evolve without breaking downstream consumers

### Repository Structure & Governance

1. Deterministic Repository Layout
  Strict folder conventions
  Clear boundaries between:
    - Source code
    - Tests
    - Documentation
    - Automation / configuration
    - Generated artifacts
  No ambiguous or overloaded directories
2. Repository Metadata
  Standardized README with:
    - Purpose
    - Usage
    - Contribution rules
    - Automation overview
  Changelog with machine-readable structure
  License clarity (including third-party license tracking)
  Ownership and maintainership declaration

### Versioning & Release Semantics

1. Deterministic Versioning Model
  Single source of truth for version
  Predictable version increments based on change intent
  Clear distinction between:
    - Breaking changes
    - Feature additions
    - Fixes
  Support for pre-release and experimental versions
2. Release Lifecycle
  Automated release creation
  Release artifacts generated consistently
  Release notes generated from structured inputs
  Immutable historical releases

### Change Management & Commit Discipline

1. Change Intent Formalization
  Each change explicitly communicates:
    - Why it exists
    - What impact it has
    - Whether it is breaking
  Human- and machine-readable change descriptions
2. Commit Policy Enforcement
  Uniform commit message structure
  Early rejection of invalid commits
  Consistent history regardless of contributor skill level
  Support for squash, rebase, and merge strategies without loss of intent

### Branching & Collaboration Model

1. Branch Taxonomy
  Clearly defined branch roles (e.g. stable, integration, development)
  No ambiguity about where changes should land
  Short-lived feature work encouraged
  Long-lived branches strictly controlled
2. Protection & Quality Gates
  Rules preventing unreviewed or unverified changes
  Mandatory quality checks before integration
  Consistent behavior across contributors
  Safe rollback paths

### Continuous Integration (CI)

1. Deterministic Build Validation
  Automated validation on every change
  Reproducible builds from clean environments
  Explicit failure reasons
  Fast feedback loops
2. Test Execution
  Automated execution of:
    - Unit tests
    - Integration tests
    - Editor-specific tests
  Test isolation and determinism
  Clear reporting of failures and coverage trends
3. Static Analysis & Quality Signals
  Code style enforcement
  API surface consistency checks
  Breaking change detection (where possible)
  Asset integrity validation

### Continuous Delivery / Deployment (CD)

1. Artifact Production
  Automated generation of distributable asset artifacts
  Deterministic packaging format
  Artifact integrity verification
  Version-tagged outputs
2. Distribution Automation
  Automated publishing to one or more distribution channels
  Credential handling without repository leakage
  Dry-run / preview modes
  Rollback or deprecation support

### Dependency & Supply-Chain Management

1. Dependency Visibility
  Explicit declaration of all dependencies
  Clear distinction between:
    - Runtime
    - Editor
    - Development-only
  Transitive dependency awareness
2. Automated Maintenance
  Continuous monitoring for:
    - Updates
    - Incompatibilities
    - Known vulnerabilities
  Automated proposals for upgrades
  Safe batching and review workflows

### Developer Experience (DX)

1. Zero-Friction Onboarding
  One-command setup (or equivalent)
  No hidden prerequisites
  Clear failure diagnostics
  Predictable local environment behavior
2. Local Automation Parity
  Local workflows mirror automated pipelines
  Developers can reproduce CI behavior locally
  No “works on my machine” divergence
3. Feedback & Observability
  Clear logs and diagnostics
  Actionable error messages
  Minimal cognitive load during failures

### Documentation & Knowledge Management

1. Living Documentation
  Documentation evolves with code
  Changes trigger documentation updates
  Outdated documentation is detectable
2. API & Usage Documentation
  Public API clearly defined
  Examples maintained and validated
  Editor tooling documented separately from runtime APIs

### Security & Trust

1. Credential Hygiene
  No secrets in source control
  Explicit secret scopes
  Rotation-friendly configuration
2. Integrity Guarantees
  Artifact authenticity verification
  Traceability from commit → build → release
  Auditability of changes and releases

### Extensibility & Evolution

1. Template Evolution Strategy
  Safe updates to the template itself
  Clear upgrade paths for downstream users
  Non-destructive customization points
2. Opt-In Complexity
  Advanced features can be enabled progressively
  Sensible defaults for small teams
  Scalability for larger organizations

### Failure & Recovery Strategy

1. Failure Containment
  Fail fast, fail loud
  Partial failures do not corrupt state
  Clear recovery steps
2. Rollback & Hotfix Support
  Ability to patch released versions safely
  Hotfix workflow without destabilizing mainline development

### Meta-Automation & Governance

1. Self-Validation
  Template validates itself
  Drift detection from intended standards
  Automation correctness tested
2. Policy as Code
  Rules are explicit, versioned, and reviewable
  Minimal reliance on undocumented conventions
  Predictable behavior across time

## Unity package creation

I found that just creating a package in Unity is not trivial ([link to the official documentation](https://docs.unity3d.com/6000.3/Documentation/Manual/cus-pkg-lp.html)).

Constraints:

- In order to be opened (generate `.meta` files, launch its tests, etc.) it must be imported in a unity project. I.e. Unity Editor does not support the development of just a package.
- In order to be distributed the git project must contain just the package without the unity project.
- In order to be considered a real template, my project should contain all boilerplate files and directories.
- The official way to create a package meant for distribution is to reference it via `file:path/to/package.json` in the unity project and develop it like that.

There are 2 main kinds of packages in Unity:

- packages with **unitypackage** extension: meant for graphical and sound assets such as 3D models, UIs, materials, SFX etc.
- packages that lives inside the Package folder in a Unity project: these are managed by the UPM and can be imported via file system (path ref), git repo (url) and tarball (a zip file) and are general purpose.

Obviously the second case is the one that fits my case study.

## Init script

- configures the unity package unique name
  - asks for domain, company and package names, defaulting to reasonable values
- configures the base namespace of the project
- configures the license under which the package should be
- configures the description of the package
- asks and uploads all secrets needed in CI to work properly
  - `UNITY_LICENSE`: path to the unity license
  - `UNITY_EMAIL`: mail used in unity
  - `UNITY_PASSWORD`: password used for the unity email
  - `SONAR_HOST`: url hosting the SonarQube instance
  - `SONAR_TOKEN`: token used to interact with the sonar host
  - `GPG_KEY_ID`: an ephemeral gpg key used to sign the artifact (Warning: this key is generated during the script, nothing will be asked to the user)
  - `GPG_PRIVATE_KEY`: the private key used to sign the artifact (Warning: this key is generated during the script, nothing will be asked to the user)
- replaces all occurrences of template values in the project with values inserted by the user
  - `__DOMAIN__`
  - `__COMPANY__`
  - `__PACKAGE__`
  - `__NAMESPACE__`
  - `__NAME__`
  - `__DESCRIPTION__`
  - `__GIT_USER__`
  - `__GIT_MAIL__`
- installs npm and dotnet dependencies
- boots the project with unity batch mode
- installs git hooks
- removes `.template` file
- removes the `init.sh` script itself
- opens the unity editor inside the sandbox
- creates the develop branch
- commits all changes made until now
- set the tag of that commit to `0.0.0`
- configures [unity smart merge](https://docs.unity3d.com/6000.3/Documentation/Manual/SmartMerge.html)

## Template vs Package

Throughout the entire creation of this template I always met a constant issue: I must configure the tool I'm working on for 2 different targets:

- the template itself
- the user of the template

Any of the tool I had worked were used to automated a part of the developer workflow. There was no reason why I should not improve also the developer workflow of the template developer instead of just improving experience of template user. This feature was not planned but I'm proud to say that it is actually valid for any tool added in this project.

## Prerequisites

As of now the project has strong assumptions on the environment from within it is executed:

- Linux-based environment
- Node.js installed and at least at version `v25.2.1`
- Unity 6000 installed
  - Therefore the .NET SDK at `v8`
- GitHub CLI installed and up-to-date
