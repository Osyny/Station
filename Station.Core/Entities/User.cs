using Station.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Station.Core.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }
        public  string LastName { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string HashPasword { get; set; }
        public RoleEnum Role { get; set; }
    }
}
