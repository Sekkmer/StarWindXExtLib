using System;
using StarWindXLib;

namespace StarWindXExtLib {
    internal class LSFSDeviceExt : DeviceExt, ILSFSDeviceExt {
        private ILSFSDevice lsfsDevice;

        public LSFSDeviceExt(ILSFSDevice lsfs) : base(lsfs) {
            lsfsDevice = lsfs;
        }

        public string DeduplicationRatio => lsfsDevice.DeduplicationRatio;

        public string UserDataSize => lsfsDevice.UserDataSize;

        public string MetaDataSize => lsfsDevice.MetaDataSize;

        public string AllocatedPhysicalSize => lsfsDevice.AllocatedPhysicalSize;

        public string DeviceFragmentedPercent => lsfsDevice.DeviceFragmentedPercent;

        public bool DeduplicationEnabled => GetPropertyValue("DeduplicationEnabled") == "yes";

        public bool IsDeviceASnapshot => GetPropertyValue("IsDeviceASnapshot") == "yes";

        public bool SnapshotInProgress => GetPropertyValue("SnapshotInProgress") == "yes";

        private string MountStatusString => GetPropertyValue("MountStatus");

        private SW_LSFS_MOUNT_STATUS GetMountStatus() {
            try {
                return (SW_LSFS_MOUNT_STATUS)Convert.ToInt32(MountStatusString);
            } catch (Exception) {
                return SW_LSFS_MOUNT_STATUS.Unknown;
            }
        }

        public SW_LSFS_MOUNT_STATUS MountStatus => GetMountStatus();

        private string ModeString => GetPropertyValue("mode");

        private SW_LSFS_MODE GetMode() {
            try {
                return (SW_LSFS_MODE)Convert.ToInt32(ModeString);
            } catch (Exception) {
                return SW_LSFS_MODE.Unknown;
            }
        }

        public SW_LSFS_MODE Mode => GetMode();

        public int BlockSize => GetPropertyValueAsInt("BlockSize");

        public int SnapshotOffset => GetPropertyValueAsInt("SnapshotOffset");
        public string SnapshotName => GetPropertyValue("SnapshotName");
        public DateTime? SnapshotTime => GetPropertyValueAsDateTime("SnapshotTime");
        public string PMCacheMode => GetPropertyValue("PMCacheMode");
        public CacheType PMCacheType => EnumFormat.ToCacheType(PMCacheMode);
        public int PMCacheSizeMB => GetPropertyValueAsInt("PMCacheSizeMB");
        public string L2CacheMode => GetPropertyValue("L2CacheMode");
        public CacheType L2CacheType => EnumFormat.ToCacheType(L2CacheMode);  
    }
}
