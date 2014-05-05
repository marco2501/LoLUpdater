$content = Get-Content C:\Users\Ilja\Documents\GitHub\LoLUpdater\lolupdater.export.ps1
$content | Foreach {$_.TrimEnd()} | Set-Content C:\Users\Ilja\Documents\GitHub\LoLUpdater\lolupdater.ps1
(gc C:\Users\Ilja\Documents\GitHub\LoLUpdater\lolupdater.ps1) | ? {$_.trim() -ne "" } | set-content C:\Users\Ilja\Documents\GitHub\LoLUpdater\loltweaks.ps1
Set-Location C:\Users\Ilja\Documents\GitHub\LoLUpdater
C:\Users\Ilja\Documents\GitHub\LoLUpdater\Compiler\signtool.exe sign /a /tr http://timestamp.comodoca.com/rfc3161 lolupdater.ps1
Remove-Item .\compiler\LoLupdater.ps1
Move-Item LoLupdater.ps1 .\compiler
Set-Location Compiler
start-process "cmd.exe" "/c .\createdemo.bat" -wait
Start-Process .\signtool.exe "sign /a /tr http://timestamp.comodoca.com/rfc3161 lolupdater.ps1 lolupdater.exe"
