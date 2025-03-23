using _0.Core.Helpers;

namespace _0.Core.Test.GeneralHelpersTests
{
    public class CryptographyHelperTests
    {
        [Theory]
        [InlineData("185f8db32271fe25f561a6fc938b2e264306ec304eda518007d1764826381969", "Hello")]
        [InlineData("a7ae1a431366f6d25f302684d2a7b67cec7ef1d1787b1d11c7ff1192bb4bdf46", "Deneme")]
        public void ComputeSha256Hash_WithHello_ReturnsCorrectHash(string expected, string data)
        {
            string actualHash = CryptographyHelper.ComputeSha256Hash(data);

            Assert.Equal(expected, actualHash);
        }
    }
}
