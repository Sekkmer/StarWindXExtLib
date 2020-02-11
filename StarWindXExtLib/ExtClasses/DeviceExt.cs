using StarWindXLib;

using System;
using System.Threading.Tasks;

namespace StarWindXExtLib
{
    internal class DeviceExt : IDeviceExt
    {
        private readonly IDevice device;

        public DeviceExt(IDevice device)
        {
            this.device = device;
        }

        protected event EventHandler Refreshed;

        #region IDevice

        public string GetPropertyValue(string propertyName)
        {
            return device.GetPropertyValue(propertyName);
        }

        public void Refresh()
        {
            device.Refresh();
            Refreshed?.Invoke(this, EventArgs.Empty);
        }

        public void ExtendDevice(int sizeToExtendInMB)
        {
            device.ExtendDevice(sizeToExtendInMB);
        }

        public void TakeSnapshot(string Name, string description)
        {
            device.TakeSnapshot(Name, description);
        }

        public void DeleteSnapshot(ulong snapshotID)
        {
            device.DeleteSnapshot(snapshotID);
        }

        public string Name => device.Name;

        public string DeviceType => device.DeviceType;

        public string DeviceId => device.DeviceId;

        public string File => device.File;

        public string TargetName => device.TargetName;

        public string TargetId => device.TargetId;

        public string Size => device.Size;

        public string CacheMode => device.CacheMode;

        public string CacheSize => device.CacheSize;

        public string CacheBlockExpiryPeriod => device.CacheBlockExpiryPeriod;

        public bool Exists => device.Exists;

        public string DeviceLUN => device.DeviceLUN;

        public bool IsSnapshotsSupported => device.IsSnapshotsSupported;

        public ICollection Snapshots => device.Snapshots;

        public SW_DEVICE_STATE State => device.State;

        #endregion

        #region Async

        public async Task RefreshAsync()
        {
            await Task.Run(() => device.Refresh());
            Refreshed?.Invoke(this, EventArgs.Empty);
        }

        public async Task ExtendDeviceAsync(int sizeToExtendInMB)
        {
            await Task.Run(() => device.ExtendDevice(sizeToExtendInMB));
        }

        public async Task TakeSnapshotAsync(string Name, string description)
        {
            await Task.Run(() => device.TakeSnapshot(Name, description));
        }

        public async Task DeleteSnapshotAsync(ulong snapshotID)
        {
            await Task.Run(() => device.DeleteSnapshot(snapshotID));
        }

        #endregion Async

        public int SectorSize => device.SectorSize;

        public CacheMode CacheModeEnum => EnumFormat.ToCacheMode(device.CacheMode);

        public int PhySectorSize => GetPropertyValueAsInt("PhySectorSize");

        public string ParentDevice => GetPropertyValue("parent");

        public string DeviceHeaderPath => GetPropertyValue("DeviceHeaderPath");

        public int L1CachePercentOfUsage => GetPropertyValueAsInt("L1CachePercentOfUsage");

        public int L1CachePercentOfHits => GetPropertyValueAsInt("L1CachePercentOfHits");

        public string Path => File.Substring(0, File.LastIndexOf('/'));

        public bool Attached => device.TargetId != "empty";

        protected int GetPropertyValueAsInt(string propertyName)
        {
            var str = GetPropertyValue(propertyName);
            if (string.IsNullOrEmpty(str)) { return 0; }
            try {
                return Convert.ToInt32(str);
            } catch (FormatException) {
                return 0;
            } catch (OverflowException) {
                return 0;
            }
        }

        protected bool GetPropertyValueAsBool(string propertyName, string trueString = "yes")
        {
            return GetPropertyValue(propertyName) == trueString;
        }

        protected DateTime? GetPropertyValueAsDateTime(string propertyName)
        {
            var value = GetPropertyValueAsInt(propertyName);
            if (value == 0) { return null; }
            return FromUnixTime(value);
        }

        protected TimeSpan GetPropertyValueAsTimeSpan(string propertyName)
        {
            return TimeSpan.FromSeconds(GetPropertyValueAsInt(propertyName));
        }

        private static DateTime FromUnixTime(int value)
        {
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            return epoch.Add(offset).AddSeconds(value);
        }

        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}