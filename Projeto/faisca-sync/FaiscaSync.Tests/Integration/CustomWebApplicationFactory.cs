using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FaiscaSync.Models;
using System.Linq;

namespace FaiscaSync.Tests.Integration
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>

    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove o contexto de produção
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<FsContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                // Adiciona um novo contexto em memória
                services.AddDbContext<FsContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                // Garante que o banco seja criado
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<FsContext>();
                db.Database.EnsureCreated();
            });
        }
    }
}
