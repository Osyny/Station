using Station.Core.Entities.Identities;
using Station.Core.Entities;

namespace Station.Web.Dtos.IdentityDtos
{
    public class UserRoleDto : EntityDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }


        public  RoleDto Role { get; set; }
        public  UserDto User { get; set; }
    }
}
