using System;

namespace StarWindXExtLib
{

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class BoolToStringAttribute : Attribute
    {
        public string TrueString { get; }
        public string FalseString { get; }

        public BoolToStringAttribute(string trueString, string falseString)
        {
            TrueString = trueString;
            FalseString = falseString;
        }
    }
}