using  API.Extensions;
using Microsoft.EntityFrameworkCore;






var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureServiceManager(); 
builder.Services.ConfigureRepositoryManager();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddDbContext<RepositoryContext>(opt =>
 opt.UseSqlite(builder.Configuration.GetConnectionString("SqlConnection"),op=>op.MigrationsAssembly("Infrastructure"))

);



var app = builder.Build();

// Configure the HTTP request pipeline.






app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
