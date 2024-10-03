using System.ComponentModel.DataAnnotations;

namespace Station.Web.Controllers.Accounts.Dtos
{
    public class LoginInput
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
