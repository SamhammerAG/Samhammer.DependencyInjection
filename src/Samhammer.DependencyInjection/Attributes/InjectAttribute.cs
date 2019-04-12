using System;
using Microsoft.Extensions.DependencyInjection;

namespace Samhammer.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectAttribute : DependencyInjectionAttribute
    {
        public InjectAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
            : base(lifetime)
        {
        }
    }
}
