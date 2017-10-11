using System;
using System.Reflection; 

namespace MyNUnit
{
    public class TestMethod : ITestMethod
    {
        private readonly MethodBase _method;
        private readonly String _ignoreReason;
        private readonly Type _expectedExceptionType;

        public TestMethod(MethodBase method, 
                          String ignoreReason = null, 
                          Type expectedExceptionType = null)
        {
            _method = method;
            _ignoreReason = ignoreReason;
            _expectedExceptionType = expectedExceptionType;
        }

        public Type ExpectedExceptionType() => _expectedExceptionType;

        public string GetName() => _method.Name;

        public bool Ignored() => _ignoreReason != null;

        public string IgnoreReason() => _ignoreReason;

        public void Invoke(object obj, object[] parameters) => _method.Invoke(obj, parameters);
    }
}
