using System.Runtime.CompilerServices;

namespace StarWindXExtLib
{
    public interface IBoolValue : IValue<bool> { }
    internal class BoolValue : AbstractValue<bool>, IBoolValue
    {
        public BoolValue([CallerMemberName] string name = null) : base(name)
        {
        }

        public string TrueString { get; set; } = "yes";
        public string FalseString { get; set; } = "no";

        protected override bool FromString(string value)
        {
            return value == TrueString;
        }

        protected override string ToString(bool value)
        {
            return value ? TrueString : FalseString;
        }

        protected override bool IsValid(string str)
        {
            return str == TrueString || str == FalseString;
        }
    }
}