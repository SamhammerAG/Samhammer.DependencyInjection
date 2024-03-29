﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Handlers
{
    public class InjectAllServiceDescriptorHandler : AttributeServiceDescriptorHandler<InjectAttribute>
    {
        public override bool MatchAdditionalCriteria(InjectAttribute attribute)
        {
            return attribute.Target == Target.All;
        }

        public override IEnumerable<ServiceDescriptor> ResolveServices(Type implementationType, InjectAttribute injectAttribute)
        {
            var serviceTypes = implementationType.GetInterfaces().ToList();

            if (serviceTypes.Count == 0)
            {
                throw new ArgumentException($"Class {implementationType} has no interfaces defined", nameof(implementationType));
            }

            foreach (var serviceType in serviceTypes)
            {
                var serviceDescriptor = new ServiceDescriptor(serviceType, implementationType, injectAttribute.LifeTime);
                yield return serviceDescriptor;
            }
        }
    }
}
