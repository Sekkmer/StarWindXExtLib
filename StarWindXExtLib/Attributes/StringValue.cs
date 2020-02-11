using System;
using System.Runtime.CompilerServices;

namespace StarWindXExtLib
{

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class StringValueAttribute : Attribute
    {
        public string Value { get; }

        public StringValueAttribute([CallerMemberName] string fieldName = null)
        {
            Value = fieldName;
        }
    }
}