namespace WebApi.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Entities;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly IList<string> _roles;

    public AuthorizeAttribute(params string[] roles)
    {
        _roles = roles ?? new string[] { };
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        // authorization
        var account = (Account)context.HttpContext.Items["Account"];

        var a = account.AccountRole.Select(x => x.Role.Name).ToArray();
        var b = _roles.Intersect(a).Any();

        if (account == null || (_roles.Any() && !_roles.Intersect(account.AccountRole.Select(x => x.Role.Name).ToArray()).Any()))
        {
            // not logged in or role not authorized
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}