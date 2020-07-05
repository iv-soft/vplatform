mkdir build
cd build
dotnet run --project ..\..\src\generators\IVySoft.VPlatform.TemplateEngine.Cmd\IVySoft.VPlatform.TemplateEngine.Cmd.csproj build -s C:\Users\vadim\source\repos\vplatform\core_model -t C:\Users\vadim\source\repos\vplatform\generated -m C:\Users\vadim\source\repos\vplatform\modules
cd ..
rmdir /S /Q build