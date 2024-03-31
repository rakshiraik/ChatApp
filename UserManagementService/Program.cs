using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagementService.Data;
using UserManagementService.Infrastructure;
using UserManagementService.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Secret"));
//var key = Encoding.ASCII.GetBytes("!@#$%^&*()!@#$%^&*()");//Encoding.ASCII.GetBytes(configValue);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,x=>{
    x.Events = new JwtBearerEvents()
    {
        OnAuthenticationFailed = context =>
        {
            // Log the authentication failure
            ILogger logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
            logger.LogError(context.Exception, "Authentication failed.");

            return Task.CompletedTask;
        }

    };

    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false // you dont want to validate lifetime 

    };

});

builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterDependency();
var app = builder.Build();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});
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
