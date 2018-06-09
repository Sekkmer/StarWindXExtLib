using StarWindXLib;

namespace StarWindXExtLib {
    public interface IDeviceExt : IDevice {
        int PhySectorSize { get; }
        string ParentDevice { get; }
        string DeviceHeaderPath { get; }
        CacheType CacheType { get; }
        int L1CachePercentOfUsage { get; }
        int L1CachePercentOfHits { get; }
        string Path { get; }
    }
}