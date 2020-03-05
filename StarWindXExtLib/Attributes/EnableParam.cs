using System;
using System.Linq;

namespace StarWindXExtLib
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class EnableParamAttribute : Attribute
    {
        public EnableParamAttribute(params string[] name)
        {
            CheckName = str => name.Contains(str);
        }

        public Predicate<string> CheckName { get; }
    }
}