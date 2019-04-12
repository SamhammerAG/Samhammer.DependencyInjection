using System;
using Microsoft.Extensions.DependencyInjection;

namespace Samhammer.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FactoryAttribute : DependencyInjectionAttribute
    {
        public FactoryAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
            : base(lifetime)
        {
        }
    }
}
