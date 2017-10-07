using System;

namespace TestReflection
{
    public class TestAttributes
    {
        public Type BeforeClassAttr { get; }
        public Type BeforeAttr { get; }
        public Type TestAttr { get; }
        public Type AfterAttr { get; }
        public Type AfterClassAttr { get; }

        private TestAttributes(
            Type beforeClassAttr, 
            Type beforeAttr, 
            Type testAttr, 
            Type afterAttr, 
            Type afterClassAttr)
        {
            BeforeClassAttr = beforeClassAttr;
            BeforeAttr = beforeAttr;
            TestAttr = testAttr;
            AfterAttr = afterAttr;
            AfterClassAttr = afterClassAttr;
        }

        private TestAttributes()
        {
            BeforeClassAttr = typeof(BeforeClassAttribute);
            BeforeAttr = typeof(BeforeAttribute);
            TestAttr = typeof(TestAttribute);
            AfterAttr = typeof(AfterAttribute);
            AfterClassAttr = typeof(AfterClassAttribute);
        }

        public static TestAttributes NewDefault()
        {
            return new TestAttributes();
        }

        public bool Contains(Type attributeType)
        {
            return BeforeClassAttr == attributeType
                   || BeforeAttr == attributeType
                   || TestAttr == attributeType
                   || AfterAttr == attributeType
                   || AfterClassAttr == attributeType;
        }
    }
}