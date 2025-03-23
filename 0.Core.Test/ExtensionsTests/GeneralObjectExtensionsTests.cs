using _0.Core.Extensions;

namespace _0.Core.Test.ExtensionsTests
{
    public class GeneralObjectExtensionsTests
    {
        [Theory]
        [InlineData("a")]
        [InlineData(5)]
        [InlineData(5.55)]
        public void HasValue_ShouldReturnTrue_WhenObjectIsNotNull(object? obj)
        {
            bool result = obj.HasValue();

            Assert.True(result);
        }

        [Fact]
        public void HasValue_ShouldReturnFalse_WhenObjectIsNull()
        {
            object? obj = null;

            bool result = obj.HasValue();

            Assert.False(result);
        }
    }
}
