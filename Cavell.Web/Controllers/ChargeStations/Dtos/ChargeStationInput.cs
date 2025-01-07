using Station.Web.Dtos;

namespace Station.Web.Controllers.ChargeStations.Dtos
{
    public class ChargeStationInput
    {
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string IPAddress { get; set; } = string.Empty;
        public bool Status { get; set; }

    }
}
