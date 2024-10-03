﻿using Station.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Core.Entities
{
    public class StationEventType : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }   
        public int EnumValue { get; set; }   
     
        
        
        // public List<StationEvent> StationEvents { get; set; }   
        
    }
}
