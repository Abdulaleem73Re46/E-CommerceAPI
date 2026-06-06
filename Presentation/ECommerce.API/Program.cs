using  API.Extensions;
using Microsoft.EntityFrameworkCore;
using Repository;
using ECommerce.Presentation;
using Microsoft.AspNetCore.HttpOverrides;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using APIMiddleware;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Frozen;
using Service.Contracts;
using Core.Shared.Externals;

using Service.Contracts;

using Service;
using Core.Shared.DataTransferObjects;
using Core.Shared.Helpers;





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers(config=>
{
    config.RespectBrowserAcceptHeader=true;
    config.InputFormatters.Insert(0,GetJsonPatchInputFormatter());
}).AddXmlDataContractSerializerFormatters()
.AddApplicationPart(typeof(ECommerce.Presentation.AssemblyReference).Assembly).AddJsonOptions(options =>
{
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

});

builder.Services.AddSwaggerGen();

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
        policy.RequireRole("user", "admin"));



var permissionFields = typeof(Permissions).GetFields()
        .Where(f => f.IsLiteral && f.IsStatic && f.FieldType == typeof(string));

    foreach (var field in permissionFields)
    {
        var permissionName = (string)field.GetValue(null);
        options.AddPolicy($"Has{permissionName.Replace(".", "")}", policy =>
            policy.RequireClaim("Permission", permissionName));
    }
});


NewtonsoftJsonInputFormatter GetJsonPatchInputFormatter()=>
new ServiceCollection().AddMvc().AddNewtonsoftJson().Services.BuildServiceProvider().GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters.OfType<NewtonsoftJsonInputFormatter>().First();




builder.Services.AddConfigureRateLimiting();

builder.Services.AddMemoryCache();
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IPaymentGateway,MockPaymentService>();
builder.Services.AddScoped<IPaymentWebhookSimulator, MockPaymentWebhookSimulator>();


var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}   


//app.UseHttpsRedirection();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});




//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("Policy");
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

app.MapControllers();

app.Run();
