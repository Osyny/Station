using Station.Web.Dtos;

namespace Station.Web.Controllers.Users.Dtos
{
    public class UsersResponse
    {
        public List<UserDto> Users { get; set; }
        public int Total { get; set; }

    }
}
