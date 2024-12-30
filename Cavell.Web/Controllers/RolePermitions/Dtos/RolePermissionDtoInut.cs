namespace Station.Web.Controllers.RolePermitions.Dtos
{
    public class RolePermissionDtoInut
    {
        public int RoleId { get; set; }
        public List<int> PermissionActionIds { get; set; }
    }
}
