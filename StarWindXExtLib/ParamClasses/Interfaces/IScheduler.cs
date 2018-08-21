using System;

namespace StarWindXExtLib {

    public interface IScheduler : IServerCommand {
        IHADeviceExt Device { get; set; }
        int Id { get; set; }
        DateTime First { get; set; }
        TimeSpan Period { get; set; }
        int Preserve { get; set; }
    }
}