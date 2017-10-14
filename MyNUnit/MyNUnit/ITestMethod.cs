using System;
namespace MyNUnit
{
    public interface ITestMethod : IMethod
    {
        bool Ignored();

        String GetIgnoreReason();

        Type GetExpectedExceptionType();

        String GetName();
    }
}
