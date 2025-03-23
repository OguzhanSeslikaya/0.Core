using System.Security.Claims;

namespace _0.Core.GeneralServices.TokenServices
{
    public partial interface ITokenService
    {
        string CreateJsonWebToken(List<Claim> claims, int expiration);
        string CreateRefreshToken();
    }
}
