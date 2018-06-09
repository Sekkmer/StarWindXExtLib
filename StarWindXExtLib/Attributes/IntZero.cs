using System;

namespace StarWindXExtLib {
    [AttributeUsage(AttributeTargets.Property)]
    public class IntZeroAttribute : Attribute {
        public string Value { get; }
        public IntZeroAttribute(string value) {
            Value = value;
        }
    }
}
