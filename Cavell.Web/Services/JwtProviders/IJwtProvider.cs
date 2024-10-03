

using Station.Core.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Station.Web.Services.JwtProviders
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
