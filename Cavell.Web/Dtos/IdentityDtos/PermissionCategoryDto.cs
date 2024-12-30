using Station.Core.Entities;
using Station.Core.Enums;

namespace Station.Web.Dtos.IdentityDtos
{
    public class PermissionCategoryDto : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public PermissionCategoryEnum Value { get; set; }
    }
}
