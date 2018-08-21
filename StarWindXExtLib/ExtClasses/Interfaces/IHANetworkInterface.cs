namespace StarWindXExtLib {

    public enum NetworkInterfaceType {

        [StringValue("unknown")]
        Unknown,

        [StringValue("sync")]
        Synchronization,

        [StringValue("heartbeat")]
        Heartbeat
    }

    public static partial class EnumFormat {

        public static string ToString(this NetworkInterfaceType format) {
            return EnumToString(format);
        }

        public static void FromString(this NetworkInterfaceType format, string str) {
            EnumFromString(format, str);
        }
    }

    public interface IHANetworkInterface {
        string IPAddress { get; }
        int Port { get; }
        bool Valid { get; }
        int PartnerId { get; }
        NetworkInterfaceType InterfaceType { get; }
    }
}