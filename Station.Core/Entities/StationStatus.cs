using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Core.Entities
{
    public class StationStatus : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int EnumValue { get; set; }

    }
}
