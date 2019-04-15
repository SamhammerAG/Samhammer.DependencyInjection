using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Handlers
{
    public interface IAttributeServiceDescriptorHandler
    {
        bool MatchAttribute(DependencyInjectionAttribute attribute);

        IEnumerable<ServiceDescriptor> ResolveServices(Type implementationType, DependencyInjectionAttribute attribute);
    }
}
