using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Abstractions;

namespace Samhammer.DependencyInjection.Test.TestData.InjectedClass
{
    [Inject(ServiceLifetime.Singleton)]
    public class ClassSingletonLifetime : IClassSingletonLifetime
    {
    }

    public interface IClassSingletonLifetime
    {
    }
}
