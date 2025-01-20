using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Station.Core;
using Station.Core.Entities;
using Station.Core.Enums;
using Station.Web.Controllers.Accounts.Dtos;
using Station.Web.Controllers.ChargeStations.Dtos;
using Station.Web.Controllers.Users.Dtos;
using Station.Web.Dtos;
using Station.Web.Host.Extentions;
using Station.Web.Services;
using Station.Web.Services.CurrentUserServices;
using Station.Web.Services.PermissionRequirementHandlers;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Station.Web.Controllers.ChargeStations
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ChargeStationController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IChargeStationsManager _stationsManager;

        private readonly ICurrentUserService _currentUserService;


        public ChargeStationController(IMapper mapper,
            ApplicationDbContext dbContext,
              IConfiguration configuration,
              IChargeStationsManager stationsManager,
              ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _configuration = configuration;
            _stationsManager = stationsManager;
            _currentUserService = currentUserService;
        }

        [PermissionAttribute(new[] { PermissionActionEnum.View })]
        [HttpGet("getAll")]
        public async Task<ChargeStationResponse> GetAll([FromQuery] DataInput input)
        {
            IQueryable<ChargeStation> query = _dbContext.ChargeStations
                .Include(s => s.Owner)
                .Include(s => s.Connectors)
                    .ThenInclude(c => c.ConnectorUiStatus);


            if (!string.IsNullOrWhiteSpace(input.FilterText))
            {
                input.FilterText = input.FilterText?.ToLower().Trim();
                var queryFilter = query
                    .Where(st => st.SerialNumber.ToLower().Contains(input.FilterText) ||
                    st.Owner.Name.ToLower().Contains(input.FilterText) ||
                     st.Name.ToLower().Contains(input.FilterText)).AsNoTracking().AsQueryable();

                query = queryFilter;
            }
            if (input.FilterOwnerId != null)
            {
                var queryFilter = query.Where(o => o.OwnerId == input.FilterOwnerId);
                query = queryFilter;
            }

            var count = await query.CountAsync();


            IList<ChargeStation> sortQuery = await GetSortQuery(input, query);
 
            var map = _mapper.Map<List<ChargeStationDto>>(sortQuery);

            foreach (var station in map)
            {
                foreach (var connector in station.Connectors)
                {
                    connector.ChargeStation = null;
                }

            }

            return new ChargeStationResponse() { ChargeStations = map, Total = count };
        }
        [HttpPost("update-create")]
        public async Task<OutputCreateResponse> UpdateOrCreate([FromBody] ChargeStationDto stationDto)
        {
            var res = new OutputCreateResponse();
            var curentUserEmail = _currentUserService.Email;
            var curentUserName = HttpContext.User.Identity.Name;
            var curentUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == curentUserEmail);

            if (stationDto.Id == 0)
            {
                res = await Create(stationDto, res, curentUser);
            }
            else
            {                         
                var stationUpdate = _mapper.Map<ChargeStation>(stationDto);

                _dbContext.ChargeStations.Update(stationUpdate);
                await _dbContext.SaveChangesAsync();
            }
            return res;
        }

        [HttpGet("getUpdateStatusesAsync")]
        public async Task<StationResponse> GetUpdateStatusesAsync()
        {
            var stations = await _stationsManager.GetUpdateStatusesAsync();

            return stations;
        }

        private async Task<OutputCreateResponse> Create(ChargeStationDto stationDto, 
            OutputCreateResponse response, User curentUser)
        {
            var error = "";

            var station = _mapper.Map<ChargeStation>(stationDto);
            station.OwnerId = null;

            var result = await _dbContext.AddAsync(station);
            await _dbContext.SaveChangesAsync();

            response.StationId = result.Entity.Id;
            return response;

        }

        private async Task<IList<ChargeStation>> GetSortQuery(DataInput input, IQueryable<ChargeStation> query)
        {
            var parse = input?.Sorting?.Split(" ");

            IList<ChargeStation>? sortQuery = null;

            if (parse != null && parse.Count() > 1)
            {
                var type = parse[0].First().ToString().ToUpper() + parse[0].Substring(1);
                var propertyInfo = typeof(User).GetProperty(type);

                switch (parse[1])
                {
                    case "asc":
                        sortQuery = await query.OrderByField(type, true).Skip((int)(input?.Skip)).
                             Take((int)(input?.Rows)).AsNoTracking().ToListAsync();

                        return sortQuery;
                    case "desc":
                        sortQuery = await query.OrderByField(type, false).Skip((int)(input?.Skip)).
                            Take((int)(input?.Rows)).AsNoTracking().ToListAsync();
                        return sortQuery;

                }
            }
            else
            {
                sortQuery = await query.OrderBy(p => p.SerialNumber).Skip((int)(input?.Skip)).
                    Take((int)(input?.Rows)).AsNoTracking().ToListAsync();
            }
            return sortQuery;
        }

        //[ProducesResponseType(typeof(StationResponse), 200)]
        //[HttpGet("getAllUpdate")]
        //public async Task<IActionResult> GetAllBySignalR()
        //{
        //    if (!_timerControl.IsTimerStarted)
        //        _timerControl.ScheduleTimer(async () =>
        //        await _stationHub.Clients.All.SendAsync("SendStationData", GetStationByFilter()), 20000);


        //    return Ok((new { Message = "Synchronized" }));
        //}
    }
}
