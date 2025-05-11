using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InfraSim.Services;
using InfraSim.Pages.Models;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.Commands;
using InfraSim.Pages.Models.Database;

namespace InfraSim
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .ConfigureServices((context, services) =>
                {
                
                    services.AddDbContext<InfraSimContext>();
                    services.AddScoped<IRepositoryFactory,  RepositoryFactory>();
                    services.AddScoped<IUnitOfWork,        UnitOfWork>();
                    services.AddScoped<IServerDataMapper,  ServerDataMapper>();
                    services.AddScoped<IServerFactory,     ServerFactory>();
                    services.AddScoped<IInfrastructureMediator, InfrastructureMediator>();
                 
                    services.AddSingleton<ICapabilityFactory, ServerCapabilityFactory>();
                    services.AddSingleton<ICommandManager,     CommandManager>();
                    services.AddSingleton<UserCounter>();

                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        using var scope = services.BuildServiceProvider().CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<InfraSimContext>();
                        db.Database.EnsureDeleted();  
                        db.Database.EnsureCreated();  
                    }
                });
                
    }
}