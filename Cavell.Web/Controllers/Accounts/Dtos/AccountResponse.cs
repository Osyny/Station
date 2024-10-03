using Station.Web.Dtos;

namespace Station.Web.Controllers.Accounts.Dtos
{
    public class AccountResponse
    {
        public UserDto User { get; set; }
        public string Token { get; set; }

    }
}
