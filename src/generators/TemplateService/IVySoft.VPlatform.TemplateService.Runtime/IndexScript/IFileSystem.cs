using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateService
{
    public interface IFileSystem
    {
        void move(string old_name, string dest);
        void replace_in_folder(string folder_name, string mask, string original, string dest);
    }
}
