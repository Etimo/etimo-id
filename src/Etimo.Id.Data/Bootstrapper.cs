using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Etimo.Id.Data
{
    public static class Bootstrapper
    {
        public static void UseEtimoIdData(this IServiceCollection services)
        {
            ServiceProvider  sp     = services.BuildServiceProvider();
            IConfiguration   config = sp.GetService<IConfiguration>();
            IHostEnvironment env    = sp.GetService<IHostEnvironment>();

            string connectionString = config.GetConnectionString("EtimoId");
            services.AddDbContext<IEtimoIdDbContext, EtimoIdDbContext>(
                options =>
                {
                    options.UseNpgsql(connectionString);
                    options.EnableSensitiveDataLogging(env.IsDevelopment());
                });
        }
    }
}
