using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Core.Entities
{
    public class Connector : BaseEntity
    {
        [ForeignKey(nameof(ConectorType))]
        public int ConectorTypeId { get; set; }
        public ConnectorType ConectorType { get; set; }

        [ForeignKey(nameof(ConectorStatus))]
        public int ConectorStatusId { get; set; }
        public ConnectorStatus ConectorStatus { get; set; }
        public int? MaxCurrent { get; set; }

        [MaxLength(1024)]
        public string Details { get; set; }
    }
}
