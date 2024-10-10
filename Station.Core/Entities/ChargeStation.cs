using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Core.Entities
{
    public class ChargeStation : BaseEntity
    {
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string IPAddress { get; set; }
        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }


        public List<StationEvent> StationEvents { get; set; }
        public List<Session> Sessions { get; set; }

        public bool Status { get; set; }
    }
}
