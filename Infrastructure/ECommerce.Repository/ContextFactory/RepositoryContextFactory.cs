using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Repository.ContextFactory;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        // Get the current directory (should be the repository project directory)
        string currentDir = Directory.GetCurrentDirectory();
        
        // Build path to API project - adjust based on your actual structure
        // From: Infrastructure/ECommerce.Repository/
        // To:   Presentation/ECommerce.API/
        string apiPath = Path.GetFullPath(Path.Combine(currentDir, "..", "..", "Presentation", "ECommerce.API"));
        
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(apiPath)
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        var builder = new DbContextOptionsBuilder<RepositoryContext>();
        var connectionString = config.GetConnectionString("SqlConnection") ?? "Data Source=ECommerce.db";
        
        builder.UseSqlite(connectionString, 
            m => m.MigrationsAssembly("ECommerce.Repository"));
        
        return new RepositoryContext(builder.Options);
    }
}