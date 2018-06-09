using System;
using System.Collections;
using System.Collections.Generic;

namespace StarWindXExtLib {
    internal class CollectionExt : StarWindXLib.ICollection {
        private List<dynamic> list = new List<dynamic>();

        public dynamic Item(object index) {
            return list[Convert.ToInt32(index)];
        }

        public IEnumerator GetEnumerator() => list.GetEnumerator();

        public int Add(object value) {
            return ((IList)list).Add(value);
        }

        public bool Contains(object value) {
            return ((IList)list).Contains(value);
        }

        public void Clear() {
            ((IList)list).Clear();
        }

        public int IndexOf(object value) {
            return ((IList)list).IndexOf(value);
        }

        public void Insert(int index, object value) {
            ((IList)list).Insert(index, value);
        }

        public void Remove(object value) {
            ((IList)list).Remove(value);
        }

        public void RemoveAt(int index) {
            ((IList)list).RemoveAt(index);
        }

        public void CopyTo(Array array, int index) {
            ((IList)list).CopyTo(array, index);
        }

        public int Count => list.Count;

        public bool IsReadOnly => ((IList)list).IsReadOnly;

        public bool IsFixedSize => ((IList)list).IsFixedSize;

        public object SyncRoot => ((IList)list).SyncRoot;

        public bool IsSynchronized => ((IList)list).IsSynchronized;
    }
}
