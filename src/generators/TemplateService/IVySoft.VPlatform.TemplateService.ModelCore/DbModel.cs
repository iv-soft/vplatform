using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace IVySoft.VPlatform.TemplateService.ModelCore
{
    public class DbModel : DbContext
    {
        public DbModel(DbContextOptions options) : base(options)
        {
        }


        public DbSet<IVySoft.VPlatform.TemplateService.ModelCore.Module> Modules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IVySoft.VPlatform.TemplateService.ModelCore.ModuleType>()
                .HasDiscriminator<string>("class")
                .HasValue<IVySoft.VPlatform.TemplateService.ModelCore.PrimitiveType>("PrimitiveType")
                .HasValue<IVySoft.VPlatform.TemplateService.ModelCore.EntityType>("EntityType")
                .HasValue<IVySoft.VPlatform.TemplateService.ModelCore.ComplexType>("ComplexType")
	    ;
        }
    }
}


