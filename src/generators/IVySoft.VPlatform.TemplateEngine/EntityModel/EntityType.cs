using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateEngine.EntityModel
{
    public class EntityType : ModelType
    {
        public List<TypeProperty> Properties = new List<TypeProperty>();

        public Type CLSType { get; set; }

        public EntityType with_string_property(string name)
        {
            this.Properties.Add(new TypeProperty
            {
                Type = new ModelType
                {
                    Namespace = "System",
                    Name = "String"
                },
                Name = name
            });

            return this;
        }
    }
}
