using Station.Web.Controllers.ChargeStations.Dtos;

namespace Station.Web.Controllers.ChargeStations
{
    public interface IChargeStationsManager
    {
        StationResponse GetUpdateStatuses();
    }
}
