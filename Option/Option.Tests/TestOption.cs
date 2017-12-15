using System;
using NUnit.Framework;

namespace Option.Tests
{
    [TestFixture]
    public class TestOption
    {
        [Test]
        public void When_Some_Then_IsSomeReturnsTrue()
        {
            Assert.True(Option<int>.Some(1).IsSome());
        }
        
        [Test]
        public void When_None_Then_IsNoneReturnsTrue()
        {
            Assert.True(Option<int>.None().IsNone());
        }

        [Test]
        public void When_Some10_Then_ValueReturns10()
        {
            Assert.AreEqual(10, Option<int>.Some(10).Value());
        }

        [Test]
        public void When_None_Then_EqualsNoneReturnsTrue()
        {
            Assert.True(Option<int>.None().Equals(Option<int>.None()));
        }
        
        [Test]
        public void When_None_Then_EqualsSomeReturnsFalse()
        {
            Assert.False(Option<int>.None().Equals(Option<int>.Some(1)));
        }

        [Test]
        public void When_Some10_Then_EqualsSome10ReturnsTrue()
        {
            Assert.True(Option<int>.Some(10).Equals(Option<int>.Some(10)));
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void When_None_Then_ValueThrowsException()
        {
            Option<int>.None().Value();
        }
        
        [Test]
        public void When_Some_Then_MapReturnsSome()
        {
            var option = Option<int>.Some(1).Map(v => v * 10);
            Assert.True(option.IsSome());
            Assert.AreEqual(10, option.Value());
        }
        
        [Test]
        public void When_None_Then_MapReturnsNone()
        {
            var option = Option<int>.None().Map(v => v * 10);
            Assert.True(option.IsNone());
        }

        [Test]
        public void When_Some_Then_FlattenReturnsSome()
        {
            var option = Option<Option<int>>.Some(Option<int>.Some(1));
            Assert.True(Option<int>.Flatten(option).IsSome());
        }
        
        [Test]
        public void When_None_Then_FlattenReturnNone()
        {
            var option = Option<Option<int>>.None();
            Assert.True(Option<int>.Flatten(option).IsNone());
        }
    }
}
