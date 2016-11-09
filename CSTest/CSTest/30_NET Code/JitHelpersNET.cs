using System;
using System.Diagnostics.Contracts;


namespace CSTest._30_NET_Code
{
    internal static class JitHelpersNET
    {
        static internal int UnsafeEnumCast<T>(T val) where T : struct		// Actually T must be 4 byte (or less) enum
        {
            Contract.Assert(typeof(T).IsEnum
                              && (Enum.GetUnderlyingType(typeof(T)) == typeof(int)
                                  || Enum.GetUnderlyingType(typeof(T)) == typeof(uint)
                                  || Enum.GetUnderlyingType(typeof(T)) == typeof(short)
                                  || Enum.GetUnderlyingType(typeof(T)) == typeof(ushort)
                                  || Enum.GetUnderlyingType(typeof(T)) == typeof(byte)
                                  || Enum.GetUnderlyingType(typeof(T)) == typeof(sbyte)),
                "Error, T must be an 4 byte (or less) enum JitHelpers.UnsafeEnumCast!");
            return UnsafeEnumCastInternal<T>(val);
        }

        static private int UnsafeEnumCastInternal<T>(T val) where T : struct		// Actually T must be 4 (or less) byte enum
        {
            // should be return (int) val; but C# does not allow, runtime does this magically
            // See getILIntrinsicImplementation for how this happens.  
            throw new InvalidOperationException();
        }

        static internal long UnsafeEnumCastLong<T>(T val) where T : struct		// Actually T must be 8 byte enum
        {
            Contract.Assert(typeof(T).IsEnum
                              && (Enum.GetUnderlyingType(typeof(T)) == typeof(long)
                                  || Enum.GetUnderlyingType(typeof(T)) == typeof(ulong)),
                "Error, T must be an 8 byte enum JitHelpers.UnsafeEnumCastLong!");
            return UnsafeEnumCastLongInternal<T>(val);
        }

        static private long UnsafeEnumCastLongInternal<T>(T val) where T : struct	// Actually T must be 8 byte enum
        {
            // should be return (int) val; but C# does not allow, runtime does this magically
            // See getILIntrinsicImplementation for how this happens.  
            throw new InvalidOperationException();
        }
    }
}
