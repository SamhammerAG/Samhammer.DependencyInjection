using System;
using System.Collections.Generic;
using System.Reflection;
using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Providers
{
    public interface ITypeResolvingStrategy
    {
        IEnumerable<Type> ResolveTypesByAttribute(IEnumerable<Assembly> assemblies, Type attributeType);
        
        Type GetMatchingInterfaceType(Type implementationType, InjectAttribute injectAttribute);
    }
}
