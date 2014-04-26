LoLUpdater
======================
Updates LoLs DLL files for a faster Air Client and maybe more FPS ingame.
![alt text](http://i.imgur.com/fN5krb2.png)


Pre-Installation
================
Put files in right location

	League must be installed on “X:\Riot Games\League of Legends” (X = any drive letter)

	Extract the zip file to “X:\Riot Games\League of Legends”

	For Garena you need to put all files in the: GarenaLoL\GameData\Apps\LoL folder

	You should now have a folder within your league folder named  “LoLUpdater-master”

	Copy all files from the “LoLUpdater-master” into the “League of Legends” folder”

	Replace existing files if prompted too

Installation
============

	Open a Powershell prompt as admin and type "Set-ExecutionPolicy RemoteSigned"

	Then use the Set-Location command to enter the script directory and type .\powershell.ps1

Checking to See if Installation Was Successful
===============================================

	Navigate to “X:\Riot Games\League of Legends”

	Type “tbb.dll” into the search bar¨

	Right click -> properties -> details (the one in game_client_sln)

	If the version of “tbb.dll” is 4.2 then the installation has been
	successful

About Source
============

You can also open the script with Powershell Studio to change the GUI.


About Signing
=============

If you want to sign the script and executable you can do it by downloading Windows SDK

Windows 8.1SDK : http://msdn.microsoft.com/en-us/windows/desktop/bg162891.aspx

Windows 8SDK : http://msdn.microsoft.com/en-us/windows/desktop/hh852363.aspx

Windows 7SDK : http://www.microsoft.com/en-us/download/details.aspx?id=8279

Find the tool "makecert.exe" and run it like this:

	makecert -n "CN=PowerShell Local Certificate Root" -a sha1 -eku 1.3.6.1.5.5.7.3.3 -r -sv root.pvk root.cer -ss Root -sr localMachine

	makecert -pe -n "CN=PowerShell User" -ss MY -a sha1 -eku 1.3.6.1.5.5.7.3.3 -iv root.pvk -ic root.cer

	
Then in a powershell admin window run this

	Set-AuthenticodeSignature c:\file.ps1 @(Get-ChildItem cert:\CurrentUser\My -codesigning)[0] -Timestampserver http://timestamp.comodoca.com/authenticode

With this signing you will be able to run this script on computers with the executionpolicy set to remotesigned

If you want to be able to run the script on all machines without changing execution policy you need to buy a certificate from for example Comodo (cheapest): https://cheapsslsecurity.com/comodo/codesigningcertificate.html

Donation
========

Paypal: Ilja.korsun@gmail.com




