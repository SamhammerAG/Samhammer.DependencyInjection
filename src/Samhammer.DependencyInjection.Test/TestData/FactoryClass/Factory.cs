using System;
using Samhammer.DependencyInjection.Abstractions;

namespace Samhammer.DependencyInjection.Test.TestData.FactoryClass
{
    [Factory]
    public class Factory
    {
        public static IClassFromFactory Build(IServiceProvider serviceProvider)
        {
            return new ClassFromFactory();
        }

        private static IClassFromFactory BuildWithPrivateMethodInvalid(IServiceProvider serviceProvider)
        {
            return new ClassFromFactory();
        }

        public IClassFromFactory BuildWithInstanceMethodInvalid(IServiceProvider serviceProvider)
        {
            return new ClassFromFactory();
        }

        public static IClassFromFactory BuildWithoutServiceProviderInvalid()
        {
            return new ClassFromFactory();
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
