using System;
using TestReflection.Attributes;

namespace TestReflection.TestSamples
{
    public class SimpleTest
    {
        [BeforeClass]
        public void BeforeClass()
        {
            Console.WriteLine("BeforeClass");
        }

        [Before]
        public void Before()
        {
            Console.WriteLine("Before");
        }

        [Test(ignoreReason:"ignore")]
        public void Test1()
        {
            Console.WriteLine("Test1");
        }
        
        [Test(typeof(NullReferenceException))]
        public void Test2()
        {
            Console.WriteLine("Test2");
            throw new ArgumentException();
        }
            
        [Test(typeof(NullReferenceException))]
        public void Test3()
        {
            Console.WriteLine("Test2");
            for (var i = 0; i < 1000000000; )
            {
                    ++i;
            }
            throw new NullReferenceException();
                    
        }  

        [After]
        public void After()
        {
            Console.WriteLine("After");
        }

        [AfterClass]
        public void AfterClass()
        {
            Console.WriteLine("AfterClass");
        }
    }
}