using Station.Core.Entities.Identities;

namespace Station.Web.Controllers.RolePermitions.Helpers
{
    public interface IHelperPermissions
    {
        Task<PermissionsClaim> GetPermissionClaimsByUserAsync(int userId);
    }
}
