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

        private TestGroup(
            IEnumerable<MethodBase> methods,
            TestAttributes testAttributes)
        {
            var beforeClassMethods = new List<IMethod>();
            var beforeMethods = new List<IMethod>();
            var testMethods = new List<ITestMethod>();
            var afterMethods = new List<IMethod>();
            var afterClassMethods = new List<IMethod>();

            foreach (var method in methods)
            {
                var methodAttributes = method.GetCustomAttributes(true).Select(attr => attr.GetType()).ToList();
                if (methodAttributes.Contains(testAttributes.BeforeClassAttribute))
                {
                    beforeClassMethods.Add(MyMethod.NewInstance(method));
                }
                else if (methodAttributes.Contains(testAttributes.BeforeAttribute))
                {
                    beforeMethods.Add(MyMethod.NewInstance(method));
                }
                else if (methodAttributes.Contains(testAttributes.TestAttribute))
                {
                    var testAttr = (TestAttribute) Attribute.GetCustomAttribute(method,
                                                                                testAttributes.TestAttribute);
                    testMethods.Add(TestMethod.NewInstance(method, testAttr.IgnoreReason, testAttr.ExpectedEceptionType));
                }
                else if (methodAttributes.Contains(testAttributes.AfterAttribute))
                {
                    afterMethods.Add(MyMethod.NewInstance(method));
                }
                else if (methodAttributes.Contains(testAttributes.AfterClassAttribute))
                {
                    afterClassMethods.Add(MyMethod.NewInstance(method));
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

        public bool IsEmpty() => !TestMethods.Any();
    }
}
