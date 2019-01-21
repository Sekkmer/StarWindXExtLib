using StarWindXLib;

using System;

namespace StarWindXExtLib {

    internal class DeviceExt : Displayable, IDeviceExt {
        private IDevice device;

        public string GetPropertyValue(string propertyName) {
            return device.GetPropertyValue(propertyName);
        }

        protected bool UpdateNeeded { get; set; } = true;

        public void Refresh() {
            device.Refresh();
            UpdateNeeded = true;
        }

        public void ExtendDevice(int sizeToExtendInMB) {
            device.ExtendDevice(sizeToExtendInMB);
        }

        public void TakeSnapshot(string Name, string description) {
            device.TakeSnapshot(Name, description);
        }

        public void DeleteSnapshot(ulong snapshotID) {
            device.DeleteSnapshot(snapshotID);
        }

        [Display(0)]
        public string Name => device.Name;

        [Display(2)]
        public string DeviceType => device.DeviceType;

        [Display(3)]
        public string DeviceId => device.DeviceId;

        [Display(4)]
        public string File => device.File;

        [Display(5, "Target Name")]
        public string TargetName => device.TargetName;

        [Display(6, "Target Id")]
        public string TargetId => device.TargetId;

        [Display(7)]
        public string Size => device.Size;

        public string CacheMode => device.CacheMode;

        [Display(8, "Cache Type")]
        public CacheMode CacheModeEnum => EnumFormat.ToCacheMode(device.CacheMode);

        [Display(9, "Cache Size")]
        public string CacheSize => device.CacheSize;

        [Display(10, "Cache Block Expiry Period")]
        public string CacheBlockExpiryPeriod => device.CacheBlockExpiryPeriod;

        [Display(11)]
        public bool Exists => device.Exists;

        [Display(12, "Device LUN")]
        public string DeviceLUN => device.DeviceLUN;

        [Display(13, "Is Snapshots Supported")]
        public bool IsSnapshotsSupported => device.IsSnapshotsSupported;

        [Display(14, CollecionType = typeof(ISnapshot))]
        public ICollection Snapshots => device.Snapshots;

        [Display(15, "Logical Sector Size")]
        public int SectorSize => GetPropertyValueAsInt("SectorSize");

        [Display(16, "Phyisc Sector Size")]
        public int PhySectorSize => GetPropertyValueAsInt("PhySectorSize");

        [Display(17)]
        public SW_DEVICE_STATE State => device.State;

        [Display(18)]
        public string ParentDevice => GetPropertyValue("parent");

        [Display(19, "Parent Device")]
        public string DeviceHeaderPath => GetPropertyValue("DeviceHeaderPath");

        [Display(20, "L1 Cache Percent Of Usage")]
        public int L1CachePercentOfUsage => GetPropertyValueAsInt("L1CachePercentOfUsage");

        [Display(21, "L1 Cache Percent Of Hits")]
        public int L1CachePercentOfHits => GetPropertyValueAsInt("L1CachePercentOfHits");

        [Display(22)]
        public string Path => File.Substring(0, File.LastIndexOf('/'));

        public override string UniqueId => DeviceId;

        public bool Attached => device.TargetId != "empty";

        protected int GetPropertyValueAsInt(string propertyName) {
            var str = GetPropertyValue(propertyName);
            if (str is null || str == "") { return 0; }
            try {
                return Convert.ToInt32(str);
            } catch (FormatException) {
                return 0;
            } catch (OverflowException) {
                return 0;
            }
        }

        protected DateTime? GetPropertyValueAsDateTime(string propertyName) {
            var value = GetPropertyValueAsInt(propertyName);
            if (value == 0) { return null; }
            return FromUnixTime(value);
        }

        protected TimeSpan GetPropertyValueAsTimeSpan(string propertyName) {
            return TimeSpan.FromSeconds(GetPropertyValueAsInt(propertyName));
        }

        public DeviceExt(IDevice device) {
            this.device = device;
        }

        private static DateTime FromUnixTime(int value) {
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            return epoch.Add(offset).AddSeconds(value);
        }

        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}