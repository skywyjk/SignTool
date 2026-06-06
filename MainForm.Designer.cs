namespace SignTool
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label LblSubjectName;
        private System.Windows.Forms.TextBox TxtSubjectName;
        private System.Windows.Forms.Label LblFriendlyName;
        private System.Windows.Forms.TextBox TxtFriendlyName;
        private System.Windows.Forms.Label LblKeySize;
        private System.Windows.Forms.NumericUpDown NudKeySize;
        private System.Windows.Forms.Label LblValidityYears;
        private System.Windows.Forms.NumericUpDown NudValidityYears;
        private System.Windows.Forms.Button BtnGenerateCert;
        private System.Windows.Forms.Button BtnExportCer;
        private System.Windows.Forms.Button BtnExportPfx;
        private System.Windows.Forms.Button BtnExportPvkSpc;
        private System.Windows.Forms.Button BtnLoadCert;
        private System.Windows.Forms.Button BtnInstallCert;
        private System.Windows.Forms.Button BtnUninstallCert;
        private System.Windows.Forms.Button BtnViewCertInfo;
        private System.Windows.Forms.Button BtnSignFile;
        private System.Windows.Forms.Button BtnVerify;
        private System.Windows.Forms.Button BtnRemoveSign;
        private System.Windows.Forms.Button BtnElevate;
        private System.Windows.Forms.Label LblUserStatus;
        private System.Windows.Forms.Label LblPfxPassword;
        private System.Windows.Forms.TextBox TxtPfxPassword;
        private System.Windows.Forms.Label LblLoadPassword;
        private System.Windows.Forms.TextBox TxtLoadPassword;
        private System.Windows.Forms.Label LblCertStatus;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabPage1;
        private System.Windows.Forms.TabPage TabPage2;
        private System.Windows.Forms.TabPage TabPage3;

        // 代码签名控件
        private System.Windows.Forms.CheckBox ChkDriverSigning;
        private System.Windows.Forms.ComboBox CmbHashAlgorithm;
        private System.Windows.Forms.Label LblHashAlgorithm;
        private System.Windows.Forms.Label LblSignType;
        private System.Windows.Forms.CheckBox ChkDualSign;
        private System.Windows.Forms.Label LblTimestampServer;
        private System.Windows.Forms.ComboBox CmbTimestampServer;
        private System.Windows.Forms.Button BtnTestTimestamp;

        // 新增控件
        private System.Windows.Forms.GroupBox GrpOrganization;
        private System.Windows.Forms.Label LblOrganization;
        private System.Windows.Forms.TextBox TxtOrganization;
        private System.Windows.Forms.Label LblOrganizationalUnit;
        private System.Windows.Forms.TextBox TxtOrganizationalUnit;
        private System.Windows.Forms.Label LblCountry;
        private System.Windows.Forms.ComboBox CmbCountry;
        private System.Windows.Forms.Label LblState;
        private System.Windows.Forms.TextBox TxtState;
        private System.Windows.Forms.Label LblLocality;
        private System.Windows.Forms.TextBox TxtLocality;
        private System.Windows.Forms.Label LblEmail;
        private System.Windows.Forms.TextBox TxtEmail;

        private System.Windows.Forms.GroupBox GrpAdvanced;
        private System.Windows.Forms.Label LblSubjectAlternativeNames;
        private System.Windows.Forms.TextBox TxtSubjectAlternativeNames;
        private System.Windows.Forms.Label LblKeyUsage;
        private System.Windows.Forms.CheckBox ChkKeyUsageDigitalSignature;
        private System.Windows.Forms.CheckBox ChkKeyUsageKeyEncipherment;

        // EV 证书控件
        private System.Windows.Forms.CheckBox ChkEVCertificate;
        private System.Windows.Forms.GroupBox GrpEVInfo;
        private System.Windows.Forms.Label LblBusinessCategory;
        private System.Windows.Forms.ComboBox CmbBusinessCategory;
        private System.Windows.Forms.Label LblJurisdictionCountry;
        private System.Windows.Forms.TextBox TxtJurisdictionCountry;
        private System.Windows.Forms.Label LblJurisdictionState;
        private System.Windows.Forms.TextBox TxtJurisdictionState;
        private System.Windows.Forms.Label LblJurisdictionLocality;
        private System.Windows.Forms.TextBox TxtJurisdictionLocality;
        private System.Windows.Forms.Label LblRegistrationNumber;
        private System.Windows.Forms.TextBox TxtRegistrationNumber;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            TabControl = new System.Windows.Forms.TabControl();
            TabPage1 = new System.Windows.Forms.TabPage();
            BtnGenerateCert = new System.Windows.Forms.Button();
            GrpEVInfo = new System.Windows.Forms.GroupBox();
            TxtRegistrationNumber = new System.Windows.Forms.TextBox();
            LblRegistrationNumber = new System.Windows.Forms.Label();
            TxtJurisdictionLocality = new System.Windows.Forms.TextBox();
            LblJurisdictionLocality = new System.Windows.Forms.Label();
            TxtJurisdictionState = new System.Windows.Forms.TextBox();
            LblJurisdictionState = new System.Windows.Forms.Label();
            TxtJurisdictionCountry = new System.Windows.Forms.TextBox();
            LblJurisdictionCountry = new System.Windows.Forms.Label();
            CmbBusinessCategory = new System.Windows.Forms.ComboBox();
            LblBusinessCategory = new System.Windows.Forms.Label();
            ChkEVCertificate = new System.Windows.Forms.CheckBox();
            GrpAdvanced = new System.Windows.Forms.GroupBox();
            ChkKeyUsageKeyEncipherment = new System.Windows.Forms.CheckBox();
            ChkKeyUsageDigitalSignature = new System.Windows.Forms.CheckBox();
            LblKeyUsage = new System.Windows.Forms.Label();
            TxtSubjectAlternativeNames = new System.Windows.Forms.TextBox();
            LblSubjectAlternativeNames = new System.Windows.Forms.Label();
            GrpOrganization = new System.Windows.Forms.GroupBox();
            TxtEmail = new System.Windows.Forms.TextBox();
            LblEmail = new System.Windows.Forms.Label();
            TxtLocality = new System.Windows.Forms.TextBox();
            LblLocality = new System.Windows.Forms.Label();
            TxtState = new System.Windows.Forms.TextBox();
            LblState = new System.Windows.Forms.Label();
            CmbCountry = new System.Windows.Forms.ComboBox();
            LblCountry = new System.Windows.Forms.Label();
            TxtOrganizationalUnit = new System.Windows.Forms.TextBox();
            LblOrganizationalUnit = new System.Windows.Forms.Label();
            TxtOrganization = new System.Windows.Forms.TextBox();
            LblOrganization = new System.Windows.Forms.Label();
            NudValidityYears = new System.Windows.Forms.NumericUpDown();
            LblValidityYears = new System.Windows.Forms.Label();
            NudKeySize = new System.Windows.Forms.NumericUpDown();
            LblKeySize = new System.Windows.Forms.Label();
            TxtFriendlyName = new System.Windows.Forms.TextBox();
            LblFriendlyName = new System.Windows.Forms.Label();
            TxtSubjectName = new System.Windows.Forms.TextBox();
            LblSubjectName = new System.Windows.Forms.Label();
            TabPage2 = new System.Windows.Forms.TabPage();
            BtnExportCer = new System.Windows.Forms.Button();
            BtnExportPfx = new System.Windows.Forms.Button();
            BtnLoadCert = new System.Windows.Forms.Button();
            BtnInstallCert = new System.Windows.Forms.Button();
            BtnUninstallCert = new System.Windows.Forms.Button();
            BtnViewCertInfo = new System.Windows.Forms.Button();
            BtnElevate = new System.Windows.Forms.Button();
            LblUserStatus = new System.Windows.Forms.Label();
            TxtLoadPassword = new System.Windows.Forms.TextBox();
            LblLoadPassword = new System.Windows.Forms.Label();
            TxtPfxPassword = new System.Windows.Forms.TextBox();
            LblPfxPassword = new System.Windows.Forms.Label();
            LblCertStatus = new System.Windows.Forms.Label();
            BtnExportPvkSpc = new System.Windows.Forms.Button();
            TabPage3 = new System.Windows.Forms.TabPage();
            BtnVerify = new System.Windows.Forms.Button();
            BtnSignFile = new System.Windows.Forms.Button();
            BtnRemoveSign = new System.Windows.Forms.Button();
            ChkDriverSigning = new System.Windows.Forms.CheckBox();
            ChkDualSign = new System.Windows.Forms.CheckBox();
            CmbHashAlgorithm = new System.Windows.Forms.ComboBox();
            CmbTimestampServer = new System.Windows.Forms.ComboBox();
            BtnTestTimestamp = new System.Windows.Forms.Button();
            LblHashAlgorithm = new System.Windows.Forms.Label();
            LblTimestampServer = new System.Windows.Forms.Label();
            LblSignType = new System.Windows.Forms.Label();
            TabControl.SuspendLayout();
            TabPage1.SuspendLayout();
            GrpEVInfo.SuspendLayout();
            GrpAdvanced.SuspendLayout();
            GrpOrganization.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NudValidityYears).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NudKeySize).BeginInit();
            TabPage2.SuspendLayout();
            TabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // TabControl
            // 
            TabControl.Controls.Add(TabPage1);
            TabControl.Controls.Add(TabPage2);
            TabControl.Controls.Add(TabPage3);
            TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            TabControl.Location = new System.Drawing.Point(0, 0);
            TabControl.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TabControl.Name = "TabControl";
            TabControl.SelectedIndex = 0;
            TabControl.Size = new System.Drawing.Size(1100, 988);
            TabControl.TabIndex = 0;
            // 
            // TabPage1
            // 
            TabPage1.Controls.Add(BtnGenerateCert);
            TabPage1.Controls.Add(GrpEVInfo);
            TabPage1.Controls.Add(ChkEVCertificate);
            TabPage1.Controls.Add(GrpAdvanced);
            TabPage1.Controls.Add(GrpOrganization);
            TabPage1.Controls.Add(NudValidityYears);
            TabPage1.Controls.Add(LblValidityYears);
            TabPage1.Controls.Add(NudKeySize);
            TabPage1.Controls.Add(LblKeySize);
            TabPage1.Controls.Add(TxtFriendlyName);
            TabPage1.Controls.Add(LblFriendlyName);
            TabPage1.Controls.Add(TxtSubjectName);
            TabPage1.Controls.Add(LblSubjectName);
            TabPage1.Location = new System.Drawing.Point(4, 33);
            TabPage1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TabPage1.Size = new System.Drawing.Size(1092, 951);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "生成证书";
            TabPage1.UseVisualStyleBackColor = true;
            // 
            // BtnGenerateCert
            // 
            BtnGenerateCert.Location = new System.Drawing.Point(440, 791);
            BtnGenerateCert.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            BtnGenerateCert.Name = "BtnGenerateCert";
            BtnGenerateCert.Size = new System.Drawing.Size(236, 49);
            BtnGenerateCert.TabIndex = 11;
            BtnGenerateCert.Text = "生成自签名证书";
            BtnGenerateCert.UseVisualStyleBackColor = true;
            BtnGenerateCert.Click += BtnGenerateCert_Click;
            // 
            // GrpEVInfo
            // 
            GrpEVInfo.Controls.Add(TxtRegistrationNumber);
            GrpEVInfo.Controls.Add(LblRegistrationNumber);
            GrpEVInfo.Controls.Add(TxtJurisdictionLocality);
            GrpEVInfo.Controls.Add(LblJurisdictionLocality);
            GrpEVInfo.Controls.Add(TxtJurisdictionState);
            GrpEVInfo.Controls.Add(LblJurisdictionState);
            GrpEVInfo.Controls.Add(TxtJurisdictionCountry);
            GrpEVInfo.Controls.Add(LblJurisdictionCountry);
            GrpEVInfo.Controls.Add(CmbBusinessCategory);
            GrpEVInfo.Controls.Add(LblBusinessCategory);
            GrpEVInfo.Location = new System.Drawing.Point(24, 628);
            GrpEVInfo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            GrpEVInfo.Name = "GrpEVInfo";
            GrpEVInfo.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            GrpEVInfo.Size = new System.Drawing.Size(1037, 141);
            GrpEVInfo.TabIndex = 10;
            GrpEVInfo.TabStop = false;
            GrpEVInfo.Text = "EV 证书信息 (仅 EV 证书需要)";
            GrpEVInfo.Visible = false;
            // 
            // TxtRegistrationNumber
            // 
            TxtRegistrationNumber.Location = new System.Drawing.Point(574, 32);
            TxtRegistrationNumber.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TxtRegistrationNumber.Name = "TxtRegistrationNumber";
            TxtRegistrationNumber.Size = new System.Drawing.Size(233, 30);
            TxtRegistrationNumber.TabIndex = 3;
            // 
            // LblRegistrationNumber
            // 
            LblRegistrationNumber.AutoSize = true;
            LblRegistrationNumber.Location = new System.Drawing.Point(448, 35);
            LblRegistrationNumber.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblRegistrationNumber.Name = "LblRegistrationNumber";
            LblRegistrationNumber.Size = new System.Drawing.Size(100, 24);
            LblRegistrationNumber.TabIndex = 2;
            LblRegistrationNumber.Text = "公司注册号";
            // 
            // TxtJurisdictionLocality
            // 
            TxtJurisdictionLocality.Location = new System.Drawing.Point(662, 75);
            TxtJurisdictionLocality.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TxtJurisdictionLocality.Name = "TxtJurisdictionLocality";
            TxtJurisdictionLocality.Size = new System.Drawing.Size(145, 30);
            TxtJurisdictionLocality.TabIndex = 9;
            // 
            // LblJurisdictionLocality
            // 
            LblJurisdictionLocality.AutoSize = true;
            LblJurisdictionLocality.Location = new System.Drawing.Point(578, 78);
            LblJurisdictionLocality.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblJurisdictionLocality.Name = "LblJurisdictionLocality";
            LblJurisdictionLocality.Size = new System.Drawing.Size(82, 24);
            LblJurisdictionLocality.TabIndex = 8;
            LblJurisdictionLocality.Text = "注册城市";
            // 
            // TxtJurisdictionState
            // 
            TxtJurisdictionState.Location = new System.Drawing.Point(378, 75);
            TxtJurisdictionState.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TxtJurisdictionState.Name = "TxtJurisdictionState";
            TxtJurisdictionState.Size = new System.Drawing.Size(186, 30);
            TxtJurisdictionState.TabIndex = 7;
            // 
            // LblJurisdictionState
            // 
            LblJurisdictionState.AutoSize = true;
            LblJurisdictionState.Location = new System.Drawing.Point(278, 78);
            LblJurisdictionState.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblJurisdictionState.Name = "LblJurisdictionState";
            LblJurisdictionState.Size = new System.Drawing.Size(82, 24);
            LblJurisdictionState.TabIndex = 6;
            LblJurisdictionState.Text = "注册省份";
            // 
            // TxtJurisdictionCountry
            // 
            TxtJurisdictionCountry.Location = new System.Drawing.Point(157, 75);
            TxtJurisdictionCountry.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TxtJurisdictionCountry.MaxLength = 2;
            TxtJurisdictionCountry.Name = "TxtJurisdictionCountry";
            TxtJurisdictionCountry.Size = new System.Drawing.Size(108, 30);
            TxtJurisdictionCountry.TabIndex = 5;
            // 
            // LblJurisdictionCountry
            // 
            LblJurisdictionCountry.AutoSize = true;
            LblJurisdictionCountry.Location = new System.Drawing.Point(24, 78);
            LblJurisdictionCountry.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblJurisdictionCountry.Name = "LblJurisdictionCountry";
            LblJurisdictionCountry.Size = new System.Drawing.Size(82, 24);
            LblJurisdictionCountry.TabIndex = 4;
            LblJurisdictionCountry.Text = "注册国家";
            // 
            // CmbBusinessCategory
            // 
            CmbBusinessCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            CmbBusinessCategory.FormattingEnabled = true;
            CmbBusinessCategory.Items.AddRange(new object[] { "Private Organization - 私营企业", "Government - 政府机构", "Government Entity - 政府实体", "Business Entity - 商业实体", "Educational Institution - 教育机构", "Non-Profit Organization - 非营利组织", "Public Organization - 公共组织", "Association - 协会", "Limited Liability Company - 有限责任公司", "Corporation - 股份公司" });
            CmbBusinessCategory.Location = new System.Drawing.Point(157, 32);
            CmbBusinessCategory.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            CmbBusinessCategory.Name = "CmbBusinessCategory";
            CmbBusinessCategory.Size = new System.Drawing.Size(265, 32);
            CmbBusinessCategory.TabIndex = 1;
            // 
            // LblBusinessCategory
            // 
            LblBusinessCategory.AutoSize = true;
            LblBusinessCategory.Location = new System.Drawing.Point(24, 35);
            LblBusinessCategory.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblBusinessCategory.Name = "LblBusinessCategory";
            LblBusinessCategory.Size = new System.Drawing.Size(82, 24);
            LblBusinessCategory.TabIndex = 0;
            LblBusinessCategory.Text = "商业类别";
            // 
            // ChkEVCertificate
            // 
            ChkEVCertificate.AutoSize = true;
            ChkEVCertificate.Location = new System.Drawing.Point(24, 593);
            ChkEVCertificate.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            ChkEVCertificate.Name = "ChkEVCertificate";
            ChkEVCertificate.Size = new System.Drawing.Size(212, 28);
            ChkEVCertificate.TabIndex = 9;
            ChkEVCertificate.Text = "生成 EV 代码签名证书";
            ChkEVCertificate.UseVisualStyleBackColor = true;
            ChkEVCertificate.CheckedChanged += ChkEVCertificate_CheckedChanged;
            // 
            // GrpAdvanced
            // 
            GrpAdvanced.Controls.Add(ChkKeyUsageKeyEncipherment);
            GrpAdvanced.Controls.Add(ChkKeyUsageDigitalSignature);
            GrpAdvanced.Controls.Add(LblKeyUsage);
            GrpAdvanced.Controls.Add(TxtSubjectAlternativeNames);
            GrpAdvanced.Controls.Add(LblSubjectAlternativeNames);
            GrpAdvanced.Location = new System.Drawing.Point(24, 402);
            GrpAdvanced.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            GrpAdvanced.Name = "GrpAdvanced";
            GrpAdvanced.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            GrpAdvanced.Size = new System.Drawing.Size(1037, 184);
            GrpAdvanced.TabIndex = 9;
            GrpAdvanced.TabStop = false;
            GrpAdvanced.Text = "高级选项";
            // 
            // ChkKeyUsageKeyEncipherment
            // 
            ChkKeyUsageKeyEncipherment.AutoSize = true;
            ChkKeyUsageKeyEncipherment.Checked = true;
            ChkKeyUsageKeyEncipherment.CheckState = System.Windows.Forms.CheckState.Checked;
            ChkKeyUsageKeyEncipherment.Location = new System.Drawing.Point(361, 127);
            ChkKeyUsageKeyEncipherment.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            ChkKeyUsageKeyEncipherment.Name = "ChkKeyUsageKeyEncipherment";
            ChkKeyUsageKeyEncipherment.Size = new System.Drawing.Size(108, 28);
            ChkKeyUsageKeyEncipherment.TabIndex = 4;
            ChkKeyUsageKeyEncipherment.Text = "密钥加密";
            ChkKeyUsageKeyEncipherment.UseVisualStyleBackColor = true;
            // 
            // ChkKeyUsageDigitalSignature
            // 
            ChkKeyUsageDigitalSignature.AutoSize = true;
            ChkKeyUsageDigitalSignature.Checked = true;
            ChkKeyUsageDigitalSignature.CheckState = System.Windows.Forms.CheckState.Checked;
            ChkKeyUsageDigitalSignature.Location = new System.Drawing.Point(157, 127);
            ChkKeyUsageDigitalSignature.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            ChkKeyUsageDigitalSignature.Name = "ChkKeyUsageDigitalSignature";
            ChkKeyUsageDigitalSignature.Size = new System.Drawing.Size(108, 28);
            ChkKeyUsageDigitalSignature.TabIndex = 3;
            ChkKeyUsageDigitalSignature.Text = "数字签名";
            ChkKeyUsageDigitalSignature.UseVisualStyleBackColor = true;
            // 
            // LblKeyUsage
            // 
            LblKeyUsage.AutoSize = true;
            LblKeyUsage.Location = new System.Drawing.Point(24, 127);
            LblKeyUsage.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblKeyUsage.Name = "LblKeyUsage";
            LblKeyUsage.Size = new System.Drawing.Size(82, 24);
            LblKeyUsage.TabIndex = 2;
            LblKeyUsage.Text = "密钥用法";
            // 
            // TxtSubjectAlternativeNames
            // 
            TxtSubjectAlternativeNames.Location = new System.Drawing.Point(24, 64);
            TxtSubjectAlternativeNames.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TxtSubjectAlternativeNames.Multiline = true;
            TxtSubjectAlternativeNames.Name = "TxtSubjectAlternativeNames";
            TxtSubjectAlternativeNames.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            TxtSubjectAlternativeNames.Size = new System.Drawing.Size(988, 48);
            TxtSubjectAlternativeNames.TabIndex = 1;
            // 
            // LblSubjectAlternativeNames
            // 
            LblSubjectAlternativeNames.AutoSize = true;
            LblSubjectAlternativeNames.Location = new System.Drawing.Point(24, 35);
            LblSubjectAlternativeNames.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblSubjectAlternativeNames.Name = "LblSubjectAlternativeNames";
            LblSubjectAlternativeNames.Size = new System.Drawing.Size(168, 24);
            LblSubjectAlternativeNames.TabIndex = 0;
            LblSubjectAlternativeNames.Text = "主题备用名称(SAN)";
            // 
            // GrpOrganization
            // 
            GrpOrganization.Controls.Add(TxtEmail);
            GrpOrganization.Controls.Add(LblEmail);
            GrpOrganization.Controls.Add(TxtLocality);
            GrpOrganization.Controls.Add(LblLocality);
            GrpOrganization.Controls.Add(TxtState);
            GrpOrganization.Controls.Add(LblState);
            GrpOrganization.Controls.Add(CmbCountry);
            GrpOrganization.Controls.Add(LblCountry);
            GrpOrganization.Controls.Add(TxtOrganizationalUnit);
            GrpOrganization.Controls.Add(LblOrganizationalUnit);
            GrpOrganization.Controls.Add(TxtOrganization);
            GrpOrganization.Controls.Add(LblOrganization);
            GrpOrganization.Location = new System.Drawing.Point(24, 205);
            GrpOrganization.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            GrpOrganization.Name = "GrpOrganization";
            GrpOrganization.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            GrpOrganization.Size = new System.Drawing.Size(1037, 184);
            GrpOrganization.TabIndex = 8;
            GrpOrganization.TabStop = false;
            GrpOrganization.Text = "组织信息 (可选)";
            // 
            // TxtEmail
            // 
            TxtEmail.Location = new System.Drawing.Point(644, 117);
            TxtEmail.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TxtEmail.Name = "TxtEmail";
            TxtEmail.Size = new System.Drawing.Size(343, 30);
            TxtEmail.TabIndex = 11;
            // 
            // LblEmail
            // 
            LblEmail.AutoSize = true;
            LblEmail.Location = new System.Drawing.Point(519, 120);
            LblEmail.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblEmail.Name = "LblEmail";
            LblEmail.Size = new System.Drawing.Size(46, 24);
            LblEmail.TabIndex = 10;
            LblEmail.Text = "邮箱";
            // 
            // TxtLocality
            // 
            TxtLocality.Location = new System.Drawing.Point(157, 117);
            TxtLocality.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TxtLocality.Name = "TxtLocality";
            TxtLocality.Size = new System.Drawing.Size(343, 30);
            TxtLocality.TabIndex = 9;
            // 
            // LblLocality
            // 
            LblLocality.AutoSize = true;
            LblLocality.Location = new System.Drawing.Point(24, 120);
            LblLocality.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblLocality.Name = "LblLocality";
            LblLocality.Size = new System.Drawing.Size(67, 24);
            LblLocality.TabIndex = 8;
            LblLocality.Text = "城市(L)";
            // 
            // TxtState
            // 
            TxtState.Location = new System.Drawing.Point(644, 75);
            TxtState.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TxtState.Name = "TxtState";
            TxtState.Size = new System.Drawing.Size(343, 30);
            TxtState.TabIndex = 7;
            // 
            // LblState
            // 
            LblState.AutoSize = true;
            LblState.Location = new System.Drawing.Point(519, 78);
            LblState.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblState.Name = "LblState";
            LblState.Size = new System.Drawing.Size(78, 24);
            LblState.TabIndex = 6;
            LblState.Text = "省份(ST)";
            // 
            // CmbCountry
            // 
            CmbCountry.FormattingEnabled = true;
            CmbCountry.Items.AddRange(new object[] { "CN - 中国", "US - 美国", "UK - 英国", "DE - 德国", "FR - 法国", "JP - 日本", "KR - 韩国", "CA - 加拿大", "AU - 澳大利亚", "IT - 意大利", "ES - 西班牙", "NL - 荷兰", "BE - 比利时", "CH - 瑞士", "AT - 奥地利", "SE - 瑞典", "NO - 挪威", "DK - 丹麦", "FI - 芬兰", "PL - 波兰", "CZ - 捷克", "HU - 匈牙利", "RO - 罗马尼亚", "BG - 保加利亚", "HR - 克罗地亚", "SI - 斯洛文尼亚", "SK - 斯洛伐克", "EE - 爱沙尼亚", "LV - 拉脱维亚", "LT - 立陶宛", "MT - 马耳他", "CY - 塞浦路斯", "LU - 卢森堡", "IS - 冰岛", "LI - 列支敦士登", "MC - 摩纳哥", "SM - 圣马力诺", "VA - 梵蒂冈", "AD - 安道尔", "FO - 法罗群岛", "GL - 格陵兰" });
            CmbCountry.Location = new System.Drawing.Point(157, 75);
            CmbCountry.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            CmbCountry.Name = "CmbCountry";
            CmbCountry.Size = new System.Drawing.Size(343, 32);
            CmbCountry.TabIndex = 5;
            // 
            // LblCountry
            // 
            LblCountry.AutoSize = true;
            LblCountry.Location = new System.Drawing.Point(24, 78);
            LblCountry.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblCountry.Name = "LblCountry";
            LblCountry.Size = new System.Drawing.Size(70, 24);
            LblCountry.TabIndex = 4;
            LblCountry.Text = "国家(C)";
            // 
            // TxtOrganizationalUnit
            // 
            TxtOrganizationalUnit.Location = new System.Drawing.Point(644, 32);
            TxtOrganizationalUnit.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TxtOrganizationalUnit.Name = "TxtOrganizationalUnit";
            TxtOrganizationalUnit.Size = new System.Drawing.Size(343, 30);
            TxtOrganizationalUnit.TabIndex = 3;
            // 
            // LblOrganizationalUnit
            // 
            LblOrganizationalUnit.AutoSize = true;
            LblOrganizationalUnit.Location = new System.Drawing.Point(519, 35);
            LblOrganizationalUnit.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblOrganizationalUnit.Name = "LblOrganizationalUnit";
            LblOrganizationalUnit.Size = new System.Drawing.Size(86, 24);
            LblOrganizationalUnit.TabIndex = 2;
            LblOrganizationalUnit.Text = "部门(OU)";
            // 
            // TxtOrganization
            // 
            TxtOrganization.Location = new System.Drawing.Point(157, 32);
            TxtOrganization.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TxtOrganization.Name = "TxtOrganization";
            TxtOrganization.Size = new System.Drawing.Size(343, 30);
            TxtOrganization.TabIndex = 1;
            // 
            // LblOrganization
            // 
            LblOrganization.AutoSize = true;
            LblOrganization.Location = new System.Drawing.Point(24, 35);
            LblOrganization.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblOrganization.Name = "LblOrganization";
            LblOrganization.Size = new System.Drawing.Size(73, 24);
            LblOrganization.TabIndex = 0;
            LblOrganization.Text = "组织(O)";
            // 
            // NudValidityYears
            // 
            NudValidityYears.Location = new System.Drawing.Point(204, 152);
            NudValidityYears.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            NudValidityYears.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            NudValidityYears.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NudValidityYears.Name = "NudValidityYears";
            NudValidityYears.Size = new System.Drawing.Size(864, 30);
            NudValidityYears.TabIndex = 7;
            NudValidityYears.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // LblValidityYears
            // 
            LblValidityYears.AutoSize = true;
            LblValidityYears.Location = new System.Drawing.Point(31, 155);
            LblValidityYears.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblValidityYears.Name = "LblValidityYears";
            LblValidityYears.Size = new System.Drawing.Size(94, 24);
            LblValidityYears.TabIndex = 6;
            LblValidityYears.Text = "有效期(年)";
            // 
            // NudKeySize
            // 
            NudKeySize.Location = new System.Drawing.Point(204, 110);
            NudKeySize.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            NudKeySize.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            NudKeySize.Minimum = new decimal(new int[] { 1024, 0, 0, 0 });
            NudKeySize.Name = "NudKeySize";
            NudKeySize.Size = new System.Drawing.Size(864, 30);
            NudKeySize.TabIndex = 5;
            NudKeySize.Value = new decimal(new int[] { 2048, 0, 0, 0 });
            // 
            // LblKeySize
            // 
            LblKeySize.AutoSize = true;
            LblKeySize.Location = new System.Drawing.Point(31, 113);
            LblKeySize.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblKeySize.Name = "LblKeySize";
            LblKeySize.Size = new System.Drawing.Size(82, 24);
            LblKeySize.TabIndex = 4;
            LblKeySize.Text = "密钥长度";
            // 
            // TxtFriendlyName
            // 
            TxtFriendlyName.Location = new System.Drawing.Point(204, 68);
            TxtFriendlyName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TxtFriendlyName.Name = "TxtFriendlyName";
            TxtFriendlyName.Size = new System.Drawing.Size(862, 30);
            TxtFriendlyName.TabIndex = 3;
            // 
            // LblFriendlyName
            // 
            LblFriendlyName.AutoSize = true;
            LblFriendlyName.Location = new System.Drawing.Point(31, 71);
            LblFriendlyName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblFriendlyName.Name = "LblFriendlyName";
            LblFriendlyName.Size = new System.Drawing.Size(82, 24);
            LblFriendlyName.TabIndex = 2;
            LblFriendlyName.Text = "友好名称";
            // 
            // TxtSubjectName
            // 
            TxtSubjectName.Location = new System.Drawing.Point(204, 25);
            TxtSubjectName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TxtSubjectName.Name = "TxtSubjectName";
            TxtSubjectName.Size = new System.Drawing.Size(862, 30);
            TxtSubjectName.TabIndex = 1;
            // 
            // LblSubjectName
            // 
            LblSubjectName.AutoSize = true;
            LblSubjectName.Location = new System.Drawing.Point(31, 28);
            LblSubjectName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblSubjectName.Name = "LblSubjectName";
            LblSubjectName.Size = new System.Drawing.Size(100, 24);
            LblSubjectName.TabIndex = 0;
            LblSubjectName.Text = "发布者名称";
            // 
            // TabPage2
            // 
            TabPage2.Controls.Add(BtnExportCer);
            TabPage2.Controls.Add(BtnExportPfx);
            TabPage2.Controls.Add(BtnLoadCert);
            TabPage2.Controls.Add(BtnInstallCert);
            TabPage2.Controls.Add(BtnUninstallCert);
            TabPage2.Controls.Add(BtnViewCertInfo);
            TabPage2.Controls.Add(BtnElevate);
            TabPage2.Controls.Add(LblUserStatus);
            TabPage2.Controls.Add(TxtLoadPassword);
            TabPage2.Controls.Add(LblLoadPassword);
            TabPage2.Controls.Add(TxtPfxPassword);
            TabPage2.Controls.Add(LblPfxPassword);
            TabPage2.Controls.Add(LblCertStatus);
            TabPage2.Controls.Add(BtnExportPvkSpc);
            TabPage2.Location = new System.Drawing.Point(4, 33);
            TabPage2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TabPage2.Size = new System.Drawing.Size(1092, 951);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "证书管理";
            TabPage2.UseVisualStyleBackColor = true;
            // 
            // BtnExportCer
            // 
            BtnExportCer.Enabled = false;
            BtnExportCer.Location = new System.Drawing.Point(542, 154);
            BtnExportCer.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            BtnExportCer.Name = "BtnExportCer";
            BtnExportCer.Size = new System.Drawing.Size(157, 49);
            BtnExportCer.TabIndex = 5;
            BtnExportCer.Text = "导出CER";
            BtnExportCer.UseVisualStyleBackColor = true;
            BtnExportCer.Click += BtnExportCer_Click;
            // 
            // BtnExportPfx
            // 
            BtnExportPfx.Enabled = false;
            BtnExportPfx.Location = new System.Drawing.Point(316, 154);
            BtnExportPfx.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            BtnExportPfx.Name = "BtnExportPfx";
            BtnExportPfx.Size = new System.Drawing.Size(189, 49);
            BtnExportPfx.TabIndex = 4;
            BtnExportPfx.Text = "导出PFX";
            BtnExportPfx.UseVisualStyleBackColor = true;
            BtnExportPfx.Click += BtnExportPfx_Click;
            // 
            // BtnLoadCert
            // 
            BtnLoadCert.Location = new System.Drawing.Point(441, 284);
            BtnLoadCert.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            BtnLoadCert.Name = "BtnLoadCert";
            BtnLoadCert.Size = new System.Drawing.Size(236, 49);
            BtnLoadCert.TabIndex = 8;
            BtnLoadCert.Text = "加载PFX证书";
            BtnLoadCert.UseVisualStyleBackColor = true;
            BtnLoadCert.Click += BtnLoadCert_Click;
            // 
            // BtnInstallCert
            // 
            BtnInstallCert.Enabled = false;
            BtnInstallCert.Location = new System.Drawing.Point(441, 353);
            BtnInstallCert.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            BtnInstallCert.Name = "BtnInstallCert";
            BtnInstallCert.Size = new System.Drawing.Size(236, 49);
            BtnInstallCert.TabIndex = 9;
            BtnInstallCert.Text = "安装到信任根证书";
            BtnInstallCert.UseVisualStyleBackColor = true;
            BtnInstallCert.Click += BtnInstallCert_Click;
            // 
            // BtnUninstallCert
            // 
            BtnUninstallCert.Enabled = false;
            BtnUninstallCert.Location = new System.Drawing.Point(441, 416);
            BtnUninstallCert.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            BtnUninstallCert.Name = "BtnUninstallCert";
            BtnUninstallCert.Size = new System.Drawing.Size(236, 49);
            BtnUninstallCert.TabIndex = 10;
            BtnUninstallCert.Text = "从信任根证书卸载";
            BtnUninstallCert.UseVisualStyleBackColor = true;
            BtnUninstallCert.Click += BtnUninstallCert_Click;
            // 
            // BtnViewCertInfo
            // 
            BtnViewCertInfo.Enabled = false;
            BtnViewCertInfo.Location = new System.Drawing.Point(441, 486);
            BtnViewCertInfo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            BtnViewCertInfo.Name = "BtnViewCertInfo";
            BtnViewCertInfo.Size = new System.Drawing.Size(236, 49);
            BtnViewCertInfo.TabIndex = 11;
            BtnViewCertInfo.Text = "查看证书信息";
            BtnViewCertInfo.UseVisualStyleBackColor = true;
            BtnViewCertInfo.Click += BtnViewCertInfo_Click;
            // 
            // BtnElevate
            // 
            BtnElevate.Location = new System.Drawing.Point(798, 353);
            BtnElevate.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            BtnElevate.Name = "BtnElevate";
            BtnElevate.Size = new System.Drawing.Size(236, 49);
            BtnElevate.TabIndex = 12;
            BtnElevate.Text = "以管理员身份重启";
            BtnElevate.UseVisualStyleBackColor = true;
            BtnElevate.Click += BtnElevate_Click;
            // 
            // LblUserStatus
            // 
            LblUserStatus.AutoSize = true;
            LblUserStatus.ForeColor = System.Drawing.Color.Blue;
            LblUserStatus.Location = new System.Drawing.Point(833, 309);
            LblUserStatus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblUserStatus.Name = "LblUserStatus";
            LblUserStatus.Size = new System.Drawing.Size(163, 24);
            LblUserStatus.TabIndex = 13;
            LblUserStatus.Text = "当前身份: 普通用户";
            // 
            // TxtLoadPassword
            // 
            TxtLoadPassword.Location = new System.Drawing.Point(316, 223);
            TxtLoadPassword.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TxtLoadPassword.Name = "TxtLoadPassword";
            TxtLoadPassword.PasswordChar = '*';
            TxtLoadPassword.Size = new System.Drawing.Size(626, 30);
            TxtLoadPassword.TabIndex = 7;
            // 
            // LblLoadPassword
            // 
            LblLoadPassword.AutoSize = true;
            LblLoadPassword.Location = new System.Drawing.Point(111, 226);
            LblLoadPassword.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblLoadPassword.Name = "LblLoadPassword";
            LblLoadPassword.Size = new System.Drawing.Size(127, 24);
            LblLoadPassword.TabIndex = 6;
            LblLoadPassword.Text = "加载密码(PFX)";
            // 
            // TxtPfxPassword
            // 
            TxtPfxPassword.Location = new System.Drawing.Point(316, 96);
            TxtPfxPassword.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TxtPfxPassword.Name = "TxtPfxPassword";
            TxtPfxPassword.PasswordChar = '*';
            TxtPfxPassword.Size = new System.Drawing.Size(626, 30);
            TxtPfxPassword.TabIndex = 3;
            // 
            // LblPfxPassword
            // 
            LblPfxPassword.AutoSize = true;
            LblPfxPassword.Location = new System.Drawing.Point(111, 99);
            LblPfxPassword.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblPfxPassword.Name = "LblPfxPassword";
            LblPfxPassword.Size = new System.Drawing.Size(127, 24);
            LblPfxPassword.TabIndex = 2;
            LblPfxPassword.Text = "导出密码(PFX)";
            // 
            // LblCertStatus
            // 
            LblCertStatus.AutoSize = true;
            LblCertStatus.Location = new System.Drawing.Point(111, 42);
            LblCertStatus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblCertStatus.Name = "LblCertStatus";
            LblCertStatus.Size = new System.Drawing.Size(109, 24);
            LblCertStatus.TabIndex = 1;
            LblCertStatus.Text = "证书状态: 无";
            // 
            // BtnExportPvkSpc
            // 
            BtnExportPvkSpc.Enabled = false;
            BtnExportPvkSpc.Location = new System.Drawing.Point(753, 154);
            BtnExportPvkSpc.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            BtnExportPvkSpc.Name = "BtnExportPvkSpc";
            BtnExportPvkSpc.Size = new System.Drawing.Size(189, 49);
            BtnExportPvkSpc.TabIndex = 12;
            BtnExportPvkSpc.Text = "导出PVK+SPC";
            BtnExportPvkSpc.UseVisualStyleBackColor = true;
            BtnExportPvkSpc.Click += BtnExportPvkSpc_Click;
            // 
            // TabPage3
            // 
            TabPage3.Controls.Add(BtnVerify);
            TabPage3.Controls.Add(BtnSignFile);
            TabPage3.Controls.Add(BtnRemoveSign);
            TabPage3.Controls.Add(ChkDriverSigning);
            TabPage3.Controls.Add(ChkDualSign);
            TabPage3.Controls.Add(CmbHashAlgorithm);
            TabPage3.Controls.Add(CmbTimestampServer);
            TabPage3.Controls.Add(BtnTestTimestamp);
            TabPage3.Controls.Add(LblHashAlgorithm);
            TabPage3.Controls.Add(LblTimestampServer);
            TabPage3.Controls.Add(LblSignType);
            TabPage3.Location = new System.Drawing.Point(4, 33);
            TabPage3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            TabPage3.Size = new System.Drawing.Size(1092, 951);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "代码签名";
            TabPage3.UseVisualStyleBackColor = true;
            // 
            // BtnVerify
            // 
            BtnVerify.Location = new System.Drawing.Point(440, 282);
            BtnVerify.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            BtnVerify.Name = "BtnVerify";
            BtnVerify.Size = new System.Drawing.Size(236, 49);
            BtnVerify.TabIndex = 1;
            BtnVerify.Text = "验证签名";
            BtnVerify.UseVisualStyleBackColor = true;
            BtnVerify.Click += BtnVerify_Click;
            // 
            // BtnSignFile
            // 
            BtnSignFile.Enabled = false;
            BtnSignFile.Location = new System.Drawing.Point(440, 212);
            BtnSignFile.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            BtnSignFile.Name = "BtnSignFile";
            BtnSignFile.Size = new System.Drawing.Size(236, 49);
            BtnSignFile.TabIndex = 0;
            BtnSignFile.Text = "签名文件";
            BtnSignFile.UseVisualStyleBackColor = true;
            BtnSignFile.Click += BtnSignFile_Click;
            // 
            // BtnRemoveSign
            // 
            BtnRemoveSign.Location = new System.Drawing.Point(440, 353);
            BtnRemoveSign.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            BtnRemoveSign.Name = "BtnRemoveSign";
            BtnRemoveSign.Size = new System.Drawing.Size(236, 49);
            BtnRemoveSign.TabIndex = 2;
            BtnRemoveSign.Text = "删除签名";
            BtnRemoveSign.UseVisualStyleBackColor = true;
            BtnRemoveSign.Click += BtnRemoveSign_Click;
            // 
            // ChkDriverSigning
            // 
            ChkDriverSigning.AutoSize = true;
            ChkDriverSigning.Location = new System.Drawing.Point(236, 71);
            ChkDriverSigning.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            ChkDriverSigning.Name = "ChkDriverSigning";
            ChkDriverSigning.Size = new System.Drawing.Size(108, 28);
            ChkDriverSigning.TabIndex = 3;
            ChkDriverSigning.Text = "驱动签名";
            ChkDriverSigning.UseVisualStyleBackColor = true;
            // 
            // ChkDualSign
            // 
            ChkDualSign.AutoSize = true;
            ChkDualSign.Location = new System.Drawing.Point(440, 71);
            ChkDualSign.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            ChkDualSign.Name = "ChkDualSign";
            ChkDualSign.Size = new System.Drawing.Size(108, 28);
            ChkDualSign.TabIndex = 6;
            ChkDualSign.Text = "双重签名";
            ChkDualSign.UseVisualStyleBackColor = true;
            // 
            // CmbHashAlgorithm
            // 
            CmbHashAlgorithm.FormattingEnabled = true;
            CmbHashAlgorithm.Items.AddRange(new object[] { "SHA256", "SHA1" });
            CmbHashAlgorithm.Location = new System.Drawing.Point(236, 117);
            CmbHashAlgorithm.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            CmbHashAlgorithm.Name = "CmbHashAlgorithm";
            CmbHashAlgorithm.Size = new System.Drawing.Size(186, 32);
            CmbHashAlgorithm.TabIndex = 5;
            CmbHashAlgorithm.Text = "SHA256";
            // 
            // CmbTimestampServer
            // 
            CmbTimestampServer.FormattingEnabled = true;
            CmbTimestampServer.Items.AddRange(new object[] { "DigiCert|http://timestamp.digicert.com", "DigiCert SHA2|http://timestamp.digicert.com/?alg=sha256", "Sectigo|http://timestamp.sectigo.com", "Sectigo SHA256|http://timestamp.sectigo.com/sha256", "Comodo|http://timestamp.comodoca.com/authenticode", "Entrust|http://timestamp.entrust.net/TSS/RFC3161sha2TS", "GoDaddy|http://tsa.starfieldtech.com", "Microsoft|http://timestamp.microsoft.com/tsa/rfc3161", "Microsoft SHA256|http://timestamp.microsoft.com/tsa/rfc31612012", "SSL.com|http://tsa.ssl.com", "SSL.com SHA256|http://tsa.ssl.com/tsa", "SecureTrust|http://timestamp.securetrust.com", "GeoTrust|http://timestamp.geotrust.com/tsa", "Thawte|http://timestamp.thawte.com", "Symantec|http://timestamp.geotrust.com/tsa", "GlobalSign|http://timestamp.globalsign.com/tsa/r6advanced1", "GlobalSign SHA256|http://timestamp.globalsign.com/tsa/r6advanced1-sha256", "D-TRUST|http://timestamp.d-trust.net/TSS/HttpTspServer", "Actalis|http://tsa01.actalis.it/tsa", "Actalis SHA256|http://tsa02.actalis.it/tsa", "Buypass|http://tsa.buypass.no/tsa", "SwissSign|http://timestamp.swisssign.net", "QuoVadis|http://tsa.quovadisglobal.com/TSS/HttpTspServer", "Certum|http://time.certum.pl", "Unizeto|http://tsa.unizeto.pl", "CZ-NIC|http://tsa.cznic.cz", "LuxTrust|http://timestamp.luxtrust.lu", "PostFinance|http://tsp.posta.ch", "EuroPKI|http://tsa.europki.pl", "ANF|http://tsa.anf.es", "FNMT|http://tsa.fnmt.es", "DGC|http://tsa.dgc.nl", "Trust2Go|http://timestamp.trust2go.com", "Verizon|http://timestamp.verizonenterprise.com", "TrustAsia|http://timestamp.trustasia.com", "WoSign|http://timestamp.wosign.com", "CFCA|http://tsa.cfca.com.cn", "SCA|http://tsa.sca.com.cn", "BJCA|http://tsa.bjca.org.cn", "iTrusChina|http://tsa.itrus.com.cn", "TWCA|http://timestamp.twca.com.tw", "KISA|http://tsa.kisa.or.kr", "JPKI|http://timestamp.jpnca.or.jp", "Singapore CA|http://tsa.singaporeca.com.sg", "AuDA|http://tsa.auda.org.au", "VeriSign AU|http://timestamp.verisign.com.au/scripts/timstamp.dll", "ASCERTIA|http://tsa.ascertia.com", "eSign|http://tsa.esigndocs.com", "Signix|http://timestamp.signix.com", "Entrust SHA256|http://timestamp.entrust.net/TSS/RFC3161sha256TS", "RFC3161|http://freetsa.org/tsr", "OpenTSA|http://tsa.opentrust.ro", "CAcert|http://timestamp.cacert.org" });
            CmbTimestampServer.Location = new System.Drawing.Point(236, 162);
            CmbTimestampServer.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            CmbTimestampServer.Name = "CmbTimestampServer";
            CmbTimestampServer.Size = new System.Drawing.Size(626, 32);
            CmbTimestampServer.TabIndex = 8;
            CmbTimestampServer.Text = "DigiCert|http://timestamp.digicert.com";
            // 
            // BtnTestTimestamp
            // 
            BtnTestTimestamp.Location = new System.Drawing.Point(872, 160);
            BtnTestTimestamp.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            BtnTestTimestamp.Name = "BtnTestTimestamp";
            BtnTestTimestamp.Size = new System.Drawing.Size(94, 38);
            BtnTestTimestamp.TabIndex = 9;
            BtnTestTimestamp.Text = "测速";
            BtnTestTimestamp.UseVisualStyleBackColor = true;
            BtnTestTimestamp.Click += BtnTestTimestamp_Click;
            // 
            // LblHashAlgorithm
            // 
            LblHashAlgorithm.AutoSize = true;
            LblHashAlgorithm.Location = new System.Drawing.Point(79, 120);
            LblHashAlgorithm.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblHashAlgorithm.Name = "LblHashAlgorithm";
            LblHashAlgorithm.Size = new System.Drawing.Size(82, 24);
            LblHashAlgorithm.TabIndex = 4;
            LblHashAlgorithm.Text = "摘要算法";
            // 
            // LblTimestampServer
            // 
            LblTimestampServer.AutoSize = true;
            LblTimestampServer.Location = new System.Drawing.Point(79, 167);
            LblTimestampServer.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblTimestampServer.Name = "LblTimestampServer";
            LblTimestampServer.Size = new System.Drawing.Size(100, 24);
            LblTimestampServer.TabIndex = 7;
            LblTimestampServer.Text = "时间戳服务";
            // 
            // LblSignType
            // 
            LblSignType.AutoSize = true;
            LblSignType.Location = new System.Drawing.Point(79, 71);
            LblSignType.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            LblSignType.Name = "LblSignType";
            LblSignType.Size = new System.Drawing.Size(82, 24);
            LblSignType.TabIndex = 2;
            LblSignType.Text = "签名类型";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1100, 988);
            Controls.Add(TabControl);
            Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            Name = "MainForm";
            Text = "代码证书数字签名工具";
            TabControl.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            TabPage1.PerformLayout();
            GrpEVInfo.ResumeLayout(false);
            GrpEVInfo.PerformLayout();
            GrpAdvanced.ResumeLayout(false);
            GrpAdvanced.PerformLayout();
            GrpOrganization.ResumeLayout(false);
            GrpOrganization.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NudValidityYears).EndInit();
            ((System.ComponentModel.ISupportInitialize)NudKeySize).EndInit();
            TabPage2.ResumeLayout(false);
            TabPage2.PerformLayout();
            TabPage3.ResumeLayout(false);
            TabPage3.PerformLayout();
            ResumeLayout(false);
        }
    }
}