namespace StarWindXExtLib
{
    public enum FailoverConfType
    {
        [StringValue("0")] Heartbeat = 0,

        [StringValue("1")] NodeMajority = 1
    }

    public static partial class EnumFormat
    {
        public static string ToString(this FailoverConfType type)
        {
            return EnumToString(type);
        }

        public static void FromString(this FailoverConfType type, string str)
        {
            EnumFromString(type, str);
        }
    }
}