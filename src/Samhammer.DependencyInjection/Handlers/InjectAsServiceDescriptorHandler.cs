using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Handlers
{
    public class InjectAsServiceDescriptorHandler : AttributeServiceDescriptorHandler<InjectAsAttribute>
    {
        public override IEnumerable<ServiceDescriptor> ResolveServices(Type implementationType, InjectAsAttribute injectAttribute)
        {
            if (injectAttribute.ServiceType == null)
            {
                throw new ArgumentNullException($"Class {implementationType} has NULL value in serviceType of attribute {injectAttribute.GetType()}", nameof(injectAttribute.ServiceType));
            }

            var serviceDescriptor = new ServiceDescriptor(injectAttribute.ServiceType, implementationType, injectAttribute.LifeTime);
            yield return serviceDescriptor;
        }
    }
}
