
using System.Linq.Expressions;
using Core.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository;




public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{

    protected RepositoryContext RepositoryContext;
      public RepositoryBase(RepositoryContext repository)
    {
        RepositoryContext=repository;
    }


    public void Create(T entity)=>RepositoryContext.Set<T>().Add(entity);

    public void Delete(T entity)=>RepositoryContext.Set<T>().Remove(entity);



    public IQueryable<T> FindAll(bool trackChanges)=>trackChanges?RepositoryContext.Set<T>():RepositoryContext.Set<T>().AsNoTracking();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)=>trackChanges?RepositoryContext.Set<T>().Where(expression):RepositoryContext.Set<T>().AsNoTracking().Where(expression);
   

    public void Update(T entity)=>RepositoryContext.Set<T>().Update(entity);

}