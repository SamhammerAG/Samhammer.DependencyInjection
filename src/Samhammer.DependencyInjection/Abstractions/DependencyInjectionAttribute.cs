using System;
using Microsoft.Extensions.DependencyInjection;

namespace Samhammer.DependencyInjection.Abstractions
{
    public abstract class DependencyInjectionAttribute : Attribute
    {
        public ServiceLifetime LifeTime { get; set; }

        public DependencyInjectionAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            LifeTime = lifetime;
        }
    }
}
