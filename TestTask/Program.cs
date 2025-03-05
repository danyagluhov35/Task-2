using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using TestTask.Core;
using TestTask.Entity;
using TestTask.IService;
using TestTask.Middleware;
using TestTask.Service;

var builder = WebApplication.CreateBuilder(args);


LogManager.Setup().LoadConfigurationFromFile("nlog.config");

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddDbContext<ApplicationContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(op =>
{
    op.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = AuthOption.Issuer,
        ValidAudience = AuthOption.Audience,
        IssuerSigningKey = AuthOption.GetSymmetricSecurityKey()
    };
});



var app = builder.Build();

app.UseStaticFiles();
app.UseDefaultFiles();
app.UseRouting();
app.UseMiddleware<JwtSecurity>();

app.MapControllerRoute
    (
        name : "default",
        pattern : "{Controller=Home}/{action=Index}/{id?}"
    );


app.Run();
