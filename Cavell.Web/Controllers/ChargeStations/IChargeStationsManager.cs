using Station.Web.Controllers.ChargeStations.Dtos;

namespace Station.Web.Controllers.ChargeStations
{
    public interface IChargeStationsManager
    {
        Task<StationResponse> GetUpdateStatuses();

        Task<StationResponse> GetUpdateStatusesAsync();
    }
}
