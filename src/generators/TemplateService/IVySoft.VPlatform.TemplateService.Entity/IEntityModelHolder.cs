using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace IVySoft.VPlatform.TemplateService.Entity
{
    public interface IEntityModelHolder
    {
        Type GetCLSType(BuildContext context, EntityType itemType);

        EntityType AddEntityType(BuildContext context, string name, string ns);

        Assembly Compile<T>(BuildContext context, string input_file, string dllPath, string asmPath, T model);
        Assembly Compile<T>(BuildContext context, IEnumerable<string> input_files, string dllPath, string asmPath, T model);

        void AddDbModel(string fullName, IServiceProvider serviceProvider);

        IServiceProvider GetDbModel<T>();
    }
}
