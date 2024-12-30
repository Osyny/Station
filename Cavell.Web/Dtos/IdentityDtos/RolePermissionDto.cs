using Station.Core.Entities.Identities;

namespace Station.Web.Dtos.IdentityDtos
{
    public class RolePermissionDto : EntityDto
    {
        public int? RoleId { get; set; }
        public int? PermissionActionId { get; set; }

        public PermissionActionDto? PermissionAction { get; set; }
        //public RoleDto? Role { get; set; } = null!;
    }
}
