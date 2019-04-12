using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Test.TestData.InjectedList
{
    [Inject]
    public class ServiceInherited : IServiceInherited
    {
    }

    public interface IServiceInherited : IService
    {
    }
}
