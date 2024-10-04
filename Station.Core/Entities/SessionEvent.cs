using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Core.Entities
{
    public class SessionEvent :BaseEntity
    {
        [ForeignKey(nameof(EventType))]
        public int TypeId { get; set; }
        public SessionEventType EventType { get; set; }

        [ForeignKey(nameof(SessionStatus))]
        public int? StationStatusId { get; set; }
        public SessionStatus SessionStatus { get; set; }

        public DateTime TimeStampe { get; set; }
        public int? NewData { get; set; }
    }
}
