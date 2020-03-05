using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StarWindXExtLib
{
    internal abstract class AbstractValue<T> : IValue<T>
    {
        protected AbstractValue(string name)
        {
            Name = name;
        }

        public IStarWindServerExt Server { get; set; }

        public string Name { get; }

        public Type ValueType => typeof(T);

        public bool Valid => IsValid(Server.GetServerParameter(Name) ?? "");

        public T Value
        {
            get => FromString(Server.GetServerParameter(Name));
            set
            {
                Server.SetServerParameter(Name, ToString(value));
                OnPropertyChanged();
            }
        }

        public dynamic DynamicValue => Value; 

        public override string ToString()
        {
            return Server.GetServerParameter(Name);
        }

        protected abstract bool IsValid(string str);

        protected abstract T FromString(string value);

        protected abstract string ToString(T value);
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}