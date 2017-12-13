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
//        private static int counter = 0;
//
//        [Test]
//        public void TestExecution() 
//        {  
//            var testMethod = Substitute.For<ITestMethod>();
//            testMethod.When(m => m.Invoke(Arg.Any<Object>(), Arg.Any<Object[]>())).Do(_ => counter++);
//            testMethod.IsIgnored.Returns(false);
//            testMethod.Name.Returns("test");
//            testMethod.ExpectedExceptionType.Returns(default(Type));
            
//            var testGroup = Substitute.For<TestGroup>();
//            var tmp = Enumerable.Repeat((IMethod) new MethodMock(), 1);
//            testGroup.BeforeClassMethods.Returns(Enumerable.Empty<IMethod>());
//            testGroup.BeforeMethods.Returns(Enumerable.Repeat((IMethod) new MethodMock(), 2));
//            testGroup.TestMethods.Returns(Enumerable.Repeat((ITestMethod) new TestMethodMock(), 3));
//            testGroup.AfterMethods.Returns(Enumerable.Repeat((IMethod) new MethodMock(), 4));
//            testGroup.AfterClassMethods.Returns(Enumerable.Repeat((IMethod) new MethodMock(), 5));
//            Assert.AreEqual(15, counter);
//        }
        
//        private class MethodMock: IMethod
//        {
//            public void Invoke(object obj, object[] args)
//            {
//                counter++;
//            }
//        }

//        private class TestMethodMock : ITestMethod
//        {
//            public bool IsIgnored { get; }
//            public string IgnoreReason { get; }
//            public Type ExpectedExceptionType { get; }
//            public string Name { get; }
//            
//            public void Invoke(object obj, object[] args)
//            {
//                counter++;
//            }
//        }
    }
}
