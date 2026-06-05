using System;
using System.IO;
using System.Text.Json;

namespace SignTool
{
    public class AppConfig
    {
        public CertificateConfig Certificate { get; set; } = new();
        public SigningConfig Signing { get; set; } = new();
        public UIConfig UI { get; set; } = new();
    }

    public class CertificateConfig
    {
        public string SubjectName { get; set; } = string.Empty;
        public string FriendlyName { get; set; } = string.Empty;
        public int KeySize { get; set; } = 2048;
        public int ValidityYears { get; set; } = 5;
        public string Organization { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Country { get; set; } = "CN";
        public string Province { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BusinessCategory { get; set; } = "Private Organization";
        public string RegistrationNumber { get; set; } = string.Empty;
        public string RegistrationCountry { get; set; } = string.Empty;
        public string RegistrationProvince { get; set; } = string.Empty;
        public string RegistrationCity { get; set; } = string.Empty;
        public string SubjectAlternativeNames { get; set; } = string.Empty;
        public bool KeyUsageDigitalSignature { get; set; } = true;
        public bool KeyUsageKeyEncipherment { get; set; } = true;
        public bool GenerateEV { get; set; } = false;
    }

    public class SigningConfig
    {
        public string HashAlgorithm { get; set; } = "SHA256";
        public string HashAlgorithm2 { get; set; } = "SHA1";
        public string TimestampServer { get; set; } = "DigiCert|http://timestamp.digicert.com";
        public bool DriverSigning { get; set; } = false;
        public bool DualSigning { get; set; } = false;
    }

    public class UIConfig
    {
        public int WindowWidth { get; set; } = 700;
        public int WindowHeight { get; set; } = 700;
        public int SelectedTab { get; set; } = 0;
    }

    public static class ConfigManager
    {
        private static readonly string ConfigPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "config.json");

        private static readonly JsonSerializerOptions _serializerOptions = new()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        public static AppConfig LoadConfig()
        {
            try
            {
                if (File.Exists(ConfigPath))
                {
                    string content = File.ReadAllText(ConfigPath);
                    return JsonSerializer.Deserialize<AppConfig>(content, _serializerOptions) ?? new AppConfig();
                }
            }
            catch
            {
            }

            return new AppConfig();
        }

        public static void SaveConfig(AppConfig config)
        {
            try
            {
                string directory = Path.GetDirectoryName(ConfigPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string content = JsonSerializer.Serialize(config, _serializerOptions);
                File.WriteAllText(ConfigPath, content);
            }
            catch
            {
            }
        }
    }
}