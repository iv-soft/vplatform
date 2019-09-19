using IVySoft.VPlatform.Source.ModuleSchema;
using IVySoft.VPlatform.Source.Xml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Microsoft.EntityFrameworkCore;
using IVySoft.VPlatform.Generator.Core;

namespace IVySoft.VPlatform.Etl.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new XmlSource<Module>
            {
                FilePath = @"C:\Users\User\projects\vplatform\projects\VPlatform\module.xml"
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

            services.AddTransient<IGenerator, Target.ModelCode.ModelCoreGenerator>();

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
                options.OutputFolder = @"C:\Users\User\projects\vplatform\projects\VPlatform\Generated";
                System.IO.Directory.CreateDirectory(options.OutputFolder);

                using (var db = serviceScope.ServiceProvider.GetService<DbModel>())
                {
                    var generators = serviceScope.ServiceProvider.GetServices<IGenerator>();
                    foreach (var generator in generators)
                    {
                        generator.Generate(db, options);
                    }
                }
            }

        }
    }
}
