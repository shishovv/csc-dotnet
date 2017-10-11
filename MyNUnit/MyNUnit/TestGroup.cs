using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyNUnit.Attributes;

namespace MyNUnit
{
    public class TestGroup
    {
        public IReadOnlyCollection<MethodBase> BeforeClassMethods { get; }
        public IReadOnlyCollection<MethodBase> BeforeMethods { get; }
        public IReadOnlyCollection<ITestMethod> TestMethods { get; }
        public IReadOnlyCollection<MethodBase> AfterMethods { get; }
        public IReadOnlyCollection<MethodBase> AfterClassMethods { get; }

        private TestGroup(
            IEnumerable<MethodBase> methods,
            TestAttributes testAttributes)
        {
            var beforeClassMethods = new List<MethodBase>();
            var beforeMethods = new List<MethodBase>();
            var testMethods = new List<ITestMethod>();
            var afterMethods = new List<MethodBase>();
            var afterClassMethods = new List<MethodBase>();

            foreach (var method in methods)
            {
                var methodAttributes = method.GetCustomAttributes().Select(attr => attr.GetType()).ToList();
                if (methodAttributes.Contains(testAttributes.BeforeClassAttribute))
                {
                    beforeClassMethods.Add(method);
                }
                else if (methodAttributes.Contains(testAttributes.BeforeAttribute))
                {
                    beforeMethods.Add(method);
                }
                else if (methodAttributes.Contains(testAttributes.TestAttribute))
                {
                    var testAttr = (TestAttribute) method.GetCustomAttribute(testAttributes.TestAttribute);
                    testMethods.Add(new TestMethod(method, testAttr.IgnoreReason, testAttr.ExpectedEceptionType));
                }
                else if (methodAttributes.Contains(testAttributes.AfterAttribute))
                {
                    afterMethods.Add(method);
                }
                else if (methodAttributes.Contains(testAttributes.AfterClassAttribute))
                {
                    afterClassMethods.Add(method);
                }
            }

            BeforeClassMethods = beforeClassMethods.AsReadOnly();
            BeforeMethods = beforeMethods.AsReadOnly();
            TestMethods = testMethods.AsReadOnly();
            AfterMethods = afterMethods.AsReadOnly();
            AfterClassMethods = afterClassMethods.AsReadOnly();
        }

        public static TestGroup NewFrom(IEnumerable<MethodBase> methods, TestAttributes testAttributes)
        {
            return new TestGroup(methods, testAttributes);
        }

        public bool IsEmpty()
        {
            return !TestMethods.Any();
        }
    }
}
