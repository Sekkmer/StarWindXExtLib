using System;
using StarWindXLib;

namespace StarWindXExtLib {
    internal abstract class AbstactValue<T> : IValue<T> {
        public IStarWindServer Server { get; set; }

        public string Name { get; }

        public Type ValueType => typeof(T);

        public bool Valid => IsValid(Server.GetServerParameter(Name));

        public T Value {
            get => FromString(Server.GetServerParameter(Name));
            set => Server.SetServerParameter(Name, ToString(value));
        }

        protected AbstactValue(string name) {
            Name = name;
        }

        protected abstract bool IsValid(string str);
        protected abstract T FromString(string value);
        protected abstract string ToString(T value);
    }
}
