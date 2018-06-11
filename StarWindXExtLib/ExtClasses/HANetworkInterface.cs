namespace StarWindXExtLib {
    public class HANetworkInterface : Displayable, IHANetworkInterface {
        [Display(0)]
        public string IPAddress { get; }
        [Display(1)]
        public int Port { get; }
        [Display(2)]
        public bool Valid { get; }
        [Display(3)]
        public int PartnerId { get; }
        [Display(4, "Interface Type")]
        public NetworkInterfaceType InterfaceType { get; }      
      
        public override string UniqueId => PartnerId.ToString() + ":" + IPAddress + ":" + Port + ":" + InterfaceType.ToString();

        public HANetworkInterface(int partnerId, NetworkInterfaceType type, string ip, int port, bool valid = true) {
            IPAddress = ip;
            Port = port;
            Valid = valid;
            PartnerId = partnerId;
            InterfaceType = type;
        }
    }
}
