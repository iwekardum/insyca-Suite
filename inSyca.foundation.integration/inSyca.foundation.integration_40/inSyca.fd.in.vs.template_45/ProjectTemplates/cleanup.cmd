FOR /d /r . %%d IN (obj) DO @IF EXIST "%%d" rd /s /q "%%d"
FOR /d /r . %%d IN (bin) DO @IF EXIST "%%d" rd /s /q "%%d"
@ECHO Reset Visual Studio Experimental Instance
PAUSE
cd %LocalAppData%\Microsoft\VisualStudio\
FOR /d /r . %%d IN (12.0Exp) DO @IF EXIST "%%d" rd /s /q "%%d"
reg delete HKEY_CURRENT_USER\Software\Microsoft\VisualStudio\12.0Exp /f
reg delete HKEY_CURRENT_USER\Software\Microsoft\VisualStudio\12.0Exp_Config /f
"C:\Program Files (x86)\Microsoft Visual Studio 12.0\VSSDK\VisualStudioIntegration\Tools\Bin\CreateExpInstance.exe" /Reset /VSInstance=12.0 /RootSuffix=Exp
