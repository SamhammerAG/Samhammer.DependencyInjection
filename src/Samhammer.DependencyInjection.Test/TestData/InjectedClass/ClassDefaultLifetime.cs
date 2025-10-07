using System;
using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Test.TestData.InjectedClass
{
    [Inject]
    public class ClassDefaultLifetime : IClassDefaultLifetime
    {
        public ClassDefaultLifetime()
        {
            Console.WriteLine("Creating service");
        }
    }

    public interface IClassDefaultLifetime
    {
    }
}
