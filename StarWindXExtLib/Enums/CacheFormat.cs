namespace StarWindXExtLib
{

    public enum FileSystemFormat
    {

        [StringValue("none")]
        NONE,

        [StringValue("fat16")]
        FAT16,

        [StringValue("fat32")]
        FAT32,

        [StringValue("raw")]
        RAW
    }

    public static partial class EnumFormat
    {

        public static string ToString(this FileSystemFormat format)
        {
            return EnumToString(format);
        }

        public static void FromString(this FileSystemFormat format, string str)
        {
            EnumFromString(format, str);
        }
    }
}