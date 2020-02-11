namespace StarWindXExtLib
{

    public enum CacheMode
    {

        [StringValue("none")]
        None,

        [StringValue("wb")]
        WriteBack,

        [StringValue("wt")]
        WriteThrough
    }

    public static partial class EnumFormat
    {

        public static string ToString(this CacheMode type)
        {
            return EnumToString(type);
        }

        public static void FromString(this CacheMode type, string str)
        {
            EnumFromString(type, str);
        }

        public static CacheMode ToCacheMode(string str)
        {
            var type = CacheMode.None;
            type.FromString(str);
            return type;
        }
    }
}