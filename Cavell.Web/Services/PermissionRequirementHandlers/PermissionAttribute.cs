
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Station.Core.Entities.Identities;
using Station.Core.Enums;
using System.Security.Claims;
using System.Security.Policy;

namespace Station.Web.Services.PermissionRequirementHandlers
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        public PermissionAttribute(PermissionActionEnum[] item) : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { item };
        }

        public class AuthorizeActionFilter : IAuthorizationFilter
        {
            private readonly PermissionActionEnum[] _item;
            public AuthorizeActionFilter(PermissionActionEnum[] item)
            {
                _item = item;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var user = context.HttpContext.User;
                if (user == null || !user.Identity.IsAuthenticated)
                {
                    var _res = new { status = 401, Message = "Unauthorized Access", Data = "Unauthorized Access" };
                    context.Result = new JsonResult(_res);
                    return;
                }

                bool isAuthorized = false;
                string message = "";
                foreach (var permission in _item)
                {
                    isAuthorized = CheckUserPermission(user,  permission);
                    if (!isAuthorized) 
                    {
                        var _res = new { status = 401, Message = $"You have not permission {permission}", Data = $"You have not permission {permission}" };
                        context.Result = new JsonResult(_res);
                        return;
                       
                    }
                }

            }


            private bool CheckUserPermission(ClaimsPrincipal user, PermissionActionEnum permissionAction)
            {
                var permissionsString = user.FindFirst("Permissions")?.Value;

                if (string.IsNullOrEmpty(permissionsString))
                {
                    return false;
                }

                var permissions = JsonConvert.DeserializeObject<PermissionsClaim>(permissionsString);

                // Now check the hierarchical structure for the specific permission action
                return HasRequiredPermission(permissions, permissionAction);
            }

            private bool HasRequiredPermission(PermissionsClaim permissions, PermissionActionEnum requiredPermission)
            {
                foreach (var category in permissions.PermissionCategoryClaims)
                {
                    if (category.Actions.Any(action => action.Value == requiredPermission))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
