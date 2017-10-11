using System;
namespace MyNUnit
{
    public interface ITestMethod
    {
        bool Ignored();

        String GetIgnoreReason();

        Type GetExpectedExceptionType();

        void Invoke(Object obj, Object[] parameters);

        String GetName();
    }
}
