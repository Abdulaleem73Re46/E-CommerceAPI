



using Microsoft.EntityFrameworkCore.Design;
using Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
namespace Repository.ContextFactory;


public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
       string path=Path.Combine(Directory.GetCurrentDirectory(),"..","Presentation","ECommerce.API");
       IConfigurationRoot config=new  ConfigurationBuilder()
       .SetBasePath(path).AddJsonFile("appsettings.json").Build();

       var builder=new DbContextOptionsBuilder<RepositoryContext>();
       var connectionstring=config.GetConnectionString("SqlConnection");
       builder.UseSqlite(connectionstring,m=>m.MigrationsAssembly("Infrastructure"));
       return new RepositoryContext(builder.Options);
        
     
    }
}