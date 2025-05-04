using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Test01.Data;
using Test01.Domain;
using Test01.Repositories;
using Test01.Repositories.Impl;
using Test01.Service;
using Test01.Service.impl;

namespace Test01.Config
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }

        public static IServiceCollection AddIdentityConfig(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                // Tùy chỉnh nếu muốn (ví dụ: yêu cầu mật khẩu mạnh)
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
