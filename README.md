# skyの自签证书工具

一个用于生成自签名代码签名证书和进行数字签名的Windows桌面应用程序。

## 功能特性

### 证书管理
- 生成自签名代码签名证书（支持普通证书和EV证书）
- 支持多种密钥长度（1024-4096位）
- 自定义证书有效期（1-50年）
- 支持组织信息配置（O、OU、C、ST、L、E-mail）
- 支持主题备用名称（SAN）
- 支持密钥用法配置

### 证书导出
- 导出为 PFX/P12 格式（带密码保护）
- 导出为 CER 格式（公钥证书）
- 导出为 PVK + SPC 格式

### 证书安装
- 安装证书到系统信任根证书颁发机构
- 从系统信任根证书颁发机构卸载证书
- 查看证书详细信息

### 代码签名
- 支持 EXE、DLL、MSI 等文件签名
- 支持驱动文件签名（SYS、CAT）
- 支持双重签名（SHA1 + SHA256）
- 支持多种摘要算法（SHA1、SHA256）
- 支持多个时间戳服务器
- 时间戳服务器测速功能
- 签名验证功能
- 删除签名功能

## 系统要求

- Windows 7 或更高版本（64位）
- .NET 8 Runtime（非自包含版本需要）

## 使用方法

### 生成证书

1. 在"生成证书"页面填写：
   - **发布者名称**：显示为签名程序的已验证发布者
   - **友好名称**：证书在系统中的显示名称
   - **密钥长度**：建议使用 2048 位或更高
   - **有效期**：证书有效年限
   - 可选：填写组织信息

2. 点击"生成自签名证书"按钮

3. 证书生成成功后，可以导出或安装证书

### 安装证书

1. 在"证书管理"页面加载证书或生成新证书
2. 点击"安装到信任根证书"按钮（需要管理员权限）
3. 确认后证书将安装到系统信任存储

### 签名文件

1. 在"代码签名"页面选择签名类型（普通签名或驱动签名）
2. 选择摘要算法（推荐 SHA256）
3. 选择时间戳服务器
4. 点击"签名文件"选择要签名的文件

## 项目结构

```
signtool/
├── MainForm.cs          # 主窗口逻辑
├── MainForm.Designer.cs # 主窗口设计
├── ConfigManager.cs     # 配置管理
├── CertificateManager.cs # 证书管理核心逻辑
├── SignTool.csproj      # 项目文件
├── logo.ico             # 应用图标
├── publish/             # 发布目录
├── install/             # 安装包相关
│   ├── SignTool.iss     # Inno Setup 脚本
│   └── output/          # 安装包输出目录
└── README.md            # 项目说明
```

## 构建说明

### 依赖
- .NET 8 SDK

### 构建命令

```bash
# 构建项目
dotnet build

# 发布为框架依赖模式
dotnet publish SignTool.csproj -c Release -r win-x64 --self-contained false -o publish

# 发布为自包含模式（单文件）
dotnet publish SignTool.csproj -c Release -r win-x64 --self-contained true -o publish /p:PublishSingleFile=true
```

### 生成安装包

1. 安装 [Inno Setup](https://jrsoftware.org/isdl.php)
2. 双击 `install/SignTool.iss` 编译安装包
3. 安装包将生成在 `install/output/` 目录

## 注意事项

1. **自签名证书仅用于测试目的**，不会被 Windows 自动信任，正式发布软件请购买 CA 签发的证书
2. 安装证书到信任根证书颁发机构需要管理员权限
3. 签名驱动文件需要安装 Windows SDK 并配置交叉证书
4. 建议使用时间戳服务确保签名在证书过期后仍然有效

## 许可证

本项目采用 [GNU Affero General Public License v3.0](LICENSE) 许可证。

## 免责声明

本工具仅供学习和测试使用，请勿用于非法用途。作者不对使用本工具产生的任何后果负责。