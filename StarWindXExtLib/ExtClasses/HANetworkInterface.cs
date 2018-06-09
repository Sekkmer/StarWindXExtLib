namespace StarWindXExtLib {
    public class HANetworkInterface : IHANetworkInterface {
        public string IPAddress { get; }

        public int Port { get; }

        public bool Valid { get; }

        public int PartnerId { get; }

        public NetworkInterfaceType InterfaceType { get; }

        public HANetworkInterface(int partnerId, NetworkInterfaceType type, string ip, int port, bool valid = true) {
            IPAddress = ip;
            Port = port;
            Valid = valid;
            PartnerId = partnerId;
            InterfaceType = type;
        }
    }
}
