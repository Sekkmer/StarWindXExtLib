using System;
using System.Runtime.CompilerServices;

namespace StarWindXExtLib {
    [AttributeUsage(AttributeTargets.Property)]
    public class FlatParamAttribute : Attribute, IConditional {

        public string Name { get; }

        public string Prefix { get; }

        public bool Enabled { get; }

        public FlatParamAttribute(bool enable = true, [CallerMemberName] string propertyName = null) {
            Name = propertyName;
            Prefix = "";
            Enabled = enable;
        }

        public FlatParamAttribute(string prefix, bool enable = true, [CallerMemberName] string propertyName = null) {
            Name = propertyName;
            Prefix = prefix;
            Enabled = enable;
        }
    }
}
