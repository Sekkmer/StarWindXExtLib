using System;
using System.ComponentModel;

namespace StarWindXExtLib
{
    public interface IAbstractValue : INotifyPropertyChanged
    {
        IStarWindServerExt Server { get; set; }
        string Name { get; }
        bool Valid { get; }
        Type ValueType { get; }
        dynamic DynamicValue { get; }
    }
}