using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace IVySoft.VPlatform.ModelCode
{
	public class ModuleType
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string BaseType { get; set; }
		public string Discriminator { get; set; }
		public string ElementName { get; set; }
		[ForeignKey(nameof(Module))]
		public int ModuleId { get; set; }
		public virtual Module Module { get; set; }
	}
	public class Module
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Namespace { get; set; }
		public bool IsExternal { get; set; }
		[InverseProperty(nameof(Association.Module))]
		public virtual IList<Association> Associations { get; set; }
		[InverseProperty(nameof(ModuleType.Module))]
		public virtual IList<ModuleType> Types { get; set; }
		[InverseProperty(nameof(ModuleDependency.Module))]
		public virtual IList<ModuleDependency> Dependencies { get; set; }
		[InverseProperty(nameof(EntityTable.Module))]
		public virtual IList<EntityTable> Tables { get; set; }
	}
	public class ModuleDependency
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		[ForeignKey(nameof(Module))]
		public int ModuleId { get; set; }
		public virtual Module Module { get; set; }
	}
	public class EntityType : IVySoft.VPlatform.ModelCode.ModuleType
	{
		public bool Abstract { get; set; }
		[InverseProperty(nameof(Property.OwnerType))]
		public virtual IList<Property> Properties { get; set; }
	}
	public class ComplexType : IVySoft.VPlatform.ModelCode.ModuleType
	{
		public bool Abstract { get; set; }
	}
	public class PrimitiveType : IVySoft.VPlatform.ModelCode.ModuleType
	{
	}
	public class Property
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Multiplicity { get; set; }
		public string Default { get; set; }
		[ForeignKey(nameof(OwnerType))]
		public int OwnerTypeId { get; set; }
		public virtual EntityType OwnerType { get; set; }
	}
	public class Association
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		[ForeignKey(nameof(Left))]
		public int LeftId { get; set; }
		public virtual AssociationEnd Left { get; set; }
		[ForeignKey(nameof(Right))]
		public int RightId { get; set; }
		public virtual AssociationEnd Right { get; set; }
		[ForeignKey(nameof(Module))]
		public int ModuleId { get; set; }
		public virtual Module Module { get; set; }
	}
	public class AssociationEnd
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Type { get; set; }
		public string Property { get; set; }
		public string Multiplicity { get; set; }
	}
	public class EntityTable
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string EntityType { get; set; }
		[ForeignKey(nameof(Module))]
		public int ModuleId { get; set; }
		public virtual Module Module { get; set; }
	}
	public class DbModel : DbContext
	{
		public DbModel(DbContextOptions options) : base(options)
		{
		}
		public DbSet<IVySoft.VPlatform.ModelCode.Module> Modules { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ComplexType<AssociationEnd>();

			modelBuilder.Entity<ModuleType>()
				.HasDiscriminator<string>("class")
				.HasValue<IVySoft.VPlatform.ModelCode.EntityType>("EntityType")
				.HasValue<IVySoft.VPlatform.ModelCode.ComplexType>("ComplexType")
				.HasValue<IVySoft.VPlatform.ModelCode.PrimitiveType>("PrimitiveType")
			;
			base.OnModelCreating(modelBuilder);
		}

		public static IServiceProvider CreateServiceProvider()
        {
			var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
			var configuration = builder.Build();

			var services = new ServiceCollection();
			services.AddSingleton<Microsoft.Extensions.Configuration.IConfiguration>(configuration);

			services.AddEntityFrameworkInMemoryDatabase();
			services.AddEntityFrameworkProxies();
			services.AddDbContext<DbModel>(opt =>
			{
				opt.UseInMemoryDatabase("InMemoryDb");
				opt.UseLazyLoadingProxies();
			});

			return services.BuildServiceProvider();
		}
	}
}
namespace IVySoft.VPlatform.ModelCode.Xml.Serialization
{
	[XmlRoot(Namespace = "IVySoft.VPlatform.ModelCode")]
	public class ModuleType
	{
		[XmlElement()]
		public string Name { get; set; }
		[XmlElement()]
		public string BaseType { get; set; }
		[XmlElement()]
		public string Discriminator { get; set; }
		[XmlElement()]
		public string ElementName { get; set; }
		public virtual object ToModel()
		{
			var result = new IVySoft.VPlatform.ModelCode.ModuleType();
			this.InitModel(result);
			return result;
		}
		protected void InitModel(IVySoft.VPlatform.ModelCode.ModuleType result)
		{
			result.Name = this.Name;
			result.BaseType = this.BaseType;
			result.Discriminator = this.Discriminator;
			result.ElementName = this.ElementName;
		}
	}
	[XmlRoot(Namespace = "IVySoft.VPlatform.ModelCode")]
	public class Module
	{
		[XmlElement()]
		public string Name { get; set; }
		[XmlElement()]
		public string Namespace { get; set; }
		[XmlElement()]
		public bool IsExternal { get; set; }
		[XmlArray()]
		public Association[] Associations { get; set; }
		[XmlArray()]
		[XmlArrayItem(ElementName = "EntityType", Type = typeof(IVySoft.VPlatform.ModelCode.Xml.Serialization.EntityType))]
		[XmlArrayItem(ElementName = "ComplexType", Type = typeof(IVySoft.VPlatform.ModelCode.Xml.Serialization.ComplexType))]
		[XmlArrayItem(ElementName = "PrimitiveType", Type = typeof(IVySoft.VPlatform.ModelCode.Xml.Serialization.PrimitiveType))]
		public ModuleType[] Types { get; set; }
		[XmlArray()]
		public ModuleDependency[] Dependencies { get; set; }
		[XmlArray()]
		public EntityTable[] Tables { get; set; }
		public virtual object ToModel()
		{
			var result = new IVySoft.VPlatform.ModelCode.Module();
			this.InitModel(result);
			return result;
		}
		protected void InitModel(IVySoft.VPlatform.ModelCode.Module result)
		{
			result.Name = this.Name;
			result.Namespace = this.Namespace;
			result.IsExternal = this.IsExternal;
			result.Associations = new List<IVySoft.VPlatform.ModelCode.Association>((this.Associations == null) ? new IVySoft.VPlatform.ModelCode.Association[0] : this.Associations.Select(x => (IVySoft.VPlatform.ModelCode.Association)x.ToModel()));
			result.Types = new List<IVySoft.VPlatform.ModelCode.ModuleType>((this.Types == null) ? new IVySoft.VPlatform.ModelCode.ModuleType[0] : this.Types.Select(x => (IVySoft.VPlatform.ModelCode.ModuleType)x.ToModel()));
			result.Dependencies = new List<IVySoft.VPlatform.ModelCode.ModuleDependency>((this.Dependencies == null) ? new IVySoft.VPlatform.ModelCode.ModuleDependency[0] : this.Dependencies.Select(x => (IVySoft.VPlatform.ModelCode.ModuleDependency)x.ToModel()));
			result.Tables = new List<IVySoft.VPlatform.ModelCode.EntityTable>((this.Tables == null) ? new IVySoft.VPlatform.ModelCode.EntityTable[0] : this.Tables.Select(x => (IVySoft.VPlatform.ModelCode.EntityTable)x.ToModel()));
		}
	}
	[XmlRoot(Namespace = "IVySoft.VPlatform.ModelCode")]
	public class ModuleDependency
	{
		[XmlElement()]
		public string Name { get; set; }
		public virtual object ToModel()
		{
			var result = new IVySoft.VPlatform.ModelCode.ModuleDependency();
			this.InitModel(result);
			return result;
		}
		protected void InitModel(IVySoft.VPlatform.ModelCode.ModuleDependency result)
		{
			result.Name = this.Name;
		}
	}
	[XmlRoot(Namespace = "IVySoft.VPlatform.ModelCode")]
	public class EntityType : IVySoft.VPlatform.ModelCode.Xml.Serialization.ModuleType
	{
		[XmlElement()]
		public bool Abstract { get; set; }
		[XmlArray()]
		public Property[] Properties { get; set; }
		public override object ToModel()
		{
			var result = new IVySoft.VPlatform.ModelCode.EntityType();
			this.InitModel(result);
			return result;
		}
		protected void InitModel(IVySoft.VPlatform.ModelCode.EntityType result)
		{
			base.InitModel(result);
			result.Abstract = this.Abstract;
			result.Properties = new List<IVySoft.VPlatform.ModelCode.Property>((this.Properties == null) ? new IVySoft.VPlatform.ModelCode.Property[0] : this.Properties.Select(x => (IVySoft.VPlatform.ModelCode.Property)x.ToModel()));
		}
	}
	[XmlRoot(Namespace = "IVySoft.VPlatform.ModelCode")]
	public class ComplexType : IVySoft.VPlatform.ModelCode.Xml.Serialization.ModuleType
	{
		[XmlElement()]
		public bool Abstract { get; set; }
		public override object ToModel()
		{
			var result = new IVySoft.VPlatform.ModelCode.ComplexType();
			this.InitModel(result);
			return result;
		}
		protected void InitModel(IVySoft.VPlatform.ModelCode.ComplexType result)
		{
			base.InitModel(result);
			result.Abstract = this.Abstract;
		}
	}
	[XmlRoot(Namespace = "IVySoft.VPlatform.ModelCode")]
	public class PrimitiveType : IVySoft.VPlatform.ModelCode.Xml.Serialization.ModuleType
	{
		public override object ToModel()
		{
			var result = new IVySoft.VPlatform.ModelCode.PrimitiveType();
			this.InitModel(result);
			return result;
		}
		protected void InitModel(IVySoft.VPlatform.ModelCode.PrimitiveType result)
		{
			base.InitModel(result);
		}
	}
	[XmlRoot(Namespace = "IVySoft.VPlatform.ModelCode")]
	public class Property
	{
		[XmlElement()]
		public string Name { get; set; }
		[XmlElement()]
		public string Type { get; set; }
		[XmlElement()]
		public string Multiplicity { get; set; }
		[XmlElement()]
		public string Default { get; set; }
		public virtual object ToModel()
		{
			var result = new IVySoft.VPlatform.ModelCode.Property();
			this.InitModel(result);
			return result;
		}
		protected void InitModel(IVySoft.VPlatform.ModelCode.Property result)
		{
			result.Name = this.Name;
			result.Type = this.Type;
			result.Multiplicity = this.Multiplicity;
			result.Default = this.Default;
		}
	}
	[XmlRoot(Namespace = "IVySoft.VPlatform.ModelCode")]
	public class Association
	{
		[XmlElement()]
		public string Name { get; set; }
		[XmlElement()]
		public AssociationEnd Left { get; set; }
		[XmlElement()]
		public AssociationEnd Right { get; set; }
		public virtual object ToModel()
		{
			var result = new IVySoft.VPlatform.ModelCode.Association();
			this.InitModel(result);
			return result;
		}
		protected void InitModel(IVySoft.VPlatform.ModelCode.Association result)
		{
			result.Name = this.Name;
			result.Left = (IVySoft.VPlatform.ModelCode.AssociationEnd)this.Left?.ToModel();
			result.Right = (IVySoft.VPlatform.ModelCode.AssociationEnd)this.Right?.ToModel();
		}
	}
	[XmlRoot(Namespace = "IVySoft.VPlatform.ModelCode")]
	public class AssociationEnd
	{
		[XmlElement()]
		public string Type { get; set; }
		[XmlElement()]
		public string Property { get; set; }
		[XmlElement()]
		public string Multiplicity { get; set; }
		public virtual object ToModel()
		{
			var result = new IVySoft.VPlatform.ModelCode.AssociationEnd();
			this.InitModel(result);
			return result;
		}
		protected void InitModel(IVySoft.VPlatform.ModelCode.AssociationEnd result)
		{
			result.Type = this.Type;
			result.Property = this.Property;
			result.Multiplicity = this.Multiplicity;
		}
	}
	[XmlRoot(Namespace = "IVySoft.VPlatform.ModelCode")]
	public class EntityTable
	{
		[XmlElement()]
		public string Name { get; set; }
		[XmlElement()]
		public string EntityType { get; set; }
		public virtual object ToModel()
		{
			var result = new IVySoft.VPlatform.ModelCode.EntityTable();
			this.InitModel(result);
			return result;
		}
		protected void InitModel(IVySoft.VPlatform.ModelCode.EntityTable result)
		{
			result.Name = this.Name;
			result.EntityType = this.EntityType;
		}
	}
}

