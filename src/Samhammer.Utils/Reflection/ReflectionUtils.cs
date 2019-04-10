using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Samhammer.Utils.Reflection
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

        public static IEnumerable<T> GetConstants<T>(Type type)
        {
            return type.GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(x => x.IsLiteral && !x.IsInitOnly && x.FieldType == typeof(T))
                .Select(x => x.GetValue(null)).Cast<T>();
        }

        public static void GetPropertyPath(Type objType, string parentName, List<string> types)
        {
            if (IsChildType(objType))
            {
                parentName = $"{parentName}.{objType.Name}";
                types.Add(parentName);
                return;
            }

            var properties = objType.GetProperties().OrderBy(x => x.Name);

            foreach (var propertyInfo in properties)
            {
                GetPropertyPath(propertyInfo, parentName, types);
            }
        }

        private static void GetPropertyPath(PropertyInfo prop, string parentName, List<string> types)
        {
            if (IsChildType(prop.PropertyType))
            {
                parentName = $"{parentName}.{prop.Name}";
                types.Add(parentName);
                return;
            }

            GetPropertyPath(prop.PropertyType, $"{parentName}.{prop.Name}", types);
        }

        private static bool IsChildType(Type objType)
        {
            return objType.IsPrimitive
                   || objType == typeof(string)
                   || (objType.IsGenericType && objType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                   || (objType.IsGenericType && objType.GetGenericTypeDefinition() == typeof(List<>));
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
