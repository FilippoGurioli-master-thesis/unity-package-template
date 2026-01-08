#load "Shell.csx"
#load "Log.csx"
#load "../Models/ProjectConfig.csx"

public static class Gpg
{
    /// <summary>
    /// Generates an ephemeral GPG key for CI artifact signing
    /// </summary>
    /// <param name="name"> The name associated with the GPG key. </param>
    /// <param name="email"> The email associated with the GPG key. </param>
    /// <returns> The generated GPG key details. </returns>
    public static GpgKeyResult GenerateCiKey(
        string name = "CI Artifact Signing",
        string email = "ci@local"
    )
    {
        Log.Info("Generating ephemeral GPG signing key...");
        string tempGpgHome = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempGpgHome);
        try
        {
            string gpgBatchConfig =
                $@"
                %no-protection
                Key-Type: eddsa
                Key-Curve: ed25519
                Name-Real: {name}
                Name-Email: {email}
                Expire-Date: 0
                %commit
            ";
            string configPath = Path.Combine(tempGpgHome, "gpg-batch.conf");
            File.WriteAllText(configPath, gpgBatchConfig);
            var env = new Dictionary<string, string> { { "GNUPGHOME", tempGpgHome } };
            Shell.Run("gpg", $"--batch --generate-key \"{configPath}\"", hideOutput: true);
            string listOutput = Shell.Run(
                "gpg",
                "--list-secret-keys --with-colons",
                hideOutput: true
            );
            string keyId = listOutput
                .Split('\n')
                .FirstOrDefault(line => line.StartsWith("sec:"))
                ?.Split(':')[4];
            if (string.IsNullOrEmpty(keyId))
                throw new Exception("Failed to extract GPG Key ID from generated key.");
            string privateKey = Shell.Run(
                "gpg",
                $"--armor --export-secret-keys {keyId}",
                hideOutput: true
            );
            Log.Info($"Successfully generated GPG Key: {keyId}");
            return new GpgKeyResult { KeyId = keyId, PrivateKey = privateKey };
        }
        finally
        {
            if (Directory.Exists(tempGpgHome))
                Directory.Delete(tempGpgHome, true);
        }
    }
}
