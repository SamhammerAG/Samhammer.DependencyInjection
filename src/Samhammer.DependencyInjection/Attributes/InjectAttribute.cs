using System;
using Microsoft.Extensions.DependencyInjection;

namespace Samhammer.DependencyInjection.Attributes
{
    /// <summary>
    /// Registers this class with interface(s) as service.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectAttribute : DependencyInjectionAttribute
    {
        public InjectAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped, Target target = Target.All)
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
