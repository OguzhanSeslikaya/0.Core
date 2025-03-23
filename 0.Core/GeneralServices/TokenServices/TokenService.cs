using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace _0.Core.GeneralServices.TokenServices
{
    public partial class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Creates a JSON Web Token (JWT) using the provided claims and expiration time.
        /// The method requires the SecurityKey from the configuration (_configuration["Token:SecurityKey"]) for signing the token.
        /// </summary>
        /// <param name="claims">A list of claims to be included in the JWT.</param>
        /// <param name="expiration">The expiration time of the JWT, in minutes.</param>
        /// <returns>A signed JWT as a string.</returns>
        public string CreateJsonWebToken(List<Claim> claims, int expiration)
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]!));
            SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new(
                expires: DateTime.UtcNow.AddMinutes(expiration),
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
                );

            JwtSecurityTokenHandler securityTokenHandler = new();

            return securityTokenHandler.WriteToken(securityToken);
        }

        /// <summary>
        /// Generates a new refresh token. The token is created using a secure random byte array 
        /// and is then converted to a Base64 encoded string.
        /// </summary>
        /// <returns>A Base64 encoded string representing a randomly generated refresh token.</returns>
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
