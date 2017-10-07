using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace TestReflection
{    
    internal class Program
    {
        private static String PATH = 
            "/Users/vladislavshishov/Documents/src/edu/dotnet/TestReflection/TestReflection/bin/Debug/";
        
        public static void Main(string[] args)
        {
            foreach (var assembly in Utils.GetAssembliesFrom(PATH))
            {
                var tmp = Utils.GetTestClassesFrom(assembly, TestAttributes.NewDefault());
                var testRunner = new TestRunner(tmp, TestAttributes.NewDefault());
                var result = testRunner.Run();
                foreach (var pair in result)
                {
                    Console.WriteLine(pair.Value);
                    Console.WriteLine("\tPASSED: " + pair.Value.Count(res => res.Res == TestResult.Result.Passed));
                    Console.WriteLine("\tFAILED: " + pair.Value.Count(res => res.Res == TestResult.Result.Failed));
                    Console.WriteLine("\tSKIPPED: " + pair.Value.Count(res => res.Res == TestResult.Result.Skipped));
                }
            }      
        }
    }
}