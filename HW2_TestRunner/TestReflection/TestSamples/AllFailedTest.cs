using System;
using TestReflection.Attributes;

namespace TestReflection.TestSamples
{
    public class AllFailedTest
    {
        [BeforeClass]
        public void BeforeClass()
        {
        }

        [Before]
        public void Before()
        {
        }

        [Test]
        public void Test1()
        {
            throw new ArithmeticException();
        }
        
        [Test(typeof(NullReferenceException))]
        public void Test2()
        {
            throw new ArithmeticException();
        }
        
        [After]
        public void After()
        {
        }

        [AfterClass]
        public void AfterClass()
        {
        }
    }
}