using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Handlers
{
    public class InjectMatchingServiceDescriptorHandler : AttributeServiceDescriptorHandler<InjectAttribute>
    {
        private DependencyResolverOptions Options { get; }

        public InjectMatchingServiceDescriptorHandler(DependencyResolverOptions options)
        {
            Options = options;
        }

        public override bool MatchAdditionalCriteria(InjectAttribute attribute)
        {
            return attribute.Target == Target.Matching;
        }

        public override IEnumerable<ServiceDescriptor> ResolveServices(Type implementationType, InjectAttribute injectAttribute)
        {
            var serviceType = Options.TypeResolvingStrategy.GetMatchingInterfaceType(implementationType, injectAttribute);
            var serviceDescriptor = new ServiceDescriptor(serviceType, implementationType, injectAttribute.LifeTime);
            yield return serviceDescriptor;
        }
    }
}
