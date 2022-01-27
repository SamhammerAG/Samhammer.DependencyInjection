using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Handlers;
using Samhammer.DependencyInjection.Test.TestData.FactoryClass;
using Xunit;

namespace Samhammer.DependencyInjection.Test.Handlers
{
    public class FactoryServiceDescriptorHandlerTest
    {
        private readonly FactoryServiceDescriptorHandler handler;

        public FactoryServiceDescriptorHandlerTest()
        {
            handler = new FactoryServiceDescriptorHandler();
        }

        [Fact]
        private void GetFactoryMethods_FromFactory()
        {
            // act
            var methods = handler.GetFactoryMethods(typeof(Factory));

            // assert
            methods.Should().HaveCount(1);
            methods.First().Should().Match(x => x.Name.Equals(nameof(Factory.Build)));
        }
    }
}
