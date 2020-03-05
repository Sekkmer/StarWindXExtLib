using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ICollection = StarWindXLib.ICollection;

namespace StarWindXExtLib
{
    internal class CollectionExt<T> : ICollection
    {
        private readonly IEnumerable<T> enumerable;

        public CollectionExt(IEnumerable<T> enumerable)
        {
            this.enumerable = enumerable;
        }

        public object Item(object index)
        {
            return enumerable.ElementAt((int) index);
        }

        public IEnumerator GetEnumerator()
        {
            return enumerable.GetEnumerator();
        }

        public int Count => enumerable.Count();
    }
}