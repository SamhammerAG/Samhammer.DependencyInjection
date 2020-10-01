using System;
using Microsoft.Extensions.DependencyInjection;

namespace Samhammer.DependencyInjection.Abstractions.Attributes
{
    /// <summary>
    /// Registers instances of factory method(s) as service to the method return type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FactoryAttribute : DependencyInjectionAttribute
    {
        public FactoryAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
            : base(lifetime)
        {
        }
    }
}
