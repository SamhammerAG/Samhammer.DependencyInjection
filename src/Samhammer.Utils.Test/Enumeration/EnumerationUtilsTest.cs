using FluentAssertions;
using Samhammer.Utils.Enumeration;
using Xunit;

namespace Samhammer.Utils.Test.Enumeration
{
    public class EnumerationUtilsTest
    {
        [Theory]
        [InlineData("Red", Color.Red, null)]
        [InlineData("green", Color.Green, null)]
        [InlineData("Red", Color.Red, Color.Green)]
        [InlineData("Fail", default(Color), null)]
        [InlineData("Fail", Color.Blue, Color.Blue)]
        public void CheckParse(string testValue, Color expected, Color? testDefault)
        {
            var actual = testDefault.HasValue
                ? EnumerationUtils.Parse<Color>(testValue, testDefault.Value)
                : EnumerationUtils.Parse<Color>(testValue);

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("Red", true)]
        [InlineData("red", true)]
        [InlineData("Fail", false)]
        public void CheckIsDefined(string value, bool expected)
        {
            var actual = EnumerationUtils.IsDefined<Color>(value);

            actual.Should().Be(expected);
        }

        public enum Color
        {
            Red,
            Green,
            Blue,
        }
    }
}
