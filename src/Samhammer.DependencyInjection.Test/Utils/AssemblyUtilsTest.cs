using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Samhammer.DependencyInjection.Attributes;
using Samhammer.DependencyInjection.Utils;
using Xunit;

namespace Samhammer.DependencyInjection.Test.Utils
{
    public class AssemblyUtilsTest
    {
        [Fact]
        public void LoadAllAssembliesOfProject()
        {
            var result = AssemblyUtils.LoadAllAssembliesOfProject();
            var expected = new List<Assembly> { typeof(AssemblyUtils).Assembly, typeof(AssemblyUtilsTest).Assembly, typeof(DependencyInjectionAttribute).Assembly };

            result.Should().BeEquivalentTo(expected);
        }
    }
}
