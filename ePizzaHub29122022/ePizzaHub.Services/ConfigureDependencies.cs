using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Respositories.Implementations;
using ePizzaHub.Respositories.Interfaces;
using ePizzaHub.Services.Implementations;
using ePizzaHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services
{
    public class ConfigureDependencies
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            //database
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });
            //repositories
            services.AddScoped<DbContext, AppDbContext>();

            services.AddScoped<IRepository<Item>, Repository<Item>>();

            services.AddScoped<IUserRepository, UserRepository>();

            //services
            services.AddScoped<IService<Item>, Service<Item>>();
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<IItemService, ItemService>();
        }
    }
}
