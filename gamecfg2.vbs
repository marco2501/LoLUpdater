Const ForReading = 1
Const ForWriting = 2

Set objFSO = CreateObject("Scripting.FileSystemObject")
Set objFile = objFSO.OpenTextFile(".\config\game.cfg", ForReading)

strText = objFile.ReadAll
objFile.Close
strNewText = Replace(strText, "EnableHUDAnimations=1", "EnableHUDAnimations=0")
strNewText = Replace(strText, "PerPixelPointLighting=1", "PerPixelPointLighting=0")
strNewText = Replace(strText, "EnableGrassSwaying=1", "EnableGrassSwaying=0")
strNewText = Replace(strText, "inking=1", "inking=0")
strNewText = Replace(strText, "AdvancedReflection=1", "AdvancedReflection=0")
strNewText = Replace(strText, "PerPixelPointLighting=1", "PerPixelPointLighting=0")

Set objFile = objFSO.OpenTextFile(".\config\game.cfg", ForWriting)
objFile.WriteLine strNewText

objFile.Close
