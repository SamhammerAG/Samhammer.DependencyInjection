using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Samhammer.DependencyInjection.Utils
{
    public static class ReflectionUtils
    {
        public static List<Type> FindAllExportedTypesWithAttribute(IEnumerable<Assembly> assemblies, Type attributeType, bool inherit = true)
        {
            var exportedTypes = assemblies
                .SelectMany(a => a.ExportedTypes)
                .Where(t => t.GetTypeInfo().IsDefined(attributeType, inherit))
                .ToList();

            return exportedTypes;
        }

        public static List<Type> FindAllExportedTypesWithParentType(IEnumerable<Assembly> assemblies, Type parentType)
        {
            var exportedTypes = assemblies
                .SelectMany(a => a.ExportedTypes)
                .Where(t => t.InheritsFrom(parentType))
                .ToList();

            return exportedTypes;
        }

        private static bool InheritsFrom(this Type type, Type baseType)
        {
            return baseType.IsInterface
                ? InheritsFromInterface(type, baseType)
                : InheritsFromClass(type, baseType);
        }

        private static bool InheritsFromClass(this Type type, Type baseType)
        {
            Type currentBaseType = type.BaseType;

            while (currentBaseType != null)
            {
                if (currentBaseType == baseType)
                {
                    return true;
                }

                currentBaseType = currentBaseType.BaseType;
            }

            return false;
        }

        private static bool InheritsFromInterface(this Type type, Type baseInterfaceType)
        {
            foreach (var interfaceType in type.GetInterfaces())
            {
                if (interfaceType == baseInterfaceType)
                {
                    return true;
                }

                if (interfaceType.IsGenericType && baseInterfaceType.IsGenericType)
                {
                    if (baseInterfaceType == interfaceType.GetGenericTypeDefinition())
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
