using Samhammer.DependencyInjection.Abstractions.Attributes;

namespace Samhammer.DependencyInjection.Test.TestData.InjectedClass
{
    [Inject(Target.All)]
    public class ClassWithMultipleInterfaces : IClassMultiple1, IClassMultiple2
    {
    }

    public interface IClassMultiple1
    {
    }

    public interface IClassMultiple2
    {
    }
}
