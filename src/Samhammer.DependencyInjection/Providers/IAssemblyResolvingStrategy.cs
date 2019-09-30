using System.Collections.Generic;
using System.Reflection;

namespace Samhammer.DependencyInjection.Providers
{
    public interface IAssemblyResolvingStrategy
    {
        IEnumerable<Assembly> ResolveAssemblies();
    }
}
