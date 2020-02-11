using System;
using System.Runtime.CompilerServices;

namespace StarWindXExtLib
{

    internal class IntValue : AbstactValue<int>
    {

        public IntValue([CallerMemberName]string name = null) : base(name) { }

        protected override int FromString(string value)
        {
            return Convert.ToInt32(value);
        }

        protected override string ToString(int value)
        {
            return value.ToString();
        }

        protected override bool IsValid(string str)
        {
            try {
                Convert.ToInt32(str);
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}