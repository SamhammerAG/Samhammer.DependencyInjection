using Samhammer.DependencyInjection.Abstractions.Attributes;

namespace Samhammer.DependencyInjection.Test.TestData.InjectedList
{
    [InjectAs(typeof(IService))]
    public class ServiceInherited : IServiceInherited
    {
    }

    public interface IServiceInherited : IService
    {
    }
}
