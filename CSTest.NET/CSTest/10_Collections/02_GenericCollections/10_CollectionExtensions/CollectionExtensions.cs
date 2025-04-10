using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSTest._10_Collections._02_GenericCollections._10_CollectionExtensions
{
    public static class CollectionExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                return true;
            }
            ICollection<T> collectionGeneric = source as ICollection<T>;
            if (collectionGeneric != null)
            {
                return collectionGeneric.Count == 0;
            }
            ICollection collection = source as ICollection;
            if (collection != null)
            {
                return collection.Count == 0;
            }

            return !source.Any();
        } 
    }
}
