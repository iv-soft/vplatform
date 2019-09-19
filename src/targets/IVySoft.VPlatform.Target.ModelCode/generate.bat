set root_folder=%~d0%~p0
dotnet run -p ..\..\generators\IVySoft.VPlatform.TemplateEngine.Cmd\IVySoft.VPlatform.TemplateEngine.Cmd.csproj generate -s %root_folder%\DataModelsTemplate.cs.vtt -t %root_folder%DataModelsTemplate.cs -n DataModelsTemplate
dotnet run -p ..\..\generators\IVySoft.VPlatform.TemplateEngine.Cmd\IVySoft.VPlatform.TemplateEngine.Cmd.csproj generate -s %root_folder%\EntityTypeTemplate.cs.vtt -t %root_folder%EntityTypeTemplate.cs -n EntityTypeTemplate
