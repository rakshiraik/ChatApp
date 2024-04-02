using ChatService.Repository;
using ChatService.Repository.Contracts;
using ChatService.Services.Error;
using ChatService.Services.Room;
using ChatService.Services.RoomUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ChatService.Infrastructure
{
    public static class BuisnessDependencyRegisrty
    {

        public static void RegisterDependency(this IServiceCollection services)
        {
            
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<IErrorService, ErrorService>();
            services.AddScoped<IRoomUserService, RoomUserService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomUserRepository, RoomUserRepository>(); 


        }
    }
}
