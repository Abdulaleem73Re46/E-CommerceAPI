using  API.Extensions;
using Microsoft.EntityFrameworkCore;
using Repository;
using ECommerce.Presentation;
using Microsoft.AspNetCore.HttpOverrides;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers(config=>
{
    config.RespectBrowserAcceptHeader=true;
}).AddXmlDataContractSerializerFormatters()
.AddApplicationPart(typeof(ECommerce.Presentation.AssemblyReference).Assembly).AddJsonOptions(options =>
{
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

});
builder.Services.AddDbContext<RepositoryContext>(opt =>
 opt.UseSqlite(builder.Configuration.GetConnectionString("SqlConnection"),
 op => op.MigrationsAssembly("ECommerce.Repository"))

);
builder.Services.ConfigureServiceManager(); 
builder.Services.ConfigureRepositoryManager();
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.ConfigureCors();
builder.Services.AddConfigurationJWT(builder.Configuration);

builder.Services.ConfigureIdentity();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => 
        policy.RequireRole("admin"));
    
    options.AddPolicy("UserOrAdmin", policy => 
        policy.RequireRole("User", "Admin"));
});
 builder.Services.ConfigureExceptionHandler();

var app = builder.Build();


app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;
        
        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = exception?.Message ?? "An error occurred while processing your request.",
            // Only include stack trace in development
            Detail = app.Environment.IsDevelopment() ? exception?.StackTrace : null
        };
        
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    });
});
// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});




//app.UseHttpsRedirection();
app.UseCors("Policy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
