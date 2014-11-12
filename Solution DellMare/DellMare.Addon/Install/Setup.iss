[Setup]
  ;DefaultGroupName        = SAP Business One
  ;UninstallDisplayIcon    = {app}\Uninstall.exe
  AppPublisher            = Seidor
  AppPublisherURL         = http://www.seidor.com.br/
  DefaultDirName          = {code:GetDefaultAddOnDir}
  DisableDirPage          = yes
  AppendDefaultDirName    = no
  AppendDefaultGroupName  = no
  DisableProgramGroupPage = yes
  AppName                 = DellMare
  AppVerName              = DellMare
  OutputDir               = Setup\1.0.0.2

[CustomMessages]
  br.MyAppVerName         = DellMare - Vrs 1.0.0.2
  br.MyDescription        = DellMare
  br.MyAppName            = SAP Business One - DellMare


[Files]
  Source: "C:\Projetos\Solution DellMare\DellMare.Addon\bin\Debug\*";                       DestDir: "{app}";           Flags: ignoreversion
  Source: "C:\Program Files\SAP\SAP Business One\AddOnInstallAPI.dll";                                Flags: dontcopy;

[Registry]
  Root: HKCU; Subkey: "Software\Seidor";         Flags: uninsdeletekey
  Root: HKCU; Subkey: "Software\Seidor\DellMare"; Flags: uninsdeletekey
  Root: HKLM; Subkey: "Software\Seidor";         Flags: uninsdeletekey
  Root: HKLM; Subkey: "Software\Seidor\DellMare"; Flags: uninsdeletekey
  Root: HKLM; Subkey: "Software\Seidor\DellMare"; ValueType: string; ValueName: "InstallPath"; ValueData: "{app}"

[Languages]
  Name: br; MessagesFile: "compiler:Default.isl"

[Messages]
  br.BeveledLabel=Seidor

;[Icons]
  ;Name: "{group}\AddOns\Desinstalar"; Filename: "{uninstallexe}"

[Code]
  var
    AddOnDir:     String;
    CommandLine:  String;

  function EndInstall: integer;                       external 'EndInstall@files:AddOnInstallAPI.dll stdcall';
  function SetAddOnFolder(srcPath : string): integer; external 'SetAddOnFolder@files:AddOnInstallAPI.dll stdcall';
  function RestartNeeded :integer;                    external 'RestartNeeded@files:AddOnInstallAPI.dll stdcall delayload ';

  //Desinstalação do Add-On
  function Uninstall(): Boolean;
    var
      ResultCode  : Integer;
      InstallDir  : String;
    begin
      EndInstall();
      RegQueryStringValue(HKEY_LOCAL_MACHINE, 'Software\Seidor\DellMare','InstallPath', InstallDir)
      Exec(InstallDir + '\unins000.exe', '', '', SW_SHOW, ewWaitUntilTerminated, ResultCode)
      Result := False
    end;

  //Retorna o Diretório de Instalação do Business One
  function PreparePaths() : Boolean;
    var
      position : integer;
      aux : string;
    begin
      aux := paramstr(6);
      CommandLine:= Copy(aux,0, 6)

      if CommandLine = '/unins' then
        begin
          Result := Uninstall();
        end
      else
        if pos('|', paramstr(6)) <> 0 then
          begin
            position := Pos('|', aux)
            AddOnDir := Copy(aux,0, position - 1)
            Result := True;
          end
        else
          begin
            MsgBox('Este Setup deve ser executado pelo Business One.', mbInformation, MB_OK)
            Result := False;
          end;
    end;

  function GetDefaultAddOnDir(Param : string): string;
  begin
      result := AddOnDir;
  end;

  function InitializeSetup(): Boolean;
  begin
    result := PreparePaths();
  end;

  function NextButtonClick(CurPageID: Integer): Boolean;
  begin
    Result := True;
    case CurPageID of
      wpFinished :
        Begin
          //Exec(AddOnDir + '\ReportViewer.exe', '/q', '', SW_SHOW, ewnoWait, ErrorCode)
          //RestartNeeded();
          EndInstall();
        end;
      end
  end;

  procedure URLLabelOnClick(Sender: TObject);
  var
    ErrorCode: Integer;
  begin
    ShellExec('open', 'http://www.seidor.com.br', '', '', SW_SHOWNORMAL, ewNoWait, ErrorCode);
  end;

  procedure InitializeWizard();
  var
    URLLabel: TNewStaticText;
    //BitmapImage:  TBitmapImage;
  begin
    URLLabel := TNewStaticText.Create(WizardForm);
    URLLabel.Caption := 'http://www.seidor.com.br';
    URLLabel.Cursor := crHand;
    URLLabel.OnClick := @URLLabelOnClick;
    URLLabel.Parent := WizardForm;
    URLLabel.Left := 180;
    URLLabel.Top := 200;
    URLLabel.Color := clWhite;
    URLLabel.Font.Style := URLLabel.Font.Style + [fsUnderline];
    URLLabel.Font.Color := clBlue;
    URLLabel.Parent :=  WizardForm.WelcomePage;
    WizardForm.NextButton.Caption := '&Próximo';
  end;

  function UpdateReadyMemo(Space, NewLine, MemoUserInfoInfo, MemoDirInfo, MemoTypeInfo, MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
  var
    S: String;
  begin
    S := 'Após a instalação do Add-On ' + {AppName} + ' certifique-se que as configurações estão ' + NewLine +
    'corretas.' + NewLine + NewLine + {AppName} + 'Versão: ' + {AppVerName} +
    NewLine + 'Seidor';
    Result := S;
  end;
