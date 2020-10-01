using Samhammer.DependencyInjection.Abstractions.Attributes;

namespace Samhammer.DependencyInjection.Test.TestData.InjectedList
{
    [InjectAs(typeof(IService))]
    public class Service2 : IService
    {
    }
}
