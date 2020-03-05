using System;
using System.Runtime.CompilerServices;

namespace StarWindXExtLib
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class StringValueAttribute : Attribute
    {
        public StringValueAttribute([CallerMemberName] string fieldName = null)
        {
            Value = fieldName;
        }

        public string Value { get; }
    }
}