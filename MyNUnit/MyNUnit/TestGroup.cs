using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyNUnit.Attributes;
using System;

namespace MyNUnit
{
    public class TestGroup
    {
        public IEnumerable<IMethod> BeforeClassMethods { get; }
        public IEnumerable<IMethod> BeforeMethods { get; }
        public IEnumerable<ITestMethod> TestMethods { get; }
        public IEnumerable<IMethod> AfterMethods { get; }
        public IEnumerable<IMethod> AfterClassMethods { get; }

        public TestGroup()
        {
            BeforeClassMethods = Enumerable.Empty<IMethod>();
            BeforeMethods = Enumerable.Empty<IMethod>();
            TestMethods = Enumerable.Empty<ITestMethod>();
            AfterMethods = Enumerable.Empty<IMethod>();
            AfterClassMethods = Enumerable.Empty<IMethod>();
        }
        
        private TestGroup(
            IEnumerable<MethodBase> methods,
            TestAttributes testAttributes)
        {
            var beforeClassMethods = new List<IMethod>();
            var beforeMethods = new List<IMethod>();
            var testMethods = new List<ITestMethod>();
            var afterMethods = new List<IMethod>();
            var afterClassMethods = new List<IMethod>();

            var attributeProcessMap = new Dictionary<Type, Action<MethodBase>>
            {
                {testAttributes.BeforeClassAttribute, method => beforeClassMethods.Add(MyMethod.NewInstance(method))},
                {testAttributes.BeforeAttribute, method => beforeMethods.Add(MyMethod.NewInstance(method))},
                {testAttributes.TestAttribute, method =>
                    {
                        var testAttr = (TestAttribute) Attribute.GetCustomAttribute(method, testAttributes.TestAttribute);
                        testMethods.Add(TestMethod.NewInstance(method, testAttr.IgnoreReason, testAttr.ExpectedEceptionType));
                    }
                },
                {testAttributes.AfterAttribute, method => afterMethods.Add(MyMethod.NewInstance(method))},
                {testAttributes.AfterClassAttribute, method => afterClassMethods.Add(MyMethod.NewInstance(method))}
            };
            
            foreach (var method in methods)
            {
                foreach (var attribute in method.GetCustomAttributes(true)
                    .OfType<BaseAttribute>()
                    .Select(attr => attr.GetType()))
                {
                    attributeProcessMap[attribute].Invoke(method);
                }
            }

            BeforeClassMethods = beforeClassMethods;
            BeforeMethods = beforeMethods;
            TestMethods = testMethods;
            AfterMethods = afterMethods;
            AfterClassMethods = afterClassMethods;
        }

        public static TestGroup NewFrom(IEnumerable<MethodBase> methods, TestAttributes testAttributes) =>
            new TestGroup(methods, testAttributes);
    }
}
