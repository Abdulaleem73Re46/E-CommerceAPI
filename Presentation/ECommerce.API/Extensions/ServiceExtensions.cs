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
using System.Text.Json;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

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
    var key=jwtSettings["Key"];


    if (string.IsNullOrEmpty(key) || key.Length < 32)
    {
        throw new InvalidOperationException("JWT Secret Key is missing or too short.");
    }
    
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       
    })
    .AddJwtBearer(opt =>
    {
        opt.RequireHttpsMetadata = false; 
        opt.SaveToken = true; 
        
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            
            RoleClaimType = ClaimTypes.Role
        };
        
    
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
        var builder = services.AddIdentityCore<User>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 8;
            o.User.RequireUniqueEmail = true;
        }).AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<RepositoryContext>()
        .AddDefaultTokenProviders();
    }



   public static void AddConfigureRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode=StatusCodes.Status429TooManyRequests;
            options.OnRejected=async(context, token) =>
            {
                context.HttpContext.Response.ContentType="application/json";
                context.HttpContext.Response.Headers["Retry-After"]="10";
                
                await context.HttpContext.Response.WriteAsJsonAsync(new
                {
                    error="Too Many Requests ",
                    message="You have been exceeded the number of requests that allowed for you ",
                    retryAfterSeconds=context.Lease.TryGetMetadata(MetadataName.RetryAfter,out var retryAfter)
                },token);  
                };
            options.AddFixedWindowLimiter(policyName:"FixedWindowRateLimiting",opt =>
            {
                opt.PermitLimit=3;
                opt.Window=TimeSpan.FromMinutes(1);
                opt.QueueLimit=0;
                //opt.QueueProcessingOrder=QueueProcessingOrder.OldestFirst; 
            });


            options.AddSlidingWindowLimiter(policyName: "SlidingWindowRateLimiting", opt =>
            {
                opt.PermitLimit=5;
                opt.SegmentsPerWindow=3;
                opt.QueueLimit=2;
                opt.QueueProcessingOrder=QueueProcessingOrder.OldestFirst;
                opt.Window=TimeSpan.FromMinutes(30);
               opt.AutoReplenishment=true;



            });    
                
                       
                       
                       
                       
     });

    }




}
public static class ExceptionMiddlewareExtensions
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