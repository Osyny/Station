using Station.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Core.Entities.Identities
{
    public class PermissionActionClaim
    {
        public string Name { get; set; }
        public PermissionActionEnum Value { get; set; }
    }

    public class PermissionCategoryClaim
    {
        public string Name { get; set; }
        public PermissionCategoryEnum Value { get; set; }
        public List<PermissionActionClaim> Actions { get; set; }
    }

    public class PermissionsClaim
    {
        public List<PermissionCategoryClaim> PermissionCategoryClaims { get; set; }
    }
}
