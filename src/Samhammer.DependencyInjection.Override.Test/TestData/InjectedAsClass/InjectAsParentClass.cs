using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Override.Test.TestData.InjectedAsClass
{
    [InjectAs(typeof(IServiceToRegister))]
    public class InjectAsParentClass : IServiceToRegister, IServiceNotRegister
    {
    }

    public interface IServiceToRegister
    {
    }

    public interface IServiceNotRegister
    {
    }
}
