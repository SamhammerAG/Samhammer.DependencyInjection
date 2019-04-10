namespace Samhammer.DependencyInjection.Test.TestData.InjectedClass
{
    public class ClassInheritedLifetimeSingleton : ClassSingletonLifetime, IClassInheritedLifetimeSingleton
    {
    }

    public interface IClassInheritedLifetimeSingleton
    {
    }
}
