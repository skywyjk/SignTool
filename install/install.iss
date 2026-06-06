[Setup]
AppId={{8E6B4E8A-7C3D-4F8E-B9A5-1C7E8D2B4F6A}
AppName=skyの自签证书工具
PrivilegesRequired=admin
AppVersion=1.0.0
AppPublisher=Sky
DefaultDirName={autopf}\skyの自签证书工具
DefaultGroupName=skyの自签证书工具
UninstallDisplayIcon={app}\SignTool.exe
OutputDir=output
OutputBaseFilename=skyの自签证书工具Setup
SetupIconFile=..\logo.ico
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
DisableProgramGroupPage=no
AllowNoIcons=yes
UsePreviousAppDir=no
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible

[Languages]
Name: "chinesesimplified"; MessagesFile: "languages\ChineseSimplified.isl"

[Tasks]
Name: "desktopicon"; Description: "创建桌面快捷方式"; GroupDescription: "附加任务:";


[Files]
Source: "..\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Registry]
Root: "HKLM"; Subkey: "Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers"; ValueType: string; ValueName: "{app}\SignTool.exe"; ValueData: "RUNASADMIN"; Flags: uninsdeletevalue
Root: "HKLM"; Subkey: "Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers"; ValueType: string; ValueName: "{app}\unins000.exe"; ValueData: "RUNASADMIN"; Flags: uninsdeletevalue


[Icons]
Name: "{group}\sky证书管理"; Filename: "{app}\SignTool.exe"
Name: "{commondesktop}\sky证书管理"; Filename: "{app}\SignTool.exe"; Tasks: desktopicon
Name: "{group}\卸载 sky证书管理"; Filename: "{uninstallexe}"

[UninstallDelete]
Type: filesandordirs; Name: "{app}"

[Run]
Filename: "{app}\SignTool.exe"; Description: "启动 skyの自签证书工具 "; Flags: nowait postinstall skipifsilent shellexec

[Code]
procedure InstallAndDeleteCertificate;
var
  CerFile: String;
  ResultCode: Integer;
  FindRec: TFindRec;
begin
  if FindFirst(ExpandConstant('{app}\*.cer'), FindRec) then
  begin
    try
      repeat
        CerFile := ExpandConstant('{app}\') + FindRec.Name;
        
        // First install the certificate to root store
        if ShellExec('', 'certutil', '-addstore root "' + CerFile + '"', '', SW_HIDE, ewWaitUntilTerminated, ResultCode) then
        begin
          // Then delete the cer file from app directory (no message popup)
          DeleteFile(CerFile);
        end;
      until not FindNext(FindRec);
    finally
      FindClose(FindRec);
    end;
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
  begin
    InstallAndDeleteCertificate;
  end;
end;

procedure InitializeWizard;
begin
  WizardForm.FormStyle := fsStayOnTop;
end;

procedure UninstallInitialize;
begin
  UninstallProgressForm.FormStyle := fsStayOnTop;
end;