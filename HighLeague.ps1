   if(Get-Process "League of Legends"){
   $handle = gwmi win32_process -f "name='League of Legends.exe'" | Select-Object -ExpandProperty "Handle"
   ([wmi]"win32_process.handle='$handle'").setPriority(128)
   }