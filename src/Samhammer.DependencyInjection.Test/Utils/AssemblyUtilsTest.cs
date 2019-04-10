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
            var assemblyUtils = typeof(AssemblyUtils).Assembly;
            var assemblyUtilsTest = typeof(AssemblyUtilsTest).Assembly;
            Assert.Contains(assemblyUtils, result);
            Assert.Contains(assemblyUtilsTest, result);
        }
    }
}
