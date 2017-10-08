using System;
using System.Collections.Generic;

namespace TestReflection
{    
    internal class Program
    {
        private static readonly TestAttributes TEST_ATTRIBUTES = TestAttributes.NewDefault();
        
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Path not specified");
            }
            
            foreach (var assembly in Utils.GetAssembliesFrom(args[0]))
            {
                foreach (var type in Utils.GetTestClassesFrom(assembly, TEST_ATTRIBUTES.TestAttribute))
                {
                    var testGroup = TestGroup.NewFrom(type.GetMethods(), TEST_ATTRIBUTES);
                    var testRunner = new TestRunner(TEST_ATTRIBUTES);
                    var testResults = testRunner.Run(Activator.CreateInstance(type), testGroup);
                    PrintTestResults(type, testResults);
                }

            }      
        }

        private static void PrintTestResults(
            Type testClass,
            IEnumerable<TestResultInfo> testResults)
        {
            Console.WriteLine($"{testClass.Name}: ");
            foreach (var testResult in testResults)
            {
                Console.WriteLine($"\t{testResult}");
            }
        }
    }
}