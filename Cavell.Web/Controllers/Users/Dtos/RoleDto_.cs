using Station.Web.Dtos;

namespace Station.Web.Controllers.Users.Dtos
{
    public class RoleDto_ : EntityDto
    {
        public long Id { get; set; }    
        public string Name { get; set; }    
        public string NormalizedName { get; set; }    
    }
}
