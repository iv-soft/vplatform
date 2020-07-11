using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IVySoft.VPlatform.TemplateService.Runtime.IndexScript
{
    class FileSystem : IFileSystem
    {
        public void move(string old_name, string dest)
        {
            if (File.Exists(old_name))
            {
                File.Move(old_name, dest);
            }
            else
            {
                Directory.Move(old_name, dest);
            }
        }

        public void replace_in_folder(string folder_name, string mask, string original, string dest)
        {
            foreach(var file in Directory.GetFiles(folder_name, mask))
            {
                var text = File.ReadAllText(file);
                File.WriteAllText(file, text.Replace(original, dest));
            }
        }
    }
}
