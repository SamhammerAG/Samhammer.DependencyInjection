using System;
using System.Diagnostics.CodeAnalysis;
using Samhammer.DependencyInjection.Override.Attributes;

namespace Samhammer.DependencyInjection.Override.Test.TestData.FactoryClass
{
    [Override("myOverride")]
    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1206:Declaration keywords should follow order", Justification = "Test class")]
    public class FactoryOverride : Factory
    {
        public new static IParentClassFromFactory Build(IServiceProvider serviceProvider)
        {
            return new OverrideClassFromFactory();
        }

        private static IParentClassFromFactory BuildWithPrivateMethodInvalid(IServiceProvider serviceProvider)
        {
            return new OverrideClassFromFactory();
        }

        public new IParentClassFromFactory BuildWithInstanceMethodInvalid(IServiceProvider serviceProvider)
        {
            return new OverrideClassFromFactory();
        }

        public new static IParentClassFromFactory BuildWithoutServiceProviderInvalid()
        {
            return new OverrideClassFromFactory();
        }

        public new static void HelperFunction(int number)
        {
        }

        public new static string HelperFunction2(int number)
        {
            return string.Empty;
        }
    }
}
