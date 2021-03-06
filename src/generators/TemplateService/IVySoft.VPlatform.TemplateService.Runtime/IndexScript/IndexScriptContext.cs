using System;

namespace IVySoft.VPlatform.TemplateService.Runtime.IndexScript
{
    public class IndexScriptContext
    {
        public BuildContext Context { get; set; }
        internal void ImportModule(string module_name)
        {
            this.Context.ImportModule(module_name);
        }
        internal void AddDirectory(string folder_name)
        {
            this.Context.AddDirectory(folder_name);
        }
        internal void CopyFileOrFolder(string file_name, string target_path)
        {
            var source_path = System.IO.Path.Combine(this.Context.SourceFolder, file_name);
            var dest_path = System.IO.Path.Combine(this.Context.GlobalContext.TargetFolder, target_path);
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dest_path));

            if (System.IO.Directory.Exists(source_path))
            {
                CopyDirectory(source_path, dest_path);
            }
            else
            {
                System.IO.File.Copy(source_path, dest_path, true);
            }

        }

        private void CopyDirectory(string source_path, string dest_path)
        {
            foreach(var file_name in System.IO.Directory.GetFiles(source_path))
            {
                var dest_file = System.IO.Path.Combine(dest_path, System.IO.Path.GetFileName(file_name));
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dest_file));
                System.IO.File.Copy(
                    file_name,
                    dest_file,
                    true);

            }
            foreach (var dir_name in System.IO.Directory.GetDirectories(source_path))
            {
                var dest_folder = System.IO.Path.Combine(dest_path, System.IO.Path.GetFileName(dir_name));
                System.IO.Directory.CreateDirectory(dest_folder);
                CopyDirectory(dir_name, dest_folder);
            }
        }

        //internal string ProcessRazorTemplate(string file_path)
        //{
        //    var templates = new Templates(
        //        new TemplateCodeGeneratorOptions
        //        {
        //            RootPath = System.IO.Path.Combine(this.Context.SourceFolder, this.CurrentFolder),
        //            TempPath = System.IO.Path.Combine(this.Context.BuildFolder, this.CurrentFolder),
        //            BaseType = typeof(TemplateInstanceBase).FullName
        //        },
        //        context =>
        //        {
        //            context.CompilerOptions.References.Add(
        //                Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(
        //                typeof(TemplateInstanceBase).Assembly.Location));
        //        });
        //    var instance = templates.Load<TemplateInstanceBase>(
        //        System.IO.Path.Combine(this.Context.SourceFolder, this.CurrentFolder, file_path));
        //    var result = instance.Execute().Result;
        //    if (string.IsNullOrWhiteSpace(instance.Layout))
        //    {
        //        return result;
        //    }

        //    return this.Context.ProcessRazorTemplate(instance.Layout, instance.Parameters);
        //}
    }
}