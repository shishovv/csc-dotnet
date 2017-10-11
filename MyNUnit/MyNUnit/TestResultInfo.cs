using System;

namespace MyNUnit
{
    public class TestResultInfo
    {
        public String TestName { get; }
        public TestResult Result { get; }
        public TimeSpan Time { get; }
        public String IgnoreReason { get; }

        public TestResultInfo(String testName, TestResult result, long time, String ignoreReason)
        {
            TestName = testName;
            Result = result;
            Time = TimeSpan.FromMilliseconds(time);
            IgnoreReason = ignoreReason;
        }

        public static TestResultInfo CreateNew(String testName, 
                                               TestResult res, 
                                               long time = 0, 
                                               String ignoreReason = null)
        {
            return new TestResultInfo(testName, res, time, ignoreReason);
        }

        public override string ToString()
        {
            return $@"{TestName} {Result.ToString().ToUpper()} {Time:ss\.fff} {IgnoreReason}";
        }

        public enum TestResult
        {
            Passed,
            Failed,
            Skipped
        }
    }
}
