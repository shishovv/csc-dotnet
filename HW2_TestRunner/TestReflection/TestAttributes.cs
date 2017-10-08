using System;
using TestReflection.Attributes;

namespace TestReflection
{
    public class TestAttributes
    {
        public Type BeforeClassAttribute { get; }
        public Type BeforeAttribute { get; }
        public Type TestAttribute { get; }
        public Type AfterAttribute { get; }
        public Type AfterClassAttribute { get; }

        private TestAttributes(
            Type beforeClassAttribute, 
            Type beforeAttribute, 
            Type testAttribute, 
            Type afterAttribute, 
            Type afterClassAttribute)
        {
            BeforeClassAttribute = beforeClassAttribute;
            BeforeAttribute = beforeAttribute;
            TestAttribute = testAttribute;
            AfterAttribute = afterAttribute;
            AfterClassAttribute = afterClassAttribute;
        }

        private TestAttributes()
        {
            BeforeClassAttribute = typeof(BeforeClassAttribute);
            BeforeAttribute = typeof(BeforeAttribute);
            TestAttribute = typeof(TestAttribute);
            AfterAttribute = typeof(AfterAttribute);
            AfterClassAttribute = typeof(AfterClassAttribute);
        }

        public static TestAttributes NewDefault()
        {
            return new TestAttributes();
        }

        public bool Contains(Type attributeType)
        {
            return BeforeClassAttribute == attributeType
                   || BeforeAttribute == attributeType
                   || TestAttribute == attributeType
                   || AfterAttribute == attributeType
                   || AfterClassAttribute == attributeType;
        }
    }
}