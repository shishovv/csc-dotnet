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

        public void Invoke(object obj, object[] args) => _method.Invoke(obj, args);
    }
}
