using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Samhammer.DependencyInjection.Providers
{
    public interface IAssemblyResolvingStrategy
    {
        IEnumerable<Assembly> ResolveAssemblies();
    }
}
