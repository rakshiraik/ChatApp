using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagementService.Data;
using UserManagementService.Infrastructure;
using UserManagementService.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ChatBotContextConnection") ?? throw new InvalidOperationException("Connection string 'ChatBotContextConnection' not found.");

builder.Services.AddDbContext<ChatBotContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ChatBotContext>();

//builder.Services.AddIdentityCore<IdentityUser>()
//    .AddEntityFrameworkStores<ChatBotContext>().AddApiEndpoints();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterDependency();
var app = builder.Build();
app.UseExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); ;
app.UseAuthorization();
app.MapControllers();
app.Run();
