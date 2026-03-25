using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace StudentFeedbackSystem.Filters
{
    public class RoleBasedAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _Roles;

        public RoleBasedAuthorizationAttribute(params string[] Roles)
        {
           this._Roles = Roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (context.ActionDescriptor.EndpointMetadata.Any(m => m is AllowAnonymousAttribute))
                return;

            if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
                return;
            }

            var userRole = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == null || !_Roles.Contains(userRole))
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
            }
        }
    }
}
