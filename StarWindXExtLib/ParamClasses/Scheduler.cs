using System;

namespace StarWindXExtLib
{
    public class Scheduler : ParameterAppender, IScheduler
    {
        [Param("device")] public string DeviceId => Device.DeviceId;

        [EnableParam("preserve")] public bool AsyncMode => Device.NodeType == NodeType.Asynchronous;

        [EnableParam("first", "period")] public bool SyncMode => Device.NodeType == NodeType.Synchronous;

        public string Command => "schedule";

        [Param("id")] public int Id { get; set; }

        [Param(false, "first")] public DateTime First { get; set; }

        [Param(false, "period")] public TimeSpan Period { get; set; }

        [Param(false, "preserve")] public int Preserve { get; set; }

        public IHADeviceExt Device { get; set; }
    }
}