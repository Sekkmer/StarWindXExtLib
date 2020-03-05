using System;

namespace StarWindXExtLib
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IntZeroAttribute : Attribute
    {
        public IntZeroAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}