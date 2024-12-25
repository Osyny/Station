using Station.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Station.Web.Dtos
{
    public class ConnectorDto : EntityDto
    {
        public int ConectorTypeId { get; set; }
        public ConnectorTypeDto ConectorType { get; set; }


        public int ConectorStatusId { get; set; }
        public ConnectorStatusDto ConectorStatus { get; set; }
        public int? MaxCurrent { get; set; }

        [MaxLength(1024)]
        public string Details { get; set; }


        public int ChargeStationId { get; set; }
        public ChargeStationDto ChargeStation { get; set; }


        public int ConnectorUiStatusId { get; set; }
        public ConnectorUiStatusDto ConnectorUiStatus { get; set; }
    }
}
