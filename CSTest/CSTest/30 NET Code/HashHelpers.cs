// Decompiled with JetBrains decompiler
// Type: System.Collections.HashHelpers
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 255ABCDF-D9D6-4E3D-BAD4-F74D4CE3D7A8
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll
// Compiler-generated code is shown

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Threading;

namespace CSTest._30_NET_Code
{
    ///[FriendAccessAllowed]
    internal static class HashHelpers
    {
        public const int HashCollisionThreshold = 100;
        public static bool s_UseRandomizedStringHashing;
        public static readonly int[] primes;
        private static ConditionalWeakTable<object, SerializationInfo> s_SerializationInfoTable;
        public const int MaxPrimeArrayLength = 2146435069;
        private const int bufferSize = 1024;
        private static RandomNumberGenerator rng;
        private static byte[] data;
        private static int currentIndex;
        private static readonly object lockObj;

        internal static ConditionalWeakTable<object, SerializationInfo> SerializationInfoTable
        {
            get
            {
                if (HashHelpers.s_SerializationInfoTable == null)
                {
                    ConditionalWeakTable<object, SerializationInfo> conditionalWeakTable = new ConditionalWeakTable<object, SerializationInfo>();
                    Interlocked.CompareExchange<ConditionalWeakTable<object, SerializationInfo>>(ref HashHelpers.s_SerializationInfoTable, conditionalWeakTable, (ConditionalWeakTable<object, SerializationInfo>)null);
                }
                return HashHelpers.s_SerializationInfoTable;
            }
        }

        //[SecuritySafeCritical]
        internal static bool UseRandomizedHashing()
        {
            return InternalUseRandomizedHashing();
        }

        //[SecurityCritical]
        //[SuppressUnmanagedCodeSecurity]
        //[DllImport("QCall", CharSet = CharSet.Unicode)]
        //private static extern bool InternalUseRandomizedHashing();

        private static bool InternalUseRandomizedHashing()
        {
            return true;
        }

        //[SecurityCritical]
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //internal static extern int InternalMarvin32HashString(string s, int strLen, long additionalEntropy);

        internal static int InternalMarvin32HashString(string s, int strLen, long additionalEntropy)
        {
            return s.GetHashCode();
        }


        static HashHelpers()
        {
            HashHelpers.s_UseRandomizedStringHashing = UseRandomizedHashing();
            HashHelpers.primes = new int[72]
            {
        3,
        7,
        11,
        17,
        23,
        29,
        37,
        47,
        59,
        71,
        89,
        107,
        131,
        163,
        197,
        239,
        293,
        353,
        431,
        521,
        631,
        761,
        919,
        1103,
        1327,
        1597,
        1931,
        2333,
        2801,
        3371,
        4049,
        4861,
        5839,
        7013,
        8419,
        10103,
        12143,
        14591,
        17519,
        21023,
        25229,
        30293,
        36353,
        43627,
        52361,
        62851,
        75431,
        90523,
        108631,
        130363,
        156437,
        187751,
        225307,
        270371,
        324449,
        389357,
        467237,
        560689,
        672827,
        807403,
        968897,
        1162687,
        1395263,
        1674319,
        2009191,
        2411033,
        2893249,
        3471899,
        4166287,
        4999559,
        5999471,
        7199369
            };
            HashHelpers.currentIndex = 1024;
            HashHelpers.lockObj = new object();
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static bool IsPrime(int candidate)
        {
            if ((candidate & 1) == 0)
                return candidate == 2;
            int num1 = (int)Math.Sqrt((double)candidate);
            int num2 = 3;
            while (num2 <= num1)
            {
                if (candidate % num2 == 0)
                    return false;
                num2 += 2;
            }
            return true;
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int GetPrime(int min)
        {
            if (min < 0)
                throw new ArgumentException(CSTest._30_NET_Code.Environment.GetResourceString("Arg_HTCapacityOverflow"));
            for (int index = 0; index < HashHelpers.primes.Length; ++index)
            {
                int num = HashHelpers.primes[index];
                if (num >= min)
                    return num;
            }
            int candidate = min | 1;
            while (candidate < int.MaxValue)
            {
                if (HashHelpers.IsPrime(candidate) && (candidate - 1) % 101 != 0)
                    return candidate;
                candidate += 2;
            }
            return min;
        }

        public static int GetMinPrime()
        {
            return HashHelpers.primes[0];
        }

        public static int ExpandPrime(int oldSize)
        {
            int min = 2 * oldSize;
            if ((uint)min > 2146435069U && 2146435069 > oldSize)
                return 2146435069;
            return HashHelpers.GetPrime(min);
        }

        public static bool IsWellKnownEqualityComparer(object comparer)
        {
            if (comparer != null && comparer != EqualityComparer<string>.Default)
                return comparer is IWellKnownStringEqualityComparer;
            return true;
        }

        public static IEqualityComparer GetRandomizedEqualityComparer(object comparer)
        {
            if (comparer == null)
                return (IEqualityComparer)new RandomizedObjectEqualityComparer();
            if (comparer == EqualityComparer<string>.Default)
                return (IEqualityComparer)new RandomizedStringEqualityComparer();
            IWellKnownStringEqualityComparer equalityComparer = comparer as IWellKnownStringEqualityComparer;
            if (equalityComparer != null)
                return equalityComparer.GetRandomizedEqualityComparer();
            return (IEqualityComparer)null;
        }

        public static object GetEqualityComparerForSerialization(object comparer)
        {
            if (comparer == null)
                return (object)null;
            IWellKnownStringEqualityComparer equalityComparer = comparer as IWellKnownStringEqualityComparer;
            if (equalityComparer != null)
                return (object)equalityComparer.GetEqualityComparerForSerialization();
            return comparer;
        }

        internal static long GetEntropy()
        {
            object obj = HashHelpers.lockObj;
            bool lockTaken = false;
            try
            {
                Monitor.Enter(obj, ref lockTaken);
                if (HashHelpers.currentIndex == 1024)
                {
                    if (HashHelpers.rng == null)
                    {
                        HashHelpers.rng = RandomNumberGenerator.Create();
                        HashHelpers.data = new byte[1024];
                    }
                    HashHelpers.rng.GetBytes(HashHelpers.data);
                    HashHelpers.currentIndex = 0;
                }
                long num = BitConverter.ToInt64(HashHelpers.data, HashHelpers.currentIndex);
                HashHelpers.currentIndex += 8;
                return num;
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(obj);
            }
        }
    }
}

