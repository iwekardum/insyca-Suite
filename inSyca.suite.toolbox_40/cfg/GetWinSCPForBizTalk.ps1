#Parameters
Param([Parameter(Mandatory=$true)][string]$downloadNoGetTo)
$Continue = $true
$downloadNoGetToExists = Test-Path $downloadNoGetTo
$WinSCPexe = "WinSCP.5.7.7\content\WinSCP.exe"
$WinSCPDllDownload = "WinSCP.5.7.7\lib\WinSCPnet.dll"
if (-not $downloadNoGetToExists)
{
    New-Item -Path $downloadNoGetTo -ItemType "Directory"
}
if (-not $downloadNoGetToExists)
{
    $Continue = $false
    Write-Host "An attempt to use the " $downloadNoGetTo " directory for a download target failed"
}
if ($Continue)
{
    $bizTalkInstallFolder = (Get-Item Env:BTSINSTALLPATH).Value
    if (-not $bizTalkInstallFolder)
    {
        $bizTalkInstallFolder = (get-itemPropertyValue 'HKLM:\SOFTWARE\Microsoft\BizTalk Server\3.0' -Name 'InstallPath')
        if (-not $bizTalkInstallFolder)
        {
            $bizTalkInstallFolder = "C:\Program Files (x86)\Microsoft BizTalk Server 2016\\"
        }
    }
    $bizTalkInstallFolderExists = Test-Path $bizTalkInstallFolder
    if (-not $bizTalkInstallFolderExists)
    {
        $Continue = $false
        Write-Host "The BizTalk Installation was not found by inspecting the enviornment or registry."
    }
    $bizTalkProductCode = (get-itemPropertyValue 'HKLM:\SOFTWARE\Microsoft\BizTalk Server\3.0' -Name 'ProductCodeCurrent')
    if ($bizTalkProductCode -eq '{B084F3A7-3E8F-4E7B-B673-EED1715D28ED}')
    {
        # running BizTalk Server 2016 CU5 with new WinSCP Version
        "Detected BizTalk Server 2016 with CU5 and this download process will use WinSCP 5.13.1"
        $winSCPexe = "WinSCP.5.13.1\tools\WinSCP.exe"
        $winSCPdll = "WinSCP.5.13.1\lib\net\WinSCPnet.dll"
        $winSCPVersion = "5.13.1"
    }
    else
    {
        # running BizTalk Server 2016 before CU5 with old WinSCP Version
        "Detected BizTalk Server 2016 before CU5 and this download process will use WinSCP 5.7.7"
        $WinSCPexe = "WinSCP.5.7.7\content\WinSCP.exe"
        $WinSCPDllDownload = "WinSCP.5.7.7\lib\WinSCPnet.dll"
        $winSCPVersion = "5.7.7"
    }
}
if ($Continue)
{
    #Download NuGet
    $sourceNugetExe = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
    Write-Host "Downloading Nuget from " $sourceNugetExe
    $targetNugetExe = "$downloadNoGetTo\nuget.exe"
    Invoke-WebRequest $sourceNugetExe -OutFile $targetNugetExe
    $targetNugetExeExists = Test-Path $targetNugetExe
    if (-not $targetNugetExeExists)
    {
        $Continue = $false
        Write-Host "The download of the Nuget EXE from " $sourceNugetExe " did not succeed"
    }
}
if ($Continue)
{
    #Download the right version of WinSCP
    Write-Host "Downloading WinSCP version $winSCPVersion from NuGet " $sourceNugetExe
    $getWinSCP = "'$targetNugetExe' Install WinSCP -Version $winSCPVersion -OutputDirectory '$downloadNoGetTo'"
     
    Invoke-Expression "& $getWinSCP"
    $WinSCPDownload = "$downloadNoGetTo\$winSCPexe"
    $WinSCPDllDownload = "$downloadNoGetTo\$winSCPdll"
    $WinSCPExists = Test-Path $WinSCPDownload
    $WinSCPDLLExists = Test-Path $WinSCPDllDownload
    if (-not $WinSCPExists -or -not $WinSCPDLLExists)
    {
        $Continue = $false
        Write-Host "WinSCP $winSCPVersion was not properly downloaded"
    }
}
if ($Continue)
{
    #Copy WinSCP items to BizTalk Folder
    Write-Host "Copying WinSCP version $winSCPVersion to BizTalk Folder"
    Copy-Item $WinSCPDownload $bizTalkInstallFolder
    Copy-Item $WinSCPDllDownload $bizTalkInstallFolder
    $WinSCPBTSExists = Test-Path "$bizTalkInstallFolder\WinSCP.exe"
    $WinSCPDLLBTSExists = Test-Path "$biztalkInstallFolder\WinSCPnet.dll"
    if (-not $WinSCPBTSExists)
    {
        $Continue = $false
        Write-Host "The WinSCP.exe file version $winSCPVersion was not properly copied to the target folder " $bizTalkInstallFolder
    }
    if (-not $WinSCPDLLBTSExists)
    {
        $Continue = $false
        Write-Host "The WinSCPnet.dll file version $winSCPVersion was not properly copied to the target folder " $bizTalkInstallFolder
    }
}
 
if (-not $Continue)
{
    Write-Host "Something went wrong during installation and the installation is not working"
    Write-Host "Please inspect the errors above and resolve them"
}
else
{
    Write-Host "WinSCP has been properly installed for BizTalk's SFTP Adapter"
}