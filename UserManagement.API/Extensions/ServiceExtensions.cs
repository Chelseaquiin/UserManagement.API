using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using UserManagement.Services.Interfaces;
using UserManagement.Services.Implementations;
using UserManagement.Data.Context;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data.Interfaces;
using UserManagement.Data.Implementations;

namespace UserManagement.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDBConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConn")
         ));
        }


        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
           services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
            services.AddScoped<IServiceFactory, ServiceFactory>();
           services.AddTransient<IUserService, UserService>();
        }
    }
}
