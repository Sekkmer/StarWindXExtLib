namespace StarWindXExtLib {

    public enum NodeType {

        [StringValue("1")]
        Synchronous = 1,

        [StringValue("2")]
        Asynchronous = 2,

        [StringValue("8")]
        Witness = 8,
    }

    public static partial class EnumFormat {

        public static string ToString(this NodeType type) {
            return EnumToString(type);
        }

        public static void FromString(this NodeType type, string str) {
            EnumFromString(type, str);
        }
    }
}