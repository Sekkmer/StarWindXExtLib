using System;
using StarWindXLib;

namespace StarWindXExtLib
{
    internal class LSFSDeviceExt : DeviceExt, ILSFSDeviceExt
    {
        private readonly ILSFSDevice lsfsDevice;

        public LSFSDeviceExt(ILSFSDevice lsfs) : base(lsfs)
        {
            lsfsDevice = lsfs;
        }

        private string MountStatusString => GetPropertyValue("MountStatus");


        private string ModeString => GetPropertyValue("mode");

        public bool DeduplicationEnabled => GetPropertyValue("DeduplicationEnabled") == "yes";

        public bool IsDeviceASnapshot => GetPropertyValue("IsDeviceASnapshot") == "yes";

        public bool SnapshotInProgress => GetPropertyValue("SnapshotInProgress") == "yes";

        public SW_LSFS_MOUNT_STATUS MountStatus => GetMountStatus();

        public SW_LSFS_MODE Mode => GetMode();

        public int BlockSize => GetPropertyValueAsInt("BlockSize");

        public int SnapshotOffset => GetPropertyValueAsInt("SnapshotOffset");

        public string SnapshotName => GetPropertyValue("SnapshotName");

        public DateTime? SnapshotTime => GetPropertyValueAsDateTime("SnapshotTime");

        public string PMCacheModeString => GetPropertyValue("PMCacheMode");

        public CacheMode PMCacheMode => EnumFormat.ToCacheMode(PMCacheModeString);

        public int PMCacheSizeMB => GetPropertyValueAsInt("PMCacheSizeMB");

        public string L2CacheModeString => GetPropertyValue("L2CacheMode");

        public CacheMode L2CacheMode => EnumFormat.ToCacheMode(L2CacheModeString);

        private SW_LSFS_MODE GetMode()
        {
            try
            {
                return (SW_LSFS_MODE) Convert.ToInt32(ModeString);
            }
            catch (Exception)
            {
                return SW_LSFS_MODE.Unknown;
            }
        }

        private SW_LSFS_MOUNT_STATUS GetMountStatus()
        {
            try
            {
                return (SW_LSFS_MOUNT_STATUS) Convert.ToInt32(MountStatusString);
            }
            catch (Exception)
            {
                return SW_LSFS_MOUNT_STATUS.Unknown;
            }
        }

        #region ILSFSDevice

        public string DeduplicationRatio => lsfsDevice.DeduplicationRatio;

        public string UserDataSize => lsfsDevice.UserDataSize;

        public string MetaDataSize => lsfsDevice.MetaDataSize;

        public string AllocatedPhysicalSize => lsfsDevice.AllocatedPhysicalSize;

        public string DeviceFragmentedPercent => lsfsDevice.DeviceFragmentedPercent;

        #endregion ILSFSDevice
    }
}