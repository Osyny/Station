namespace Station.Web.Services.CurrentUserServices
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        string Email { get; }
    }
}
