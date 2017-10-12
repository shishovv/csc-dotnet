using System;
using NUnit.Framework;

namespace Option.Tests
{
    [TestFixture]
    public class TestOption
    {
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestBaseFunctionality()
        {
            var option = Option<String>.Some("test");
            Assert.True(option.IsSome());
            Assert.AreEqual("test", option.Value());

            option = Option<String>.None();
            Assert.True(option.IsNone());
            var tmp = option.Value();
        }

        [Test]
        public void TestMap()
        {
            var option = Option<int>.Some(10);
            Assert.AreEqual(Option<int>.Some(100), option.Map(v => v * 10));

            option = Option<int>.None().Map(v => v * 10);
            Assert.True(option.IsNone());
            Assert.AreEqual(Option<int>.None(), option);
        }

        [Test]
        public void TestFlatten()
        {
            var wrapper = Option<Option<int>>.Some(Option<int>.Some(10));
            Assert.AreEqual(Option<int>.Some(10), Option<int>.Flatten(wrapper));

            wrapper = Option<Option<int>>.Some(Option<int>.None());
            Assert.True(Option<int>.Flatten(wrapper).IsNone());
        }
    }
}
