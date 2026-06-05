using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using System.Diagnostics;
using System.Linq;

namespace SignTool
{
    public class CertificateOptions
    {
        public string SubjectName { get; set; } = "Code Signing Certificate";
        public string FriendlyName { get; set; } = "CodeSigningCert";
        public int KeySize { get; set; } = 2048;
        public int ValidityYears { get; set; } = 5;

        // 组织信息
        public string Organization { get; set; } = "";
        public string OrganizationalUnit { get; set; } = "";
        public string Country { get; set; } = "";
        public string State { get; set; } = "";
        public string Locality { get; set; } = "";
        public string Email { get; set; } = "";

        // 高级选项
        public string[] SubjectAlternativeNames { get; set; } = [];
        public bool IncludeKeyUsageDigitalSignature { get; set; } = true;
        public bool IncludeKeyUsageKeyEncipherment { get; set; } = true;

        // EV 证书选项
        public bool IsEVCertificate { get; set; } = false;
        public string BusinessCategory { get; set; } = "";  // 商业类别
        public string JurisdictionCountry { get; set; } = "";  // 注册国家
        public string JurisdictionState { get; set; } = "";  // 注册州/省
        public string JurisdictionLocality { get; set; } = "";  // 注册城市
        public string RegistrationNumber { get; set; } = "";  // 公司注册号
    }

    public static class CertificateManager
    {
        public static string LastError { get; private set; } = "";
        
        public static X509Certificate2 GenerateSelfSignedCertificate(CertificateOptions options)
        {
            using RSA rsa = RSA.Create(options.KeySize);

            // 构建完整的主题名称
            string subject = $"CN={options.SubjectName}";

            if (!string.IsNullOrEmpty(options.Organization))
                subject += $", O={options.Organization}";
            if (!string.IsNullOrEmpty(options.OrganizationalUnit))
                subject += $", OU={options.OrganizationalUnit}";
            if (!string.IsNullOrEmpty(options.Country))
                subject += $", C={options.Country}";
            if (!string.IsNullOrEmpty(options.State))
                subject += $", ST={options.State}";
            if (!string.IsNullOrEmpty(options.Locality))
                subject += $", L={options.Locality}";
            if (!string.IsNullOrEmpty(options.Email))
                subject += $", E={options.Email}";

            CertificateRequest request = new(
                subject,
                rsa,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1
            );

            // 基本约束
            request.CertificateExtensions.Add(
                new X509BasicConstraintsExtension(false, false, 0, false)
            );

            // 密钥用法
            X509KeyUsageFlags keyUsageFlags = 0;
            if (options.IncludeKeyUsageDigitalSignature)
                keyUsageFlags |= X509KeyUsageFlags.DigitalSignature;
            if (options.IncludeKeyUsageKeyEncipherment)
                keyUsageFlags |= X509KeyUsageFlags.KeyEncipherment;

            request.CertificateExtensions.Add(
                new X509KeyUsageExtension(keyUsageFlags, false)
            );

            // 增强密钥用法 - 代码签名
            OidCollection oids = [new Oid("1.3.6.1.5.5.7.3.3")];
            
            // EV 证书添加额外的 OID
            if (options.IsEVCertificate)
            {
                // Microsoft EV Code Signing OID - 用于 Windows 内核模式驱动签名
                oids.Add(new Oid("1.3.6.1.4.1.311.10.3.10"));
            }
            
            request.CertificateExtensions.Add(
                new X509EnhancedKeyUsageExtension(oids, false)
            );

            // EV 证书添加额外的扩展
            if (options.IsEVCertificate)
            {
                // 添加 EV 证书策略扩展 (使用自定义扩展)
                // OID: 2.23.140.1.1.1 - EV Code Signing Certificate Policy
                
                // 创建证书策略扩展 (手动构建 ASN.1 结构)
                string policyOid = "2.23.140.1.1.1";
                byte[] policyValue = BuildCertificatePolicyExtension(policyOid);
                request.CertificateExtensions.Add(
                    new X509Extension(
                        new Oid("2.5.29.32"),  // Certificate Policies OID
                        policyValue,
                        false
                    )
                );

                // 添加 Jurisdiction 信息扩展 (使用 CAB Forum OID)
                if (!string.IsNullOrEmpty(options.JurisdictionCountry))
                {
                    // Jurisdiction Country OID: 1.3.6.1.4.1.311.60.2.1.3
                    request.CertificateExtensions.Add(
                        new X509Extension(
                            new Oid("1.3.6.1.4.1.311.60.2.1.3"),
                            System.Text.Encoding.UTF8.GetBytes(options.JurisdictionCountry),
                            false
                        )
                    );
                }
                
                if (!string.IsNullOrEmpty(options.JurisdictionState))
                {
                    // Jurisdiction State OID: 1.3.6.1.4.1.311.60.2.1.2
                    request.CertificateExtensions.Add(
                        new X509Extension(
                            new Oid("1.3.6.1.4.1.311.60.2.1.2"),
                            System.Text.Encoding.UTF8.GetBytes(options.JurisdictionState),
                            false
                        )
                    );
                }
                
                if (!string.IsNullOrEmpty(options.JurisdictionLocality))
                {
                    // Jurisdiction Locality OID: 1.3.6.1.4.1.311.60.2.1.1
                    request.CertificateExtensions.Add(
                        new X509Extension(
                            new Oid("1.3.6.1.4.1.311.60.2.1.1"),
                            System.Text.Encoding.UTF8.GetBytes(options.JurisdictionLocality),
                            false
                        )
                    );
                }
                
                if (!string.IsNullOrEmpty(options.BusinessCategory))
                {
                    // Business Category OID: 2.5.4.15
                    request.CertificateExtensions.Add(
                        new X509Extension(
                            new Oid("2.5.4.15"),
                            System.Text.Encoding.UTF8.GetBytes(options.BusinessCategory),
                            false
                        )
                    );
                }
                
                if (!string.IsNullOrEmpty(options.RegistrationNumber))
                {
                    // Registration Number OID: 1.3.6.1.4.1.311.60.2.1.4
                    request.CertificateExtensions.Add(
                        new X509Extension(
                            new Oid("1.3.6.1.4.1.311.60.2.1.4"),
                            System.Text.Encoding.UTF8.GetBytes(options.RegistrationNumber),
                            false
                        )
                    );
                }
            }

            // 主题备用名称
            if (options.SubjectAlternativeNames.Length > 0)
            {
                SubjectAlternativeNameBuilder sanBuilder = new();
                foreach (string san in options.SubjectAlternativeNames)
                {
                    if (!string.IsNullOrWhiteSpace(san))
                    {
                        // 判断是否为DNS格式或IP地址
                        if (IsIPAddress(san))
                        {
                            sanBuilder.AddIpAddress(System.Net.IPAddress.Parse(san));
                        }
                        else
                        {
                            sanBuilder.AddDnsName(san);
                        }
                    }
                }
                request.CertificateExtensions.Add(sanBuilder.Build());
            }

            DateTime notBefore = DateTime.Now;
            DateTime notAfter = notBefore.AddYears(options.ValidityYears);

            X509Certificate2 cert = request.CreateSelfSigned(notBefore, notAfter);
            cert.FriendlyName = options.FriendlyName;

            return cert;
        }

        private static bool IsIPAddress(string value)
        {
            return System.Net.IPAddress.TryParse(value, out _);
        }

        private static byte[] BuildCertificatePolicyExtension(string policyOid)
        {
            // 构建 Certificate Policies 扩展的 ASN.1 结构
            // SEQUENCE { SEQUENCE { OID } }
            
            using MemoryStream ms = new();
            using BinaryWriter writer = new(ms);
            
            // 将 OID 转换为 DER 编码
            byte[] oidBytes = EncodeOid(policyOid);
            
            // PolicyInformation SEQUENCE
            byte[] policyInfo = BuildSequence(oidBytes);
            
            // CertificatePolicies SEQUENCE
            byte[] certificatePolicies = BuildSequence(policyInfo);
            
            return certificatePolicies;
        }

        private static byte[] EncodeOid(string oid)
        {
            string[] parts = oid.Split('.');
            int[] values = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                values[i] = int.Parse(parts[i]);
            
            using MemoryStream ms = new();
            
            // 第一个字节 = 40 * first + second
            ms.WriteByte((byte)(40 * values[0] + values[1]));
            
            // 其余部分使用 base-128 编码
            for (int i = 2; i < values.Length; i++)
            {
                EncodeBase128(ms, values[i]);
            }
            
            // OID TAG = 0x06
            byte[] content = ms.ToArray();
            using MemoryStream result = new();
            result.WriteByte(0x06);  // OID tag
            WriteLength(result, content.Length);
            result.Write(content, 0, content.Length);
            return result.ToArray();
        }

        private static void EncodeBase128(MemoryStream stream, int value)
        {
            if (value < 128)
            {
                stream.WriteByte((byte)value);
                return;
            }
            
            byte[] bytes = new byte[4];
            int count = 0;
            while (value > 0)
            {
                bytes[count++] = (byte)(value & 0x7F);
                value >>= 7;
            }
            
            for (int i = count - 1; i >= 0; i--)
            {
                if (i > 0)
                    stream.WriteByte((byte)(bytes[i] | 0x80));
                else
                    stream.WriteByte(bytes[i]);
            }
        }

        private static byte[] BuildSequence(byte[] content)
        {
            using MemoryStream ms = new();
            ms.WriteByte(0x30);  // SEQUENCE tag
            WriteLength(ms, content.Length);
            ms.Write(content, 0, content.Length);
            return ms.ToArray();
        }

        private static void WriteLength(MemoryStream stream, int length)
        {
            if (length < 128)
            {
                stream.WriteByte((byte)length);
            }
            else if (length < 256)
            {
                stream.WriteByte(0x81);
                stream.WriteByte((byte)length);
            }
            else
            {
                stream.WriteByte(0x82);
                stream.WriteByte((byte)(length >> 8));
                stream.WriteByte((byte)(length & 0xFF));
            }
        }

        public static bool ExportCertificateToPfx(X509Certificate2 cert, string filePath, string password)
        {
            try
            {
                byte[] pfxData = cert.Export(X509ContentType.Pfx, password);
                File.WriteAllBytes(filePath, pfxData);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ExportCertificateToCer(X509Certificate2 cert, string filePath)
        {
            try
            {
                byte[] cerData = cert.Export(X509ContentType.Cert);
                File.WriteAllBytes(filePath, cerData);
                return true;
            }
            catch
            {
                return false;
            }
        }



        public static bool RemoveSignature(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    LastError = "文件不存在";
                    return false;
                }

                string backupPath = filePath + ".bak";
                File.Copy(filePath, backupPath, true);

                string signtoolPath = FindSignTool();
                if (string.IsNullOrEmpty(signtoolPath))
                {
                    LastError = "未找到 signtool.exe";
                    return false;
                }

                string[] removeCommands = [
                    "remove /c /u \"{0}\"",
                    "remove /a /c /u \"{0}\"",
                    "remove /all /c /u \"{0}\"",
                    "remove /c /u /s \"{0}\"",
                    "remove /a /c /u /s \"{0}\"",
                    "remove /f /c /u \"{0}\"",
                    "remove /f /a /c /u \"{0}\"",
                    "strip \"{0}\""
                ];

                foreach (string cmdTemplate in removeCommands)
                {
                    string arguments = string.Format(cmdTemplate, filePath);
                    
                    ProcessStartInfo startInfo = new()
                    {
                        FileName = signtoolPath,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WorkingDirectory = Path.GetDirectoryName(filePath) ?? Environment.CurrentDirectory
                    };

                    using Process process = Process.Start(startInfo);
                    process?.WaitForExit();
                }

                if (IsValidPEFile(filePath))
                {
                    File.Delete(backupPath);
                    return true;
                }
                else
                {
                    File.Copy(backupPath, filePath, true);
                    File.Delete(backupPath);
                    LastError = "删除签名失败，文件已恢复";
                    return false;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        private static bool IsValidPEFile(string filePath)
        {
            try
            {
                byte[] data = File.ReadAllBytes(filePath);
                if (data.Length < 0x40)
                    return false;

                if (data[0] != 'M' || data[1] != 'Z')
                    return false;

                int peOffset = BitConverter.ToInt32(data, 0x3C);
                if (peOffset + 4 > data.Length)
                    return false;

                return data[peOffset] == 'P' && data[peOffset + 1] == 'E' && 
                       data[peOffset + 2] == '\0' && data[peOffset + 3] == '\0';
            }
            catch
            {
                return false;
            }
        }

        public static bool ExportCertificateToPvkSpc(X509Certificate2 cert, string pvkFilePath, string spcFilePath, string password)
        {
            try
            {
                // 导出 SPC (证书文件)
                byte[] spcData = cert.Export(X509ContentType.Cert);
                File.WriteAllBytes(spcFilePath, spcData);

                // 导出 PVK (私钥文件)
                if (cert.HasPrivateKey)
                {
                    byte[] pvkData = ExportPrivateKeyToPvk(cert, password);
                    File.WriteAllBytes(pvkFilePath, pvkData);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private static byte[] ExportPrivateKeyToPvk(X509Certificate2 cert, string password)
        {
            using RSA rsa = cert.GetRSAPrivateKey() ?? throw new InvalidOperationException("证书没有私钥");

            // 获取私钥参数
            RSAParameters parameters = rsa.ExportParameters(true);

            // 构建 PVK 文件头
            // PVK 文件格式: PVK1 + 标志位 + 密钥长度 + 加密标志 + 私钥数据长度 + 私钥数据
            using MemoryStream ms = new();
            using BinaryWriter writer = new(ms);

            // PVK 魔数 - 正确的是 PVK1 (0x314B5650)
            writer.Write(0x314B5650); // "PVK1"

            // 标志位 (PVK_V1_FORMAT)
            writer.Write((int)0x00000000);

            // 密钥长度
            writer.Write(parameters.Modulus.Length * 8);

            // 加密标志 (如果有密码则加密)
            bool isEncrypted = !string.IsNullOrEmpty(password);
            writer.Write(isEncrypted ? (int)0x00000001 : (int)0x00000000);

            // 私钥数据 (以 DER 格式存储)
            byte[] privateKeyDer = BuildRsaPrivateKeyDer(parameters);
            
            if (isEncrypted)
            {
                // 使用简单的加密 (PVK 使用 RC4 加密)
                byte[] encryptedKey = EncryptWithPassword(privateKeyDer, password);
                writer.Write(encryptedKey.Length);
                writer.Write(encryptedKey);
            }
            else
            {
                writer.Write(privateKeyDer.Length);
                writer.Write(privateKeyDer);
            }

            return ms.ToArray();
        }

        private static byte[] BuildRsaPrivateKeyDer(RSAParameters parameters)
        {
            // 构建 RSA 私钥的 DER 编码
            // RSAPrivateKey ::= SEQUENCE {
            //     version INTEGER,
            //     modulus INTEGER,
            //     publicExponent INTEGER,
            //     privateExponent INTEGER,
            //     prime1 INTEGER,
            //     prime2 INTEGER,
            //     exponent1 INTEGER,
            //     exponent2 INTEGER,
            //     coefficient INTEGER
            // }

            using MemoryStream ms = new();
            using BinaryWriter writer = new(ms);

            // 开始构建 SEQUENCE
            MemoryStream innerMs = new();
            using BinaryWriter innerWriter = new(innerMs);

            // version (0)
            WriteDerInteger(innerWriter, 0);

            // modulus
            WriteDerInteger(innerWriter, parameters.Modulus);

            // publicExponent
            WriteDerInteger(innerWriter, parameters.Exponent);

            // privateExponent
            WriteDerInteger(innerWriter, parameters.D);

            // prime1
            WriteDerInteger(innerWriter, parameters.P);

            // prime2
            WriteDerInteger(innerWriter, parameters.Q);

            // exponent1 (d mod (p-1))
            WriteDerInteger(innerWriter, parameters.DP);

            // exponent2 (d mod (q-1))
            WriteDerInteger(innerWriter, parameters.DQ);

            // coefficient (q^-1 mod p)
            WriteDerInteger(innerWriter, parameters.InverseQ);

            // 封装到 SEQUENCE
            byte[] innerData = innerMs.ToArray();
            writer.Write((byte)0x30); // SEQUENCE tag
            WriteDerLength(writer, innerData.Length);
            writer.Write(innerData);

            return ms.ToArray();
        }

        private static void WriteDerInteger(BinaryWriter writer, byte[] value)
        {
            // 确保正数的最高位不为1 (DER 编码要求)
            if (value.Length > 0 && (value[0] & 0x80) != 0)
            {
                writer.Write((byte)0x02); // INTEGER tag
                WriteDerLength(writer, value.Length + 1);
                writer.Write((byte)0x00); // 前置零字节
                writer.Write(value);
            }
            else
            {
                writer.Write((byte)0x02); // INTEGER tag
                WriteDerLength(writer, value.Length);
                writer.Write(value);
            }
        }

        private static void WriteDerInteger(BinaryWriter writer, int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            
            // 移除前导零
            int startIndex = 0;
            while (startIndex < bytes.Length - 1 && bytes[startIndex] == 0)
                startIndex++;
            
            byte[] trimmed = new byte[bytes.Length - startIndex];
            Array.Copy(bytes, startIndex, trimmed, 0, trimmed.Length);
            
            WriteDerInteger(writer, trimmed);
        }

        private static void WriteDerLength(BinaryWriter writer, int length)
        {
            if (length < 128)
            {
                writer.Write((byte)length);
            }
            else if (length < 256)
            {
                writer.Write((byte)0x81);
                writer.Write((byte)length);
            }
            else
            {
                writer.Write((byte)0x82);
                writer.Write((byte)(length >> 8));
                writer.Write((byte)(length & 0xFF));
            }
        }

        private static byte[] EncryptWithPassword(byte[] data, string password)
        {
            // PVK 使用简单的 RC4 加密，密钥由密码派生
            byte[] key = new byte[16];
            byte[] passwordBytes = System.Text.Encoding.ASCII.GetBytes(password);
            
            // 简单的密钥派生
            for (int i = 0; i < passwordBytes.Length; i++)
            {
                key[i % 16] ^= passwordBytes[i];
            }

            // RC4 加密
            return Rc4Encrypt(data, key);
        }

        private static byte[] Rc4Encrypt(byte[] data, byte[] key)
        {
            byte[] s = new byte[256];
            byte[] result = new byte[data.Length];
            
            // 初始化 S 盒
            for (int i = 0; i < 256; i++)
                s[i] = (byte)i;
            
            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + s[i] + key[i % key.Length]) % 256;
                (s[i], s[j]) = (s[j], s[i]);
            }
            
            // 加密
            int i2 = 0, j2 = 0;
            for (int k = 0; k < data.Length; k++)
            {
                i2 = (i2 + 1) % 256;
                j2 = (j2 + s[i2]) % 256;
                (s[i2], s[j2]) = (s[j2], s[i2]);
                byte t = (byte)((s[i2] + s[j2]) % 256);
                result[k] = (byte)(data[k] ^ s[t]);
            }
            
            return result;
        }

        public static bool InstallCertificateToTrustedRoot(X509Certificate2 cert)
        {
            try
            {
                using X509Store store = new(StoreName.Root, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadWrite);
                if (!store.Certificates.Contains(cert))
                {
                    store.Add(cert);
                }

                store.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UninstallCertificateFromTrustedRoot(X509Certificate2 cert)
        {
            try
            {
                using X509Store store = new(StoreName.Root, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadWrite);
                if (store.Certificates.Contains(cert))
                {
                    store.Remove(cert);
                }

                store.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetCertificateInfo(X509Certificate2 cert)
        {
            if (cert == null)
            {
                return "无证书";
            }

            string info = $"主题: {cert.Subject}\n";
            info += $"颁发者: {cert.Issuer}\n";
            info += $"有效期: {cert.NotBefore:yyyy-MM-dd HH:mm:ss} 至 {cert.NotAfter:yyyy-MM-dd HH:mm:ss}\n";
            info += $"序列号: {cert.SerialNumber}\n";
            info += $"签名算法: {cert.SignatureAlgorithm.FriendlyName}\n";
            info += $"公钥算法: {cert.GetKeyAlgorithm()}\n";
            info += $"公钥大小: {cert.GetKeyAlgorithmParameters()}\n";
            info += $"SHA1指纹: {cert.GetCertHashString()}\n";
            info += $"SHA256指纹: {cert.GetCertHashString(HashAlgorithmName.SHA256)}\n";
            info += $"是否有私钥: {cert.HasPrivateKey}\n";

            if (cert.HasPrivateKey)
            {
                using RSA rsa = cert.GetRSAPrivateKey();
                if (rsa != null)
                {
                    info += $"私钥算法: RSA\n";
                    info += $"私钥大小: {rsa.KeySize}位\n";
                }
            }

            // 显示证书扩展信息
            foreach (X509Extension ext in cert.Extensions)
            {
                if (ext is X509SubjectAlternativeNameExtension san)
                {
                    info += $"\n主题备用名称:\n";
                    foreach (var dnsName in san.EnumerateDnsNames())
                    {
                        info += $"  DNS: {dnsName}\n";
                    }
                    foreach (var ip in san.EnumerateIPAddresses())
                    {
                        info += $"  IP: {ip}\n";
                    }
                }
            }

            return info;
        }

        public static X509Certificate2 LoadCertificateFromPfx(string filePath, string password)
        {
            try
            {
                return new X509Certificate2(filePath, password, X509KeyStorageFlags.Exportable);
            }
            catch
            {
                return null;
            }
        }

        public static bool SignFile(string filePath, X509Certificate2 cert, string hashAlgorithm = "SHA256", bool isDriverSigning = false, string crossCertificatePath = "", string timestampServer = "http://timestamp.digicert.com")
        {
            string tempPfxPath = Path.GetTempFileName() + ".pfx";
            string password = Guid.NewGuid().ToString();

            try
            {
                if (!ExportCertificateToPfx(cert, tempPfxPath, password))
                {
                    LastError = "导出PFX证书失败";
                    return false;
                }

                string signtoolPath = FindSignTool();
                if (string.IsNullOrEmpty(signtoolPath))
                {
                    LastError = "未找到signtool.exe，请安装Windows SDK";
                    return false;
                }

                string arguments = $"sign /f \"{tempPfxPath}\" /p \"{password}\" /fd {hashAlgorithm} /t \"{timestampServer}\"";
                
                // 驱动签名需要添加交叉证书参数
                if (isDriverSigning && !string.IsNullOrEmpty(crossCertificatePath))
                {
                    arguments += $" /ac \"{crossCertificatePath}\"";
                }
                
                arguments += $" \"{filePath}\"";

                ProcessStartInfo psi = new()
                {
                    FileName = signtoolPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using Process process = Process.Start(psi);
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                
                if (process.ExitCode != 0)
                {
                    LastError = $"签名失败 (退出码: {process.ExitCode})\n输出: {output}\n错误: {error}";
                    return false;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                LastError = $"签名异常: {ex.Message}";
                return false;
            }
            finally
            {
                if (File.Exists(tempPfxPath))
                {
                    File.Delete(tempPfxPath);
                }
            }
        }

        /// <summary>
        /// 双重签名：同时使用 SHA1 和 SHA256 签名
        /// </summary>
        public static bool DualSignFile(string filePath, X509Certificate2 cert, bool isDriverSigning = false, string crossCertificatePath = "", string timestampServer = "http://timestamp.digicert.com")
        {
            string tempPfxPath = Path.GetTempFileName() + ".pfx";
            string password = Guid.NewGuid().ToString();

            try
            {
                if (!ExportCertificateToPfx(cert, tempPfxPath, password))
                {
                    return false;
                }

                string signtoolPath = FindSignTool();
                if (string.IsNullOrEmpty(signtoolPath))
                {
                    return false;
                }

                // 第一步：使用 SHA1 签名
                string arguments1 = $"sign /f \"{tempPfxPath}\" /p \"{password}\" /fd SHA1 /t \"{timestampServer}\"";
                if (isDriverSigning && !string.IsNullOrEmpty(crossCertificatePath))
                {
                    arguments1 += $" /ac \"{crossCertificatePath}\"";
                }
                arguments1 += $" \"{filePath}\"";

                ProcessStartInfo psi1 = new()
                {
                    FileName = signtoolPath,
                    Arguments = arguments1,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using Process process1 = Process.Start(psi1);
                process1.WaitForExit();
                if (process1.ExitCode != 0)
                {
                    return false;
                }

                // 第二步：添加 SHA256 签名（使用 /as 参数追加签名）
                string arguments2 = $"sign /f \"{tempPfxPath}\" /p \"{password}\" /fd SHA256 /as /tr \"{timestampServer}\" /td SHA256";
                if (isDriverSigning && !string.IsNullOrEmpty(crossCertificatePath))
                {
                    arguments2 += $" /ac \"{crossCertificatePath}\"";
                }
                arguments2 += $" \"{filePath}\"";

                ProcessStartInfo psi2 = new()
                {
                    FileName = signtoolPath,
                    Arguments = arguments2,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using Process process2 = Process.Start(psi2);
                process2.WaitForExit();
                return process2.ExitCode == 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (File.Exists(tempPfxPath))
                {
                    File.Delete(tempPfxPath);
                }
            }
        }

        public static bool DualSignDriver(string filePath, X509Certificate2 cert, string timestampServer = "http://timestamp.digicert.com")
        {
            string crossCertPath = FindCrossCertificate();
            return DualSignFile(filePath, cert, true, crossCertPath, timestampServer);
        }

        public static bool SignDriver(string filePath, X509Certificate2 cert, string hashAlgorithm = "SHA256", string timestampServer = "http://timestamp.digicert.com")
        {
            string crossCertPath = FindCrossCertificate();
            return SignFile(filePath, cert, hashAlgorithm, true, crossCertPath, timestampServer);
        }

        /// <summary>
        /// 测试时间戳服务器响应时间
        /// </summary>
        public static long TestTimestampServer(string url)
        {
            try
            {
                using System.Net.Http.HttpClient client = new();
                client.Timeout = TimeSpan.FromSeconds(10);
                
                DateTime startTime = DateTime.Now;
                byte[] response = client.GetByteArrayAsync(url).Result;
                DateTime endTime = DateTime.Now;
                
                return (long)(endTime - startTime).TotalMilliseconds;
            }
            catch
            {
                return -1;
            }
        }

        private static string FindCrossCertificate()
        {
            string[] possiblePaths =
            [
                @"C:\Program Files (x86)\Windows Kits\10\CrossCertificates\crosscert.dll",
                @"C:\Program Files (x86)\Windows Kits\10\bin\x64\crosscert.dll",
                @"C:\Program Files (x86)\Windows Kits\8.1\CrossCertificates\crosscert.dll",
                @"C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\crosscert.dll",
                @"C:\Program Files (x86)\Windows Kits\10\bin\10.0.22621.0\x64\crosscert.dll",
                @"C:\Program Files (x86)\Windows Kits\10\bin\10.0.19041.0\x64\crosscert.dll",
                @"C:\Program Files (x86)\Windows Kits\10\bin\10.0.18362.0\x64\crosscert.dll",
                @"C:\Program Files (x86)\Windows Kits\10\bin\10.0.17763.0\x64\crosscert.dll",
                @"C:\Program Files (x86)\Windows Kits\10\bin\10.0.17134.0\x64\crosscert.dll",
                @"C:\Program Files (x86)\Windows Kits\10\bin\x86\crosscert.dll",
                @"C:\Program Files (x86)\Windows Kits\10\CrossCertificates\*.cer",
                @"C:\Program Files (x86)\Windows Kits\10\CrossCertificates"
            ];

            foreach (string path in possiblePaths)
            {
                if (path.EndsWith("*.cer"))
                {
                    string dir = path[..^5];
                    if (Directory.Exists(dir))
                    {
                        string[] cerFiles = Directory.GetFiles(dir, "*.cer");
                        if (cerFiles.Length > 0)
                        {
                            return cerFiles[0];
                        }
                    }
                }
                else if (path.EndsWith("CrossCertificates"))
                {
                    if (Directory.Exists(path))
                    {
                        string[] cerFiles = Directory.GetFiles(path, "*.cer");
                        if (cerFiles.Length > 0)
                        {
                            return cerFiles[0];
                        }
                        string[] dllFiles = Directory.GetFiles(path, "*.dll");
                        if (dllFiles.Length > 0)
                        {
                            return dllFiles[0];
                        }
                    }
                }
                else if (File.Exists(path))
                {
                    return path;
                }
            }

            return "";
        }

        public static bool IsCrossCertificateAvailable()
        {
            return FindCrossCertificate() != "";
        }

        public static bool IsSignToolAvailable()
        {
            return FindSignTool() != null;
        }

        private static string FindSignTool()
        {
            string[] possiblePaths =
            [
                @"C:\Program Files (x86)\Windows Kits\10\bin\10.0.22621.0\x64\signtool.exe",
                @"C:\Program Files (x86)\Windows Kits\10\bin\x64\signtool.exe",
                @"C:\Program Files (x86)\Windows Kits\10\bin\10.0.19041.0\x64\signtool.exe",
                @"C:\Program Files (x86)\Windows Kits\8.1\bin\x64\signtool.exe",
                @"C:\Program Files (x86)\Microsoft SDKs\ClickOnce\SignTool\signtool.exe"
            ];

            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            return null;
        }

        public static bool VerifySignature(string filePath)
        {
            string signtoolPath = FindSignTool();
            if (string.IsNullOrEmpty(signtoolPath))
            {
                return false;
            }

            try
            {
                ProcessStartInfo psi = new()
                {
                    FileName = signtoolPath,
                    Arguments = $"verify /pa \"{filePath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using Process process = Process.Start(psi);
                process.WaitForExit();
                return process.ExitCode == 0;
            }
            catch
            {
                return false;
            }
        }
    }
}