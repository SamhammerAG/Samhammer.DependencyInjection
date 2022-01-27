using Samhammer.DependencyInjection.Override.Attributes;

namespace Samhammer.DependencyInjection.Override.Test.TestData.InjectedClass
{
    [Override("myOverride")]
    public class InjectOverrideClass : InjectParentClass
    {
    }
}
