
using Microsoft.IdentityModel.Tokens;
using Station.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Station.Web.Services.JwtProviders
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration _configuration;
        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            Claim[] claims = [new("userName", user.UserName.ToString()),
            new("userRole", user.Role.ToString())];

            var key = _configuration["Authentication:JwtBearer:SecurityKey"];
            var encodingKey = Encoding.UTF8.GetBytes(key);
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(encodingKey), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddSeconds(12));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }

        //public string GenerateToken(User user)
        //{
        //    var identity = new ClaimsIdentity([new Claim(ClaimTypes.Role, user.Role.ToString()),
        //    new Claim(ClaimTypes.Name, user.UserName.ToString())]);

        //    var key = _configuration["Authentication:JwtBearer:SecurityKey"];
        //    var encodingKey = Encoding.UTF8.GetBytes(key);
        //    var signingCredentials = new SigningCredentials(
        //        new SymmetricSecurityKey(encodingKey), SecurityAlgorithms.HmacSha256);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = identity,
        //        Expires = DateTime.UtcNow.AddDays(1),
        //        SigningCredentials = signingCredentials,
        //    };
        //    //var token = new JwtSecurityToken(
        //    //    claims: claims,
        //    //    signingCredentials: signingCredentials,
        //    //    expires: DateTime.UtcNow.AddDays(1));
        //    var jwtTokenHandler = new JwtSecurityTokenHandler();
        //    var token = jwtTokenHandler.CreateToken(tokenDescriptor);



        //    return jwtTokenHandler.WriteToken(token);
        //}
    }
}
