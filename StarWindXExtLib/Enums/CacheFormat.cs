namespace StarWindXExtLib {

    public enum CacheFormat {

        [StringValue("none")]
        NONE,

        [StringValue("fat16")]
        FAT16,

        [StringValue("fat32")]
        FAT32,

        [StringValue("raw")]
        RAW
    }

    public static partial class EnumFormat {

        public static string ToString(this CacheFormat format) {
            return EnumToString(format);
        }

        public static void FromString(this CacheFormat format, string str) {
            EnumFromString(format, str);
        }
    }
}