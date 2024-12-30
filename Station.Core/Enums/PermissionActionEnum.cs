using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Core.Enums
{
    public enum PermissionActionEnum
    {
        [Display(Name = "Edit")]
        Edit = 1,
        [Display(Name = "Create")]
        Create = 2, 
        [Display(Name = "View")]
        View = 3,   
        [Display(Name = "Delete")]
        Delete = 4,
    }
}
