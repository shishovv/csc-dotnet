using System;

namespace MyNUnit
{
    public class TestResultInfo
    {
        public string TestName { get; }
        public TestResult Result { get; }
        public TimeSpan Time { get; }
        public string IgnoreReason { get; }

        private TestResultInfo(string testName, TestResult result, long time, string ignoreReason)
        {
            TestName = testName;
            Result = result;
            Time = TimeSpan.FromMilliseconds(time);
            IgnoreReason = ignoreReason;
        }

        public static TestResultInfo CreateNew(string testName, 
                                               TestResult res, 
                                               long time = 0, 
                                               string ignoreReason = null) => 
            new TestResultInfo(testName, res, time, ignoreReason);

        public override string ToString() => 
            $@"{TestName} {Result.ToString().ToUpper()} {Time:ss\.fff} {IgnoreReason}";

        public enum TestResult
        {
            Passed,
            Failed,
            Skipped
        }
    }
}
