using System;
using System.Collections.Generic;
using StarWindXLib;

namespace StarWindXExtLib
{
    internal class HAParnerExt : IHAPartnerExt
    {
        internal HAParnerExt(IHADeviceExt device, int partnerId)
        {
            Device = device;
            PartnerId = partnerId;
        }

        private IHADeviceExt Device { get; }

        public int PartnerId { get; }

        public string TargetName => Device.GetPartnerTargetName(PartnerId);

        public int Priority => Device.GetPartnerPriority(PartnerId);

        public SW_HA_NODE_TYPE Type => Device.GetPartnerType(PartnerId);

        public string StorageDeviceType => Device.GetPartnerStorageDeviceType(PartnerId);

        public IEnumerable<IHANetworkInterface> SynchronizationChannels =>
            Device.GetPartnerSynchronizationChannels(PartnerId);

        public IEnumerable<IHANetworkInterface> HeartbeatChannels => Device.GetPartnerHeartbeatChannels(PartnerId);

        public bool SynchronizationValidConnection => Device.GetPartnerSynchronizationValidConnection(PartnerId);

        public bool HeartbeatValidConnection => Device.GetPartnerHeartbeatValidConnection(PartnerId);

        public SW_HA_SYNC_STATUS SyncStatus => Device.GetPartnerSyncStatus(PartnerId);

        public int SyncPercent => Device.GetPartnerSyncPercent(PartnerId);

        public string SyncType => Device.GetPartnerSyncType(PartnerId);

        public string SyncElapsedTime => Device.GetPartnerSyncElapsedTime(PartnerId);

        public string SyncEstimatedTime => Device.GetPartnerSyncEstimatedTime(PartnerId);

        public bool TrackerFrozen => Device.GetPartnerTrackerFrozen(PartnerId);

        public string TrackerSnapshotStorage => Device.GetPartnerTrackerSnapshotStorage(PartnerId);

        public DateTime? TrackerMountTime => Device.GetPartnerTrackerMountTime(PartnerId);

        public string TrackerMountSnapshot => Device.GetPartnerTrackerMountSnapshot(PartnerId);

        public string GetPropertyValue(string value)
        {
            return Device.GetPartnerParameter(value, PartnerId);
        }

        public IServerControl RemoveInterface(IHANetworkInterface iface)
        {
            return Device.RemovePartnerInterface(iface);
        }

        public IServerControl AddInterface(IHANetworkInterface iface, int priority)
        {
            return Device.AddPartnerInterface(iface, priority);
        }

        public IServerCommand RestoreHAFromPartner()
        {
            var command = new ServerCommand("restoreCurrentHANode");
            command.AppendParam("deviceID", Device.DeviceId);
            command.AppendParam("partnetTargetName", TargetName);
            return command;
        }

        internal void ParentRefreshed()
        {
            Refreshed?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Refreshed;
    }
}