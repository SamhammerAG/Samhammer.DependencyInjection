using System.Collections.Generic;
using System.Reflection;
using Samhammer.DependencyInjection.Providers;
using Samhammer.DependencyInjection.Utils;

namespace Samhammer.DependencyInjection.Strategy
{
    public class DefaultAssemblyResolvingStrategy : IAssemblyResolvingStrategy
    {
        public IEnumerable<Assembly> ResolveAssemblies()
        {
            return AssemblyUtils.LoadAllAssembliesOfProject();
        }
    }
}
