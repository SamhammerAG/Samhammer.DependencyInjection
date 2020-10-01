using Samhammer.DependencyInjection.Abstractions.Attributes;

namespace Samhammer.DependencyInjection.Test.TestData.InjectedAsClass
{
    [InjectAs(typeof(IServiceToRegister))]
    public class ClassWithSpecificService : IServiceToRegister, IServiceNotRegister
    {
    }

    public interface IServiceToRegister
    {
    }

    public interface IServiceNotRegister
    {
    }
}
