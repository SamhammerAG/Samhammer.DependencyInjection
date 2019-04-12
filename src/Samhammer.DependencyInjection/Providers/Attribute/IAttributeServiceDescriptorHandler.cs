using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Abstractions;

namespace Samhammer.DependencyInjection.Providers.Attribute
{
    public interface IAttributeServiceDescriptorHandler
    {
        IEnumerable<ServiceDescriptor> ResolveServices(Type implementationType, DependencyInjectionAttribute attribute);

        bool MatchAttribute(DependencyInjectionAttribute attribute);
    }
}
