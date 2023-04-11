using Samhammer.DependencyInjection.Test.TestData.InjectedClass;

namespace Samhammer.DependencyInjection.Test.TestData.DecorateClass
{
    public class ClassDefaultLifetimeDecorate : IClassDefaultLifetime
    {
        public ClassDefaultLifetimeDecorate(IClassDefaultLifetime classDefaultLifetime)
        {
        }
    }
}
