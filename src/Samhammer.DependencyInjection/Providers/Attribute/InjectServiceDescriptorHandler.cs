using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Abstractions;

namespace Samhammer.DependencyInjection.Providers.Attribute
{
    public class InjectServiceDescriptorHandler : IAttributeServiceDescriptorHandler
    {
        private ILogger<InjectServiceDescriptorHandler> Logger { get; }

        public InjectServiceDescriptorHandler(ILogger<InjectServiceDescriptorHandler> logger)
        {
            Logger = logger;
        }

        public bool MatchAttribute(DependencyInjectionAttribute attribute)
        {
            return attribute.GetType() == typeof(InjectAttribute);
        }

        public IEnumerable<ServiceDescriptor> ResolveServices(Type implementationType, DependencyInjectionAttribute attribute)
        {
            var injectAttribute = (InjectAttribute) attribute;
            var serviceTypes = implementationType.GetInterfaces().ToList();

            if (serviceTypes.Count == 0)
            {
                Logger.LogWarning("Implementation {ServiceImpl} has no interfaces defined", implementationType);
            }

            foreach (var serviceType in serviceTypes)
            {
                var serviceDescriptor = new ServiceDescriptor(serviceType, implementationType, injectAttribute.LifeTime);
                yield return serviceDescriptor;
            }
        }
    }
}
