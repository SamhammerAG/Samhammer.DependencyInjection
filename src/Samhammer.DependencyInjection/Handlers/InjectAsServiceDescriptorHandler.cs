using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Handlers
{
    public class InjectAsServiceDescriptorHandler : AttributeServiceDescriptorHandler<InjectAsAttribute>
    {
        private ILogger<InjectAsServiceDescriptorHandler> Logger { get; }

        public InjectAsServiceDescriptorHandler(ILogger<InjectAsServiceDescriptorHandler> logger)
        {
            Logger = logger;
        }

        public override IEnumerable<ServiceDescriptor> ResolveServices(Type implementationType, InjectAsAttribute injectAttribute)
        {
            if (injectAttribute.ServiceType == null)
            {
                Logger.LogError("Class {ServiceImpl} has NULL value in serviceType of attribute {Attribute}.", implementationType.GetType(), injectAttribute.GetType());
                throw new ArgumentNullException(nameof(injectAttribute.ServiceType));
            }

            var serviceDescriptor = new ServiceDescriptor(injectAttribute.ServiceType, implementationType, injectAttribute.LifeTime);
            yield return serviceDescriptor;
        }
    }
}
