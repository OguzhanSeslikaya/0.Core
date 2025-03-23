using _0.Core.Helpers;

namespace _0.Core.Test.GeneralHelpersTests
{
    public class CacheHelperTests
    {
        [Theory]
        [InlineData("", null, "", null)]
        [InlineData("", "")]
        [InlineData("")]
        public void GenerateKey_ShouldReturnEmptyString_WhenAllValuesAreNullOrEmpty(string expected, params object[] keyParams)
        {
            var result = CacheHelper.GenerateKey(keyParams);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("string_42_3,14_True", "string", 42, 3.14, true)]
        public void GenerateKey_ShouldHandleDifferentDataTypesCorrectly(string expected, params object[] keyParams)
        {
            var result = CacheHelper.GenerateKey(keyParams);

            Assert.Equal(expected, result);
        }
    }
}
