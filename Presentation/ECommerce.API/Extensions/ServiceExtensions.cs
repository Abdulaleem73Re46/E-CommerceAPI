

using Core.Contracts;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Repository;
using Service.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Service;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Diagnostics;
using Core.Entities.Exceptions;
using Core.Entities.ErrorDetails;
namespace API.Extensions;



public static class ServiceExtensions
{
    


public static void  ConfigureRepositoryManager(this IServiceCollection service)=>service.AddScoped<IRepositoryManager,RepositoryManager>(); 
   
public static  void ConfigureServiceManager(this IServiceCollection services)=>services.AddScoped<IServiceManager,ServiceManager>(); 

public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("Policy",Builder=>
            
            Builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

    }




}




public static class ExceptionMiddleWareExetsion
{
    

public static void ConfigureExceptionHandler(this WebApplication app)
    {

        app.UseExceptionHandler(appError =>
        {
         appError.Run(async context =>
         {
             context.Response.ContentType="application/json";
             var contextFeature=context.Features.Get<IExceptionHandlerFeature>();
             if (contextFeature != null)
             {
                 context.Response.StatusCode=contextFeature.Error switch
                 {
                    NotFoundException=>StatusCodes.Status404NotFound,
                    BadHttpRequestException=>StatusCodes.Status400BadRequest,
                    _=>StatusCodes.Status500InternalServerError
                 };
                    var message=context.Response.StatusCode==StatusCodes.Status500InternalServerError?"Internal Server ERror":contextFeature.Error.Message;
                    await context.Response.WriteAsync(new ErrorDetail()
                    {
                        StatusCode=context.Response.StatusCode,
                      Message=contextFeature.Error.Message,




                    }.ToString());



             }  





         }) ;  





        });



    }








}

