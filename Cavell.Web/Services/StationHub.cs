using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Renci.SshNet.Messages;
using Station.Web.Controllers.ChargeStations;
using Station.Web.Controllers.ChargeStations.Dtos;

namespace Station.Web.Services
{

    public class StationHub : Hub<ISignalrDemoHub>
    {
        private readonly IChargeStationsManager _stationsManager;
        public StationHub(IChargeStationsManager stationsManager)
        {
            _stationsManager = stationsManager;
        }
        public async void Hello() 
        {
           await Clients.Caller.DisplayMessage("MESSAGE from stationHub!!!!");

        }

        public async Task UpdateStatuses()
        {
            while (true)
            {
                var res = _stationsManager.GetUpdateStatuses().Result;
                await Clients.Caller.GetUpdateStatuses(res);
                await Task.Delay(1000);
            }

        }

        public override async Task OnConnectedAsync()
        
        {
            Console.WriteLine(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            Console.WriteLine(Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }
    }
}
