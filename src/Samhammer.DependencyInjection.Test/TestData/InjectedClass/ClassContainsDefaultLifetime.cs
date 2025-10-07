using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Test.TestData.InjectedClass
{
    [Inject]
    public class ClassContainsDefaultLifetime : IClassContainsDefaultLifetime
    {
        public ClassContainsDefaultLifetime(IClassDefaultLifetime classDefault)
        {
            Console.WriteLine($"Creating service {classDefault.ToString()}");
        }
    }

    public interface IClassContainsDefaultLifetime
    {
    }
}
