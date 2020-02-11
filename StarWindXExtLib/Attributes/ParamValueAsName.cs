using System;
using System.Runtime.CompilerServices;

namespace StarWindXExtLib
{

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ParamValueAsNameAttribute : Attribute, IConditional
    {
        public string Name { get; }

        public bool Enabled { get; }

        public string ValueProperyName { get; set; } = "";

        public ParamValueAsNameAttribute([CallerMemberName] string propertyName = null)
        {
            Name = propertyName;
            ValueProperyName = Name + "Value";
            Enabled = true;
        }

        public ParamValueAsNameAttribute(bool enable, [CallerMemberName] string propertyName = null)
        {
            Name = propertyName;
            ValueProperyName = Name + "Value";
            Enabled = enable;
        }
    }
}