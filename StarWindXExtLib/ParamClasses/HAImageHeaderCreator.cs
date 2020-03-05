using System;
using System.Collections.Generic;

namespace StarWindXExtLib
{
    public class HAImageHeaderCreator : ServerControl, IHAImageHeaderCreator
    {
        public HAImageHeaderCreator(IAdvancedHANodes nodes)
        {
            Nodes = nodes;
        }

        public HAImageHeaderCreator(List<IAdvancedHANode> list)
        {
            Nodes = new AdvancedHANodes(list);
        }

        public HAImageHeaderCreator()
        {
            Nodes = new AdvancedHANodes();
        }

        public override string RequestValue { get; protected set; } = "";

        [Param("Type")] public string TypeString => "ImageFile_HA";

        [Param("size")] public int DeviceSize => Convert.ToInt32(Device.Size) / 1024 / 1024;

        [Param] [BoolToString("1", "0")] public bool IsAutoSynchEnabled => ThisNode.IsAutoSynchEnabled;

        [Param] public int SynchSessionsCount => ThisNode.SynchSessionsCount;

        [Param] public int Offset => ThisNode.Offset;

        [Param("eui64")] public string Eui64 => SerialId;

        [Param("revision")] public string Revision => "0001";

        [Param(false, "task")] public string TaskString => "#p0:\"" + Nodes.First.Task.Value + "\"";

        [EnableParam("task")] public bool EnableTask => Nodes.First.Task.Enabled;

        [Param] public FailoverConfType FailoverConfType => ThisNode.FailoverConfType;

        [Param("Replicator")] public string ReplicatorString => "#p" + 0 + "=" + Replicator;

        private IAdvancedHANode ThisNode => Nodes.First;
        public override string SendTo => "HAImage";

        public override string Request => "CreateHeader";

        [Param]
        public string DeviceHeaderPath =>
            Device.DeviceHeaderPath.Substring(0, Device.DeviceHeaderPath.LastIndexOf('.')) + "_HA.swdsk";

        [Param("file")] public string DeviceName => Device.Name;

        [FlatParam] public IAdvancedHANodes Nodes { get; set; }

        [FlatParam] public ICacheParam Cache => ThisNode.Cache;

        [Param("serial")] public string SerialId { get; set; }

        [Param("product")] public string Product { get; set; } = "STARWIND        ";

        [Param("vendor")] public string Vendor { get; set; } = "STARWIND";

        public int Replicator { get; set; } = 0;
        public IDeviceExt Device { get; set; }

        public string OwnTargetName => ThisNode.TargetName;

        public void LoadNodes(List<IAdvancedHANode> list)
        {
            Nodes.LoadNodes(list);
        }
    }
}