using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RadMock2021.DataModel;

namespace RadMock2021
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            SeedData(host);
            host.Run();
        }

        private static void SeedData(IHost host)
        {
            // seed data via DependencyInjection
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                // make sure the Seeder has been set up in the Datamodel in webAPI so it call be called here
                // and then call it in the start up
                var seed = scope.ServiceProvider.GetService<ApplicationDbSeeder>();
                seed.Seed().Wait();
            }

            //throw new NotImplementedException();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
