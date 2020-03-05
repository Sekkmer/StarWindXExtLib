using System;

namespace StarWindXExtLib
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class BoolToStringAttribute : Attribute
    {
        public BoolToStringAttribute(string trueString, string falseString)
        {
            TrueString = trueString;
            FalseString = falseString;
        }

        public string TrueString { get; }
        public string FalseString { get; }
    }
}