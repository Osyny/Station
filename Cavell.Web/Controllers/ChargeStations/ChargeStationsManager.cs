using AutoMapper;
using MySql.Data.MySqlClient;
using Station.Core.Entities;
using Station.Web.Controllers.ChargeStations.Dtos;
using Station.Web.Dtos;
using System.Data;

namespace Station.Web.Controllers.ChargeStations
{
    public class ChargeStationsManager : IChargeStationsManager
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private string connectionStr = "";
        public ChargeStationsManager(IConfiguration configuration,
            IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public StationResponse GetUpdateStatuses()
        {
            List<ChargeStation> stations = new List<ChargeStation>();
            ChargeStation station;

            var data = GetDetailsFromDb();
            foreach (DataRow row in data.Result.Rows)
            {
                station = new ChargeStation
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Status = Convert.ToInt32(row["Status"]) == 1 ? true : false,
                };
                stations.Add(station);
            }

            var map = _mapper.Map<List<ChargeStationDto>>(stations);

            return new StationResponse() { ChargeStations = map };
        }

        private async Task<DataTable> GetDetailsFromDb()
        {

            var connectionString = _configuration.GetConnectionString("Default");
            if (connectionString != null)
            {
                connectionStr = connectionString;
            }
            string dbStr = "stationdb";
            if (connectionStr.Contains("localhost"))
            {
                dbStr = "LocalStationDB";
            }
            var query = $"SELECT Id, Status FROM {dbStr}.ChargeStations";
            DataTable dataTable = new DataTable();

            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionStr))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            dataTable.Load(rdr);
                        }
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
