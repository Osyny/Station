using Station.Core.Entities;

namespace Station.Web.Dtos
{
    public class ChargeStationDto
    {
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public OwnerDto Owner { get; set; }

        public bool Status { get; set; }
    }
}
