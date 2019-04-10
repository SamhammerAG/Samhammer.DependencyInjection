using Samhammer.DependencyInjection.Abstractions;

namespace Samhammer.DependencyInjection.Test.TestData.InjectedClass
{
    [Inject]
    public class ClassDefaultLifetime : IClassDefaultLifetime
    {
    }

    public interface IClassDefaultLifetime
    {
    }
}
