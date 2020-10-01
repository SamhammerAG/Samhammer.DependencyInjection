using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Abstractions.Attributes;

namespace Samhammer.DependencyInjection.Handlers
{
    public class InjectMatchingServiceDescriptorHandler : AttributeServiceDescriptorHandler<InjectAttribute>
    {
        private ILogger<InjectMatchingServiceDescriptorHandler> Logger { get; }

        public InjectMatchingServiceDescriptorHandler(ILogger<InjectMatchingServiceDescriptorHandler> logger)
        {
            Logger = logger;
        }

        public override bool MatchAdditionalCriteria(InjectAttribute attribute)
        {
            return attribute.Target == Target.Matching;
        }

        public override IEnumerable<ServiceDescriptor> ResolveServices(Type implementationType, InjectAttribute injectAttribute)
        {
            var matchingInterfaceName = $"I{implementationType.GetTypeInfo().Name}";
            var serviceType = implementationType.GetInterface(matchingInterfaceName);

            if (serviceType == null)
            {
                Logger.LogError("Class {ServiceImpl} has no matching interface {InterfaceName} defined", implementationType, matchingInterfaceName);
                throw new ArgumentException($"Class {implementationType} has no matching interface {matchingInterfaceName} defined", nameof(implementationType));
            }

            var serviceDescriptor = new ServiceDescriptor(serviceType, implementationType, injectAttribute.LifeTime);
            yield return serviceDescriptor;
        }
    }
}
