using System;
using Microsoft.Extensions.DependencyInjection;

namespace Samhammer.DependencyInjection.Abstractions.Attributes
{
    /// <summary>
    /// Registers this class with specific service.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectAsAttribute : DependencyInjectionAttribute
    {
        public Type ServiceType { get; }

        public InjectAsAttribute(Type serviceType, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            : base(lifetime)
        {
            ServiceType = serviceType;
        }
    }
}
