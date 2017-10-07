using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace TestReflection
{
    public class TestRunner
    {
        private readonly Dictionary<Type, TestGroup> _tests = new Dictionary<Type, TestGroup>();
        private readonly TestAttributes _testAttributes;

        public TestRunner(
            IEnumerable<Type> testClasses, 
            TestAttributes testAttributes)
        {
            _testAttributes = testAttributes;
            
            foreach (var testClass in testClasses)
            {                
                var testGroup = new TestGroup(testClass.GetMethods(), testAttributes);
                if (!testGroup.IsEmpty())
                {
                    _tests[testClass] = testGroup;
                }
            }
        }

        public IEnumerable<KeyValuePair<Type, IEnumerable<TestResult>>> Run()
        {
            var testResults = new Dictionary<Type, IEnumerable<TestResult>>();
            foreach (var tests in _tests)
            {
                testResults[tests.Key] = RunTestGroup(Activator.CreateInstance(tests.Key), tests.Value);
            }
            return testResults;
        }

        private IEnumerable<TestResult> RunTestGroup(Object obj, TestGroup testGroup)
        {
            var results = new List<TestResult>();
            
            testGroup.BeforeClassMethods.ForEach(method => method.Invoke(obj, null));
            testGroup.TestMethods.ForEach(testMethod =>
            {
                results.Add(RunTest(obj, testMethod, testGroup.BeforeMethods, testGroup.AfterMethods));
            });
            testGroup.AfterClassMethods.ForEach(method => method.Invoke(obj, null));
            
            return results;
        }

        private TestResult RunTest(Object obj, MethodInfo testMethod, IEnumerable<MethodInfo> beforeMethods,
            IEnumerable<MethodInfo> afterMethods)
        {
            var testAttr = (TestAttribute) testMethod.GetCustomAttribute(_testAttributes.TestAttr);
            if (testAttr.IgnoreReason == null)
            {
                foreach (var beforeMethod in beforeMethods)
                {
                    beforeMethod.Invoke(obj, null);
                }

                TestResult testResult;
                var stopWatch = Stopwatch.StartNew();
                try
                {
                    testMethod.Invoke(obj, null);
                    stopWatch.Stop();
                    if (testAttr.ExpectedEceptionType != null)
                    {
                        testResult = TestResult.CreateNew(TestResult.Result.Failed, stopWatch.ElapsedMilliseconds);
                    }
                    else
                    {
                        testResult = TestResult.CreateNew(TestResult.Result.Passed, stopWatch.ElapsedMilliseconds);
                    }
                }
                catch (Exception e)
                {
                    stopWatch.Stop();
                    if (e.InnerException?.GetType() == (testAttr.ExpectedEceptionType))
                    {
                        testResult = TestResult.CreateNew(TestResult.Result.Passed, stopWatch.ElapsedMilliseconds);
                    }
                    else
                    {
                        testResult = TestResult.CreateNew(TestResult.Result.Failed, stopWatch.ElapsedMilliseconds);
                    }
                }
                foreach (var afterMethod in afterMethods)
                {
                    afterMethod.Invoke(obj, null);
                }
                
                return testResult;
            }
            return TestResult.CreateNew(TestResult.Result.Skipped, 0, testAttr.IgnoreReason);
        }
    }
}