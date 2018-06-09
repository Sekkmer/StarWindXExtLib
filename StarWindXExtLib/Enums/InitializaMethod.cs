namespace StarWindXExtLib {
    public enum InitializaMethod {
        [StringValue] Clear,
        [StringValue] NotSynchronize,
        [StringValue] SyncFromFirst,
        [StringValue] SyncFromSecond,
        [StringValue] SyncFromThird,
    }

    public static partial class EnumFormat {
        public static string ToString(this InitializaMethod format) {
            return EnumToString(format);
        }

        public static void FromString(this InitializaMethod format, string str) {
            EnumFromString(format, str);
        }
    }
}
