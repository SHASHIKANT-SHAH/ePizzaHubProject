﻿using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Respositories.Implementations;
using ePizzaHub.Respositories.Interfaces;
using ePizzaHub.Services.Implementations;
using ePizzaHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


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
            services.AddScoped<IRepository<CartItem>, Repository<CartItem>>();
            services.AddScoped<IRepository<Cart>, Repository<Cart>>();
            services.AddScoped<IRepository<PaymentDetail>, Repository<PaymentDetail>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            //services
            services.AddScoped<IService<Item>, Service<Item>>();
            services.AddScoped<IService<Order>, Service<Order>>();
            services.AddScoped<IService<PaymentDetail>, Service<PaymentDetail>>();

            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
