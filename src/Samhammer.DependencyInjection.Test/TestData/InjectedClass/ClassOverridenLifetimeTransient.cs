using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Attributes;

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
