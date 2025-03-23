using _0.Core.GeneralServices.TokenServices;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace _0.Core.Test.GeneralServicesTests.TokenServicesTests
{
    public class TokenServiceTests
    {
        [Fact]
        public void CreateJsonWebToken_ShouldReturnValidJwt_WhenValidClaimsAndExpirationAreProvided()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config["Token:SecurityKey"]).Returns("testSecurityKeytestSecurityKeytestSecurityKeytestSecurityKey");

            var tokenService = new TokenService(mockConfiguration.Object);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testUser"),
                new Claim(ClaimTypes.Role, "TestRole")
            };
            int expiration = 30;

            var token = tokenService.CreateJsonWebToken(claims, expiration);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            Assert.NotNull(jsonToken);
            Assert.Equal("testUser", jsonToken?.Claims.First(c => c.Type == ClaimTypes.Name).Value);
            Assert.Equal("TestRole", jsonToken?.Claims.First(c => c.Type == ClaimTypes.Role).Value);
            Assert.True(jsonToken?.ValidTo > DateTime.UtcNow);
            Assert.True(jsonToken?.ValidTo <= DateTime.UtcNow.AddMinutes(expiration));
        }

        [Fact]
        public void CreateRefreshToken_ShouldReturnBase64String_WhenCalled()
        {
            var tokenService = new TokenService(new Mock<IConfiguration>().Object);

            var refreshToken = tokenService.CreateRefreshToken();

            Assert.NotNull(refreshToken);
            Assert.False(string.IsNullOrEmpty(refreshToken));
            Assert.Matches("^[A-Za-z0-9+/=]*$", refreshToken);
        }
    }
}
