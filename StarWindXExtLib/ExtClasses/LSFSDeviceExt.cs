using StarWindXLib;

using System;

namespace StarWindXExtLib {

    internal class LSFSDeviceExt : DeviceExt, ILSFSDeviceExt {
        private ILSFSDevice lsfsDevice;

        public LSFSDeviceExt(ILSFSDevice lsfs) : base(lsfs) {
            lsfsDevice = lsfs;
        }

        [Display(1000, "Deduplication Ratio")]
        public string DeduplicationRatio => lsfsDevice.DeduplicationRatio;

        [Display(1001, "User Data Size")]
        public string UserDataSize => lsfsDevice.UserDataSize;

        [Display(1002, "Meta Data Size")]
        public string MetaDataSize => lsfsDevice.MetaDataSize;

        [Display(1003, "Allocated Physical Size")]
        public string AllocatedPhysicalSize => lsfsDevice.AllocatedPhysicalSize;

        [Display(1004, "Device Fragmented Percent")]
        public string DeviceFragmentedPercent => lsfsDevice.DeviceFragmentedPercent;

        [Display(1005, "Deduplication Enabled")]
        public bool DeduplicationEnabled => GetPropertyValue("DeduplicationEnabled") == "yes";

        [Display(1006, "Is Device A Snapshot")]
        public bool IsDeviceASnapshot => GetPropertyValue("IsDeviceASnapshot") == "yes";

        [Display(1007, "Snapshot In Progress")]
        public bool SnapshotInProgress => GetPropertyValue("SnapshotInProgress") == "yes";

        [Display(1008, "Mount Status String")]
        private string MountStatusString => GetPropertyValue("MountStatus");

        private SW_LSFS_MOUNT_STATUS GetMountStatus() {
            try {
                return (SW_LSFS_MOUNT_STATUS)Convert.ToInt32(MountStatusString);
            } catch (Exception) {
                return SW_LSFS_MOUNT_STATUS.Unknown;
            }
        }

        [Display(1009, "Mount Status")]
        public SW_LSFS_MOUNT_STATUS MountStatus => GetMountStatus();

        private string ModeString => GetPropertyValue("mode");

        private SW_LSFS_MODE GetMode() {
            try {
                return (SW_LSFS_MODE)Convert.ToInt32(ModeString);
            } catch (Exception) {
                return SW_LSFS_MODE.Unknown;
            }
        }

        [Display(1010)]
        public SW_LSFS_MODE Mode => GetMode();

        [Display(1011, "Block Size")]
        public int BlockSize => GetPropertyValueAsInt("BlockSize");

        [Display(1012, "Snapshot Offset")]
        public int SnapshotOffset => GetPropertyValueAsInt("SnapshotOffset");

        [Display(1013, "Snapshot Name")]
        public string SnapshotName => GetPropertyValue("SnapshotName");

        [Display(1014, "Snapshot Time")]
        public DateTime? SnapshotTime => GetPropertyValueAsDateTime("SnapshotTime");

        public string PMCacheModeString => GetPropertyValue("PMCacheMode");

        [Display(1015, "PM Cache Type")]
        public CacheMode PMCacheMode => EnumFormat.ToCacheMode(PMCacheModeString);

        [Display(1016, "PM Cache Size MB")]
        public int PMCacheSizeMB => GetPropertyValueAsInt("PMCacheSizeMB");

        public string L2CacheModeString => GetPropertyValue("L2CacheMode");

        [Display(1017, "L2 Cache Type")]
        public CacheMode L2CacheMode => EnumFormat.ToCacheMode(L2CacheModeString);
    }
}