using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Station.Core.Entities.Identities
{
    public partial class RolePermission : BaseEntity
    {
        public int? RoleId { get; set; }
        public int? PermissionActionId { get; set; }

        [JsonIgnore]
        public virtual PermissionAction? PermissionAction { get; set; } = null!;

        [JsonIgnore]
        public virtual Role? Role { get; set; } = null!;
    }
}
