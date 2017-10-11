using System;
using MyNUnit.Attributes;

namespace MyNUnit.TestSamples
{
    public class DebugTest
    {
        [BeforeClass]
        public void BeforeClass()
        {
            //            Console.WriteLine("BeforeClass");
        }

        [Before]
        public void Before()
        {
            //            Console.WriteLine("Before");
        }

        [Test(ignoreReason: "ignore")]
        public void Test1()
        {
            //            Console.WriteLine("Test1");
        }

        [Test(typeof(NullReferenceException))]
        public void Test2()
        {
            throw new ArgumentException();
            //            Console.WriteLine("Test2");
        }

        [Test(typeof(NullReferenceException))]
        public void Test3()
        {
            throw new NullReferenceException();
            //            Console.WriteLine("Test2");
        }

        [After]
        public void After()
        {
            //            Console.WriteLine("After");
        }

        [AfterClass]
        public void AfterClass()
        {
            //            Console.WriteLine("AfterClass");
        }

    }
}
