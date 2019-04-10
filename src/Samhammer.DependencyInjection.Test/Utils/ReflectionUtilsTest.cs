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
        [Fact]
        public void FindAllExportedTypesWithAttribute()
        {
            var assemblies = new[] { typeof(ReflectionUtilsTest).Assembly };
            var attribute = typeof(TestAttribute);

            var result = ReflectionUtils.FindAllExportedTypesWithAttribute(assemblies, attribute);

            Assert.Contains(typeof(FirstClass), result);
            Assert.Contains(typeof(FirstClassChild), result);
            Assert.DoesNotContain(typeof(SecondClass), result);
        }

        [Fact]
        public void FindAllExportedTypesWithParentType()
        {
            var assemblies = new[] { typeof(ReflectionUtilsTest).Assembly };
            var parentType = typeof(FirstClass);

            var result = ReflectionUtils.FindAllExportedTypesWithParentType(assemblies, parentType);

            Assert.Contains(typeof(FirstClassChild), result);
            Assert.DoesNotContain(typeof(FirstClass), result);
            Assert.DoesNotContain(typeof(SecondClass), result);
        }

        [Fact]
        public void FindAllExportedTypesWithInterface()
        {
            var assemblies = new[] { typeof(ReflectionUtilsTest).Assembly };
            var parentType = typeof(IFirst);

            var result = ReflectionUtils.FindAllExportedTypesWithParentType(assemblies, parentType);

            Assert.Contains(typeof(FirstClass), result);
            Assert.Contains(typeof(FirstClassChild), result);
            Assert.DoesNotContain(typeof(SecondClass), result);
        }

        [Fact]
        public void FindAllExportedTypesWithInterfaceGeneric()
        {
            var assemblies = new[] { typeof(ReflectionUtilsTest).Assembly };
            var parentType = typeof(IFirstGeneric<FirstClass>);

            var result = ReflectionUtils.FindAllExportedTypesWithParentType(assemblies, parentType);

            Assert.Contains(typeof(FirstClass), result);
            Assert.Contains(typeof(FirstClassChild), result);
            Assert.DoesNotContain(typeof(SecondClass), result);
        }

        [Fact]
        public void FindAllExportedTypesWithInterfaceGenericDefinition()
        {
            var assemblies = new[] { typeof(ReflectionUtilsTest).Assembly };
            var parentType = typeof(IFirstGeneric<>);

            var result = ReflectionUtils.FindAllExportedTypesWithParentType(assemblies, parentType);

            Assert.Contains(typeof(FirstClass), result);
            Assert.Contains(typeof(FirstClassChild), result);
            Assert.DoesNotContain(typeof(SecondClass), result);
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

#pragma warning disable CS0414, SA1401, SA1402
    [AttributeUsage(AttributeTargets.Class)]
    public class TestAttribute : Attribute
    {
    }

    public interface IFirst
    {
    }

    public interface IFirstGeneric<T>
    {
    }

    [Test]
    public class FirstClass : IFirst, IFirstGeneric<FirstClass>
    {
    }

    public class FirstClassChild : FirstClass
    {
    }

    public class SecondClass
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
#pragma warning restore CS0414, SA1401, SA1402
}
