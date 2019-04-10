using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Samhammer.Utils.Collection;
using Xunit;

namespace Samhammer.Utils.Test.Collection
{
    public class CollectionExtensionsTest
    {
        [Theory]
        [InlineData(null, true)]
        [InlineData(new object[] { }, true)]
        [InlineData(new object[] { "asd", "def" }, false)]
        public void IsNullOrEmpty(IEnumerable<object> collection, bool expected)
        {
            Assert.Equal(expected, collection.IsNullOrEmpty());
        }

        [Theory]
        [InlineData(null, new[] { "ROLI" })]
        [InlineData(new string[] { }, new[] { "ROLI" })]
        [InlineData(new[] { "" }, new[] { "" })]
        [InlineData(new[] { "asd", "def" }, new[] { "asd", "def" })]
        public void DefaultIfNullOrEmpty(IEnumerable<string> collection, IEnumerable<string> expected)
        {
            var result = collection.DefaultIfNullOrEmpty("ROLI").ToList();
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(new[] { "asd", null }, true, null)]
        [InlineData(new[] { "asd" }, false, null)]
        [InlineData(new[] { "ROLI" }, true)]
        [InlineData(new[] { "roli" }, true)]
        [InlineData(new string[] { }, false)]
        [InlineData(new[] { "" }, false)]
        [InlineData(new[] { "asd", "def" }, false)]
        [InlineData(new[] { "asd", null }, false)]
        public void Contains(IEnumerable<string> collection, bool expected, string value = "ROLI")
        {
            var result = collection.Contains(value, StringComparison.OrdinalIgnoreCase);
            result.Should().Be(expected);
        }
    }
}
