using FluentAssertions;
using Samhammer.Utils.String;
using Xunit;

namespace Samhammer.Utils.Test.String
{
    public class StringExtensionsTest
    {
        [Theory]
        [InlineData("UserModel", "Model", "User")]
        [InlineData("abc", "b", "ac")]
        public void RemoveString(string input, string remove, string expected)
        {
            var result = input.RemoveString(remove);
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("UserModel", "userModel")]
        [InlineData("U", "u")]
        public void ToLowerFirstChar(string input, string expected)
        {
            var result = input.ToLowerFirstChar();
            result.Should().Be(expected);
        }
    }
}
