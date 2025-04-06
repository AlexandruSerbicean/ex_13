using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InfraSim.Pages.Models;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.Database;

namespace InfraSim
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddSingleton<ICapabilityFactory, ServerCapabilityFactory>();

                    services.AddSingleton<IServerFactory, ServerFactory>();

                    services.AddSingleton<IInfrastructureMediator, InfrastructureMediator>();

                    services.AddSingleton<IServerCapability, ServerCapability>();

                    services.AddDbContext<InfraSimContext>();
                    
                    services.AddSingleton<IRepositoryFactory, RepositoryFactory>();
                });
    }
}
