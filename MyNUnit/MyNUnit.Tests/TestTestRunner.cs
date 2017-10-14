using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;

namespace MyNUnit.Tests
{
    [TestFixture]
    public class TestTestRunner
    {
        //private static readonly int BEFORE_CLASS = 0;
        //private static readonly int BEFORE = 1;
        //private static readonly int TEST = 2;
        //private static readonly int AFTER = 3;
        //private static readonly int AFTER_CLASS = 4;

        private TestRunner _testRunner;

        [SetUp]
        public void SetUp() 
        {
            _testRunner = new TestRunner();
        }

        [Test]
        public void TestExecution() 
        {
            var counter = 0;
            var method = Substitute.For<IMethod>();
            //method.When(m => m.Invoke(Arg.Any<Object>(), Arg.Any<Object[]>())).Do(_ => counter++);
            var testMethod = Substitute.For<ITestMethod>();
            //testMethod.When(m => m.Invoke(Arg.Any<Object>(), Arg.Any<Object[]>())).Do(_ => counter++);
            testMethod.Ignored().Returns(false);
            testMethod.GetName().Returns("test");
            testMethod.GetExpectedExceptionType().Returns(default(Type));
            var testGroup = Substitute.For<TestGroup>();
            testGroup.BeforeClassMethods.Returns(Enumerable.Repeat(method, 1));
            testGroup.BeforeMethods.Returns(Enumerable.Repeat(method, 2));
            testGroup.TestMethods.Returns(Enumerable.Repeat(testMethod, 3));
            testGroup.AfterMethods.Returns(Enumerable.Repeat(method, 4));
            testGroup.AfterClassMethods.Returns(Enumerable.Repeat(method, 5));
            Assert.AreEqual(15, counter);
        }
    }
}
