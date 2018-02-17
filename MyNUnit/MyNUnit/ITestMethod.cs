using System;
namespace MyNUnit
{
    public interface ITestMethod : IMethod
    {
        bool IsIgnored { get; }
        string IgnoreReason { get; }
        Type ExpectedExceptionType { get; }
        string Name { get; }
    }
}
