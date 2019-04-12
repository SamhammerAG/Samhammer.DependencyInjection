using System;
using System.Collections.Generic;
using FluentAssertions;
using Samhammer.DependencyInjection.Utils;
using Xunit;

namespace Samhammer.DependencyInjection.Test.Utils
{
    public class ReflectionUtilsTest
    {
        [Theory]
        [InlineData(typeof(ClassAttribute), true, new[] { typeof(TestClass), typeof(ChildOfTestClass), typeof(TestClass2) })]
        [InlineData(typeof(ClassAttribute), false, new[] { typeof(TestClass), typeof(TestClass2) })]
        [InlineData(typeof(Class2Attribute), false, new[] { typeof(TestClass2) })]
        public void FindAllExportedTypesWithAttribute(Type attribute, bool inherit, IEnumerable<Type> expectedTypes)
        {
            var assemblies = new[] { typeof(ReflectionUtilsTest).Assembly };

            var result = ReflectionUtils.FindAllExportedTypesWithAttribute(assemblies, attribute, inherit);

            result.Should().BeEquivalentTo(expectedTypes);
        }

        [Theory]
        [InlineData(typeof(TestClass), new[] { typeof(ChildOfTestClass) })]
        [InlineData(typeof(IInterface), new[] { typeof(TestClass), typeof(ChildOfTestClass) })]
        [InlineData(typeof(IInterfaceGeneric<TestClass>), new[] { typeof(TestClass), typeof(ChildOfTestClass) })]
        [InlineData(typeof(IInterfaceGeneric<>), new[] { typeof(TestClass), typeof(ChildOfTestClass) })]
        public void FindAllExportedTypesWithParentType(Type parentType, IEnumerable<Type> expectedTypes)
        {
            var assemblies = new[] { typeof(ReflectionUtilsTest).Assembly };

            var result = ReflectionUtils.FindAllExportedTypesWithParentType(assemblies, parentType);

            result.Should().BeEquivalentTo(expectedTypes);
        }
    }

#pragma warning disable CS0414, SA1401, SA1402, IDE0044
    [AttributeUsage(AttributeTargets.Class)]
    public class ClassAttribute : Attribute
    {
    }

    public class Class2Attribute : ClassAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Interface)]
    public class InterfaceAttribute : Attribute
    {
    }

    public interface IInterface
    {
    }

    // ReSharper disable once UnusedTypeParameter
    public interface IInterfaceGeneric<T>
    {
    }

    [Interface]
    public interface IInterfaceWithAttribute
    {
    }

    public interface IChildOfInterfaceWithAttribute : IInterfaceWithAttribute
    {
    }

    [Class]
    public class TestClass : IInterface, IInterfaceGeneric<TestClass>, IInterfaceWithAttribute
    {
    }

    public class ChildOfTestClass : TestClass
    {
    }

    [Class2]
    public class TestClass2
    {
    }
#pragma warning restore CS0414, SA1401, SA1402, IDE0044
}
