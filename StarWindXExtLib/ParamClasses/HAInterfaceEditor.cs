﻿namespace StarWindXExtLib {

    public class HAInterfaceEditor : ServerControl, IHAInterfaceEditor {
        public override string SendTo => Device.DeviceId;

        public override string Request => "AddRemovePartnerInterface";

        public override string RequestValue { get; protected set; } = "";

        [Param("action")]
        [BoolToString("add", "remove")]
        [EnableParam("Priority")]
        public bool ActionIsAdd { get; set; }
        [Param] public string PartnerTargetName => Device.GetPartnerTargetName(IFace.PartnerId);
        [Param] public NetworkInterfaceType ChannelType => IFace.InterfaceType;
        [Param] public string IP => IFace.IPAddress;
        [Param] public int Port => IFace.Port;
        [Param(false)]
        public int Priority { get; set; }

        public IHADeviceExt Device { get; }
        public IHANetworkInterface IFace { get; }

        public HAInterfaceEditor(IHADeviceExt device, IHANetworkInterface iface) {
            Device = device;
            IFace = iface;
        }
    }
}