using StarWindXLib;

namespace StarWindXExtLib
{
    public class HANetworkInterface : IHANetworkInterface
    {
        public HANetworkInterface(int partnerId, NetworkInterfaceType type, string ip, string port, bool valid = true)
        {
            IPAddress = ip;
            Port = port;
            Valid = valid;
            PartnerId = partnerId;
            InterfaceType = type;
        }

        internal HANetworkInterface(int partnerId, NetworkInterfaceType type, string data)
        {
            var array = data.Split('$');
            IPAddress = array[0];
            Port = array[1];
            Valid = array[2] == "1";
            PartnerId = partnerId;
            InterfaceType = type;
        }

        public string IPAddress { get; }

        public string Port { get; }

        public string SubnetMask => "";

        public string MACAddress => "";

        public bool Valid { get; }

        public int PartnerId { get; }

        public NetworkInterfaceType InterfaceType { get; }
    }

    public class NetworkInterfaceExt : IHANetworkInterface
    {
        public readonly INetworkInterface iface;

        public NetworkInterfaceExt(INetworkInterface iface)
        {
            this.iface = iface;
        }

        public bool Valid => true;

        public int PartnerId => -1;

        public NetworkInterfaceType InterfaceType => NetworkInterfaceType.Unknown;

        public string IPAddress => iface.IPAddress;

        public string Port => iface.Port;

        public string SubnetMask => iface.SubnetMask;

        public string MACAddress => iface.MACAddress;
    }
}