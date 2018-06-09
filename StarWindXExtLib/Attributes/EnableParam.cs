using System;
using System.Linq;

namespace StarWindXExtLib {
    [AttributeUsage(AttributeTargets.Property)]
    public class EnableParamAttribute : Attribute {
        public Predicate<string> CheckName { get; }

        public EnableParamAttribute(params string[] name) {
            CheckName = str => name.Contains(str);
        }
    }
}
