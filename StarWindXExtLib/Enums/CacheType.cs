namespace StarWindXExtLib {

    public enum CacheType {

        [StringValue("none")]
        None,

        [StringValue("wb")]
        WriteBack,

        [StringValue("wt")]
        WriteThrough
    }

    public static partial class EnumFormat {

        public static string ToString(this CacheType type) {
            return EnumToString(type);
        }

        public static void FromString(this CacheType type, string str) {
            EnumFromString(type, str);
        }

        public static CacheType ToCacheType(string str) {
            var type = CacheType.None;
            type.FromString(str);
            return type;
        }
    }
}