using System;
using System.Collections.Generic;

namespace StarWindXExtLib {

    public class HAImageHeaderCreator : ServerControl, IHAImageHeaderCreator {
        public override string SendTo => "HAImage";

        public override string Request => "CreateHeader";

        public override string RequestValue { get; protected set; } = "";

        public string DeviceHeaderPath => Device.DeviceHeaderPath.Substring(0, Device.DeviceHeaderPath.LastIndexOf('.')) + "_HA.swdsk";

        [Param("Type")]
        public string TypeString => "ImageFile_HA";

        [Param("file")]
        public string DeviceName => Device.Name;

        [Param("size")]
        public int DeviceSize => Convert.ToInt32(Device.Size);

        [FlatParam]
        public IAdvancedHANodes Nodes { get; set; }

        [Param]
        [BoolToString("1", "0")]
        public bool IsAutoSynchEnabled => ThisNode.IsAutoSynchEnabled;

        [Param]
        public int SynchSessionsCount => ThisNode.SynchSessionsCount;

        [Param]
        public int Offset => ThisNode.Offset;

        [FlatParam]
        public ICacheParam Cache => ThisNode.Cache;

        [Param("serial")]
        public string SerialId { get; set; }

        [Param("eui64")]
        public string Eui64 => SerialId;

        [Param("revision")]
        public string Revision => "0001";

        [Param("product")]
        public string Product { get; set; } = "STARWIND";

        [Param("vendor")]
        public string Vendor { get; set; } = "STARWIND";

        [Param(false, "task")]
        public string TaskString => "#p0:\"" + Nodes.First.Task.Value + "\"";

        [EnableParam("task")]
        public bool EnableTask => Nodes.First.Task.Enabled;

        [Param]
        public FailoverConfType FailoverConfType => ThisNode.FailoverConfType;

        [Param("Replicator")]
        public string ReplicatorString {
            get {
                var str = "";
                for (var i = 1; i <= Nodes.Count; i++) {
                    if (i > 1) { str += ";"; }
                    str += "#p" + i.ToString() + "=" + Replicator.ToString();
                }
                return str;
            }
        }

        public int Replicator { get; set; } = 0;

        private IAdvancedHANode ThisNode => Nodes.First;
        public IDeviceExt Device { get; set; }

        public string OwnTargetName => ThisNode.TargetName;

        public HAImageHeaderCreator(IAdvancedHANodes nodes) {
            Nodes = nodes;
        }

        public HAImageHeaderCreator(List<IAdvancedHANode> list) {
            Nodes = new AdvancedHANodes(list);
        }
    }
}