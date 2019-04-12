using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Providers.Attribute;
using Samhammer.DependencyInjection.Test.TestData.FactoryClass;
using Xunit;

namespace Samhammer.DependencyInjection.Test.Providers.Attribute
{
    public class FactoryServiceDescriptorHandlerTest
    {
        private readonly FactoryServiceDescriptorHandler handler;

        public FactoryServiceDescriptorHandlerTest()
        {
            var logger = NSubstitute.Substitute.For<ILogger<FactoryServiceDescriptorHandler>>();
            handler = new FactoryServiceDescriptorHandler(logger);
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
