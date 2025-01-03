using Station.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Station.Core.Entities.Identities
{
    public class Role : BaseEntity
    {

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        // Navigation properties
        [JsonIgnore]
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        [JsonIgnore]
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
