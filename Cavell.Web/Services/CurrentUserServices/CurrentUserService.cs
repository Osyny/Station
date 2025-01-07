using System.Security.Claims;

namespace Station.Web.Services.CurrentUserServices
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            int.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
            UserId = userId;
            Email = httpContextAccessor.HttpContext?.User?.Claims?
                .Where(x => x.Type == "Email").FirstOrDefault()?.Value;
        }

        public int UserId { get; }
        public string Email { get; }
    }
}
