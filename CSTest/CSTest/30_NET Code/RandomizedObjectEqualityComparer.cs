// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.RandomizedObjectEqualityComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: B85DA6F1-FFD3-4C95-AF10-155C235A661D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace CSTest._30_NET_Code
{
    internal sealed class RandomizedObjectEqualityComparer : IEqualityComparer, IWellKnownStringEqualityComparer
    {
        private long _entropy;

        public RandomizedObjectEqualityComparer()
        {
            this._entropy = HashHelpersNET.GetEntropy();
        }

        public bool Equals(object x, object y)
        {
            if (x != null)
            {
                if (y != null)
                    return x.Equals(y);
                return false;
            }
            return y == null;
        }

        //[SecuritySafeCritical]
        public int GetHashCode(object obj)
        {
            if (obj == null)
                return 0;
            string str = obj as string;
            if (str == null)
                return obj.GetHashCode();
            string s = str;
            int length = s.Length;
            long additionalEntropy = this._entropy;
            return HashHelpersNET.InternalMarvin32HashString(s, length, additionalEntropy);
        }

        public override bool Equals(object obj)
        {
            RandomizedObjectEqualityComparer equalityComparer = obj as RandomizedObjectEqualityComparer;
            if (equalityComparer != null)
                return this._entropy == equalityComparer._entropy;
            return false;
        }

        public override int GetHashCode()
        {
            return this.GetType().Name.GetHashCode() ^ (int)(this._entropy & (long)int.MaxValue);
        }

        IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
        {
            return (IEqualityComparer)new RandomizedObjectEqualityComparer();
        }

        IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
        {
            return (IEqualityComparer)null;
        }
    }
}

