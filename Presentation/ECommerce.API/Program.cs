using  API.Extensions;
using Microsoft.EntityFrameworkCore;
using Repository;
using ECommerce.Presentation;
using Microsoft.AspNetCore.HttpOverrides;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

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
});


NewtonsoftJsonInputFormatter GetJsonPatchInputFormatter()=>
new ServiceCollection().AddMvc().AddNewtonsoftJson().Services.BuildServiceProvider().GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters.OfType<NewtonsoftJsonInputFormatter>().First();




builder.Services.AddConfigureRateLimiting();

builder.Services.AddMemoryCache();

builder.Services.AddScoped<IPaymentGateway,MockPaymentService>();
builder.Services.AddScoped<IPaymentWebhookSimulator, MockPaymentWebhookSimulator>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}   



// app.UseExceptionHandler(exceptionHandlerApp =>
// {
//     exceptionHandlerApp.Run(async context =>
//     {
//         context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//         context.Response.ContentType = "application/json";
        
//         var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
//         var exception = exceptionHandlerPathFeature?.Error;
        
//         var response = new
//         {
//             StatusCode = context.Response.StatusCode,
//             Message = exception?.Message ?? "An error occurred while processing your request.",
        
//             Detail = app.Environment.IsDevelopment() ? exception?.StackTrace : null
//         };
        
//         await context.Response.WriteAsync(JsonSerializer.Serialize(response));
//     });
// });

// app.UseMiddleware<ExceptionMiddleWare>();
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
