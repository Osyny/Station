using Station.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Core.Enums
{
    public enum PermissionCategoryEnum
    {
        //[Display(Name = "Role")]
        //Role = 1,
        [Display(Name = "Charge Station")]
        ChargeStation = 1,
        [Display(Name = "User")]
        User = 2
    }
}
