function tester{
do {
$test = Get-Process "League of Legends"
}
until ($test -ne $null)
}

function tester2{


tester
 do {
 $handle = gwmi win32_process -f "name='League of Legends.exe'" | Select-Object -ExpandProperty "Handle"
 ([wmi]"win32_process.handle='$handle'").setPriority(128)
 Start-Sleep "10"}
 until ($handle -eq $null)
 }
 
 tester
 do {$handle = gwmi win32_process -f "name='League of Legends.exe'" | Select-Object -ExpandProperty "Handle"
 ([wmi]"win32_process.handle='$handle'").setPriority(128)
 Start-Sleep "10"}
 until ($handle -eq $null)
 
 do {
 tester2
 }
 while ({Get-Service "HighLeague"})
 