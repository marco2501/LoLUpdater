Pre-Installation
================
Put files in right location

	League must be installed on “X:\Riot Games\League of Legends” (X = any drive letter)

	Extract the zip file to “X:\Riot Games\League of Legends”

	For Garena you need to put all files in the: GarenaLoL\GameData\Apps\LoL folder

	You should now have a folder within your league folder named  “LoLUpdater-master”

	Copy all files from the “LoLUpdater-master” into the “League of Legends” folder”

Installation
============

>>[Powershell Studio](http://www.sapien.com/software/powershell_studio) is required to run this program

Open the psf file in powershell studio and build it, you will get a lolupdater.export.ps1 file which is the finished file.

Sign the script by downloading Windows SDK

[Windows 8.1](http://msdn.microsoft.com/en-us/windows/desktop/bg162891.aspx)

[Windows 8](http://msdn.microsoft.com/en-us/windows/desktop/hh852363.aspx)

[Windows 7](http://www.microsoft.com/en-us/download/details.aspx?id=8279)

Find the tool "makecert.exe" and run it like this (from the command prompt as administrator):

	makecert -n "CN=PowerShell Local Certificate Root" -a sha1 -eku 1.3.6.1.5.5.7.3.3 -r -sv root.pvk root.cer -ss Root -sr localMachine

	makecert -pe -n "CN=PowerShell User" -ss MY -a sha1 -eku 1.3.6.1.5.5.7.3.3 -iv root.pvk -ic root.cer

	
Then in a powershell admin window run this

	Set-AuthenticodeSignature c:\file.ps1 @(Get-ChildItem cert:\CurrentUser\My -codesigning)[0] -Timestampserver http://timestamp.comodoca.com/authenticode

	Open a Powershell prompt as administrator and type "Set-ExecutionPolicy RemoteSigned" then use the "Set-Location" command to move into the script directory, then type .\lolupdater.ps1 to execute the script.

Checking to See if Installation Was Successful
===============================================

	Navigate to “X:\Riot Games\League of Legends”

	Type “tbb.dll” into the search bar¨

	Right click -> properties -> details (the one in game_client_sln)

	If the version of “tbb.dll” is 4.2 then the installation has been
	successful





