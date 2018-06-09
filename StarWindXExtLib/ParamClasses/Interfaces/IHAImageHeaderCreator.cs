namespace StarWindXExtLib {
    public interface IHAImageHeaderCreator : IServerControl, ICache {
        IAdvancedHANodes Nodes { get; set; }
        string SerialId { get; set; }
        string Product { get; set; }
        string Vendor { get; set; }
        int Replicator { get; set; }
        IDeviceExt Device { get; set; }

        string DeviceName { get; }
        string OwnTargetName { get; }
        string DeviceHeaderPath { get; }
    }
}
