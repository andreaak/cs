using CSTest._30_NET_Code;

namespace CSTest._03_Structure._03_Nullable
{

    public struct NullableNET<T> where T : struct
    {
        private bool hasValue;
        internal T value;

        public NullableNET(T value)
        {
            this.value = value;
            this.hasValue = true;
        }

        public bool HasValue
        {
            get { return hasValue; }
        }

        public T Value
        {
            get
            {
                if (!hasValue)
                {
                    ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_NoValue);
                }
                return value;
            }
        }

        public T GetValueOrDefault()
        {
            return value;
        }

        public T GetValueOrDefault(T defaultValue)
        {
            return hasValue ? value : defaultValue;
        }

        public override bool Equals(object other)
        {
            if (!hasValue) return other == null;
            if (other == null) return false;
            return value.Equals(other);
        }

        public override int GetHashCode()
        {
            return hasValue ? value.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return hasValue ? value.ToString() : "";
        }

        public static implicit operator NullableNET<T>(T value)
        {
            return new NullableNET<T>(value);
        }

        public static explicit operator T(NullableNET<T> value)
        {
            return value.Value;
        }

        // The following already obsoleted methods were removed:
        //   public int CompareTo(object other)
        //   public int CompareTo(Nullable<T> other)
        //   public bool Equals(Nullable<T> other)
        //   public static Nullable<T> FromObject(object value)
        //   public object ToObject()
        //   public string ToString(string format)
        //   public string ToString(IFormatProvider provider)
        //   public string ToString(string format, IFormatProvider provider)

        // The following newly obsoleted methods were removed:
        //   string IFormattable.ToString(string format, IFormatProvider provider)
        //   int IComparable.CompareTo(object other)
        //   int IComparable<Nullable<T>>.CompareTo(Nullable<T> other)
        //   bool IEquatable<Nullable<T>>.Equals(Nullable<T> other)
    }
}
