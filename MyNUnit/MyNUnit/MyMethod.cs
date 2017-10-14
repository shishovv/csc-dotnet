using System;
using System.Reflection;

namespace MyNUnit
{
    public class MyMethod : IMethod
    {
        private readonly MethodBase _method;

        private MyMethod(MethodBase method)
        {
            _method = method;
        }

        public static MyMethod NewInstance(MethodBase method) => new MyMethod(method);

        public void Invoke(Object obj, Object[] args) => _method.Invoke(obj, args);
    }
}
