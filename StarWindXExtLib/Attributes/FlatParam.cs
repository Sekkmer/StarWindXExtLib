using System;
using System.Runtime.CompilerServices;

namespace StarWindXExtLib
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FlatParamAttribute : Attribute, IConditional
    {
        public FlatParamAttribute(bool enable = true, [CallerMemberName] string propertyName = null)
        {
            Name = propertyName;
            Prefix = "";
            Enabled = enable;
        }

        public FlatParamAttribute(string prefix, bool enable = true, [CallerMemberName] string propertyName = null)
        {
            Name = propertyName;
            Prefix = prefix;
            Enabled = enable;
        }

        public string Prefix { get; }
        public string Name { get; }

        public bool Enabled { get; }
    }
}