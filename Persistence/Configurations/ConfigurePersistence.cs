using Application.Commons;
using Application.ToDoItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.ToDoItems;

namespace Persistence.Configurations
{
    public static class ConfigurePersistence
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // register dbContext
            services.AddDbContext<ToDoContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // register repos
            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
