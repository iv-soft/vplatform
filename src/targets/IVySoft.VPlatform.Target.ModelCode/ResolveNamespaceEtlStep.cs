using System;
using IVySoft.VPlatform.Etl.Core;
using IVySoft.VPlatform.Utils;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public class ResolveNamespaceEtlStep : IEtlStep
    {
        public string[] Dependencies
        {
            get
            {
                return new string[]
                {
                    "IVySoft.VPlatform.Target.ModelCode.Xml.Serialization.LoadDependenciesEtlStep"
                };
            }
        }

        public void Process(IEtlContext context)
        {
            var db = (IDataModel)context.DataModel;
            foreach (var module in db.Modules)
            {
                if (string.IsNullOrWhiteSpace(module.Namespace))
                {
                    module.Namespace = Translit.TranslateFolderName(module.Name);
                }
            }
        }
    }
}