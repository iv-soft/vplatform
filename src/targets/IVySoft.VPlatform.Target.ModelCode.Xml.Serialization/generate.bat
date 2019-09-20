set this_folder=%~d0%~p0
dotnet run -p %this_folder%..\..\generators\IVySoft.VPlatform.TemplateEngine.Cmd\IVySoft.VPlatform.TemplateEngine.Cmd.csproj generate -s %this_folder%\DataModelsTemplate.cs.vtt -t %this_folder%DataModelsTemplate.cs -n DataModelsTemplate
dotnet run -p %this_folder%..\..\generators\IVySoft.VPlatform.TemplateEngine.Cmd\IVySoft.VPlatform.TemplateEngine.Cmd.csproj generate -s %this_folder%\EntityTypeTemplate.cs.vtt -t %this_folder%EntityTypeTemplate.cs -n EntityTypeTemplate
