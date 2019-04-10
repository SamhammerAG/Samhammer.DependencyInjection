using System;
using Microsoft.Extensions.DependencyInjection;

namespace Samhammer.DependencyInjection.Abstractions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FactoryAttribute : Attribute
    {
        public ServiceLifetime LifeTime { get; set; }

        public FactoryAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            LifeTime = lifetime;
        }
    }
}
