using System.Reflection;
using Samhammer.Utils.Reflection;
using Xunit;

namespace Samhammer.Utils.Test.Reflection
{
    public class AssemblyUtilsTest
    {
        [Fact]
        public void LoadAllAssembliesOfProject()
        {
            var result = AssemblyUtils.LoadAllAssembliesOfProject();
            var assemblyUtils = typeof(AssemblyUtils).Assembly;
            var assemblyUtilsTest = typeof(AssemblyUtilsTest).Assembly;
            Assert.Contains(assemblyUtils, result);
            Assert.Contains(assemblyUtilsTest, result);
        }
    }
}
