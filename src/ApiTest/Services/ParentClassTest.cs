using Samhammer.DependencyInjection.Attributes;

namespace ApiTest.Services
{
    [InjectAb]
    public class ParentClassTest : IParentClassTest
    {
        public ParentClassTest(IChildClassTest childClassTest)
        {
            Console.WriteLine("Creating test class");
        }

        public void PrintSomething()
        {
            Console.WriteLine("Printing in test class");
        }
    }

    public interface IParentClassTest
    {
        public void PrintSomething();
    }
}
