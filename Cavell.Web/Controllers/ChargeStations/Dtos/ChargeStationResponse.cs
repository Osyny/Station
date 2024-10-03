using Station.Web.Dtos;

namespace Station.Web.Controllers.ChargeStations.Dtos
{
    public class ChargeStationResponse
    {
        public List<ChargeStationDto> ChargeStations { get; set; }
        public int Total { get; set; }
    }
}
