using System;
using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Override.Test.TestData.FactoryClass
{
    [Factory]
    public class Factory
    {
        public static IParentClassFromFactory Build(IServiceProvider serviceProvider)
        {
            return new ParentClassFromFactory();
        }

        private static IParentClassFromFactory BuildWithPrivateMethodInvalid(IServiceProvider serviceProvider)
        {
            return new ParentClassFromFactory();
        }

        public IParentClassFromFactory BuildWithInstanceMethodInvalid(IServiceProvider serviceProvider)
        {
            return new ParentClassFromFactory();
        }

        public static IParentClassFromFactory BuildWithoutServiceProviderInvalid()
        {
            return new ParentClassFromFactory();
        }

        public static void HelperFunction(int number)
        {
        }

        public static string HelperFunction2(int number)
        {
            return string.Empty;
        }
    }
}
