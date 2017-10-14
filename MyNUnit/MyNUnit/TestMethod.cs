using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace MyNUnit
{
    public class TestMethod : ITestMethod
    {
        private readonly MethodBase _method;
        private readonly String _ignoreReason;
        private readonly Type _expectedExceptionType;

        public TestMethod(MethodBase method, 
                          String ignoreReason, 
                          Type expectedExceptionType)
        {
            _method = method;
            _ignoreReason = ignoreReason;
            _expectedExceptionType = expectedExceptionType;
        }

        public static TestMethod NewInstance(MethodBase method,
                                             String ignoreReason = null,
                                             Type expectedExceptionType = null) =>
        new TestMethod(method, ignoreReason, expectedExceptionType);

        public Type GetExpectedExceptionType() => _expectedExceptionType;

        public string GetName() => _method.Name;

        public bool Ignored() => _ignoreReason != null;

        public string GetIgnoreReason() => _ignoreReason;

        public void Invoke(Object obj, Object[] parameters) => _method.Invoke(obj, parameters);
    }
}
