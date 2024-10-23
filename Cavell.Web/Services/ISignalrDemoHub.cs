using Station.Web.Controllers.ChargeStations.Dtos;

namespace Station.Web.Services
{
    public interface ISignalrDemoHub
    {
        Task DisplayMessage(string message);
        Task GetUpdateStatuses(StationResponse stationResponse);
    }
}
