using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text.Json;

namespace Station.Web.Services.LogTo
{
    public class LogToService
    {
        private readonly IConfiguration _configuration;

        public LogToService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<RsaSecurityKey> GetRsaSigningKeyAsync()
        {
            string jwksUri = _configuration["Authentication:LogTo:JwksUri"];
            JsonElement jwksJson = await FetchJwksAsync(jwksUri);
            RsaSecurityKey signingKey = GetSigningKey(jwksJson);
            return signingKey;
        }

        private async Task<JsonElement> FetchJwksAsync(string jwksUri)
        {
            using var httpClient = new HttpClient();
            var jwksResponse = await httpClient.GetStringAsync(jwksUri);
            return JsonDocument.Parse(jwksResponse).RootElement;
        }
        private RsaSecurityKey GetSigningKey(JsonElement jwks)
        {
            foreach (var key in jwks.GetProperty("keys").EnumerateArray())
            {
                if (key.GetProperty("kty").GetString() != "RSA")
                {
                    continue;
                }
                var rsaKey = new RsaSecurityKey(new RSAParameters
                {
                    Modulus = Base64UrlEncoder.DecodeBytes(key.GetProperty("n").GetString()),
                    Exponent = Base64UrlEncoder.DecodeBytes(key.GetProperty("e").GetString())
                })
                {
                    KeyId = key.GetProperty("kid").GetString()
                };
                return rsaKey;
            }
            return null;
        }
    }
}
