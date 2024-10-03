using System.ComponentModel.DataAnnotations;

namespace Station.Web.Dtos
{
    public class UserDto : EntityDto
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public required string LastName { get; set; }
        public bool IsActive { get; set; }

        public string Email { get; set; }

        public string HashPasword { get; set; }
        public required string RoleName { get; set; }
    }
}
