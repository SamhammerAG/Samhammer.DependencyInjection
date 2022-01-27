using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Override.Test.TestData.FactoryClass;
using Samhammer.DependencyInjection.Override.Test.TestData.InjectedAllClass;
using Samhammer.DependencyInjection.Override.Test.TestData.InjectedAsClass;
using Samhammer.DependencyInjection.Override.Test.TestData.InjectedClass;
using Xunit;

namespace Samhammer.DependencyInjection.Override.Test
{
    public class DependencyResolverTest
    {
        private readonly IServiceCollection serviceCollection;

        private ServiceProvider serviceProvider;

        public DependencyResolverTest()
        {
            serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder.ClearProviders());
        }

        [Fact]
        private void ResolveDependencies_WithoutUseOverride()
        {
            // act
            Action act = () => serviceCollection.ResolveDependencies();

            // assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("Class Samhammer.DependencyInjection.Override.Test.TestData.InjectedClass.InjectOverrideClass has no matching interface IInjectOverrideClass defined (Parameter 'implementationType')");
        }

        [Fact]
        private void GetClass_InjectWithNonMatchingOverride()
        {
            // act
            serviceCollection.ResolveDependencies(o => { o.UseOverride("nonExistingOverride"); });
            serviceProvider = serviceCollection.BuildServiceProvider();

            IInjectParentClass service = serviceProvider.GetService<IInjectParentClass>();

            // assert
            service.Should().NotBeNull().And.BeOfType<InjectParentClass>();
        }
        
        [Fact]
        private void GetClass_InjectWithMatchingOverride()
        {
            // act
            serviceCollection.ResolveDependencies(o => { o.UseOverride("myOverride"); });
            serviceProvider = serviceCollection.BuildServiceProvider();

            IInjectParentClass service = serviceProvider.GetService<IInjectParentClass>();

            // assert
            service.Should().NotBeNull().And.BeOfType<InjectOverrideClass>();
        }
        
        [Fact]
        private void GetClass_InjectAsWithNonMatchingOverride()
        {
            // act
            serviceCollection.ResolveDependencies(o => { o.UseOverride("nonExistingOverride"); });
            serviceProvider = serviceCollection.BuildServiceProvider();

            IServiceToRegister service = serviceProvider.GetService<IServiceToRegister>();
            IServiceNotRegister serviceNotExpected = serviceProvider.GetService<IServiceNotRegister>();

            // assert
            service.Should().NotBeNull().And.BeOfType<InjectAsParentClass>();
            serviceNotExpected.Should().BeNull();
        }
        
        [Fact]
        private void GetClass_InjectAsWithMatchingOverride()
        {
            // act
            serviceCollection.ResolveDependencies(o => { o.UseOverride("myOverride"); });
            serviceProvider = serviceCollection.BuildServiceProvider();

            IServiceToRegister service = serviceProvider.GetService<IServiceToRegister>();
            IServiceNotRegister serviceNotExpected = serviceProvider.GetService<IServiceNotRegister>();

            // assert
            service.Should().NotBeNull().And.BeOfType<InjectAsOverrideClass>();
            serviceNotExpected.Should().BeNull();
        }
        
        [Fact]
        private void GetClass_FromFactoryWithNonMatchingOverride()
        {
            // act
            serviceCollection.ResolveDependencies(o => { o.UseOverride("nonExistingOverride"); });
            serviceProvider = serviceCollection.BuildServiceProvider();

            IParentClassFromFactory service = serviceProvider.GetService<IParentClassFromFactory>();

            // assert
            service.Should().NotBeNull().And.BeOfType<ParentClassFromFactory>();
        }
        
        [Fact]
        private void GetClass_FromFactoryWithMatchingOverride()
        {
            // act
            serviceCollection.ResolveDependencies(o => { o.UseOverride("myOverride"); });
            serviceProvider = serviceCollection.BuildServiceProvider();

            IParentClassFromFactory service = serviceProvider.GetService<IParentClassFromFactory>();

            // assert
            service.Should().NotBeNull().And.BeOfType<OverrideClassFromFactory>();
        }
        
        [Fact]
        private void GetClass_InjectAllWithNonMatchingOverride()
        {
            // act
            serviceCollection.ResolveDependencies(o => { o.UseOverride("nonExistingOverride"); });
            serviceProvider = serviceCollection.BuildServiceProvider();

            IClassMultiple1 service1 = serviceProvider.GetService<IClassMultiple1>();
            IClassMultiple2 service2 = serviceProvider.GetService<IClassMultiple2>();
            IClassMultiple3 service3 = serviceProvider.GetService<IClassMultiple3>();

            // assert
            service1.Should().NotBeNull().And.BeOfType<InjectAllParentClass>();
            service2.Should().NotBeNull().And.BeOfType<InjectAllParentClass>();
            service3.Should().BeNull();
        }
        
        [Fact]
        private void GetClass_InjectAllWithMatchingOverride()
        {
            // act
            serviceCollection.ResolveDependencies(o => { o.UseOverride("myOverride"); });
            serviceProvider = serviceCollection.BuildServiceProvider();

            IClassMultiple1 service1 = serviceProvider.GetService<IClassMultiple1>();
            IClassMultiple2 service2 = serviceProvider.GetService<IClassMultiple2>();
            IClassMultiple3 service3 = serviceProvider.GetService<IClassMultiple3>();

            // assert
            service1.Should().NotBeNull().And.BeOfType<InjectAllOverrideClass>();
            service2.Should().NotBeNull().And.BeOfType<InjectAllOverrideClass>();
            service3.Should().NotBeNull().And.BeOfType<InjectAllOverrideClass>();
        }
    }
}
