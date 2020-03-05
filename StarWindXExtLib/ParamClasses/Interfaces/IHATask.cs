using System;

namespace StarWindXExtLib
{
    public interface IHATask
    {
        string Value { get; }

        NodeType NodeType { get; set; }
        int NodeTypeInt { get; }
        DateTime Time { get; set; }
        int Unknown1 { get; set; }
        int RepeatInterval { get; set; }
        int Unknown2 { get; set; }
        int Preserve { get; set; }
        bool Enabled { get; set; }
    }
}