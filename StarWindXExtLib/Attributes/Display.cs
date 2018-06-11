using System;
using System.Runtime.CompilerServices;

namespace StarWindXExtLib {
    public class DisplayAttribute : Attribute {

        public int Index { get; }
        public string Name { get; }

        public Type CollecionType { get; set; }

        public DisplayAttribute(int index, [CallerMemberName] string propertyName = null) {
            Index = index;
            Name = propertyName;
        }
    }
}
