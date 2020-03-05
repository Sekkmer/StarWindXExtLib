using System;
using System.Reflection;

namespace StarWindXExtLib
{
    public static partial class EnumFormat
    {
        public static string EnumToString(object obj)
        {
            if (obj.GetType().GetField(obj.ToString()).GetCustomAttribute<StringValueAttribute>() is var attr)
                return attr.Value;
            return obj.ToString();
        }

        public static void EnumFromString(object obj, string str)
        {
            var type = obj.GetType();
            if (type.BaseType != typeof(Enum)) throw new ArgumentException("obj must be of type System.Enum");

            foreach (var value in Enum.GetValues(type))
                if (type.GetField(value.ToString()).GetCustomAttribute<StringValueAttribute>() is var attr &&
                    string.Compare(attr.Value, str, true) == 0)
                {
                    obj = value;
                    return;
                }

            throw new ArgumentException("string is not recognised");
        }
    }
}