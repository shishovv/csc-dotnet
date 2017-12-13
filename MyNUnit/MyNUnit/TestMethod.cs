using System;
using System.Reflection;

namespace MyNUnit
{
    public class TestMethod : ITestMethod
    {
        public bool IsIgnored => IgnoreReason != null;
        public string IgnoreReason { get; }
        public Type ExpectedExceptionType { get; }
        public string Name => Method.Name;

        private MethodBase Method { get; }
        
        private TestMethod(MethodBase method, 
            string ignoreReason, 
            Type expectedExceptionType)
        {
            Method = method;
            IgnoreReason = ignoreReason;
            ExpectedExceptionType = expectedExceptionType;
        }

        public static TestMethod NewInstance(
            MethodBase method,
            string ignoreReason = null,
            Type expectedExceptionType = null) => new TestMethod(method, ignoreReason, expectedExceptionType);
        
        public void Invoke(object obj, object[] args) => Method.Invoke(obj, args);
    }
}
