using System.Threading.Tasks;
using StarWindXLib;

namespace StarWindXExtLib
{
    public interface IDeviceExt : IDevice
    {
        int PhySectorSize { get; }
        string ParentDevice { get; }
        string DeviceHeaderPath { get; }
        CacheMode CacheModeEnum { get; }
        int L1CachePercentOfUsage { get; }
        int L1CachePercentOfHits { get; }
        string Path { get; }
        bool Attached { get; }

        Task RefreshAsync();
        Task ExtendDeviceAsync(int sizeToExtendInMB);
        Task TakeSnapshotAsync(string Name, string description);
        Task DeleteSnapshotAsync(ulong snapshotID);
    }
}