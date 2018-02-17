using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace MyNUnit
{
    public class TestRunner
    {
        public IEnumerable<TestResultInfo> Run(object testClassInstance, TestGroup testGroup)
        {
            var results = new List<TestResultInfo>();

            InvokeMethods(testGroup.BeforeClassMethods, testClassInstance, null);
            foreach (var testMethod in testGroup.TestMethods)
            {
                if (testMethod.IsIgnored)
                {
                    results.Add(TestResultInfo.CreateNew(testMethod.Name,
                                                         TestResultInfo.TestResult.Skipped,
                                                         ignoreReason:testMethod.IgnoreReason));
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

        private static void InvokeMethods(IEnumerable<IMethod> methods, object instance, object[] args)
        {
            foreach (var method in methods)
            {
                method.Invoke(instance, args);
            }
        }

        private static TestResultInfo RunTest(object testClassInstance, 
                                       ITestMethod testMethod)
        {
            var stopWatch = Stopwatch.StartNew();
            try
            {
                testMethod.Invoke(testClassInstance, null);
                stopWatch.Stop();
                return TestResultInfo.CreateNew(
                    testMethod.Name,
                    testMethod.ExpectedExceptionType != null 
                        ? TestResultInfo.TestResult.Failed
                        : TestResultInfo.TestResult.Passed,
                    stopWatch.ElapsedMilliseconds);
            }
            catch (TargetInvocationException e)
            {
                stopWatch.Stop();
                return TestResultInfo.CreateNew(
                    testMethod.Name,
                    e.InnerException?.GetType() == testMethod.ExpectedExceptionType
                        ? TestResultInfo.TestResult.Passed
                        : TestResultInfo.TestResult.Failed,
                    stopWatch.ElapsedMilliseconds);
            }
        }
    }
}
