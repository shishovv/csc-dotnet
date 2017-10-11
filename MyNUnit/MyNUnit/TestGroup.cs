using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyNUnit;

namespace MyNUnit
{
    public class TestGroup
    {
        public List<MethodBase> BeforeClassMethods { get; } = new List<MethodBase>();
        public List<MethodBase> BeforeMethods { get; } = new List<MethodBase>();
        public List<MethodBase> TestMethods { get; } = new List<MethodBase>();
        public List<MethodBase> AfterMethods { get; } = new List<MethodBase>();
        public List<MethodBase> AfterClassMethods { get; } = new List<MethodBase>();

        private TestGroup(
            IEnumerable<MethodBase> methods,
            TestAttributes testAttributes)
        {
            foreach (var method in methods)
            {
                var methodAttributes = method.GetCustomAttributes().Select(attr => attr.GetType()).ToList();
                if (methodAttributes.Contains(testAttributes.BeforeClassAttribute))
                {
                    BeforeClassMethods.Add(method);
                }
                else if (methodAttributes.Contains(testAttributes.BeforeAttribute))
                {
                    BeforeMethods.Add(method);
                }
                else if (methodAttributes.Contains(testAttributes.TestAttribute))
                {
                    TestMethods.Add(method);
                }
                else if (methodAttributes.Contains(testAttributes.AfterAttribute))
                {
                    AfterMethods.Add(method);
                }
                else if (methodAttributes.Contains(testAttributes.AfterClassAttribute))
                {
                    AfterClassMethods.Add(method);
                }
            }
        }

        public static TestGroup NewFrom(IEnumerable<MethodInfo> methods, TestAttributes testAttributes)
        {
            return new TestGroup(methods, testAttributes);
        }

        public bool IsEmpty()
        {
            return !TestMethods.Any();
        }
    }
}
