using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Core.Entities
{
    public class StationEvent : BaseEntity
    {
        [ForeignKey(nameof(EventType))]
        public int TypeId { get; set; }
        public StationEventType EventType { get; set; } 

        [ForeignKey(nameof(StationStatus))]
        public int StationStatusId { get; set; }
        public StationStatus StationStatus { get; set; }

        public DateTime? TimeStampe { get; set; }
        public int NewData { get; set; }

        [ForeignKey(nameof(ChargeStation))]
        public int ChargeStationId { get; set; }
        public ChargeStation ChargeStation { get; set; }

    }
}
