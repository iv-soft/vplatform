using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateEngine.Razor;
using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateService.Entity
{
    internal class EntityManager : IEntityManager, IBuildContextDependent
    {
        private readonly IEntityModelHolder holder_;
        private BuildContext context_;

        public EntityManager(IEntityModelHolder holder)
        {
            this.holder_ = holder;
        }

        public List<T> get_collection<T>(string name)
        {
            object value;
            if (!this.context_.TryGetVariable(name, out value))
            {
                throw new Exception($"Collection {name} is not found");
            }

            return (List<T>)value;
        }

        public void create_collection(string name, EntityType itemType)
        {
            var t = this.GetCLSType(itemType);
            this.context_.SetVariable(name, Activator.CreateInstance(
                typeof(List<object>).GetGenericTypeDefinition().MakeGenericType(t)));
        }

        public Type GetCLSType(EntityType itemType)
        {
            return this.holder_.GetCLSType(this.context_, itemType);
        }

        public EntityType add_entity_type(string name, string ns)
        {
            return this.holder_.AddEntityType(this.context_, name, ns);
        }

        public void SetBuildContext(BuildContext context)
        {
            this.context_ = context;
        }
    }
}