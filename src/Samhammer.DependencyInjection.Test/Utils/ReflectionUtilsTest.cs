using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Samhammer.DependencyInjection.Utils;
using Xunit;

namespace Samhammer.DependencyInjection.Test.Utils
{
    public class ReflectionUtilsTest
    {
        [Theory]
        [InlineData(typeof(ClassAttribute), true, new[] { typeof(TestClass), typeof(ChildOfTestClass) })]
        [InlineData(typeof(ClassAttribute), false, new[] { typeof(TestClass) })]
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

        [Fact]
        public void GetConstants()
        {
            var actual = ReflectionUtils.GetConstants<string>(typeof(TestConstantClass)).ToList();

            var expected = new List<string>
            {
                TestConstantClass.PublicConstStringVarName,
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetPropertyPath()
        {
            var expected = new List<string>
            {
                "TestClassA.AProp1",
                "TestClassA.TestClassB.BProp1",
                "TestClassA.TestClassB.TestClassC.CProp1",
            };

            var testClass = new TestClassA();
            var result = new List<string>();

            ReflectionUtils.GetPropertyPath(testClass.GetType(), nameof(TestClassA), result);

            expected.Should().BeEquivalentTo(result);
        }
    }

#pragma warning disable CS0414, SA1401, SA1402, IDE0044
    [AttributeUsage(AttributeTargets.Class)]
    public class ClassAttribute : Attribute
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

    public class TestClassA
    {
        public TestClassB TestClassB { get; set; }

        public int AProp1 { get; set; }
    }

    public class TestClassB
    {
        public TestClassC TestClassC { get; set; }

        public List<string> BProp1 { get; set; }
    }

    public class TestClassC
    {
        public string CProp1 { get; set; }
    }

    public class TestConstantClass
    {
        public const string PublicConstStringVarName = "PublicConstStringVarName";
        public const int PublicConstIntVarName = 1;
        public static string PublicStaticStringVarName = "PublicStaticStringVarName";
        public string PublicStringVarName = "PublicStringVarName";

        // ReSharper disable once UnusedMember.Local
        private const string PrivateConstStringVarName = "PrivateConstStringVarName";
        private static string privateStaticStringVarName = "PrivateStaticStringVarName";
        private string privateStringVarName = "PrivateStringVarName";
    }
#pragma warning restore CS0414, SA1401, SA1402, IDE0044
}
