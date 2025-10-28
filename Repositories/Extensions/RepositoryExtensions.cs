using App.Repositories.Vehicles;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public  static IServiceCollection AddRepositories(this IServiceCollection services,IConfiguration configuration) 
        {
          services.AddDbContext<AppDbContext>(options =>
            {

                var connectionstrings = configuration.GetSection(ConnectiobStringOptions.Key).Get<ConnectiobStringOptions>();
                options.UseSqlServer(connectionstrings!.SqlServer, sqlServerOptionsAction => sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName));
            });
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped(typeof (IGenericRepository<>),typeof (GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
