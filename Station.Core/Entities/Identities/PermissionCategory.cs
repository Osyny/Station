using Station.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Station.Core.Entities.Identities
{
    public class PermissionCategory
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public PermissionCategoryEnum Value { get; set; }


        [JsonIgnore]
        public virtual ICollection<PermissionAction> PermissionActions { get; set; } = new List<PermissionAction>();
    }
}
