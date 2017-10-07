using System;

namespace TestReflection
{
    public class TestResult
    {

        public Result Res { get; }
        public TimeSpan Time { get;  }
        public String IgnoreReason { get; }

        public TestResult(Result res, long time, String ignoreReason)
        {
            Res = res;
            Time = TimeSpan.FromMilliseconds(time);
            IgnoreReason = ignoreReason;
        }

        public static TestResult CreateNew(Result res, long time, String IgnoreReason = null)
        {
            return new TestResult(res, time, IgnoreReason);
        }

        public enum Result
        {
            Passed,
            Failed,
            Skipped
        }
    }
}