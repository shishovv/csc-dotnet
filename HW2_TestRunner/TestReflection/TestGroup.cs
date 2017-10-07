using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestReflection
{
    public class TestGroup
    {
        public List<MethodInfo> BeforeClassMethods { get; } = new List<MethodInfo>();
        public List<MethodInfo> BeforeMethods { get; } = new List<MethodInfo>();
        public List<MethodInfo> TestMethods { get; } = new List<MethodInfo>();
        public List<MethodInfo> AfterMethods { get; } = new List<MethodInfo>();
        public List<MethodInfo> AfterClassMethods { get; } = new List<MethodInfo>();
            
        public TestGroup(
            IEnumerable<MethodInfo> methods, 
            TestAttributes testAttributes)
        {
            foreach (var method in methods)
            {
                var attrs = method.GetCustomAttributes().Select(attr => attr.GetType());
                if (attrs.Contains(testAttributes.BeforeClassAttr))
                {
                    BeforeClassMethods.Add(method);
                }
                else if (attrs.Contains(testAttributes.BeforeAttr))
                {
                    BeforeMethods.Add(method);
                }
                else if (attrs.Contains(testAttributes.TestAttr))
                {
                    TestMethods.Add(method);
                }
                else if (attrs.Contains(testAttributes.AfterAttr))
                {
                    AfterMethods.Add(method);
                }
                else if (attrs.Contains(testAttributes.AfterClassAttr))
                {
                    AfterClassMethods.Add(method);
                }
            }
        }

        public bool IsEmpty()
        {
            return !TestMethods.Any();
        }
    }
}