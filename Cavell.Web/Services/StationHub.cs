using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Station.Web.Controllers.ChargeStations;

namespace Station.Web.Services
{

    public class StationHub : Hub<ISignalrDemoHub>
    {
        private readonly IChargeStationsManager _stationsManager;
        public StationHub(IChargeStationsManager stationsManager) {
            _stationsManager = stationsManager;
        }
        public void Hello() 
        {
            Clients.Caller.DisplayMessage("MESSAGE from stationHub!!!!");

        }

        public void GetUpdateStatuses()
        {
           var res = _stationsManager.GetUpdateStatuses();
            Clients.Caller.GetUpdateStatuses(res);

        }
    }
}
