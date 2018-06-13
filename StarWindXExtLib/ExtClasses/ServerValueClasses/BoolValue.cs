using System.Runtime.CompilerServices;

namespace StarWindXExtLib {
    internal class BoolValue : AbstactValue<bool> {
        public string TrueString { get; set; } = "yes";
        public string FalseString { get; set; } = "no";

        public BoolValue([CallerMemberName]string name = null) : base(name) { }

        protected override bool FromString(string value) {
            return value == TrueString;
        }

        protected override string ToString(bool value) {
            return value ? TrueString : FalseString;
        }

        protected override bool IsValid(string str) {
            return str == TrueString || str == FalseString;
        }
    }
}
