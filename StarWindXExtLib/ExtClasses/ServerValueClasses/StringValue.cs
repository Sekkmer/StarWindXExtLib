using System.Runtime.CompilerServices;

namespace StarWindXExtLib
{
    public interface IStringValue : IValue<string> { }

    internal class StringValue : AbstractValue<string>, IStringValue
    {
        public StringValue([CallerMemberName] string name = null) : base(name)
        {
        }

        protected override string FromString(string value)
        {
            return value;
        }

        protected override string ToString(string value)
        {
            return value;
        }

        protected override bool IsValid(string str)
        {
            return true;
        }
    }
}