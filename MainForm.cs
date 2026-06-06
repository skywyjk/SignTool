using System;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Security.Principal;

namespace SignTool
{
    public partial class MainForm : Form
    {
        private X509Certificate2 _currentCertificate;
        private AppConfig _config;

        public MainForm()
        {
            InitializeComponent();
            LoadIconFromResource();
            LoadConfiguration();
            UpdateUserStatus();
            LoadCertificates();
            this.FormClosing += MainForm_FormClosing;
        }

        private void UpdateUserStatus()
        {
            try
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new(identity);
                bool isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
                
                if (isAdmin)
                {
                    LblUserStatus.Text = "当前身份: 管理员";
                    LblUserStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    LblUserStatus.Text = "当前身份: 普通用户";
                    LblUserStatus.ForeColor = System.Drawing.Color.Blue;
                }
            }
            catch
            {
                LblUserStatus.Text = "当前身份: 未知";
                LblUserStatus.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfiguration();
        }

        private void LoadConfiguration()
        {
            _config = ConfigManager.LoadConfig();
            
            // 证书配置
            TxtSubjectName.Text = _config.Certificate.SubjectName;
            TxtFriendlyName.Text = _config.Certificate.FriendlyName;
            NudKeySize.Value = _config.Certificate.KeySize;
            NudValidityYears.Value = _config.Certificate.ValidityYears;
            TxtOrganization.Text = _config.Certificate.Organization;
            CmbCountry.SelectedItem = _config.Certificate.Country;
            TxtEmail.Text = _config.Certificate.Email;
            CmbBusinessCategory.SelectedItem = _config.Certificate.BusinessCategory;
            TxtRegistrationNumber.Text = _config.Certificate.RegistrationNumber;
            TxtSubjectAlternativeNames.Text = _config.Certificate.SubjectAlternativeNames;
            ChkKeyUsageDigitalSignature.Checked = _config.Certificate.KeyUsageDigitalSignature;
            ChkKeyUsageKeyEncipherment.Checked = _config.Certificate.KeyUsageKeyEncipherment;

            // 签名配置
            CmbHashAlgorithm.SelectedItem = _config.Signing.HashAlgorithm;
            CmbTimestampServer.SelectedItem = _config.Signing.TimestampServer;
            ChkDriverSigning.Checked = _config.Signing.DriverSigning;

            // UI配置
            TabControl.SelectedIndex = _config.UI.SelectedTab;
        }

        private void SaveConfiguration()
        {
            // 证书配置
            _config.Certificate.SubjectName = TxtSubjectName.Text;
            _config.Certificate.FriendlyName = TxtFriendlyName.Text;
            _config.Certificate.KeySize = (int)NudKeySize.Value;
            _config.Certificate.ValidityYears = (int)NudValidityYears.Value;
            _config.Certificate.Organization = TxtOrganization.Text;
            _config.Certificate.Country = CmbCountry.SelectedItem?.ToString() ?? string.Empty;
            _config.Certificate.Email = TxtEmail.Text;
            _config.Certificate.BusinessCategory = CmbBusinessCategory.SelectedItem?.ToString() ?? string.Empty;
            _config.Certificate.RegistrationNumber = TxtRegistrationNumber.Text;
            _config.Certificate.SubjectAlternativeNames = TxtSubjectAlternativeNames.Text;
            _config.Certificate.KeyUsageDigitalSignature = ChkKeyUsageDigitalSignature.Checked;
            _config.Certificate.KeyUsageKeyEncipherment = ChkKeyUsageKeyEncipherment.Checked;

            // 签名配置
            _config.Signing.HashAlgorithm = CmbHashAlgorithm.SelectedItem?.ToString() ?? string.Empty;
            _config.Signing.TimestampServer = CmbTimestampServer.SelectedItem?.ToString() ?? string.Empty;
            _config.Signing.DriverSigning = ChkDriverSigning.Checked;

            // UI配置
            _config.UI.SelectedTab = TabControl.SelectedIndex;

            ConfigManager.SaveConfig(_config);
        }

        private void LoadIconFromResource()
        {
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                
                string[] resourceNames = assembly.GetManifestResourceNames();
                foreach (string name in resourceNames)
                {
                    if (name.EndsWith("logo.ico", StringComparison.OrdinalIgnoreCase))
                    {
                        using System.IO.Stream stream = assembly.GetManifestResourceStream(name);
                        if (stream != null)
                        {
                            this.Icon = new System.Drawing.Icon(stream);
                            return;
                        }
                    }
                }
                
                string[] possibleNames = ["SignTool.logo.ico", "logo.ico", "Resources.logo.ico"];
                foreach (string name in possibleNames)
                {
                    using System.IO.Stream stream = assembly.GetManifestResourceStream(name);
                    if (stream != null)
                    {
                        this.Icon = new System.Drawing.Icon(stream);
                        return;
                    }
                }
            }
            catch
            {
                // 图标加载失败时保持默认
            }
        }

        /// <summary>
        /// 从下拉框选中项中提取国家代码（例如从 "CN - 中国" 提取 "CN"）
        /// </summary>
        private static string GetCountryCode(string selectedValue)
        {
            if (string.IsNullOrWhiteSpace(selectedValue))
                return "";
            
            // 提取前两个字母作为国家代码
            int dashIndex = selectedValue.IndexOf(' ');
            if (dashIndex >= 2)
                return selectedValue[..dashIndex].Trim();
            
            // 如果没有找到空格，直接返回
            return selectedValue;
        }

        /// <summary>
        /// 从下拉框选中项中提取商业类别（例如从 "Private Organization - 私营企业" 提取 "Private Organization"）
        /// </summary>
        private static string GetBusinessCategory(string selectedValue)
        {
            if (string.IsNullOrWhiteSpace(selectedValue))
                return "";
            
            // 提取 " - " 前面的英文部分
            int dashIndex = selectedValue.IndexOf(" - ");
            if (dashIndex > 0)
                return selectedValue[..dashIndex].Trim();
            
            // 如果没有找到分隔符，直接返回
            return selectedValue;
        }

        /// <summary>
        /// 从下拉框选中项中提取时间戳服务器URL（格式："名称|URL"）
        /// </summary>
        private string GetTimestampServerUrl()
        {
            string selected = CmbTimestampServer.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrWhiteSpace(selected))
                return "http://timestamp.digicert.com";
            
            int pipeIndex = selected.IndexOf('|');
            if (pipeIndex > 0 && pipeIndex < selected.Length - 1)
                return selected[(pipeIndex + 1)..].Trim();
            
            return selected;
        }

        /// <summary>
        /// 测速按钮点击事件 - 使用多线程并行处理
        /// </summary>
        private async void BtnTestTimestamp_Click(object sender, EventArgs e)
        {
            BtnTestTimestamp.Enabled = false;
            BtnTestTimestamp.Text = "测速中...";

            try
            {
                // 获取所有时间戳服务器
                List<string> serverList = [];
                foreach (var item in CmbTimestampServer.Items)
                {
                    string server = item?.ToString();
                    if (!string.IsNullOrWhiteSpace(server))
                    {
                        serverList.Add(server);
                    }
                }

                // 使用多线程并行测试所有服务器
                List<Task<Tuple<string, long>>> tasks = [];
                foreach (string server in serverList)
                {
                    int pipeIndex = server.IndexOf('|');
                    string name = pipeIndex > 0 ? server[..pipeIndex] : server;
                    string url = pipeIndex > 0 ? server[(pipeIndex + 1)..].Trim() : server;

                    tasks.Add(Task.Run(async () =>
                    {
                        long latency = await Task.Run(() => CertificateManager.TestTimestampServer(url));
                        return Tuple.Create(name, latency);
                    }));
                }

                // 等待所有任务完成
                Tuple<string, long>[] results = await Task.WhenAll(tasks);

                // 按延迟排序
                var sortedResults = results.OrderBy(r => r.Item2).ToList();

                // 生成结果文本
                StringBuilder sb = new();
                sb.AppendLine("时间戳服务器测速结果:");
                sb.AppendLine();
                
                int index = 1;
                foreach (var result in sortedResults)
                {
                    if (result.Item2 >= 0)
                    {
                        sb.AppendLine($"{index}. {result.Item1}: {result.Item2}ms");
                    }
                    else
                    {
                        sb.AppendLine($"{index}. {result.Item1}: 连接失败");
                    }
                    index++;
                }

                MessageBox.Show(sb.ToString(), "测速结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                BtnTestTimestamp.Enabled = true;
                BtnTestTimestamp.Text = "测速";
            }
        }

        private void BtnGenerateCert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtSubjectName.Text))
            {
                MessageBox.Show("请输入发布者名称，这将显示为签名程序的已验证发布者", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // EV 证书需要组织信息
            if (ChkEVCertificate.Checked)
            {
                if (string.IsNullOrWhiteSpace(TxtOrganization.Text))
                {
                    MessageBox.Show("EV 证书需要填写组织名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(CmbCountry.SelectedItem?.ToString()))
                {
                    MessageBox.Show("EV 证书需要选择国家", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                CertificateOptions options = new()
                {
                    SubjectName = TxtSubjectName.Text,
                    FriendlyName = TxtFriendlyName.Text,
                    KeySize = (int)NudKeySize.Value,
                    ValidityYears = (int)NudValidityYears.Value,
                    Organization = TxtOrganization.Text,
                    OrganizationalUnit = TxtOrganizationalUnit.Text,
                    Country = GetCountryCode(CmbCountry.SelectedItem?.ToString() ?? ""),
                    State = TxtState.Text,
                    Locality = TxtLocality.Text,
                    Email = TxtEmail.Text,
                    IncludeKeyUsageDigitalSignature = ChkKeyUsageDigitalSignature.Checked,
                    IncludeKeyUsageKeyEncipherment = ChkKeyUsageKeyEncipherment.Checked,
                    SubjectAlternativeNames = TxtSubjectAlternativeNames.Text
                        .Split(separator, StringSplitOptions.RemoveEmptyEntries),
                    IsEVCertificate = ChkEVCertificate.Checked,
                    BusinessCategory = GetBusinessCategory(CmbBusinessCategory.SelectedItem?.ToString() ?? ""),
                    JurisdictionCountry = TxtJurisdictionCountry.Text,
                    JurisdictionState = TxtJurisdictionState.Text,
                    JurisdictionLocality = TxtJurisdictionLocality.Text,
                    RegistrationNumber = TxtRegistrationNumber.Text
                };

                _currentCertificate = CertificateManager.GenerateSelfSignedCertificate(options);

                string certType = ChkEVCertificate.Checked ? "EV 代码签名证书" : "代码签名证书";
                MessageBox.Show($"{certType}生成成功！\n\n注意：自签名证书仅用于测试目的，不会被 Windows 自动信任。\n如需正式使用，请购买 CA 签发的证书。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LblCertStatus.Text = $"已生成{certType}";
                BtnExportPfx.Enabled = true;
                BtnExportCer.Enabled = true;
                BtnExportPvkSpc.Enabled = true;
                BtnSignFile.Enabled = true;
                BtnBatchSign.Enabled = true;
                BtnInstallCert.Enabled = true;
                BtnUninstallCert.Enabled = true;
                BtnViewCertInfo.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"证书生成失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static readonly char[] separator = ['\r', '\n'];

        private void ChkEVCertificate_CheckedChanged(object sender, EventArgs e)
        {
            GrpEVInfo.Visible = ChkEVCertificate.Checked;
            if (ChkEVCertificate.Checked)
            {
                // EV 证书建议使用更长的密钥
                NudKeySize.Value = 4096;
            }
        }

        private void BtnExportPfx_Click(object sender, EventArgs e)
        {
            using SaveFileDialog saveDialog = new();
            saveDialog.Filter = "PFX证书文件 (*.pfx)|*.pfx|P12证书文件 (*.p12)|*.p12";
            saveDialog.Title = "保存PFX/P12证书";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string password = TxtPfxPassword.Text;
                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("请输入证书密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = CertificateManager.ExportCertificateToPfx(_currentCertificate, saveDialog.FileName, password);
                if (success)
                {
                    string ext = saveDialog.FileName.EndsWith(".p12", StringComparison.OrdinalIgnoreCase) ? "P12" : "PFX";
                    MessageBox.Show($"{ext}证书导出成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("证书导出失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnExportCer_Click(object sender, EventArgs e)
        {
            using SaveFileDialog saveDialog = new();
            saveDialog.Filter = "CER证书文件 (*.cer)|*.cer";
            saveDialog.Title = "保存CER证书";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                bool success = CertificateManager.ExportCertificateToCer(_currentCertificate, saveDialog.FileName);
                if (success)
                {
                    MessageBox.Show("CER证书导出成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("CER证书导出失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnExportPvkSpc_Click(object sender, EventArgs e)
        {
            using SaveFileDialog pvkDialog = new();
            pvkDialog.Filter = "PVK私钥文件 (*.pvk)|*.pvk";
            pvkDialog.Title = "保存PVK私钥文件";
            pvkDialog.FileName = "certificate.pvk";

            if (pvkDialog.ShowDialog() == DialogResult.OK)
            {
                using SaveFileDialog spcDialog = new();
                spcDialog.Filter = "SPC证书文件 (*.spc)|*.spc";
                spcDialog.Title = "保存SPC证书文件";
                spcDialog.FileName = "certificate.spc";

                if (spcDialog.ShowDialog() == DialogResult.OK)
                {
                    string password = TxtPfxPassword.Text;
                    bool success = CertificateManager.ExportCertificateToPvkSpc(_currentCertificate, pvkDialog.FileName, spcDialog.FileName, password);
                    if (success)
                    {
                        MessageBox.Show("PVK + SPC 证书导出成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("PVK + SPC 证书导出失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnLoadCert_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openDialog = new();
            openDialog.Filter = "PKCS#12证书文件 (*.pfx;*.p12)|*.pfx;*.p12|PFX证书文件 (*.pfx)|*.pfx|P12证书文件 (*.p12)|*.p12";
            openDialog.Title = "选择PFX/P12证书";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string password = TxtLoadPassword.Text;
                _currentCertificate = CertificateManager.LoadCertificateFromPfx(openDialog.FileName, password);

                if (_currentCertificate != null)
                {
                    MessageBox.Show("证书加载成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LblCertStatus.Text = "已加载证书";
                    BtnExportPfx.Enabled = true;
                    BtnExportCer.Enabled = true;
                    BtnExportPvkSpc.Enabled = true;
                    BtnSignFile.Enabled = true;
                    BtnBatchSign.Enabled = true;
                    BtnInstallCert.Enabled = true;
                    BtnUninstallCert.Enabled = true;
                    BtnViewCertInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("证书加载失败，请检查密码是否正确", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnSignFile_Click(object sender, EventArgs e)
        {
            if (_currentCertificate == null)
            {
                MessageBox.Show("请先生成或加载证书", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CertificateManager.IsSignToolAvailable())
            {
                MessageBox.Show("未找到signtool.exe，请安装Windows SDK或确保signtool.exe在系统路径中", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using OpenFileDialog openDialog = new();
            if (ChkDriverSigning.Checked)
            {
                openDialog.Filter = "驱动文件 (*.sys;*.cat)|*.sys;*.cat|所有文件 (*.*)|*.*";
                openDialog.Title = "选择要签名的驱动文件";
            }
            else
            {
                openDialog.Filter = "可执行文件 (*.exe;*.dll;*.msi)|*.exe;*.dll;*.msi|所有文件 (*.*)|*.*";
                openDialog.Title = "选择要签名的文件";
            }

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string hashAlgorithm = CmbHashAlgorithm.SelectedItem?.ToString() ?? "SHA256";
                string timestampServer = GetTimestampServerUrl();
                bool success;

                // 双重签名：同时使用 SHA1 和 SHA256
                if (ChkDualSign.Checked)
                {
                    if (ChkDriverSigning.Checked)
                    {
                        if (!CertificateManager.IsCrossCertificateAvailable())
                        {
                            MessageBox.Show("未找到交叉证书，请安装 Windows SDK", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        success = CertificateManager.DualSignDriver(openDialog.FileName, _currentCertificate, timestampServer);
                    }
                    else
                    {
                        success = CertificateManager.DualSignFile(openDialog.FileName, _currentCertificate, false, "", timestampServer);
                    }
                }
                else
                {
                    // 单一签名
                    if (ChkDriverSigning.Checked)
                    {
                        if (!CertificateManager.IsCrossCertificateAvailable())
                        {
                            MessageBox.Show("未找到交叉证书，请安装 Windows SDK", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        success = CertificateManager.SignDriver(openDialog.FileName, _currentCertificate, hashAlgorithm, timestampServer);
                    }
                    else
                    {
                        success = CertificateManager.SignFile(openDialog.FileName, _currentCertificate, hashAlgorithm, false, "", timestampServer);
                    }
                }

                if (success)
            {
                string signType = ChkDriverSigning.Checked ? "驱动" : "代码";
                string signMode = ChkDualSign.Checked ? "双重签名(SHA1+SHA256)" : hashAlgorithm;
                MessageBox.Show($"{signType}签名成功！\n摘要算法: {signMode}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string errorMsg = "签名失败，请确保文件有写入权限且不是正在运行的程序";
                if (!string.IsNullOrEmpty(CertificateManager.LastError))
                {
                    errorMsg += $"\n\n详细信息:\n{CertificateManager.LastError}";
                }
                MessageBox.Show(errorMsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }
        }

        private void BtnVerify_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openDialog = new();
            openDialog.Filter = "所有文件 (*.*)|*.*";
            openDialog.Title = "选择要验证的文件";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                bool isValid = CertificateManager.VerifySignature(openDialog.FileName);
                if (isValid)
                {
                    MessageBox.Show("签名验证成功！文件签名有效。", "验证结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("签名验证失败！文件可能未签名或签名无效。", "验证结果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnInstallCert_Click(object sender, EventArgs e)
        {
            if (_currentCertificate == null)
            {
                MessageBox.Show("请先生成或加载证书", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("安装证书到受信任的根证书颁发机构需要管理员权限，确定继续吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            bool success = CertificateManager.InstallCertificateToTrustedRoot(_currentCertificate);
            if (success)
            {
                MessageBox.Show("证书安装成功！签名后的程序将显示正确的发布者信息。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("证书安装失败，请以管理员身份运行程序", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUninstallCert_Click(object sender, EventArgs e)
        {
            if (_currentCertificate == null)
            {
                MessageBox.Show("请先生成或加载证书", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("确定要从受信任的根证书颁发机构卸载此证书吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            bool success = CertificateManager.UninstallCertificateFromTrustedRoot(_currentCertificate);
            if (success)
            {
                MessageBox.Show("证书卸载成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("证书卸载失败，请以管理员身份运行程序", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnViewCertInfo_Click(object sender, EventArgs e)
        {
            if (_currentCertificate == null)
            {
                MessageBox.Show("请先生成或加载证书", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string info = CertificateManager.GetCertificateInfo(_currentCertificate);
            MessageBox.Show(info, "证书详细信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnRemoveSign_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openDialog = new();
            openDialog.Filter = "可执行文件 (*.exe;*.dll;*.sys)|*.exe;*.dll;*.sys|所有文件 (*.*)|*.*";
            openDialog.Title = "选择要删除签名的文件";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openDialog.FileName;

                DialogResult result = MessageBox.Show(
                    $"确定要删除文件 \"{System.IO.Path.GetFileName(filePath)}\" 的数字签名吗？\n\n此操作将移除文件中的所有数字签名信息。",
                    "确认删除签名",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    bool success = CertificateManager.RemoveSignature(filePath);
                    if (success)
                    {
                        MessageBox.Show("签名删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string errorMessage = CertificateManager.LastError;
                        MessageBox.Show($"删除签名失败: {errorMessage}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnElevate_Click(object sender, EventArgs e)
        {
            try
            {
                // 检查是否已经是管理员
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new(identity);
                if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    MessageBox.Show("程序已经以管理员身份运行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 提示用户确认
                DialogResult result = MessageBox.Show(
                    "此操作将以管理员身份重启程序。是否继续？",
                    "管理员提权",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // 创建进程启动信息
                    ProcessStartInfo startInfo = new()
                    {
                        UseShellExecute = true,
                        WorkingDirectory = Environment.CurrentDirectory,
                        FileName = Application.ExecutablePath,
                        Verb = "runas" // 关键参数，请求管理员权限
                    };

                    try
                    {
                        // 启动新进程
                        Process.Start(startInfo);
                        // 关闭当前进程
                        Application.Exit();
                    }
                    catch (System.ComponentModel.Win32Exception)
                    {
                        // 用户取消了UAC提示
                        MessageBox.Show("操作已取消。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"提权失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnBatchSign_Click(object sender, EventArgs e)
        {
            if (_currentCertificate == null)
            {
                MessageBox.Show("请先生成或加载证书", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CertificateManager.IsSignToolAvailable())
            {
                MessageBox.Show("未找到signtool.exe，请安装Windows SDK或确保signtool.exe在系统路径中", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using OpenFileDialog openDialog = new();
            openDialog.Multiselect = true; // 允许多选
            if (ChkDriverSigning.Checked)
            {
                openDialog.Filter = "驱动文件 (*.sys;*.cat)|*.sys;*.cat|所有文件 (*.*)|*.*";
                openDialog.Title = "选择要批量签名的驱动文件（可多选）";
            }
            else
            {
                openDialog.Filter = "可执行文件 (*.exe;*.dll;*.msi)|*.exe;*.dll;*.msi|所有文件 (*.*)|*.*";
                openDialog.Title = "选择要批量签名的文件（可多选）";
            }

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string[] selectedFiles = openDialog.FileNames;
                if (selectedFiles.Length == 0)
                {
                    return;
                }

                string hashAlgorithm = CmbHashAlgorithm.SelectedItem?.ToString() ?? "SHA256";
                string timestampServer = GetTimestampServerUrl();
                bool useDualSign = ChkDualSign.Checked;

                // 禁用按钮并显示进度
                BtnBatchSign.Enabled = false;
                BtnBatchSign.Text = $"签名中... 0/{selectedFiles.Length}";

                int successCount = 0;
                int failCount = 0;
                StringBuilder errorMessages = new();

                // 逐个签名文件
                for (int i = 0; i < selectedFiles.Length; i++)
                {
                    string filePath = selectedFiles[i];
                    string fileName = System.IO.Path.GetFileName(filePath);

                    // 更新进度
                    BtnBatchSign.Text = $"签名中... {i + 1}/{selectedFiles.Length}";
                    BtnBatchSign.Refresh();

                    bool success;
                    if (useDualSign)
                    {
                        if (ChkDriverSigning.Checked)
                        {
                            success = CertificateManager.DualSignDriver(filePath, _currentCertificate, timestampServer);
                        }
                        else
                        {
                            success = CertificateManager.DualSignFile(filePath, _currentCertificate, false, "", timestampServer);
                        }
                    }
                    else
                    {
                        if (ChkDriverSigning.Checked)
                        {
                            success = CertificateManager.SignDriver(filePath, _currentCertificate, hashAlgorithm, timestampServer);
                        }
                        else
                        {
                            success = CertificateManager.SignFile(filePath, _currentCertificate, hashAlgorithm, false, "", timestampServer);
                        }
                    }

                    if (success)
                    {
                        successCount++;
                    }
                    else
                    {
                        failCount++;
                        errorMessages.AppendLine($"失败: {fileName}");
                        if (!string.IsNullOrEmpty(CertificateManager.LastError))
                        {
                            errorMessages.AppendLine($"  原因: {CertificateManager.LastError}");
                        }
                    }

                    // 让 UI 有机会更新
                    await Task.Delay(10);
                }

                // 恢复按钮
                BtnBatchSign.Enabled = true;
                BtnBatchSign.Text = "批量签名";

                // 显示结果
                string resultMessage = $"批量签名完成！\n\n成功: {successCount} 个文件\n失败: {failCount} 个文件";
                if (failCount > 0)
                {
                    resultMessage += $"\n\n失败文件:\n{errorMessages}";
                }

                MessageBox.Show(resultMessage, "批量签名结果", MessageBoxButtons.OK, 
                    failCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }
        }

        private void CmbCertificateStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCertificates();
        }

        private void BtnRefreshCerts_Click(object sender, EventArgs e)
        {
            LoadCertificates();
        }

        private void LoadCertificates()
        {
            LstCertificates.Items.Clear();
            TxtCertDetails.Text = string.Empty;
            SetCertificateButtonsEnabled(false);

            try
            {
                X509Store store = GetSelectedStore();
                store.Open(OpenFlags.ReadOnly);

                foreach (X509Certificate2 cert in store.Certificates)
                {
                    string certName = GetCertificateDisplayName(cert);
                    ListViewItem item = new(certName);
                    item.SubItems.Add(cert.IssuerName.Name);
                    
                    // 获取算法信息
                    string algorithm = GetCertificateAlgorithm(cert);
                    item.SubItems.Add(algorithm);
                    
                    // 获取签名哈希算法
                    string hashAlgorithm = GetSignatureHashAlgorithm(cert);
                    item.SubItems.Add(hashAlgorithm);
                    
                    item.SubItems.Add(cert.NotBefore.ToShortDateString());
                    item.SubItems.Add(cert.NotAfter.ToShortDateString());
                    
                    // 计算剩余天数
                    TimeSpan timeLeft = cert.NotAfter - DateTime.Now;
                    string daysLeftStr = timeLeft.TotalDays > 0 
                        ? $"{(int)timeLeft.TotalDays} 天" 
                        : "已过期";
                    item.SubItems.Add(daysLeftStr);
                    
                    // 获取证书类型
                    string certType = GetCertificateType(cert);
                    item.SubItems.Add(certType);
                    
                    item.Tag = cert;
                    
                    // 如果已过期，标记为红色
                    if (timeLeft.TotalDays <= 0)
                    {
                        item.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (timeLeft.TotalDays < 30)
                    {
                        item.ForeColor = System.Drawing.Color.Orange;
                    }
                    
                    LstCertificates.Items.Add(item);
                }

                store.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载证书失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string GetCertificateAlgorithm(X509Certificate2 cert)
        {
            string algoName = cert.PublicKey.Oid.FriendlyName ?? cert.PublicKey.Oid.Value;
            
            // 尝试获取密钥长度
            int? keySize = null;
            try
            {
                var rsa = cert.GetRSAPublicKey();
                if (rsa != null)
                {
                    keySize = rsa.KeySize;
                }
                else
                {
                    var ecdsa = cert.GetECDsaPublicKey();
                    if (ecdsa != null)
                    {
                        keySize = ecdsa.KeySize;
                    }
                }
            }
            catch { }
            
            if (keySize.HasValue)
            {
                return $"{algoName} ({keySize.Value}位)";
            }
            return algoName;
        }

        private static string GetSignatureHashAlgorithm(X509Certificate2 cert)
        {
            string signatureAlgo = cert.SignatureAlgorithm.FriendlyName ?? cert.SignatureAlgorithm.Value;
            
            // 从签名算法中提取哈希算法
            if (signatureAlgo.Contains("sha256", StringComparison.CurrentCultureIgnoreCase))
                return "SHA256";
            if (signatureAlgo.Contains("sha384", StringComparison.CurrentCultureIgnoreCase))
                return "SHA384";
            if (signatureAlgo.Contains("sha512", StringComparison.CurrentCultureIgnoreCase))
                return "SHA512";
            if (signatureAlgo.Contains("sha1", StringComparison.CurrentCultureIgnoreCase))
                return "SHA1";
            if (signatureAlgo.Contains("md5", StringComparison.CurrentCultureIgnoreCase))
                return "MD5";
            
            // 尝试从 OID 判断
            string oidValue = cert.SignatureAlgorithm.Value;
            return oidValue switch
            {
                "1.2.840.113549.1.1.11" => "SHA256",
                "1.2.840.113549.1.1.12" => "SHA384",
                "1.2.840.113549.1.1.13" => "SHA512",
                "1.2.840.113549.1.1.5" => "SHA1",
                "1.2.840.113549.1.1.4" => "MD5",
                _ => signatureAlgo
            };
        }

        private static string GetCertificateDisplayName(X509Certificate2 cert)
        {
            // 尝试多个来源获取证书名称
            if (!string.IsNullOrEmpty(cert.FriendlyName))
                return cert.FriendlyName;

            string simpleName = cert.GetNameInfo(X509NameType.SimpleName, true);
            if (!string.IsNullOrEmpty(simpleName) && !simpleName.StartsWith("CN="))
                return simpleName;

            // 从 Subject 中提取 CN
            string subject = cert.Subject;
            var cnMatch = MyRegex().Match(subject);
            if (cnMatch.Success)
                return cnMatch.Groups[1].Value;

            // 最后使用完整的 Subject
            return subject;
        }

        private X509Store GetSelectedStore()
        {
            string storeName = CmbCertificateStore.SelectedItem?.ToString() ?? "Personal - 个人";
            StoreName name;
            StoreLocation location = StoreLocation.CurrentUser;

            switch (storeName)
            {
                case "Personal - 个人":
                    name = StoreName.My;
                    break;
                case "TrustedPeople - 受信任的人":
                    name = StoreName.TrustedPeople;
                    break;
                case "TrustedPublisher - 受信任的发布者":
                    name = StoreName.TrustedPublisher;
                    break;
                case "CA - 证书颁发机构":
                    name = StoreName.CertificateAuthority;
                    break;
                case "Root - 受信任的根证书颁发机构":
                    name = StoreName.Root;
                    location = StoreLocation.LocalMachine;
                    break;
                default:
                    name = StoreName.My;
                    break;
            }

            return new X509Store(name, location);
        }

        private static string GetCertificateType(X509Certificate2 cert)
        {
            List<string> types = [];

            // 查找扩展密钥用法扩展
            foreach (X509Extension extension in cert.Extensions)
            {
                if (extension.Oid?.Value == "2.5.29.37") // Extended Key Usage
                {
                    if (extension is X509EnhancedKeyUsageExtension eku)
                    {
                        foreach (var oid in eku.EnhancedKeyUsages)
                        {
                            switch (oid.Value)
                            {
                                case "1.3.6.1.5.5.7.3.3":
                                    if (!types.Contains("代码签名"))
                                        types.Add("代码签名");
                                    break;
                                case "1.3.6.1.5.5.7.3.1":
                                    if (!types.Contains("服务器认证"))
                                        types.Add("服务器认证");
                                    break;
                                case "1.3.6.1.5.5.7.3.2":
                                    if (!types.Contains("客户端认证"))
                                        types.Add("客户端认证");
                                    break;
                                case "1.3.6.1.5.5.7.3.4":
                                    if (!types.Contains("电子邮件保护"))
                                        types.Add("电子邮件保护");
                                    break;
                                case "1.3.6.1.5.5.7.3.8":
                                    if (!types.Contains("时间戳"))
                                        types.Add("时间戳");
                                    break;
                                case "1.3.6.1.5.5.7.3.5":
                                    if (!types.Contains("IPSec终端系统"))
                                        types.Add("IPSec终端系统");
                                    break;
                                case "1.3.6.1.5.5.7.3.6":
                                    if (!types.Contains("IPSec隧道"))
                                        types.Add("IPSec隧道");
                                    break;
                                case "1.3.6.1.5.5.7.3.7":
                                    if (!types.Contains("IPSec用户"))
                                        types.Add("IPSec用户");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        // 备用方案：直接解析扩展数据
                        try
                        {
                            string extensionData = extension.Format(true);
                            if (extensionData.Contains("1.3.6.1.5.5.7.3.3"))
                            {
                                if (!types.Contains("代码签名"))
                                    types.Add("代码签名");
                            }
                            if (extensionData.Contains("1.3.6.1.5.5.7.3.1"))
                            {
                                if (!types.Contains("服务器认证"))
                                    types.Add("服务器认证");
                            }
                            if (extensionData.Contains("1.3.6.1.5.5.7.3.2"))
                            {
                                if (!types.Contains("客户端认证"))
                                    types.Add("客户端认证");
                            }
                        }
                        catch { }
                    }
                }
                else if (extension.Oid?.Value == "2.5.29.15") // Key Usage
                {
                    // 可以添加密钥用法识别
                }
            }

            if (types.Count == 0)
            {
                // 根据证书属性判断类型
                if (cert.Subject.Contains("CN=Microsoft"))
                {
                    types.Add("微软证书");
                }
                else if (cert.Subject.Contains("root", StringComparison.CurrentCultureIgnoreCase) || cert.Issuer.Contains("root", StringComparison.CurrentCultureIgnoreCase))
                {
                    types.Add("根证书");
                }
                else
                {
                    types.Add("其他");
                }
            }

            return string.Join(", ", types);
        }

        private void LstCertificates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstCertificates.SelectedItems.Count > 0)
            {
                if (LstCertificates.SelectedItems[0].Tag is X509Certificate2 cert)
                {
                    DisplayCertificateDetails(cert);
                    SetCertificateButtonsEnabled(true);
                }
            }
            else
            {
                TxtCertDetails.Text = string.Empty;
                SetCertificateButtonsEnabled(false);
            }
        }

        private void DisplayCertificateDetails(X509Certificate2 cert)
        {
            StringBuilder details = new();
            details.AppendLine($"证书名称: {cert.FriendlyName ?? cert.GetNameInfo(X509NameType.SimpleName, true)}");
            details.AppendLine($"主题: {cert.Subject}");
            details.AppendLine($"颁发者: {cert.Issuer}");
            details.AppendLine($"序列号: {cert.SerialNumber}");
            details.AppendLine($"有效期从: {cert.NotBefore}");
            details.AppendLine($"有效期至: {cert.NotAfter}");
            details.AppendLine($"版本: {cert.Version}");
            details.AppendLine($"签名算法: {cert.SignatureAlgorithm.FriendlyName}");
            details.AppendLine($"公钥算法: {cert.GetKeyAlgorithm()}");
            details.AppendLine($"密钥长度: {cert.GetRSAPublicKey()?.KeySize ?? cert.GetECDsaPublicKey()?.KeySize ?? cert.GetDSAPublicKey()?.KeySize ?? 0} 位");
            
            if (cert.HasPrivateKey)
            {
                details.AppendLine("是否有私钥: 是");
            }
            else
            {
                details.AppendLine("是否有私钥: 否");
            }

            details.AppendLine("\n扩展信息:");
            foreach (var extension in cert.Extensions)
            {
                try
                {
                    details.AppendLine($"  {extension.Oid?.FriendlyName ?? extension.Oid?.Value}: {extension.Format(true)}");
                }
                catch
                {
                    details.AppendLine($"  {extension.Oid?.FriendlyName ?? extension.Oid?.Value}: [无法解析]");
                }
            }

            TxtCertDetails.Text = details.ToString();
        }

        private void SetCertificateButtonsEnabled(bool enabled)
        {
            BtnViewSystemCertInfo.Enabled = enabled;
            BtnExportSystemCert.Enabled = enabled;
            BtnDeleteSystemCert.Enabled = enabled;
            
            if (enabled && LstCertificates.SelectedItems.Count > 0)
            {
                BtnUseSystemCert.Enabled = LstCertificates.SelectedItems[0].Tag is X509Certificate2 cert && cert.HasPrivateKey;
            }
            else
            {
                BtnUseSystemCert.Enabled = false;
            }
        }

        private void BtnViewSystemCertInfo_Click(object sender, EventArgs e)
        {
            if (LstCertificates.SelectedItems.Count > 0)
            {
                if (LstCertificates.SelectedItems[0].Tag is X509Certificate2 cert)
                {
                    string info = CertificateManager.GetCertificateInfo(cert);
                    MessageBox.Show(info, "证书详细信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnExportSystemCert_Click(object sender, EventArgs e)
        {
            if (LstCertificates.SelectedItems.Count > 0)
            {
                if (LstCertificates.SelectedItems[0].Tag is X509Certificate2 cert)
                {
                    using SaveFileDialog saveDialog = new();
                    saveDialog.Filter = "DER 编码证书 (*.cer)|*.cer|PFX 证书 (*.pfx)|*.pfx|所有文件 (*.*)|*.*";
                    saveDialog.Title = "导出证书";
                    saveDialog.FileName = $"{cert.GetNameInfo(X509NameType.SimpleName, true)}.cer";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            string filePath = saveDialog.FileName;
                            if (filePath.EndsWith(".pfx", StringComparison.OrdinalIgnoreCase))
                            {
                                if (cert.HasPrivateKey)
                                {
                                    byte[] pfxData = cert.Export(X509ContentType.Pfx, "");
                                    System.IO.File.WriteAllBytes(filePath, pfxData);
                                    MessageBox.Show("证书导出成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("该证书没有私钥，无法导出为PFX格式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                byte[] cerData = cert.Export(X509ContentType.Cert);
                                System.IO.File.WriteAllBytes(filePath, cerData);
                                MessageBox.Show("证书导出成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"导出证书失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void BtnDeleteSystemCert_Click(object sender, EventArgs e)
        {
            if (LstCertificates.SelectedItems.Count > 0)
            {
                if (LstCertificates.SelectedItems[0].Tag is X509Certificate2 cert)
                {
                    DialogResult result = MessageBox.Show(
                        $"确定要删除证书 \"{cert.FriendlyName ?? cert.GetNameInfo(X509NameType.SimpleName, true)}\" 吗？\n\n此操作将从系统证书存储中永久删除该证书。",
                        "确认删除",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            X509Store store = GetSelectedStore();
                            store.Open(OpenFlags.ReadWrite);
                            store.Remove(cert);
                            store.Close();

                            MessageBox.Show("证书删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCertificates();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"删除证书失败: {ex.Message}\n\n可能需要管理员权限", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void BtnUseSystemCert_Click(object sender, EventArgs e)
        {
            if (LstCertificates.SelectedItems.Count > 0)
            {
                if (LstCertificates.SelectedItems[0].Tag is X509Certificate2 cert && cert.HasPrivateKey)
                {
                    _currentCertificate = cert;
                    TxtCertDetails.Text = string.Empty;
                    LoadCertificates();

                    TabControl.SelectedTab = TabPage3;
                    MessageBox.Show("已选择该证书进行签名操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        [System.Text.RegularExpressions.GeneratedRegex(@"CN=([^,]+)")]
        private static partial System.Text.RegularExpressions.Regex MyRegex();
    }
}