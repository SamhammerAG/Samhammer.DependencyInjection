using System;
using Microsoft.Extensions.DependencyInjection;

namespace Samhammer.DependencyInjection.Abstractions.Attributes
{
    /// <summary>
    /// Registers this class with interface(s) as service.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectAttribute : DependencyInjectionAttribute
    {
        public InjectAttribute(Target target = Target.Matching, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            : base(lifetime)
        {
            Target = target;
        }

        public Target Target { get; }
    }

    public enum Target
    {
        /// <summary>
        /// Registers only to interface with matching name. (IClassName)
        /// </summary>
        Matching,

        /// <summary>
        /// Registers to all implemented interfaces.
        /// </summary>
        All,
    }
}
