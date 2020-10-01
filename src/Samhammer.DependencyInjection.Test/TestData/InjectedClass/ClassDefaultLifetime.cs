using Samhammer.DependencyInjection.Abstractions.Attributes;

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
