using System;
namespace MyNUnit.Attributes
{
    public class TestAttribute : BaseAttribute
    {
        public Type ExpectedEceptionType { get; }
        public string IgnoreReason { get; }

        public TestAttribute(Type expectedEceptionType = null, string ignoreReason = null)
        {
            ExpectedEceptionType = expectedEceptionType;
            IgnoreReason = ignoreReason;
        }
    }
}
