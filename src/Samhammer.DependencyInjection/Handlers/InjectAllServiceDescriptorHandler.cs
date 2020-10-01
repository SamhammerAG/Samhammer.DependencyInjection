using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Abstractions.Attributes;

namespace Samhammer.DependencyInjection.Handlers
{
    public class InjectAllServiceDescriptorHandler : AttributeServiceDescriptorHandler<InjectAttribute>
    {
        private ILogger<InjectAllServiceDescriptorHandler> Logger { get; }

        public InjectAllServiceDescriptorHandler(ILogger<InjectAllServiceDescriptorHandler> logger)
        {
            Logger = logger;
        }

        public override bool MatchAdditionalCriteria(InjectAttribute attribute)
        {
            return attribute.Target == Target.All;
        }

        public override IEnumerable<ServiceDescriptor> ResolveServices(Type implementationType, InjectAttribute injectAttribute)
        {
            var serviceTypes = implementationType.GetInterfaces().ToList();

            if (serviceTypes.Count == 0)
            {
                Logger.LogError("Class {ServiceImpl} has no interfaces defined", implementationType);
                throw new ArgumentException(nameof(implementationType));
            }

            foreach (var serviceType in serviceTypes)
            {
                var serviceDescriptor = new ServiceDescriptor(serviceType, implementationType, injectAttribute.LifeTime);
                yield return serviceDescriptor;
            }
        }
    }
}
