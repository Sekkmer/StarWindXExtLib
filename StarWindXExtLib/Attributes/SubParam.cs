using System;
using System.Runtime.CompilerServices;

namespace StarWindXExtLib
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SubParamAttribute : Attribute, IConditional
    {
        public SubParamAttribute([CallerMemberName] string propertyName = null)
        {
            Name = propertyName;
            Enabled = true;
        }

        public SubParamAttribute(bool enable, [CallerMemberName] string propertyName = null)
        {
            Name = propertyName;
            Enabled = enable;
        }

        public string Prefix { get; set; } = "";
        public string Name { get; }

        public bool Enabled { get; }
    }
}