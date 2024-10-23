using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Station.Core;
using Station.Core.Entities;
using Station.Web.Controllers.ChargeStations.Dtos;
using Station.Web.Controllers.Users.Dtos;
using Station.Web.Dtos;
using Station.Web.Host.Extentions;
using Station.Web.Services;
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


        public ChargeStationController(IMapper mapper,
            ApplicationDbContext dbContext,
              IConfiguration configuration)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _configuration = configuration;

        }

       

        [HttpGet("getAll")]
        public async Task<ChargeStationResponse> GetAll([FromQuery] DataInput input)
        {
            IQueryable<ChargeStation> query = _dbContext.ChargeStations.Include(s => s.Owner);


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


            return new ChargeStationResponse() { ChargeStations = map, Total = count };
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
