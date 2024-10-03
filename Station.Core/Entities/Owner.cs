using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Core.Entities
{
    public class Owner : BaseEntity
    {
        public string Name { get; set; }

        public List<ChargeStation> ChargeStations { get; set; }
    }
}
