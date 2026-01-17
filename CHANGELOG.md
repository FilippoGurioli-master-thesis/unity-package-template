## [1.4.0](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/1.3.0...1.4.0) (2026-01-17)

### Features

* create a validation step before running the init script ([7d8244c](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/7d8244c71985d0bf3bc3722eeecfb0a68f7868fc))

## [1.3.0](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/1.2.4...1.3.0) (2026-01-17)

### Features

* create a script to open the unity editor without passing from the flaky unityhub ([8d60670](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/8d60670a3d5bf236b7598fcbf4e0d32638a5659c))

### Bug Fixes

* open unity to create meta file for generated csproj ([f273f73](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/f273f73f19328c16b5526639c5b24a40fdaea971))

### General maintenance

* merge main into develop ([8a45361](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/8a453615bb1cba9ce20bb196041c71e3104b0c83))

## [1.2.4](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/1.2.3...1.2.4) (2026-01-16)

### Bug Fixes

* checkout and tag performed only if not present ([555c360](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/555c3606de526947aab577b0a6fd84a37b3dfe1c))
* correctly point to default path for unity license ([e3d537b](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/e3d537b837c56b6ffb31384d950c6aedec75738a))
* correctly set tag and branch protection through rulesets ([5d387ea](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/5d387ea6adf44d5bbf3d7bcdca98553c5a1610c9))
* put tag set in a try catch ([beab957](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/beab9576d66ae8032803b9797c4269504234e580))
* tag and checkout checked with try catch ([7fde113](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/7fde1136c6941f23e2a4d31612441c428011d6d3))

### General maintenance

* improve try catch logic for checkout and tag ([ee28484](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/ee284843aab84ef61b75fd71f1b4f297a8dc4b69))
* remove catch filter in ghp ([beece3f](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/beece3f4c9088dd5d5e3bafb7a5b6e10e65fb3a9))
* set as warnings console.err strings ([aed3027](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/aed3027fb67a326fdf5ae8d1f94d128ecde49599))

### Style improvements

* use config.SandboxPath ([acad76b](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/acad76bf20b525f1d35931e713caff2f04914723))

## [1.2.3](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/1.2.2...1.2.3) (2026-01-16)

### Bug Fixes

* **gh:** ghp correctly create in init ([181bc8f](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/181bc8f96ea3d759cbfee5cc31c0d8d78e800201))

## [1.2.2](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/1.2.1...1.2.2) (2026-01-16)

### Dependency updates

* **core-deps:** update dependency com.unity.inputsystem to v1.18.0 ([#27](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/27)) ([c3fa542](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/c3fa542fe996e5b3339ffd04d0350bc2ef49be51))
* **core-deps:** update dependency unity editor ([#26](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/26)) ([a1e5dcd](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/a1e5dcd75a9f7be49b22f032e07a2c71fb57ae43))

### Bug Fixes

* add missing using statements in init scripts ([3132683](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/313268309518346cd5030cad1b13f23a433b8b96))

### Documentation

* update readme ([f28fabd](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/f28fabdbecdd9d76d510b6dab9ccd506b65b8b35))
* update readme ([7389f85](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/7389f8527d07e3be675727440e6822c2f32a888a))

### Build and continuous integration

* comment launch-init since it currelty not support non-interactive mode ([0cade4e](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/0cade4e8ba0a2d22b59c7d4cc623b398a108a288))
* create launch-init job ([e544f29](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/e544f29349e27d7ba3bea41768e3c079fb276b68))

## [1.2.1](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/1.2.0...1.2.1) (2026-01-13)

### Documentation

* update README ([cd84257](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/cd842571498d22dda960600577ab9127fd237535))

## [1.2.0](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/1.1.1...1.2.0) (2026-01-12)

### Features

* set right command on the init script to add hooks ([647502f](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/647502faba08796b70165b29bbbc733315ee6809))

### Dependency updates

* **deps:** add husky to .net deps ([8e0e5e0](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/8e0e5e0e269afa6b717b8070249755e226e5ebff))

### Documentation

* update README ([68bebd5](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/68bebd5c41cd8d3672d81472328382ae7b5556eb))
* update README ([88c4182](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/88c41820c897de7cbb1275b6263d4733cc6f2864))
* update README ([80a67dd](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/80a67dd6b76ea8844c40d091d3cbb630cd5b7400))

### Build and continuous integration

* **deps:** update actions/setup-node action to v6 ([#15](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/15)) ([a9c037d](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/a9c037dbb4f7b7a9f861f50add8e0c3713ce8fc8))
* **deps:** update actions/upload-artifact action to v5 ([#17](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/17)) ([1bd6810](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/1bd6810c3123e4d0a9f5b94ed38ccfc5a6ff857f))
* **deps:** update actions/upload-artifact action to v6 ([#18](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/18)) ([221dcd3](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/221dcd3bf821c72cca9d78d628be36e8150dc76e))
* **deps:** update actions/upload-pages-artifact action to v4 ([#16](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/16)) ([9b3ee76](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/9b3ee7620ff09edd1ee9ebe53a295835702917ab))
* enforce conventional commit with husky.net ([7291405](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/7291405c2d9591742851aef61b0626c199bffa77))
* set pre-commit validations with husky ([fa61991](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/fa619918c379321335667c2df08f5761e2f1a1da))
* uninstall lefthook and commit lint ([2faab36](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/2faab368f5e0fb8a73411a6a7d806faee7a4db7c))

### General maintenance

* remove sign script for local signing ([1f9cd31](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/1f9cd3137ef5192e6d14f603164284d5b62ceddb))
* use Log instead of console write in validate scene ([13d595d](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/13d595daa5fb048baee99e3af17310c2b2fb8a96))

### Refactoring

* change  validate meta file to a csx file ([988b974](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/988b974a0030d21203e89eb2ff67472bc4540868))
* change build doc from sh to csx script ([3bc0e81](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/3bc0e817d2154e8d699a6314dbb3e47a0a07f6b5))
* change update-unity-package-version from js to csx script ([47c3653](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/47c3653e8cebce343682822fd25b9a2b717627ee))
* change validate scnene file to a csx script ([53744ed](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/53744ed99b3649980c41b5eb77210b168f017512))

## [1.1.1](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/1.1.0...1.1.1) (2026-01-08)

### Revert previous changes

* remove promote to main and the push of PAT ([3f21238](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/3f212387eaa13bc7e6976a04383029d06415d037))

## [1.1.0](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/1.0.0...1.1.0) (2026-01-08)

### Features

* complete init script with csx ([e768d3c](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/e768d3c2f5f66cd669cd886353c308d23e9feb27))
* create generate csproj function in env service ([9106f0a](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/9106f0a48741b321ebaf1662c003107e384b851a))
* create gpg ([afe621a](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/afe621a229a96831d8c206f1d7135c249cd0e4ce))
* create license service ([fda550b](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/fda550ba2b274028a3e82d235ea244331c888d29))
* create template service and github service ([009f30e](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/009f30ef61f7a6db0ecece3524f90a523954c9a0))
* create unity and environment services ([059ae73](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/059ae7301819b103c7b34d4a449b2297f8fb72b1))
* expand path in license path prompt ([e43e5ad](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/e43e5adce2c82d72f3c6560cc746150527486fcc))
* generate license ([054b7e1](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/054b7e1304c911106a59e19b82ac812241f31947))
* **init:** create csx script that gathers infos ([cfec9c7](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/cfec9c7008869d9f40810c38990385228ad5ed27))
* start configuring init csx ([c0a052d](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/c0a052da0ff5aecc8614dfa2e1672eabdb11bb08))

### Dependency updates

* **core-deps:** update dependency unity editor ([#25](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/25)) ([619d461](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/619d4615ecd66b243e8fc9c45351fb5792619738))
* **deps:** install dotnet-script ([c2896ff](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/c2896ff74573f7fe89782e2b8fadb29840835590))

### Bug Fixes

* correct github service ([c4fa7d0](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/c4fa7d04686a610491710abb5203f9023c3a146e))
* follow official doc for path of unityYAMLmerge ([07d6c1b](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/07d6c1bb6ca4d71692c81416eb85e51768ede17c))
* import gpg in configurator ([a595e9f](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/a595e9f1cc39665236b43cb79b79376ec5af16d0))
* update ignore patterns in template service ([f18a244](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/f18a2449eda227a95ce7937c2079a346d04830d8))
* use --body in set secret from file to not be posix based ([d49d7e9](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/d49d7e9ea4228e68caccd5207e8d593e2dd6cf9b))
* use GetUnityLicensePath in configurator ([133dd86](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/133dd86673e135a3cc9f18c24edc20d27ced0116))

### Documentation

* update README with .unity validation and SONAR_HOST_URL ([2034354](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/20343545f328c023891047b79d9c383682c2d878))

### Build and continuous integration

* add RELEASE_STEP_PAT to GitHub Actions in env ([4a52dca](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/4a52dca650291bfb71f84ece533bbad4ee99fb9f))
* correct promotion job ([1bb76b4](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/1bb76b4d2f45b9571ce02a77ec4653307dc5580c))
* create init.sh and init.ps1 to execute underlying init.csx script ([4fee7e9](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/4fee7e95b7d3e0b2f6a004b4ad3e260714ec7d57))
* **deps:** update actions/checkout action to v5 ([#10](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/10)) ([c6e406c](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/c6e406cda6749187c7b3025e0e9c55411f877e9e))
* **deps:** update actions/checkout action to v6 ([#11](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/11)) ([c533738](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/c533738a9369a580667dc5f46b5ca3c619442dbf))
* **deps:** update actions/download-artifact action to v7 ([#12](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/12)) ([93aacac](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/93aacac08f8d02add8b7d3b3543e4a1fe399601f))
* **deps:** update actions/setup-dotnet action to v5 ([#13](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/13)) ([4193dff](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/4193dff257a4f64f2f56d4ca76940cdc4f7f1e80))
* **deps:** update actions/setup-node action to v5 ([#14](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/14)) ([0b15e88](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/0b15e88ec72a8aca7632047de2f94ee618947035))
* rename Tools to .automation ([f824966](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/f8249665cbf8f714a190d405eb82435a4e49dae8))
* Update git push command to use token authentication ([67b50d2](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/67b50d21ac82007a7ce57873ec42c5cb17119f79))

### General maintenance

* minor change ([0293cb2](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/0293cb2157d4ce7f05aa1472efc7a7d569d5b7c8))
* remove unused generate-csproj-for-docfx.js file ([6f56c22](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/6f56c221bb96f1d67fcbb962205e5dd54ea1f2fd))
* remove useless log ([8f39c3e](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/8f39c3e05a6662a2294f1b1ee857659a38ccde47))
* **renovate:** set abandonmentThreshold to 3 years ([9039da4](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/9039da45f52738e882586d6db42d93400221d896))

### Style improvements

* format csx files ([d86c88b](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/d86c88b111aef5a4bd65df4584d89f04bf3ea7e2))

### Refactoring

* move Tools -> .automation ([6de1d33](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/6de1d338938e6a251ff63f825cbdc1a63236f0fa))
* move unity version detection to unity service ([a5627c5](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/a5627c560cf8064ef4352233cf16e71e48af9801))
* rename GitHubService.cs -> .csx ([ef30779](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/ef3077980fce7195c58a4d050e4e4ac8067bfc6f))

## [1.0.0](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/0.1.8...1.0.0) (2026-01-06)

### ⚠ BREAKING CHANGES

* create a first working template with all features in place

### Documentation

* **todo:** remove an item ([ef15d0c](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/ef15d0c55417101a5771f9507ba4d95ee3d926d9))
* **todo:** update list ([e61897e](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/e61897ec2ea99ad3b58ab66c6ca97f2456b0683a))
* **todo:** update list ([b9c8b2d](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/b9c8b2d51fa4e2c5878ad826fcb85749f71d8a80))
* update documentation ([44d763d](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/44d763d891eb885e47885a05158294af68c5f628))
* update readme ([c056ba9](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/c056ba9a2d4e99454b9766ddff024bf7ce717e17))
* update readme ([d7417f6](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/d7417f6160992c0933a59483915197e9342ed982))
* update readme ([b2238b4](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/b2238b4e175373d434c25a5e39f208537f2be658))

### Build and continuous integration

* configure sonarqube to download coverage data ([21b6e54](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/21b6e540a6077514199d2cbd58ac85252eae8915))
* correct indentation ([3feebae](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/3feebaed8f8dbba940729f9dea5c2ea1ef6a5f4a))
* create promote-to-main job ([d6cca48](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/d6cca4836d6db27107836d89f0b4076cf8b0be33))
* **deps:** update actions/cache action to v5 ([#9](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/9)) ([e85d4bb](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/e85d4bbc85b3e5676014a41d9f4ce5b613315be0))
* **init:** append basic branch rules creation ([ab0da78](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/ab0da78ae28763ada947ba53086eeed810d2dba6))
* **init:** configure unityYAML smart merge ([7e2d3ff](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/7e2d3ff39103c2d3c8419de919bee758c7167a39))
* **init:** forget to ask package as with default ([b4549df](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/b4549df7dcae98964ce2abb933e9bff85ab6f998))
* **init:** gh cli set automatically ghp to gha ([1586ad3](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/1586ad3fbbf4d5fa9944c899a43481038cb0218a))
* **init:** push PAT secret ([eb17e3b](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/eb17e3b31ba90f5a9581dc41debd9012e6229d83))
* **init:** use read -s for password ([a674788](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/a674788852ff9bded7dfd50aa9e626a3d067d99b))
* update sonarqube to generate csproj and sln before running ([41fb000](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/41fb000ae87323a3ace29016016276d09d3f6f4c))
* update sonarqube to use dotnet-sonarscanner ([51184a1](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/51184a143bbf0fd500a08b08d1b3c84a61c2d10b))

### General maintenance

* create a first working template with all features in place ([de8b661](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/de8b661f5d2cc6d964984ba6b707d03fd6de0cb6))

## [0.1.8](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/0.1.7...0.1.8) (2026-01-06)

### Dependency updates

* **core-deps:** update dependency com.unity.inputsystem to v1.17.0 ([#7](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/7)) ([b3ac0b0](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/b3ac0b0a68d504c17f2b120a21174957d142ea89))
* **core-deps:** update dependency unity editor ([#8](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/8)) ([634022a](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/634022ad1ad9e980a81f0fa74b6ca69f1c78fffa))
* **deps:** update commitlint monorepo to v20.3.0 ([#23](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/23)) ([1726d1e](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/1726d1e9ba47e2c8f47e67b953d9f143161916b5))

### Documentation

* correct typo in readme ([ab0d916](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/ab0d9169e03ebcfce75e29991035245162d13a94))
* **todo:** remove issue and PR template ([db3a5ad](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/db3a5ad6a0fd13e5267b142f88f552d08d788b3c))
* **todo:** update list with priorities ([a65ea8b](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/a65ea8bc955dd8b0859a1d88740cf9fde24ba57f))
* update readme ([bb8e2d7](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/bb8e2d7a06bd7e913b1b68cc9ab3276991943eab))

### Build and continuous integration

* **init:** fix duplicate gpg secret push ([72f9066](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/72f90660c238f4feaf9c9967b155c504f8d6b4d1))
* **init:** upload of GPG secrets ([a88941f](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/a88941fb68ec5438481b02d0436ab328eb8540bc))
* remove passphrase ([884b36c](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/884b36cc7d581afd7437f4e1054f026642cc796e))
* **semrel:** correctly set the package.zip.sha256.sig path ([1a9d98b](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/1a9d98bcfda3abd4da684ea7ac63e15362e85e2c))

### General maintenance

* **config:** migrate config renovate.json ([54170d4](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/54170d45bba94270359e598e950107c08d81fd31))
* **config:** migrate Renovate config pull request [#24](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/24) from FilippoGurioli-master-thesis/renovate/migrate-config ([3a1d5d7](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/3a1d5d7ca4dda4232029dc508822122a7439b80f))

## [0.1.7](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/0.1.6...0.1.7) (2026-01-05)

### Dependency updates

* **core-deps:** update dependency com.unity.collab-proxy to v2.11.2 ([#6](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/6)) ([04fea90](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/04fea90b71c29becb7871c688e16a6411c5cde5e))
* **core-deps:** update dependency com.unity.timeline to v1.8.10 ([#3](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/3)) ([578dc64](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/578dc64102b47310f541d75c4149598faf6b57b7))
* **core-deps:** update dependency com.unity.visualscripting to v1.9.9 ([#4](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/4)) ([d294469](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/d294469859f81a79a5548160c0ecd50ab96116e8))
* **deps:** update dependency csharpier to v1.2.5 ([#5](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/5)) ([ae7a806](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/ae7a806d3ea936618f7348cc8faf758219744fb8))

### Documentation

* **todo:** remove signing from the list ([da3e1ca](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/da3e1ca961b8b3cb5ff72bdb99a914f2b1a4eef0))

### Build and continuous integration

* add issue and pr templates ([0ff9a59](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/0ff9a599eb854e143ef28cef1fca13389aa8f99f))
* set merge-multiple to true ([46cc345](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/46cc345aa60ff11149de7d0dc32f0751285fbdd5))

## [0.1.6](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/0.1.5...0.1.6) (2026-01-05)

### Dependency updates

* **core-deps:** update dependency com.unity.ide.visualstudio to v2.0.26 ([#2](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/2)) ([b1e6572](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/b1e6572ae406fa3894e824395b2b7b9d59d41ad0))

### Build and continuous integration

* add a template sign step ([09c62b4](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/09c62b4742768091613a9516237c6081974668fc))
* add include in build matrix ([795d0e5](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/795d0e505782518c9095eb294f50f61ef967c2e6))
* add name for the test job ([2cc576a](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/2cc576a69c67da39ab402dd04aa07d1292ea676f))
* add signing job ([6c8444f](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/6c8444fbab7b63bfafb44e7b85cdc4112acbbd5b))
* add sonarqube ([27cd9ac](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/27cd9ac730a607f54f6eff23eb3cb0ee03170a93))
* change from path to pattern in download artifact step ([8f77f52](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/8f77f52bfa4866c492a94dd383065c4d4c3dc8b1))
* configure sonar qube ([fda3514](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/fda3514cc548278ace37bc59d76e88f81ce42185))
* correct indentation ([7d6b91d](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/7d6b91df5875bc9d4d174824fc21c5c72b81ff26))
* correct project path package->project ([cde3bbd](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/cde3bbde91c043433eb7fb3fd53ba097cdfe791c))
* correct typo ([2486e04](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/2486e04b1e0eac757d1d3107615c63adc8343e0d))
* correct typo ([4e1ba3e](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/4e1ba3e17c71087a5796abc269599c272a35cb72))
* create a build step that runs only if not in template develop ([554daf9](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/554daf9bf8e8882d73807a6b8239788d7bd0815d))
* extend sign to template too ([b2fcb74](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/b2fcb74b6a821d13062735703fd89aecd30cfc77))
* give names to signing jobs ([8f47ba7](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/8f47ba7c5f25264f50bef04122c3b5f4dac743e9))
* **init:** checkout to develop at the very end ([1b980fb](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/1b980fb52852515cb420e962dc9e3bbf291a1ea1))
* **init:** dump renovate installation ([0e9f09f](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/0e9f09fc8b7471d42acef3750facc1d3aa81a58d))
* **init:** move renovate assignee to the template customer ([18c9cbd](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/18c9cbdf01db42ad0aa82b9c0b6dce1769833150))
* **init:** script also asks and create a valid license ([1deddd9](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/1deddd9233a1430b0fc2ad2b2c23b54771cdf2fc))
* **init:** upload secrets ([daf00ed](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/daf00ed69daa7da8e3c90b2f14f34bafd1db34fb))
* pipeline trigger also in PR to develop ([e4c3548](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/e4c35483f9f860ba406927eb69c90264eb154045))
* **releae:** add missing props ([ed8441f](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/ed8441fb2b7d008aa02de094a8692477e23c9434))
* **release:** correct if statement and needs ([62f3adc](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/62f3adc493828ef47d22d9c6c206a131352b7539))
* same check for documentation ([4391aea](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/4391aeae5bf939115320f08f60bf35c49a7a4145))
* **sign:** create an artifact signing script ([e51939a](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/e51939af04308d51d8b5e63a61343152e6006c0c))
* uncomment sonarqube quality gate ([fe6075e](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/fe6075ed3f4e098d717624a857339e61cdd26682))
* unite all checks under 2 gates (package, template) ([c304b41](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/c304b41c273722711f41830b7a15895274515e8e))
* update branch name dev->develop ([4ae4d7f](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/4ae4d7f986e180c2da414d4788359af5cd703628))
* update if in release ([0b91b05](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/0b91b052c46eeb1fa8d63ca43105b53cd544e53f))
* update release conditions so that it waits for quality gate ([e43216b](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/e43216bd70eea06e3c59b55f90e4fcdf6530e2ea))
* update release to dispatch even when build is skipped ([51215f6](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/51215f68c2fd3f3e048b86efde71f9fe211b45b1))

### General maintenance

* add todo list ([1bc7c50](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/1bc7c50cf13a6c17c7052c1e4f0530b0923bf4ba))
* Configure Renovate - Merge pull request [#1](https://github.com/FilippoGurioli-master-thesis/unity-package-template/issues/1) from FilippoGurioli-master-thesis/renovate/configure ([9b1d6c1](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/9b1d6c139f07436eea21780fdadbe2e5b9b8e372))
* **ignore:** add generated csproj inside __NAMESPACE__ ([3be77a9](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/3be77a9e60143b925c03a2e32a669d224619d588))
* **init:** move unity_init.log from root to unity project dir ([5824328](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/5824328e9142fdcdcb60057579597f55cb37a6e1))
* Merge branch 'main' into develop ([1cc1180](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/1cc118027f181463697d8f2f2656b5f50f1a746c))
* **renovate:** add abandonments feature ([597a825](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/597a8250ca6f941f2ddc9eb41b28ee9763f61836))
* **renovate:** add abandonments:recommended ([31ba518](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/31ba5180cb5d5129c51a677c647fc623cacdb433))
* **renovate:** add automerge for any kind of new version ([5e54efa](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/5e54efabbe17e5685dfc91e9421fa86ee6031b09))
* **renovate:** add package rule to ignore unity modules ([3f05a5d](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/3f05a5d5eebb8a060d403c109b5d7e0f299091cd))
* **renovate:** completely change renovate config ([36931d0](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/36931d07de956e143e67ab72907d56b6cd89675f))
* **renovate:** configure a custom manager to parse unity manifest.json ([c660ee2](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/c660ee2b46b4478f0c78d96c51afe80dbfe2d522))
* **renovate:** configure a custom manager to parse unity manifest.json ([f9d7e0b](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/f9d7e0bdd9140ed5e14910630a6a837843ea98ec))
* **renovate:** configure correctly notification suppression ([c02885a](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/c02885a5b77aa199fe1274d121419b1191ae2486))
* **renovate:** configure unity manager to correctly find the manifest file ([97d3e73](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/97d3e73cd843f5b74b23e1062bea65b9728e1e7a))
* **renovate:** configure unity manager to correctly find the manifest file ([e9e9153](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/e9e9153a1470518b7cb0663a45e5986e6ce0e57e))
* **renovate:** configure unity manager to correctly find the manifest file ([bc3935d](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/bc3935d2a13b0f27e2964b543d528419353bea6b))
* **renovate:** correct typo ([89e2a35](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/89e2a355f896e36ccdad645a414c2733b965b584))
* **renovate:** improve how to ignore __NAMESPACE__/package.json ([e897695](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/e897695757c0009ddb58cf2ca600fb2495dedebc))
* **renovate:** improve regex for unity custom manager ([8d034ae](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/8d034ae3e9e00ec67d300a23e44d3f827212676e))
* **renovate:** improve regex for unity custom manager ([f0a0181](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/f0a0181244b7a1a25eec86681d7d8abdf6578b88))
* **renovate:** improve unity regex manager to ignore unity modules ([8174f01](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/8174f011dbddabd7c7f92f3a2c1b6242182d5e8f))
* **renovate:** remove an invalid comma ([3f47a74](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/3f47a74d338d293ccc18654785dfe73e44436991))
* **renovate:** remove java related configuration ([0b65406](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/0b654069464ad259b00f4512560feaeccc644cc7))
* **renovate:** set core-deps as scope for unity related updates ([83bedd0](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/83bedd0060f8eaaf2d51366e27498868d958fbf4))
* **renovate:** set develop as base branch ([ec3d601](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/ec3d60154bab6fb31df8aba77cb77f467545661a))
* **renovate:** silent the lookup warning ([ad684e8](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/ad684e8303213d655a62ea6f45b67bc21fb9fa39))
* **renovate:** try one last time to ignore com.unity.modules from regex ([6a784b5](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/6a784b51cc46f358b9e53ed1b5e32e4eb2754f3e))
* **renovate:** update conf to use extensions instead of package rules ([879e0f3](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/879e0f3360bcf2e7e395af71f4920481241d9114))
* **renovate:** update conf to watch for unity manifest updates ([bcb77e9](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/bcb77e971ac1415e4a17bd209438704575df8d62))
* **renovate:** use extensions where possible ([b313f7c](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/b313f7cad492bde7fcc1c8943346bd36e07c684a))
* **renovate:** use extensions where possible ([11632b7](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/11632b7bf642ada5aba5fa1a668db0236b2961d3))

## [0.1.5](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/0.1.4...0.1.5) (2026-01-01)

### Documentation

* add template infos in package documentation ([0fff0a2](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/0fff0a2577c3ed2037de90f13796c092a8ddd25c))

### Tests

* add a test cs script to test API doc generation ([a37fe8a](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/a37fe8a39e7ee90852ac8b60d354388b9547974c))

### Build and continuous integration

* correctly install docfx in remote ci machine ([7ae1a07](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/7ae1a07a2f321a5ecb3a284ac5f717a27e3b82f9))
* update csproj generation so that it adds also unity dlls ([3ef4089](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/3ef40895eb27c41cc4f456fde68e87293137866d))
* upload package docs if .template is absent ([ecf0d51](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/ecf0d512ee5e4a2784dd308d422c743a790adf4f))

### General maintenance

* ignore api folder generate from docfx ([92c5027](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/92c502758c9d64dc6730d9a30523fd9d287d38f0))
* ignore bin and obj of __NAMESPACE__ ([36f67b9](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/36f67b980f3ff057949a1435fa78ed19bb75fc40))

## [0.1.4](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/0.1.3...0.1.4) (2025-12-31)

### Documentation

* configure docfx for package ([707fbfe](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/707fbfe89474b3776a5f5ba5c46a49255f03307c))

### Build and continuous integration

* create generate-csproj-for-docfx script ([470e469](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/470e469633e2a737cf6a78a89830e9b942c5e229))
* **init:** await unity dll file generation ([7928699](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/79286996b104df49ea2ab3237f3ea6ff957caddb))
* **init:** fix typo ([9b45b7e](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/9b45b7ea99b5ff7911f7cc2d7d2ddc1a990f8dd1))
* **init:** fix unity startup error ([fe0825b](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/fe0825b29dd35ea9a4b46ccf1b3e3ece8f1fec2e))
* **init:** put csproj generation before commit ([9392912](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/93929120c5892859beaea7b98ce3421166b48dca))
* **init:** unity editor opens after all unity startups ([125c8d1](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/125c8d1923353531a356d01fce57fd3c48c5c5ca))
* **init:** update init to check for unity version ([726e9e9](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/726e9e944c8748cd665d80178721857e5734e36b))
* use csproj generation script in init ([6c39262](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/6c39262fc749e7df5a9f235e457b97ef42c6e3eb))

### General maintenance

* fix bash syntax issue ([299276b](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/299276bed447251ed9173620013646d5a89fb4c4))
* improve logging ([75dda4e](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/75dda4ebaeddc0ff01ab6906dc3fa3a2b61b5990))

## [0.1.3](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/0.1.2...0.1.3) (2025-12-31)

### Documentation

* **template:** add style ([4a70bd1](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/4a70bd10022185ffd2a8f33548037ca181c454c9))
* **template:** create docfx file for template package ([cb25e98](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/cb25e9866571122e65b820a958c01922277212d9))
* **template:** create test manual and test image ([5063dfd](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/5063dfdf607bf31139a36b4850bd0baf0323e554))

### Build and continuous integration

* give correct permissions ([abf7a99](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/abf7a99281be630cf09f37284703f2c85f6bc652))
* give correct permissions ([8629df7](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/8629df7482d6352ec1beca993cecb3deac66a0d3))
* give correct permissions ([057604d](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/057604ddc3ed0c36491b4b0f777e3044a3c310e4))
* merge workflows ([1ea329a](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/1ea329a9fcf0384d5737f2424fd572220ca26051))

### General maintenance

* remove useless configuration ([451f0b3](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/451f0b31a2825464d566a62a5a839df0be879931))

## [0.1.2](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/0.1.1...0.1.2) (2025-12-30)

### Documentation

* configure path to API correctly ([7c17a9f](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/7c17a9f0badbc2ffb3d0ded94a7525308be00295))
* create documentation for template ([adb09e7](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/adb09e78353e195f532c42f8634a2de7418e8ea4))
* remove docfx once again ([78ad1fc](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/78ad1fc231a4c3432b5d438cd2b650b571a2b06b))

### Build and continuous integration

* create documentation deploy to GHP ([0c6d5ef](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/0c6d5efd7837641a3c1397d11870ef0789f4e0b9))
* momentaneuosly delete documentation pipeline ([07edb43](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/07edb437523858a4a39c6cf6b99b70abf183b207))

### General maintenance

* format ([7e22e70](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/7e22e708c0bf565d4d1e7937d0af56e73cde0a3d))

## [0.1.1](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/0.1.0...0.1.1) (2025-12-30)

### Documentation

* create the docfx structure ([02505ce](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/02505ce1d2d51a2777a521ffebe22bd9f3c6f2a4))
* restart on docfx ([7a3f597](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/7a3f5977afc1308e112910c32046a74efcdbc2ce))
* setup docfx to parse md files ([7192b5d](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/7192b5d6740583a88260e6fd487f623fbf516f36))

### Build and continuous integration

* create documentation deploy pipeline ([8f3e6c3](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/8f3e6c3e6a8f688093483e08795f7f427b18f0cb))
* remove NPM TOKEN usage (unused) ([7cd6ffa](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/7cd6ffabbb7d305c90cd06cc7ad453bd4a8bc612))

## [0.1.0](https://github.com/FilippoGurioli-master-thesis/unity-package-template/compare/0.0.0...0.1.0) (2025-12-29)

### Features

* add unity project that uses unity package template ([686f077](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/686f07795762464f592ea5fb9f6518dd42c04e39))

### Dependency updates

* **deps:** install commit lint ([79be6f8](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/79be6f84284bd4c8edd0d6fd720be83327653c6d))
* **deps:** install semrel ([994f3b8](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/994f3b8e1daef9809165070d05cc1ee38ad06189))

### Build and continuous integration

* add csharpier ([f582c5b](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/f582c5b429b272aaeee92019cd07b9fdb2f5e326))
* **commitlint:** configure properly ([3c7a7ba](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/3c7a7bad16f6f5be7bb111baf7aa064a5192d72c))
* configure release workflow ([01c3761](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/01c3761270236bc204e6c4f6c0dbf8804a95cac8))
* create .template file ([886644f](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/886644f986273131543bd548e77d82971568a690))
* **hook:** configure lefthook properly ([523a06e](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/523a06e9e0d199df5a72fbc5b3e9017d0b2e4fe0))
* **init:** create init script and template package ([04b8316](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/04b831664e15ab5f77701f18f8265b3f25ede8a6))
* **semrel:** configure properly ([5fe1b18](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/5fe1b18b125c6ed97c825e27fddc2138197a8ec1))
* **semrel:** uncomment github step ([778e6f1](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/778e6f1b6852e3a0696d2af0ac4fb33eb9242a15))

### General maintenance

* add useful dumps at the end of init ([57971a0](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/57971a0ec5c01e16963a87b9eb1ab241e13ec8b8))
* typo ([597d65b](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/597d65b2ec1278712126c271fa817c2430dc6f40))

### Refactoring

* rename from template project to sandbox.__namespace__ ([572c83c](https://github.com/FilippoGurioli-master-thesis/unity-package-template/commit/572c83cf238efc722ae785f6be9fa1916a9c5bc4))
