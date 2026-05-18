using Core.Contracts;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Repository;
using Service.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Service;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Diagnostics;
using Core.Entities.Exceptions;
using Core.Entities.ErrorDetails;
using Microsoft.AspNetCore.StaticAssets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Core.Entities;

namespace API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureRepositoryManager(this IServiceCollection service) 
        => service.AddScoped<IRepositoryManager, RepositoryManager>(); 
   
    public static void ConfigureServiceManager(this IServiceCollection services) 
        => services.AddScoped<IServiceManager, ServiceManager>(); 

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("Policy", builder =>
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
    }

    public static void AddConfigurationJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        
        // ✅ تصحيح الخطأ الإملائي وإضافة fallback
        var key = Environment.GetEnvironmentVariable("SECRETKEY") ??   // تصحيح: SECRETKEY
                  jwtSettings["Key"] ?? 
                  configuration["Jwt:Key"] ??
                  "YourSuperSecretKeyThatIsAtLeast32CharactersLong123!";
        
        if (string.IsNullOrEmpty(key) || key.Length < 32)
        {
            throw new InvalidOperationException("JWT Secret Key is missing or too short. Please provide a key with at least 32 characters.");
        }
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings["Issuer"] ?? "https://localhost:5276",
                
                ValidateAudience = true,
                ValidAudience = jwtSettings["Audience"] ?? "https://localhost:5276",
                
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                
                // ✅ مهم جداً: تحديد نوع الدور
                RoleClaimType = System.Security.Claims.ClaimTypes.Role
            };
            
            // ✅ منع إعادة التوجيه إلى صفحة Login
            opt.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    var result = System.Text.Json.JsonSerializer.Serialize(new 
                    { 
                        error = "Unauthorized", 
                        message = "Invalid or missing token" 
                    });
                    return context.Response.WriteAsync(result);
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";
                    var result = System.Text.Json.JsonSerializer.Serialize(new 
                    { 
                        error = "Forbidden", 
                        message = "You don't have permission to access this resource" 
                    });
                    return context.Response.WriteAsync(result);
                }
            };
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User, IdentityRole>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 8;
            o.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<RepositoryContext>()
        .AddDefaultTokenProviders();
    }
}

public static class ExceptionMiddleWareExtension
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadHttpRequestException => StatusCodes.Status400BadRequest,
                        UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                        _ => StatusCodes.Status500InternalServerError
                    };
                    
                    var message = context.Response.StatusCode == StatusCodes.Status500InternalServerError 
                        ? "Internal Server Error" 
                        : contextFeature.Error.Message;
                    
                    await context.Response.WriteAsync(new ErrorDetail()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = message,
                    }.ToString());
                }
            });
        });
    }
}