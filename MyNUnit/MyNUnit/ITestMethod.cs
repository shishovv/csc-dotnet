using System;
namespace MyNUnit
{
    public interface ITestMethod
    {
        bool Ignored();

        String IgnoreReason();

        Type ExpectedExceptionType();

        void Invoke(Object obj, Object[] parameters);

        String GetName();
    }
}
