using _0.Core.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _0.Core.Helpers
{
    public static class TokenHelper
    {
        public static Claim? GetUserIdFromExpiredToken(string token, string claimName, string securityKey)
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(securityKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal? principal = null;
            SecurityToken? securityToken = null;
            JwtSecurityToken? jwtSecurityToken = null;

            try
            {
                principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                jwtSecurityToken = securityToken as JwtSecurityToken;
            }
            catch
            {
                return null;
            }

            if (!jwtSecurityToken.HasValue() ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            Claim? claim = principal.Claims.FirstOrDefault(a => a.Type == claimName);
            if (!claim.HasValue())
                return null;

            return claim;
        }

        public static IEnumerable<Claim> GetClaims(string token)
        {
            try
            {
                if (!(new JwtSecurityTokenHandler().ReadToken(token) is JwtSecurityToken jwtSecurityToken))
                {
                    return [];
                }

                return jwtSecurityToken.Claims;
            }
            catch
            {
                return [];
            }
        }
    }
}
