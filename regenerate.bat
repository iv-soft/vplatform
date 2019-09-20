call src\targets\IVySoft.VPlatform.Target.ModelCode\generate.bat 
call src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\generate.bat

set this_folder=%~d0%~p0
dotnet run -p %this_folder%src\etl\IVySoft.VPlatform.Etl.Cmd\IVySoft.VPlatform.Etl.Cmd.csproj %this_folder%projects\VPlatform\module.xml 

xcopy /y %this_folder%projects\VPlatform\Generated\IVySoft.VPlatform.Target.ModelCode\*.* %this_folder%src\targets\IVySoft.VPlatform.Target.ModelCode\
xcopy /y %this_folder%projects\VPlatform\Generated\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\*.* %this_folder%src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\
