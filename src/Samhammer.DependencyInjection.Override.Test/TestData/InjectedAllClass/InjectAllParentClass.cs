using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Override.Test.TestData.InjectedAllClass
{
    [Inject(Target.All)]
    public class InjectAllParentClass : IClassMultiple1, IClassMultiple2
    {
    }

    public interface IClassMultiple1
    {
    }

    public interface IClassMultiple2
    {
    }
}
