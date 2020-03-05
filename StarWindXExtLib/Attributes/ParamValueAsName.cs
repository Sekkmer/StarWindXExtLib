using System;
using System.Runtime.CompilerServices;

namespace StarWindXExtLib
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ParamValueAsNameAttribute : Attribute, IConditional
    {
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

        public string ValueProperyName { get; set; } = "";
        public string Name { get; }

        public bool Enabled { get; }
    }
}