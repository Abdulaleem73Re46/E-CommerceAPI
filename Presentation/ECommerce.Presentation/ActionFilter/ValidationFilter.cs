
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace ECommerce.Presentation;

public class ValidationFilterAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.RouteData.Values["action"];
        var controller = context.RouteData.Values["controller"];

        // Find any argument that is a DTO (class/record, not a primitive)
        var dto = context.ActionArguments
            .FirstOrDefault(x => x.Value?.GetType().Namespace?.Contains("DataTransferObjects") == true).Value;

        if (dto is null)
            return; // No DTO to validate, skip

        if (!context.ModelState.IsValid)
        {
            Console.WriteLine($"Validation failed - Controller: {controller}, Action: {action}");
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}