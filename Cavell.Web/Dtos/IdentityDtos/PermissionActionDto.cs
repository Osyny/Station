using Station.Core.Entities.Identities;
using Station.Core.Enums;

namespace Station.Web.Dtos.IdentityDtos
{
    public class PermissionActionDto : EntityDto
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public PermissionActionEnum Value { get; set; }

        public int? PermissionCategoryId { get; set; }

        public virtual PermissionCategoryDto? PermissionCategory { get; set; }
    }
}
