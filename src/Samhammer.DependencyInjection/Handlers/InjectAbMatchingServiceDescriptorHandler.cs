using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection;
using Samhammer.DependencyInjection.Attributes;
using Samhammer.DependencyInjection.Handlers;

namespace Samhammer.DependencyInjection.Handlers
{
    public class InjectAbMatchingServiceDescriptorHandler : AttributeServiceDescriptorHandler<InjectAbAttribute>
    {
        private DependencyResolverOptions Options { get; }

        public InjectAbMatchingServiceDescriptorHandler(DependencyResolverOptions options)
        {
            Options = options;
        }

        public override bool MatchAdditionalCriteria(InjectAbAttribute attribute)
        {
            return attribute.Target == Target.Matching;
        }

        public override IEnumerable<ServiceDescriptor> ResolveServices(Type implementationType, InjectAbAttribute injectAttribute)
        {
            var serviceType = GetMatchingInterfaceType(implementationType, injectAttribute);
            var serviceDescriptor = new ServiceDescriptor(serviceType, provider =>
            {
                var instance = ActivatorUtilities.CreateInstance(provider, implementationType);
                return instance;
            }, injectAttribute.LifeTime);
            yield return serviceDescriptor;
        }

        private Type GetMatchingInterfaceType(Type implementationType, InjectAbAttribute injectAttribute)
        {
            var matchingInterfaceName = $"I{implementationType.GetTypeInfo().Name}";
            var serviceType = implementationType.GetInterface(matchingInterfaceName);

            if (serviceType == null)
            {
                throw new ArgumentException($"Class {implementationType} has no matching interface {matchingInterfaceName} defined", nameof(implementationType));
            }

            return serviceType;
        }
    }
}
