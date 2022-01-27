using Samhammer.DependencyInjection.Override.Attributes;

namespace Samhammer.DependencyInjection.Override.Test.TestData.InjectedAllClass
{
    [Override("myOverride")]
    public class InjectAllOverrideClass : InjectAllParentClass, IClassMultiple3
    {
    }
    
    public interface IClassMultiple3
    {
    }
}
