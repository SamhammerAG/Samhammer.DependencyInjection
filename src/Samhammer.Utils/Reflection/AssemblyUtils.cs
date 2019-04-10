using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace Samhammer.Utils.Reflection
{
    public class AssemblyUtils
    {
        public static List<Assembly> LoadAllAssembliesOfProject()
        {
            var dependencyContext = DependencyContext.Default;
            return dependencyContext == null ? LoadAssembliesByEntryAssembly() : LoadAssembliesByDependencyContext(dependencyContext);
        }

        private static List<Assembly> LoadAssembliesByEntryAssembly()
        {
            var assembly = Assembly.GetEntryAssembly();
            var dependencyNames = assembly.GetReferencedAssemblies();
            var assemblies = new List<Assembly> { assembly };

            foreach (var dependencyName in dependencyNames)
            {
                try
                {
                    // Try to load the referenced assembly...
                    assemblies.Add(Assembly.Load(dependencyName));
                }
                catch
                {
                    // Failed to load assembly. Skip it.
                }
            }

            return assemblies;
        }

        private static List<Assembly> LoadAssembliesByDependencyContext(DependencyContext context)
        {
            var assemblyNames = context.RuntimeLibraries
                .Where(c => c.Type.Equals("project"))
                .Select(d => d.Name);

            return assemblyNames
                .Select(a => Assembly.Load(new AssemblyName(a)))
                .ToList();
        }
    }
}
