using  API.Extensions;
using Microsoft.EntityFrameworkCore;
using Repository;
using ECommerce.Presentation;
using Microsoft.AspNetCore.HttpOverrides;
using System.Text.Json.Serialization;





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
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.ConfigureCors();
// builder.Services.ConfigureExceptionHandler();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});




//app.UseHttpsRedirection();
app.UseCors("Policy");
app.UseAuthorization();

app.MapControllers();

app.Run();
