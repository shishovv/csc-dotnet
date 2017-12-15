using System;

namespace Option
{
    public class Option<T>
    {
        private static readonly Option<T> NONE = new Option<T>(); 
        
        private readonly T _value;
        private readonly bool _isSome;

        private Option()
        {
            _value = default(T);
            _isSome = false;
        }

        private Option(T value)
        {
            _value = value;
            _isSome = true;
        }

        public static Option<T> Some(T value) => new Option<T>(value);

        public static Option<T> None() => NONE;

        public bool IsNone() => !_isSome;

        public bool IsSome() => _isSome;

        public T Value() => _isSome ? _value : throw new NullReferenceException();

        public Option<U> Map<U>(Func<T, U> f) => _isSome ? Option<U>.Some(f.Invoke(_value)) : Option<U>.None();

        public static Option<T> Flatten(Option<Option<T>> wrapper) => wrapper.IsNone() ? NONE : wrapper.Value();

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (!(obj is Option<T>))
            {
                return false;
            }
            var that = (Option<T>) obj;
            if (_isSome)
            {
                return that._isSome && _value.Equals(that._value);
            }
            return that.IsNone();
        }

        public override int GetHashCode()
        {
            var hash = _isSome.GetHashCode();
            return _isSome ? 31 * hash + _value.GetHashCode() : hash;
        }
    }
}
