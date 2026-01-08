/// <summary>
/// Project configuration model
/// </summary>
public class ProjectConfig
{
    // --- 1. Identity & Naming (The Core) ---
    /// <summary>Git username</summary>
    public string GitUser { get; set; }

    /// <summary>Git email</summary>
    public string GitMail { get; set; }

    /// <summary>Git repository URL</summary>
    public string GitRepo { get; set; }

    /// <summary>Project domain (e.g., com, org)</summary>
    public string Domain { get; set; }

    /// <summary>Company name</summary>
    public string Company { get; set; }

    /// <summary>Package name</summary>
    public string Package { get; set; }

    /// <summary>Namespace for the project</summary>
    public string Namespace { get; set; }

    /// <summary>Display name of the package</summary>
    public string DisplayName { get; set; }

    /// <summary>Description of the package</summary>
    public string Description { get; set; }

    // --- 2. Artifacts & Legal ---
    /// <summary>License type of the project</summary>
    public string LicenseType { get; set; }

    /// <summary>Year for the license</summary>
    public string ProjectYear { get; set; } = DateTime.Now.Year.ToString();

    // --- 3. Secrets & Remote Integration ---
    /// <summary>Unity email for authentication</summary>
    public string UnityEmail { get; set; }

    /// <summary>Unity password for authentication</summary>
    public string UnityPassword { get; set; }

    /// <summary>Path to the Unity license file</summary>
    public string UnityLicensePath { get; set; }

    /// <summary>SonarQube server URL</summary>
    public string SonarUrl { get; set; }

    /// <summary>SonarQube authentication token</summary>
    public string SonarToken { get; set; }

    // --- 4. Computed Paths (Helpers for Services) ---
    /// <summary>Computed package identifier</summary>
    public string PackageId => $"{Domain}.{Company}.{Package}".ToLower();

    /// <summary>Computed sandbox path</summary>
    public string SandboxPath => $"Sandbox.{Namespace}";

    /// <summary>Computed full repository name</summary>
    public string RepoFullName => $"{GitUser}/{GitRepo}";

    /// <summary>GPG Key for signing artifacts</summary>
    public GpgKeyResult GpgKey { get; set; }

    public override string ToString()
    {
        // Generate a full log of the configuration for debugging
        return $@"
            --- Project Configuration ---
            Git User: {GitUser}
            Git Mail: {GitMail}
            Git Repo: {GitRepo}
            Domain: {Domain}
            Company: {Company}
            Package: {Package}
            Namespace: {Namespace}
            Display Name: {DisplayName}
            Description: {Description}
            License Type: {LicenseType}
            Project Year: {ProjectYear}
            Unity Email: {UnityEmail}
            Unity License Path: {UnityLicensePath}
            SonarQube URL: {SonarUrl}
            Gpg Key ID: {GpgKey.KeyId}
            Sonar Token: {(string.IsNullOrEmpty(SonarToken) ? "<not set>" : "<set>")}
            Unity Password: {(string.IsNullOrEmpty(UnityPassword) ? "<not set>" : "<set>")}
        ";
    }
}

/// <summary>
/// Result of GPG key generation
/// </summary>
public struct GpgKeyResult
{
    /// <summary>
    /// The Key ID of the generated GPG key
    /// </summary>
    public string KeyId;

    /// <summary>
    /// The private key in ASCII armored format
    /// </summary>
    public string PrivateKey;
}
