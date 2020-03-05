using System;
using StarWindXLib;

namespace StarWindXExtLib
{
    public enum SW_LSFS_MODE
    {
        Unknown = 0,
        Normal = 1,
        ReadOnly = 2,
        ManagementMode = 7
    }

    public enum SW_LSFS_MOUNT_STATUS
    {
        Unknown = -1,
        Failed = 0,
        Created = 1,
        Creating = 2,
        Mounting = 3
    }

    public interface ILSFSDeviceExt : IDeviceExt, ILSFSDevice
    {
        bool DeduplicationEnabled { get; }
        bool IsDeviceASnapshot { get; }
        bool SnapshotInProgress { get; }
        SW_LSFS_MOUNT_STATUS MountStatus { get; }
        SW_LSFS_MODE Mode { get; }
        int BlockSize { get; }
        int SnapshotOffset { get; }
        string SnapshotName { get; }
        DateTime? SnapshotTime { get; }
        string PMCacheModeString { get; }
        CacheMode PMCacheMode { get; }
        int PMCacheSizeMB { get; }
        string L2CacheModeString { get; }
        CacheMode L2CacheMode { get; }
    }
}