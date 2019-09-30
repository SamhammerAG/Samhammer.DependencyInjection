using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace Samhammer.DependencyInjection.Utils
{
    public class AssemblyUtils
    {
        public static IEnumerable<Assembly> LoadAllAssembliesOfProject()
        {
            var dependencyContext = DependencyContext.Default;
            return dependencyContext == null ? LoadAssembliesByEntryAssembly() : LoadAssembliesByDependencyContext(dependencyContext);
        }

        private static IEnumerable<Assembly> LoadAssembliesByEntryAssembly()
        {
            var assembly = Assembly.GetEntryAssembly();
            var dependencyNames = assembly.GetReferencedAssemblies();

            yield return assembly;

            Assembly loadedAssembly = null;
            foreach (var dependencyName in dependencyNames)
            {
                try
                {
                    // Try to load the referenced assembly...
                    loadedAssembly = Assembly.Load(dependencyName);
                }
                catch
                {
                    // Failed to load assembly. Skip it.
                    loadedAssembly = null;
                }

                if (loadedAssembly != null)
                {
                    yield return loadedAssembly;
                }
            }
        }

        private static IEnumerable<Assembly> LoadAssembliesByDependencyContext(DependencyContext context)
        {
            var assemblyNames = context.RuntimeLibraries
                .Where(c => c.Type.Equals("project"))
                .Select(d => d.Name);

            return assemblyNames.Select(a => Assembly.Load(new AssemblyName(a)));
        }
    }
}
