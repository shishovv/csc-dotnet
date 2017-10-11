using System;
using MyNUnit.Attributes;

namespace MyNUnit
{
    public class TestAttributes
    {
        public Type BeforeClassAttribute { get; }
        public Type BeforeAttribute { get; }
        public Type TestAttribute { get; }
        public Type AfterAttribute { get; }
        public Type AfterClassAttribute { get; }

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
