

using Station.Core.Entities;
using Station.Core.Entities.Identities;
using System.IdentityModel.Tokens.Jwt;

namespace Station.Web.Services.JwtProviders
{
    public interface IJwtProvider
    {
        string GenerateTokenAsync(User user, PermissionsClaim permissionsClaim, Role userRole);
    }
}
