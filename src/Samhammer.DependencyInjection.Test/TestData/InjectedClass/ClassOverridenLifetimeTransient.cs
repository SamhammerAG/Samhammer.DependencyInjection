using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Abstractions.Attributes;

namespace Samhammer.DependencyInjection.Test.TestData.InjectedClass
{
    [Inject(lifetime: ServiceLifetime.Transient)]
    public class ClassOverridenLifetimeTransient : ClassSingletonLifetime, IClassOverridenLifetimeTransient
    {
    }

    public interface IClassOverridenLifetimeTransient
    {
    }
}
