using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Override.Test.TestData.InjectedClass
{
    [Inject]
    public class InjectParentClass : IInjectParentClass
    {
    }

    public interface IInjectParentClass
    {
    }
}
