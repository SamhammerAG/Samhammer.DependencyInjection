using System;
using Microsoft.Extensions.DependencyInjection;

namespace Samhammer.DependencyInjection.Abstractions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectAttribute : Attribute
    {
        public ServiceLifetime LifeTime { get; set; }

        public InjectAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            LifeTime = lifetime;
        }
    }
}
