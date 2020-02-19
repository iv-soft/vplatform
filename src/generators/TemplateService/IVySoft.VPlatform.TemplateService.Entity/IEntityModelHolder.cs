using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateService.Entity
{
    public interface IEntityModelHolder
    {
        Type GetCLSType(BuildContext context, EntityType itemType);

        EntityType AddEntityType(BuildContext context, string name, string ns);


    }
}
