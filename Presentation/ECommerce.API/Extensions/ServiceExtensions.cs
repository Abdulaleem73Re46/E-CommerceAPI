

using Core.Contracts;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Repository;
using Service.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Service;

namespace API.Extensions;



public static class ServiceExtensions
{
    


public static void  ConfigureRepositoryManager(this IServiceCollection service)=>service.AddScoped<IRepositoryManager,RepositoryManager>(); 
   
public static  void ConfigureServiceManager(this IServiceCollection services)=>services.AddScoped<IServiceManager,ServiceManager>(); 






}

