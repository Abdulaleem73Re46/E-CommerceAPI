



using Microsoft.EntityFrameworkCore.Design;
using Repository;
namespace Repository.ContextFactory;


public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {

        throw new NotImplementedException();
     
    }
}