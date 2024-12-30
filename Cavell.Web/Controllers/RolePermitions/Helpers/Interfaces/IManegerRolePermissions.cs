using Station.Core.Entities.Identities;
using Station.Web.Controllers.Users.Dtos;

namespace Station.Web.Controllers.RolePermitions.Helpers.Interfaces
{
    public interface IManegerRolePermissions
    {
        Task<UserRole> CreatedUserRole(UserRoleInput role);
    }
}
