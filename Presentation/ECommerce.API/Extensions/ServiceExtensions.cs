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
using System.Security.Claims;
<<<<<<< HEAD
=======
using System.Text.Json;
>>>>>>> 9efd85e50f987e4293c4535f1171a1ce25504d06

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
    
    var key = Environment.GetEnvironmentVariable("SECRETKEY") ??
              jwtSettings["Key"] ?? 
              configuration["Jwt:Key"] ??
              "YourSuperSecretKeyThatIsAtLeast32CharactersLong123!";
    
    if (string.IsNullOrEmpty(key) || key.Length < 32)
    {
<<<<<<< HEAD
        var jwtSettings = configuration.GetSection("JwtSettings");
    var secretKey = jwtSettings["Key"];
    
    if (string.IsNullOrEmpty(secretKey))
        throw new InvalidOperationException("JWT Key is missing in appsettings.json");

=======
        throw new InvalidOperationException("JWT Secret Key is missing or too short.");
    }
    
>>>>>>> 9efd85e50f987e4293c4535f1171a1ce25504d06
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
<<<<<<< HEAD
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = ClaimTypes.Role
        };
=======
        opt.RequireHttpsMetadata = false; // Add this for development
        opt.SaveToken = true; // Add this
        
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"] ?? "https://localhost:5276",
>>>>>>> 9efd85e50f987e4293c4535f1171a1ce25504d06
            
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"] ?? "https://localhost:5276",
            
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            
            RoleClaimType = ClaimTypes.Role
        };
        
        // 🔑 CRITICAL FIX - This prevents redirect to login page
        opt.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync(JsonSerializer.Serialize(new 
                { 
                    error = "Authentication failed", 
                    message = context.Exception.Message 
                }));
            },
            OnChallenge = context =>
            {
                // ✅ This prevents the default redirect behavior
                context.HandleResponse();
                
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    
                    var result = JsonSerializer.Serialize(new 
                    { 
                        error = "Unauthorized", 
                        message = "Invalid or missing token. Please provide a valid JWT token in the Authorization header."
                    });
                    
                    return context.Response.WriteAsync(result);
                }
                
                return Task.CompletedTask;
            },
            OnForbidden = context =>
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new 
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
public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        // ✅ Change this - don't use UseExceptionHandler with empty config
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