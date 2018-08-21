using StarWindXLib;

using System;

namespace StarWindXExtLib {

    public interface IAbstactValue {
        IStarWindServer Server { get; set; }
        string Name { get; }
        bool Valid { get; }
        Type ValueType { get; }
    }
}