using _0.Core.Helpers;
using _0.Core.Test.GeneralHelpersTests.Dtos;

namespace _0.Core.Test.GeneralHelpersTests
{
    public class ConversationHelperTests
    {
        public static IEnumerable<object[]> ValidDictionaryDataForToObject =>
            new List<object[]>
            {
                    new object[]
                    {
                        new Dictionary<string, string>
                        {
                            { "Name", "John" },
                            { "Age", "30" }
                        },
                        "John", 30
                    },
                    new object[]
                    {
                        new Dictionary<string, string>
                        {
                            { "Name", "Rick" },
                            { "Age", "29" }
                        },
                        "Rick", 29
                    }
            };

        [Theory]
        [MemberData(nameof(ValidDictionaryDataForToObject))]
        public void ToObject_ShouldMapValidDataCorrectly(Dictionary<string, string> data, string expectedName, int expectedAge)
        {
            var result = ConversationHelper.ToObject<ConversationHelpersToObjectTestClass>(data);

            Assert.Equal(expectedName, result.Name);
            Assert.Equal(expectedAge, result.Age);
        }
    }
}
