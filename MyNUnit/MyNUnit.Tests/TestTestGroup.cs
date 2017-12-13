using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using NSubstitute;
using System;

namespace MyNUnit.Tests
{
    [TestFixture]
    public class TestTestGroup
    {
        [Test]
        public void TestTestGroupInstantiation()
        {
            var testAttrs = TestAttributes.NewDefault();
            var testGroup = TestGroup.NewFrom(GetTestMethods(testAttrs), testAttrs);
            Assert.AreEqual(1, testGroup.BeforeClassMethods.Count());
            Assert.AreEqual(1, testGroup.BeforeMethods.Count());
            Assert.AreEqual(0, testGroup.TestMethods.Count());
            Assert.AreEqual(1, testGroup.AfterMethods.Count());
            Assert.AreEqual(1, testGroup.AfterClassMethods.Count());
        }

        private static IEnumerable<MethodBase> GetTestMethods(TestAttributes testAttributes)
        {
            var beforeClassMethod = Substitute.For<MethodBase>();
            beforeClassMethod.GetCustomAttributes(Arg.Any<bool>())
                .Returns(new[] { Activator.CreateInstance(testAttributes.BeforeClassAttribute) });

            var beforeMethod = Substitute.For<MethodBase>();
            beforeMethod.GetCustomAttributes(Arg.Any<bool>())
                .Returns(new[] { Activator.CreateInstance(testAttributes.BeforeAttribute) });

//            var testMethod = Substitute.For<MethodBase>();
//            testMethod.GetCustomAttributes(Arg.Any<bool>())
//                .Returns(new Object[] { Activator.CreateInstance(testAttributes.TestAttribute, new object[] {null, null}) });

            var afterMethod = Substitute.For<MethodBase>();
            afterMethod.GetCustomAttributes(Arg.Any<bool>())
                .Returns(new[] { Activator.CreateInstance(testAttributes.AfterAttribute) });

            var afterClassMethod = Substitute.For<MethodBase>();
            afterClassMethod.GetCustomAttributes(Arg.Any<bool>())
                .Returns(new[] { Activator.CreateInstance(testAttributes.AfterClassAttribute) });

            return new MethodBase[] {
                beforeClassMethod,
                beforeMethod,
                afterMethod,
                afterClassMethod };
        }
    }
}
