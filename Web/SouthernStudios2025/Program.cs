using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using SouthernStudios2025.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models; 
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SouthernStudios2025.Data;
using SouthernStudios2025.Entities;
using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SouthernStudios2025.Views;

namespace SouthernStudios2025;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder => builder.CaptureStartupErrors(true).UseStartup<Startup>());
    }
}

/*var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    
    builder.Services.AddCors();

    builder.Services.AddHsts(options =>
    {
        options.MaxAge = TimeSpan.MaxValue;
        options.Preload = true;
        options.IncludeSubDomains = true;
    });

    builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(connectionString)
    );

    builder.Services.AddIdentity<Users, Role>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 8;
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
        options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
        options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
    })
    .AddEntityFrameworkStores<DataContext>();
    builder.Services.AddControllers();

    builder.Services.AddMvcCore().AddApiExplorer();

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            };
        });

    builder.Services.AddAuthorization();

    // This is where swagger is built
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(x =>
    {
        x.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "SouthernStudios2025",
            Version = "v1",
            Description = "Backend for SouthernStudios2025 website",
        });

        x.CustomSchemaIds(apiDesc => apiDesc.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
        x.MapType(typeof(IFormFile), () => new OpenApiSchema { Type = "file", Format = "binary" });
    });

    
    var app = builder.Build();
    
    app.UseRouting();
    app.MapControllers();
    app.Run(); */