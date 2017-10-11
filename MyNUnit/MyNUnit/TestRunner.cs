using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

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

            InvokeMethods(testGroup.BeforeClassMethods, testClassInstance, null);
            foreach (var testMethod in testGroup.TestMethods)
            {
                if (testMethod.Ignored())
                {
                    results.Add(TestResultInfo.CreateNew(testMethod.GetName(),
                                                         TestResultInfo.TestResult.Skipped,
                                                         ignoreReason:testMethod.IgnoreReason()));
                }
                else
                {
                    InvokeMethods(testGroup.BeforeMethods, testClassInstance, null);
                    results.Add(RunTest(testClassInstance, testMethod));
                    InvokeMethods(testGroup.AfterMethods, testClassInstance, null);
                }
            }
            InvokeMethods(testGroup.AfterClassMethods, testClassInstance, null);
            return results;
        }

        private void InvokeMethods(IEnumerable<MethodBase> methods, Object instance, Object[] args)
        {
            foreach (var method in methods)
            {
                method.Invoke(instance, args);
            }
        }

        private TestResultInfo RunTest(Object testClassInstance, 
                                       ITestMethod testMethod)
        {
            var stopWatch = Stopwatch.StartNew();
            try
            {
                testMethod.Invoke(testClassInstance, null);
                stopWatch.Stop();
                return TestResultInfo.CreateNew(
                    testMethod.GetName(),
                    testMethod.ExpectedExceptionType() != null 
                        ? TestResultInfo.TestResult.Failed
                        : TestResultInfo.TestResult.Passed,
                    stopWatch.ElapsedMilliseconds);
            }
            catch (Exception e)
            {
                stopWatch.Stop();
                return TestResultInfo.CreateNew(
                    testMethod.GetName(),
                    e.InnerException?.GetType() == testMethod.ExpectedExceptionType()
                        ? TestResultInfo.TestResult.Passed
                        : TestResultInfo.TestResult.Failed,
                    stopWatch.ElapsedMilliseconds);
            }
        }
    }
}
