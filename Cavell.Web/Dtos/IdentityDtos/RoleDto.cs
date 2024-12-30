using Station.Core.Entities.Identities;

namespace Station.Web.Dtos.IdentityDtos
{
    public class RoleDto : EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation properties

        public  List<UserRoleDto> UserRoles { get; set; }

        public List<RolePermissionDto> RolePermissions { get; set; }
    }
}
