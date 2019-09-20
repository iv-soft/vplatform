using IVySoft.VPlatform.Source.Xml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Microsoft.EntityFrameworkCore;
using IVySoft.VPlatform.Generator.Core;
using IVySoft.VPlatform.Target.ModelCode.Xml.Serialization;
using IVySoft.VPlatform.Etl.Core;

namespace IVySoft.VPlatform.Etl.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new XmlSource<Module>
            {
                FilePath = args[0]
            };

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);

            services.AddEntityFrameworkInMemoryDatabase();
            services.AddEntityFrameworkProxies();
            // Add a database context (ApplicationDbContext) using an in-memory 
            // database for testing.
            services.AddDbContext<DbModel>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
                options.UseLazyLoadingProxies();
            });

            services.AddTransient<IEtlStep, Target.ModelCode.ModelCoreGenerator>();
            services.AddTransient<IEtlStep, Target.ModelCode.Xml.Serialization.ModelCoreGenerator>();
            services.AddTransient<IEtlStep, Target.ModelCode.ResolveTypeEtlStep>();
            services.AddTransient<IEtlStep, Target.ModelCode.ResolveNamespaceEtlStep>();
            services.AddTransient<IEtlStep, LoadDependenciesEtlStep>();

           // Create a new service provider.
           var sp = services.BuildServiceProvider();

            using (var serviceScope = sp.CreateScope())
            {
                using (var db = serviceScope.ServiceProvider.GetService<DbModel>())
                {
                    db.Modules.Add(source.Load().ToModel());
                    db.SaveChanges();
                }
            }

            using (var serviceScope = sp.CreateScope())
            {
                var options = serviceScope.ServiceProvider.GetRequiredService<IOptions<GeneratorOptions>>().Value;
                options.ModulesFolder = System.IO.Path.GetDirectoryName(args[0]);
                options.OutputFolder = System.IO.Path.Combine(options.ModulesFolder, "Generated");
                System.IO.Directory.CreateDirectory(options.OutputFolder);

                using (var db = serviceScope.ServiceProvider.GetService<DbModel>())
                {
                    var context = new EtlContext();
                    context.DataModel = db;
                    context.Set(options);
                    context.Execute(serviceScope.ServiceProvider.GetServices<IEtlStep>());
                }
            }

        }
    }
}
