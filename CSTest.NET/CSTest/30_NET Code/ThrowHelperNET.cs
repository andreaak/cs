using System;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;

namespace CSTest._30_NET_Code
{
    internal static class ThrowHelperNET
    {
        internal static void ThrowArgumentOutOfRangeException()
        {
            ThrowArgumentOutOfRangeException(ExceptionArgumentNET.index, ExceptionResourceNET.ArgumentOutOfRange_Index);
        }

        internal static void ThrowWrongKeyTypeArgumentException(object key, Type targetType)
        {
            throw new ArgumentException(EnvironmentNET.GetResourceString("Arg_WrongType", key, targetType), "key");
        }

        internal static void ThrowWrongValueTypeArgumentException(object value, Type targetType)
        {
            throw new ArgumentException(EnvironmentNET.GetResourceString("Arg_WrongType", value, targetType), "value");
        }

        internal static void ThrowKeyNotFoundException()
        {
            throw new System.Collections.Generic.KeyNotFoundException();
        }

        internal static void ThrowArgumentException(ExceptionResourceNET resource)
        {
            throw new ArgumentException(EnvironmentNET.GetResourceString(GetResourceName(resource)));
        }

        internal static void ThrowArgumentException(ExceptionResourceNET resource, ExceptionArgumentNET argument)
        {
            throw new ArgumentException(EnvironmentNET.GetResourceString(GetResourceName(resource)), GetArgumentName(argument));
        }

        internal static void ThrowArgumentNullException(ExceptionArgumentNET argument)
        {
            throw new ArgumentNullException(GetArgumentName(argument));
        }

        internal static void ThrowArgumentOutOfRangeException(ExceptionArgumentNET argument)
        {
            throw new ArgumentOutOfRangeException(GetArgumentName(argument));
        }

        internal static void ThrowArgumentOutOfRangeException(ExceptionArgumentNET argument, ExceptionResourceNET resource)
        {

            if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
            {
                // Dev11 474369 quirk: Mango had an empty message string:
                throw new ArgumentOutOfRangeException(GetArgumentName(argument), String.Empty);
            }
            else
            {
                throw new ArgumentOutOfRangeException(GetArgumentName(argument),
                                                      EnvironmentNET.GetResourceString(GetResourceName(resource)));
            }
        }

        internal static void ThrowInvalidOperationException(ExceptionResourceNET resource)
        {
            throw new InvalidOperationException(EnvironmentNET.GetResourceString(GetResourceName(resource)));
        }

        internal static void ThrowSerializationException(ExceptionResourceNET resource)
        {
            throw new SerializationException(EnvironmentNET.GetResourceString(GetResourceName(resource)));
        }

        internal static void ThrowSecurityException(ExceptionResourceNET resource)
        {
            throw new System.Security.SecurityException(EnvironmentNET.GetResourceString(GetResourceName(resource)));
        }

        internal static void ThrowNotSupportedException(ExceptionResourceNET resource)
        {
            throw new NotSupportedException(EnvironmentNET.GetResourceString(GetResourceName(resource)));
        }

        internal static void ThrowUnauthorizedAccessException(ExceptionResourceNET resource)
        {
            throw new UnauthorizedAccessException(EnvironmentNET.GetResourceString(GetResourceName(resource)));
        }

        internal static void ThrowObjectDisposedException(string objectName, ExceptionResourceNET resource)
        {
            throw new ObjectDisposedException(objectName, EnvironmentNET.GetResourceString(GetResourceName(resource)));
        }

        // Allow nulls for reference types and Nullable<U>, but not for value types.
        internal static void IfNullAndNullsAreIllegalThenThrow<T>(object value, ExceptionArgumentNET argName)
        {
            // Note that default(T) is not equal to null for value types except when T is Nullable<U>. 
            if (value == null && !(default(T) == null))
                ThrowHelperNET.ThrowArgumentNullException(argName);
        }

        //
        // This function will convert an ExceptionArgument enum value to the argument name string.
        //
        internal static string GetArgumentName(ExceptionArgumentNET argument)
        {
            string argumentName = null;

            switch (argument)
            {
                case ExceptionArgumentNET.array:
                    argumentName = "array";
                    break;

                case ExceptionArgumentNET.arrayIndex:
                    argumentName = "arrayIndex";
                    break;

                case ExceptionArgumentNET.capacity:
                    argumentName = "capacity";
                    break;

                case ExceptionArgumentNET.collection:
                    argumentName = "collection";
                    break;

                case ExceptionArgumentNET.list:
                    argumentName = "list";
                    break;

                case ExceptionArgumentNET.converter:
                    argumentName = "converter";
                    break;

                case ExceptionArgumentNET.count:
                    argumentName = "count";
                    break;

                case ExceptionArgumentNET.dictionary:
                    argumentName = "dictionary";
                    break;

                case ExceptionArgumentNET.dictionaryCreationThreshold:
                    argumentName = "dictionaryCreationThreshold";
                    break;

                case ExceptionArgumentNET.index:
                    argumentName = "index";
                    break;

                case ExceptionArgumentNET.info:
                    argumentName = "info";
                    break;

                case ExceptionArgumentNET.key:
                    argumentName = "key";
                    break;

                case ExceptionArgumentNET.match:
                    argumentName = "match";
                    break;

                case ExceptionArgumentNET.obj:
                    argumentName = "obj";
                    break;

                case ExceptionArgumentNET.queue:
                    argumentName = "queue";
                    break;

                case ExceptionArgumentNET.stack:
                    argumentName = "stack";
                    break;

                case ExceptionArgumentNET.startIndex:
                    argumentName = "startIndex";
                    break;

                case ExceptionArgumentNET.value:
                    argumentName = "value";
                    break;

                case ExceptionArgumentNET.name:
                    argumentName = "name";
                    break;

                case ExceptionArgumentNET.mode:
                    argumentName = "mode";
                    break;

                case ExceptionArgumentNET.item:
                    argumentName = "item";
                    break;

                case ExceptionArgumentNET.options:
                    argumentName = "options";
                    break;

                case ExceptionArgumentNET.view:
                    argumentName = "view";
                    break;

                case ExceptionArgumentNET.sourceBytesToCopy:
                    argumentName = "sourceBytesToCopy";
                    break;

                default:
                    Contract.Assert(false, "The enum value is not defined, please checked ExceptionArgumentName Enum.");
                    return string.Empty;
            }

            return argumentName;
        }

        //
        // This function will convert an ExceptionResource enum value to the resource string.
        //
        internal static string GetResourceName(ExceptionResourceNET resource)
        {
            string resourceName = null;

            switch (resource)
            {
                case ExceptionResourceNET.Argument_ImplementIComparable:
                    resourceName = "Argument_ImplementIComparable";
                    break;

                case ExceptionResourceNET.Argument_AddingDuplicate:
                    resourceName = "Argument_AddingDuplicate";
                    break;

                case ExceptionResourceNET.ArgumentOutOfRange_BiggerThanCollection:
                    resourceName = "ArgumentOutOfRange_BiggerThanCollection";
                    break;

                case ExceptionResourceNET.ArgumentOutOfRange_Count:
                    resourceName = "ArgumentOutOfRange_Count";
                    break;

                case ExceptionResourceNET.ArgumentOutOfRange_Index:
                    resourceName = "ArgumentOutOfRange_Index";
                    break;

                case ExceptionResourceNET.ArgumentOutOfRange_InvalidThreshold:
                    resourceName = "ArgumentOutOfRange_InvalidThreshold";
                    break;

                case ExceptionResourceNET.ArgumentOutOfRange_ListInsert:
                    resourceName = "ArgumentOutOfRange_ListInsert";
                    break;

                case ExceptionResourceNET.ArgumentOutOfRange_NeedNonNegNum:
                    resourceName = "ArgumentOutOfRange_NeedNonNegNum";
                    break;

                case ExceptionResourceNET.ArgumentOutOfRange_SmallCapacity:
                    resourceName = "ArgumentOutOfRange_SmallCapacity";
                    break;

                case ExceptionResourceNET.Arg_ArrayPlusOffTooSmall:
                    resourceName = "Arg_ArrayPlusOffTooSmall";
                    break;

                case ExceptionResourceNET.Arg_RankMultiDimNotSupported:
                    resourceName = "Arg_RankMultiDimNotSupported";
                    break;

                case ExceptionResourceNET.Arg_NonZeroLowerBound:
                    resourceName = "Arg_NonZeroLowerBound";
                    break;

                case ExceptionResourceNET.Argument_InvalidArrayType:
                    resourceName = "Argument_InvalidArrayType";
                    break;

                case ExceptionResourceNET.Argument_InvalidOffLen:
                    resourceName = "Argument_InvalidOffLen";
                    break;

                case ExceptionResourceNET.Argument_ItemNotExist:
                    resourceName = "Argument_ItemNotExist";
                    break;

                case ExceptionResourceNET.InvalidOperation_CannotRemoveFromStackOrQueue:
                    resourceName = "InvalidOperation_CannotRemoveFromStackOrQueue";
                    break;

                case ExceptionResourceNET.InvalidOperation_EmptyQueue:
                    resourceName = "InvalidOperation_EmptyQueue";
                    break;

                case ExceptionResourceNET.InvalidOperation_EnumOpCantHappen:
                    resourceName = "InvalidOperation_EnumOpCantHappen";
                    break;

                case ExceptionResourceNET.InvalidOperation_EnumFailedVersion:
                    resourceName = "InvalidOperation_EnumFailedVersion";
                    break;

                case ExceptionResourceNET.InvalidOperation_EmptyStack:
                    resourceName = "InvalidOperation_EmptyStack";
                    break;

                case ExceptionResourceNET.InvalidOperation_EnumNotStarted:
                    resourceName = "InvalidOperation_EnumNotStarted";
                    break;

                case ExceptionResourceNET.InvalidOperation_EnumEnded:
                    resourceName = "InvalidOperation_EnumEnded";
                    break;

                case ExceptionResourceNET.NotSupported_KeyCollectionSet:
                    resourceName = "NotSupported_KeyCollectionSet";
                    break;

                case ExceptionResourceNET.NotSupported_ReadOnlyCollection:
                    resourceName = "NotSupported_ReadOnlyCollection";
                    break;

                case ExceptionResourceNET.NotSupported_ValueCollectionSet:
                    resourceName = "NotSupported_ValueCollectionSet";
                    break;


                case ExceptionResourceNET.NotSupported_SortedListNestedWrite:
                    resourceName = "NotSupported_SortedListNestedWrite";
                    break;


                case ExceptionResourceNET.Serialization_InvalidOnDeser:
                    resourceName = "Serialization_InvalidOnDeser";
                    break;

                case ExceptionResourceNET.Serialization_MissingKeys:
                    resourceName = "Serialization_MissingKeys";
                    break;

                case ExceptionResourceNET.Serialization_NullKey:
                    resourceName = "Serialization_NullKey";
                    break;

                case ExceptionResourceNET.Argument_InvalidType:
                    resourceName = "Argument_InvalidType";
                    break;

                case ExceptionResourceNET.Argument_InvalidArgumentForComparison:
                    resourceName = "Argument_InvalidArgumentForComparison";
                    break;

                case ExceptionResourceNET.InvalidOperation_NoValue:
                    resourceName = "InvalidOperation_NoValue";
                    break;

                case ExceptionResourceNET.InvalidOperation_RegRemoveSubKey:
                    resourceName = "InvalidOperation_RegRemoveSubKey";
                    break;

                case ExceptionResourceNET.Arg_RegSubKeyAbsent:
                    resourceName = "Arg_RegSubKeyAbsent";
                    break;

                case ExceptionResourceNET.Arg_RegSubKeyValueAbsent:
                    resourceName = "Arg_RegSubKeyValueAbsent";
                    break;

                case ExceptionResourceNET.Arg_RegKeyDelHive:
                    resourceName = "Arg_RegKeyDelHive";
                    break;

                case ExceptionResourceNET.Security_RegistryPermission:
                    resourceName = "Security_RegistryPermission";
                    break;

                case ExceptionResourceNET.Arg_RegSetStrArrNull:
                    resourceName = "Arg_RegSetStrArrNull";
                    break;

                case ExceptionResourceNET.Arg_RegSetMismatchedKind:
                    resourceName = "Arg_RegSetMismatchedKind";
                    break;

                case ExceptionResourceNET.UnauthorizedAccess_RegistryNoWrite:
                    resourceName = "UnauthorizedAccess_RegistryNoWrite";
                    break;

                case ExceptionResourceNET.ObjectDisposed_RegKeyClosed:
                    resourceName = "ObjectDisposed_RegKeyClosed";
                    break;

                case ExceptionResourceNET.Arg_RegKeyStrLenBug:
                    resourceName = "Arg_RegKeyStrLenBug";
                    break;

                case ExceptionResourceNET.Argument_InvalidRegistryKeyPermissionCheck:
                    resourceName = "Argument_InvalidRegistryKeyPermissionCheck";
                    break;

                case ExceptionResourceNET.NotSupported_InComparableType:
                    resourceName = "NotSupported_InComparableType";
                    break;

                case ExceptionResourceNET.Argument_InvalidRegistryOptionsCheck:
                    resourceName = "Argument_InvalidRegistryOptionsCheck";
                    break;

                case ExceptionResourceNET.Argument_InvalidRegistryViewCheck:
                    resourceName = "Argument_InvalidRegistryViewCheck";
                    break;

                default:
                    Contract.Assert(false, "The enum value is not defined, please checked ExceptionArgumentName Enum.");
                    return string.Empty;
            }

            return resourceName;
        }

    }

    //
    // The convention for this enum is using the argument name as the enum name
    // 
    internal enum ExceptionArgumentNET
    {
        obj,
        dictionary,
        dictionaryCreationThreshold,
        array,
        info,
        key,
        collection,
        list,
        match,
        converter,
        queue,
        stack,
        capacity,
        index,
        startIndex,
        value,
        count,
        arrayIndex,
        name,
        mode,
        item,
        options,
        view,
        sourceBytesToCopy,
    }

    //
    // The convention for this enum is using the resource name as the enum name
    // 
    internal enum ExceptionResourceNET
    {
        Argument_ImplementIComparable,
        Argument_InvalidType,
        Argument_InvalidArgumentForComparison,
        Argument_InvalidRegistryKeyPermissionCheck,
        ArgumentOutOfRange_NeedNonNegNum,
        ArgumentOutOfRange_NeedNonNegNumRequired,

        Arg_ArrayPlusOffTooSmall,
        Arg_NonZeroLowerBound,
        Arg_RankMultiDimNotSupported,
        Arg_RegKeyDelHive,
        Arg_RegKeyStrLenBug,
        Arg_RegSetStrArrNull,
        Arg_RegSetMismatchedKind,
        Arg_RegSubKeyAbsent,
        Arg_RegSubKeyValueAbsent,

        Argument_AddingDuplicate,
        Serialization_InvalidOnDeser,
        Serialization_MissingKeys,
        Serialization_NullKey,
        Argument_InvalidArrayType,
        NotSupported_KeyCollectionSet,
        NotSupported_ValueCollectionSet,
        ArgumentOutOfRange_SmallCapacity,
        ArgumentOutOfRange_Index,
        Argument_InvalidOffLen,
        Argument_ItemNotExist,
        ArgumentOutOfRange_Count,
        ArgumentOutOfRange_InvalidThreshold,
        ArgumentOutOfRange_ListInsert,
        NotSupported_ReadOnlyCollection,
        InvalidOperation_CannotRemoveFromStackOrQueue,
        InvalidOperation_EmptyQueue,
        InvalidOperation_EnumOpCantHappen,
        InvalidOperation_EnumFailedVersion,
        InvalidOperation_EmptyStack,
        ArgumentOutOfRange_BiggerThanCollection,
        InvalidOperation_EnumNotStarted,
        InvalidOperation_EnumEnded,
        NotSupported_SortedListNestedWrite,
        InvalidOperation_NoValue,
        InvalidOperation_RegRemoveSubKey,
        Security_RegistryPermission,
        UnauthorizedAccess_RegistryNoWrite,
        ObjectDisposed_RegKeyClosed,
        NotSupported_InComparableType,
        Argument_InvalidRegistryOptionsCheck,
        Argument_InvalidRegistryViewCheck,

        //Argument_ImplementIComparable,
        //ArgumentOutOfRange_NeedNonNegNum,
        //Arg_ArrayPlusOffTooSmall,
        //Argument_AddingDuplicate,
        Serialization_MismatchedCount,
        Serialization_MissingValues,
        //Arg_RankMultiDimNotSupported,
        //Arg_NonZeroLowerBound,
        //Argument_InvalidArrayType,
        //NotSupported_KeyCollectionSet,
        //ArgumentOutOfRange_SmallCapacity,
        //ArgumentOutOfRange_Index,
        //Argument_InvalidOffLen,
        //NotSupported_ReadOnlyCollection,
        //InvalidOperation_CannotRemoveFromStackOrQueue,
        //InvalidOperation_EmptyCollection,
        //InvalidOperation_EmptyQueue,
        //InvalidOperation_EnumOpCantHappen,
        //InvalidOperation_EnumFailedVersion,
        //InvalidOperation_EmptyStack,
        //InvalidOperation_EnumNotStarted,
        //InvalidOperation_EnumEnded,
        //NotSupported_SortedListNestedWrite,
        //NotSupported_ValueCollectionSet,
    }
}
