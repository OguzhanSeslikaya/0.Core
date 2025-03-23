using _0.Core.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _0.Core.Test.GeneralHelpersTests
{
    public class TokenHelperTests
    {
        [Theory]
        [InlineData("UserId", "123")]
        public void GetUserIdFromExpiredToken_ShouldReturnClaim_WhenTokenIsValidButExpired(string claimName, string claimValue)
        {
            var securityKey = "securityKeysecurityKeysecurityKeysecurityKeysecurityKeysecurityKey";

            var expiredToken = GenerateJsonWebToken(claimName, claimValue, securityKey);

            var result = TokenHelper.GetUserIdFromExpiredToken(expiredToken, claimName, securityKey);

            Assert.NotNull(result);
            Assert.Equal(claimName, result.Type);
            Assert.Equal(claimValue, result.Value);
        }

        [Theory]
        [InlineData("invalid.token.string", "UserId", "123")]
        [InlineData("a", "UserId", "123")]
        [InlineData("", "UserId", "123")]
        public void GetUserIdFromExpiredToken_ShouldReturnNull_WhenTokenIsInvalid(string invalidToken, string claimName, string claimValue)
        {
            var securityKey = "securityKeysecurityKeysecurityKeysecurityKeysecurityKeysecurityKey";

            var result = TokenHelper.GetUserIdFromExpiredToken(invalidToken, claimName, securityKey);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("denemeName", "denemeValue")]
        public void GetClaims_ShouldReturnClaims_WhenTokenIsValid(string claimName1, string claimValue1)
        {
            var securityKey = "securityKeysecurityKeysecurityKeysecurityKeysecurityKeysecurityKey";

            string validToken = GenerateJsonWebToken(claimName1, claimValue1, securityKey);

            IEnumerable<Claim> claims = TokenHelper.GetClaims(validToken);

            Assert.NotEmpty(claims);
            Assert.Contains(claims, c => c.Type == claimName1 && c.Value == claimValue1);
        }

        [Fact]
        public void GetClaims_ShouldReturnEmptyList_WhenTokenIsInvalid()
        {
            string invalidToken = "invalid.token.string";

            IEnumerable<Claim> claims = TokenHelper.GetClaims(invalidToken);

            Assert.Empty(claims);
        }

        private string GenerateJsonWebToken(string claimName, string claimValue, string securityKey)
        {
            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(claimName, claimValue)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
