using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Toledo.Core.Enumerations;

namespace Toledo.Api.ActionFilters;

//TODO verify ActionFilter
public class MayPerfom : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("===============");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("===============");
        ClaimsPrincipal session = context.HttpContext.User;
        Guid userId = Guid.Parse(session.FindFirstValue(ClaimTypes.NameIdentifier));
        EnumRole role = Enum.Parse<EnumRole>(session.FindFirstValue(ClaimTypes.Role));
        Console.WriteLine(role);
        if (
            !new List<EnumRole>()
            {
                EnumRole.CENSOR,
                EnumRole.ADMIN,
                EnumRole.SUPER_ADMIN
            }.Contains(role)
        )
            throw new GraphQLException(Errors.Exceptions.NOT_PERMISSION);
    }
}
