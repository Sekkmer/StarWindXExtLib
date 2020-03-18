using System;

namespace StarWindXExtLib
{

    public static partial class EnumFormat
    {
        public static string ToString(this StarWindXLib.SW_HA_NODE_TYPE type)
        {
            return ((int)type).ToString();
        }

        public static void FromString(ref this StarWindXLib.SW_HA_NODE_TYPE type, string str)
        {
            type = (StarWindXLib.SW_HA_NODE_TYPE) Enum.Parse(type.GetType(), str);
        }
    }
}