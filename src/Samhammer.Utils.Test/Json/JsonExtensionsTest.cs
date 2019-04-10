using FluentAssertions;
using Xunit;
using Samhammer.Utils.Json;

namespace Samhammer.Utils.Test.Json
{
    public class JsonExtensionsTest
    {
        [Fact]
        public void ToJson_ShouldUseCamelCaseAndIndent()
        {
            var contract = new TestContract
            {
                PublicKey = "text",
                Flag = true,
            };

            var expected = "{\r\n  \"publicKey\": \"text\",\r\n  \"flag\": true\r\n}";

            var result = contract.ToJson();
            result.Should().Be(expected);
        }

        private class TestContract
        {
            public string PublicKey { get; set; }

            public bool Flag { get; set; }
        }
    }
}
