using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateEngine.Razor;
using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateService.Entity
{
    internal class EntityModelHolder : IEntityModelHolder
    {
        private Dictionary<string, EntityType> entityTypes = new Dictionary<string, EntityType>();
        public Dictionary<string, EntityType> EntityTypes
        {
            get
            {
                return this.entityTypes;
            }
        }

        public Type GetCLSType(BuildContext context, EntityType itemType)
        {
            if (null != itemType.CLSType)
            {
                return itemType.CLSType;
            }

            var input_file = System.IO.Path.Combine(
                context.GlobalContext.ModulesFolder,
                "core",
                "entity",
                "entity.vcs");

            if (!System.IO.File.Exists(input_file))
            {
                throw new Exception($"File {input_file} not found");
            }

            var templates = new RazorTemplates(
                new TemplateCodeGeneratorOptions
                {
                    RootPath = System.IO.Path.Combine(context.GlobalContext.ModulesFolder, "core", "entity"),
                    TempPath = context.GlobalContext.BuildFolder,
                    BaseType = typeof(TextTemplateWithModel<EntityType>).UserFriendlyName()
                },
                options =>
                {
                    options.CompilerOptions.References.Add(
                        MetadataReference.CreateFromFile(typeof(EntityType).Assembly.Location));
                    options.CompilerOptions.References.Add(
                        MetadataReference.CreateFromFile(typeof(List<object>).GetGenericTypeDefinition().Assembly.Location));
                });

            var template = templates.Load<TextTemplateWithModel<EntityType>>(input_file);
            template.Model = itemType;
            var asm = templates.Compile(template.Execute().Result,
                System.IO.Path.Combine(context.GlobalContext.BuildFolder, "entity." + itemType.Namespace + "." + itemType.Name + ".dll"));
            context.GlobalContext.References.Add(MetadataReference.CreateFromFile(asm.Location));
            itemType.CLSType = asm.GetType(itemType.Namespace + "." + itemType.Name);
            return itemType.CLSType;
        }

        public EntityType AddEntityType(BuildContext context, string name, string ns)
        {
            if (this.entityTypes.ContainsKey(ns + "." + name))
            {
                throw new Exception($"Entity type {ns}.{name} already exists");
            }

            var result = new EntityType
            {
                Name = name,
                Namespace = ns
            };

            this.entityTypes.Add(ns + "." + name, result);

            return result;
        }


    }
}