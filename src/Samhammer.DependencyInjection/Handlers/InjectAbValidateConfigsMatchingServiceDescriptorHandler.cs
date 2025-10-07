using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection;
using Samhammer.DependencyInjection.Attributes;
using Samhammer.DependencyInjection.Handlers;

namespace Samhammer.DependencyInjection.Handlers
{
    public class InjectAbValidateConfigsMatchingServiceDescriptorHandler : AttributeServiceDescriptorHandler<InjectAbWithConfigsAttribute>
    {
        private DependencyResolverOptions Options { get; }

        public InjectAbValidateConfigsMatchingServiceDescriptorHandler(DependencyResolverOptions options)
        {
            Options = options;
        }

        public override bool MatchAdditionalCriteria(InjectAbWithConfigsAttribute attribute)
        {
            return attribute.Target == Target.Matching;
        }

        public override IEnumerable<ServiceDescriptor> ResolveServices(Type implementationType, InjectAbWithConfigsAttribute injectAttribute)
        {
            var serviceType = GetMatchingInterfaceType(implementationType, injectAttribute);
            var serviceDescriptor = new ServiceDescriptor(serviceType, provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var requiredConfigs = injectAttribute.ConfigSections.Split(',');
                foreach (var requiredConfig in requiredConfigs)
                {
                    if (string.IsNullOrWhiteSpace(configuration[requiredConfig]))
                    {
                        throw new InvalidOperationException($"Please setup config section {requiredConfig} to enable this service");
                    }
                }

                var instance = ActivatorUtilities.CreateInstance(provider, implementationType);
                return instance;
            }, injectAttribute.LifeTime);
            yield return serviceDescriptor;
        }

        private Type GetMatchingInterfaceType(Type implementationType, InjectAbWithConfigsAttribute injectAttribute)
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
