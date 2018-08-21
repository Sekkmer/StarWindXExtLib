using System;
using System.Runtime.CompilerServices;

namespace StarWindXExtLib {

    [AttributeUsage(AttributeTargets.Property)]
    public class ParamAttribute : Attribute, IConditional {
        public string Name { get; }

        public bool Enabled { get; }

        public ParamAttribute([CallerMemberName] string propertyName = null) {
            Name = propertyName;
            Enabled = true;
        }

        public ParamAttribute(bool enable, [CallerMemberName] string propertyName = null) {
            Name = propertyName;
            Enabled = enable;
        }
    }
}