using Microsoft.AspNetCore.Authentication;
using System.Configuration;
using UserManagementService.Services.Auth;
using UserManagementService.Services.Error;
using UserManagementService.Services.User;

namespace UserManagementService.Infrastructure
{
    public static class BuisnessDependencyRegisrty
    {
        public static void RegisterDependency(this IServiceCollection services) 
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IErrorService, ErrorService>();
            services.AddScoped<IAuthService, AuthService>(); 
        }
    }
}
