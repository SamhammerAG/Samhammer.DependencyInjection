using Samhammer.DependencyInjection.Override.Attributes;

namespace Samhammer.DependencyInjection.Override.Test.TestData.InjectedAsClass
{
    [Override("myOverride")]
    public class InjectAsOverrideClass : InjectAsParentClass
    {
    }
}
