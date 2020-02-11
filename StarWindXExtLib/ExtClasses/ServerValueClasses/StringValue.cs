using System.Runtime.CompilerServices;

namespace StarWindXExtLib
{

    internal class StringValue : AbstactValue<string>
    {

        public StringValue([CallerMemberName]string name = null) : base(name)
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