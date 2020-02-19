using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateService.Entity
{
    public interface IEntityManager
    {
        EntityType add_entity_type(string name, string ns);
        void create_collection(string name, EntityType itemType);
        List<T> get_collection<T>(string name);


    }
}
