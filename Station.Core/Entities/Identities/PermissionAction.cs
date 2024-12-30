using Station.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Station.Core.Entities.Identities
{
    public partial class PermissionAction
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public PermissionActionEnum Value { get; set; }

        public int? PermissionCategoryId { get; set; }

        [JsonIgnore]
        public virtual PermissionCategory? PermissionCategory { get; set; }

        [JsonIgnore]
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
