using Station.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Station.Web.Dtos
{
    public class OwnerDto : EntityDto
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }

       // public List<ChargeStationDto> ChargeStations { get; set; }
    }
}
