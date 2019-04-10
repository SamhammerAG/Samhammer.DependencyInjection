using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Abstractions;

namespace Samhammer.DependencyInjection.Test.TestData.InjectedClass
{
    [Inject(ServiceLifetime.Transient)]
    public class ClassOverridenLifetimeTransient : ClassSingletonLifetime, IClassOverridenLifetimeTransient
    {
    }

    public interface IClassOverridenLifetimeTransient
    {
    }
}
