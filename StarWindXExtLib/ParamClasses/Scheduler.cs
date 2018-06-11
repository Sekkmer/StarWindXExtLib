using System;

namespace StarWindXExtLib {

    public class Scheduler : ParameterAppender, IScheduler {
        public string Command => "schedule";

        [Param("device")]
        public string DeviceId => Device.DeviceId;
        [Param("id")]
        public int Id { get; set; }
        [Param(false, "first")]
        public DateTime First { get; set; }
        [Param(false, "period")]
        public TimeSpan Period { get; set; }
        [Param(false, "preserve")]
        public int Preserve { get; set; }

        [EnableParam("preserve")]
        public bool AsyncMode => Device.NodeType == NodeType.Asynchronous;
        [EnableParam("first", "period")]
        public bool SyncMode => Device.NodeType == NodeType.Synchronous;

        public IHADeviceExt Device { get; set; }
    }
}
