using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Core.Entities
{
    public class ConnectorEvent : BaseEntity
    {
        
        [ForeignKey(nameof(EventType))]
        public int TypeId { get; set; }
        public ConnectorEventType EventType { get; set; }

        public DateTime TimeStamp { get; set; }

        [ForeignKey(nameof(ConnectorStatus))]
        public int? NewConnectorStatusId { get; set; }
        public ConnectorStatus ConnectorStatus { get; set; }

        public int? NewData { get; set; }

    }
}
