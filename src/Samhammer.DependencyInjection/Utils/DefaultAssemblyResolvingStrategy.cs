using System.Collections.Generic;
using System.Reflection;
using Samhammer.DependencyInjection.Providers;

namespace Samhammer.DependencyInjection.Utils
{
    public class DefaultAssemblyResolvingStrategy : IAssemblyResolvingStrategy
    {
        public IEnumerable<Assembly> ResolveAssemblies()
        {
            return AssemblyUtils.LoadAllAssembliesOfProject();
        }
    }
}
