using Station.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Station.Web.Controllers.Accounts.Dtos
{
    public class RegisterInput
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
        [Required]
        public required string UserName { get; set; }

        [Required]
        public RoleEnum Role { get; set; }
    }
}
