using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Abstractions;

namespace Samhammer.DependencyInjection.Providers.Attribute
{
    public class InjectServiceDescriptorHandler : AttributeServiceDescriptorHandler<InjectAttribute>
    {
        private ILogger<InjectServiceDescriptorHandler> Logger { get; }

        public InjectServiceDescriptorHandler(ILogger<InjectServiceDescriptorHandler> logger)
        {
            Logger = logger;
        }

        public override IEnumerable<ServiceDescriptor> ResolveServices(Type implementationType, InjectAttribute injectAttribute)
        {
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
