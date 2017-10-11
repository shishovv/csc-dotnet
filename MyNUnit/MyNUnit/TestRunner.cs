using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using MyNUnit.Attributes;

namespace MyNUnit
{
    public class TestRunner
    {
        private readonly TestAttributes _testAttributes;

        public TestRunner(
            TestAttributes testAttributes)
        {
            _testAttributes = testAttributes;
        }

        public IEnumerable<TestResultInfo> Run(Object testClassInstance, TestGroup testGroup)
        {
            var results = new List<TestResultInfo>();

            testGroup.BeforeClassMethods.ForEach(method => method.Invoke(testClassInstance, null));
            testGroup.TestMethods.ForEach(testMethod =>
            {
                results.Add(RunTest(testClassInstance, testMethod, testGroup.BeforeMethods, testGroup.AfterMethods));
            });
            testGroup.AfterClassMethods.ForEach(method => method.Invoke(testClassInstance, null));

            return results;
        }

        private TestResultInfo RunTest(
            Object testClassInstance,
            MethodBase testMethod,
            IEnumerable<MethodBase> beforeMethods,
            IEnumerable<MethodBase> afterMethods
            )
        {
            var testAttr = (TestAttribute) testMethod.GetCustomAttribute(_testAttributes.TestAttribute);
            if (testAttr.IgnoreReason != null)
                return TestResultInfo.CreateNew(testMethod.Name, TestResultInfo.TestResult.Skipped, 0,
                    testAttr.IgnoreReason);

            foreach (var beforeMethod in beforeMethods)
            {
                beforeMethod.Invoke(testClassInstance, null);
            }

            TestResultInfo testResultInfo;
            var stopWatch = Stopwatch.StartNew();
            try
            {
                testMethod.Invoke(testClassInstance, null);
                stopWatch.Stop();
                testResultInfo = TestResultInfo.CreateNew(
                    testMethod.Name,
                    testAttr.ExpectedEceptionType != null
                        ? TestResultInfo.TestResult.Failed
                        : TestResultInfo.TestResult.Passed,
                    stopWatch.ElapsedMilliseconds);
            }
            catch (Exception e)
            {
                stopWatch.Stop();
                testResultInfo = TestResultInfo.CreateNew(
                    testMethod.Name,
                    e.InnerException?.GetType() == testAttr.ExpectedEceptionType
                        ? TestResultInfo.TestResult.Passed
                        : TestResultInfo.TestResult.Failed,
                    stopWatch.ElapsedMilliseconds);
            }
            foreach (var afterMethod in afterMethods)
            {
                afterMethod.Invoke(testClassInstance, null);
            }

            return testResultInfo;
        }
    }
}
