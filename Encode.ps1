$fileName = "C:\Users\Ilja\Documents\GitHub\Binaries\Adobe Air.dll"
$fileContent = get-content $fileName
$fileContentBytes = [System.Text.Encoding]::UTF8.GetBytes($fileContent)
$fileContentEncoded = [System.Convert]::ToBase64String($fileContentBytes)
$fileContentEncoded | set-content ($fileName + ".b64")





$Content = Get-Content -Path 'C:\Users\Ilja\Documents\GitHub\Binaries\msvcp120.dll' -Encoding byte
[System.Convert]::ToBase64String($Content) | Out-File "C:\msvcp120.txt"

$Content = Get-Content -Path 'C:\Users\Ilja\Documents\GitHub\Binaries\msvcr120.dll' -Encoding byte
[System.Convert]::ToBase64String($Content) | Out-File "C:\msvcr120.txt"

$Content = Get-Content -Path 'C:\Users\Ilja\Documents\GitHub\Binaries\tbb.dll' -Encoding byte
[System.Convert]::ToBase64String($Content) | Out-File "C:\tbb.txt"

$Content = [System.IO.File]::ReadAllBytes('C:\Users\Ilja\Documents\GitHub\Binaries\Adobe Air.dll')
[System.Convert]::ToBase64String($Content) | Out-File "C:\Adobe Air.txt"

$Content = [System.IO.File]::ReadAllBytes('C:\Users\Ilja\Documents\GitHub\Binaries\NPSWF32.dll')
[System.Convert]::ToBase64String($Content) | Out-File "C:\NPSWF32.txt"
