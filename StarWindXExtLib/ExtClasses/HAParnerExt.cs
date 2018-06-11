using System;
using StarWindXLib;

namespace StarWindXExtLib {
    internal class HAParnerExt : IHAPartnerExt {
        private IHADeviceExt Device { get; }

        internal HAParnerExt(IHADeviceExt device, int partnerId) {
            Device = device;
            PartnerId = partnerId;
        }

        [Display(0, "Partner Id")]
        public int PartnerId { get; }
        [Display(1, "Target Name")]
        public string TargetName => Device.GetPartnerTargetName(PartnerId);
        [Display(2)]
        public int Priority => Device.GetPartnerPriority(PartnerId);
        [Display(3)]
        public NodeType Type => Device.GetPartnerType(PartnerId);
        [Display(4, "Storage Device Type")]
        public string StorageDeviceType => Device.GetPartnerStorageDeviceType(PartnerId);
        [Display(5, "Synchronization Channels", CollecionType = typeof(IHANetworkInterface))]
        public ICollection SynchronizationChannels => Device.GetPartnerSynchronizationChannels(PartnerId);
        [Display(6, "Heartbeat Channels", CollecionType = typeof(IHANetworkInterface))]
        public ICollection HeartbeatChannels => Device.GetPartnerHeartbeatChannels(PartnerId);
        [Display(7, "Synchronization Connection Is Valid")]
        public bool SynchronizationValidConnection => Device.GetPartnerSynchronizationValidConnection(PartnerId);
        [Display(8, "Heartbeat Connection Is Valid")]
        public bool HeartbeatValidConnection => Device.GetPartnerHeartbeatValidConnection(PartnerId);
        [Display(9, "Sync Status")]
        public SW_HA_SYNC_STATUS SyncStatus => Device.GetPartnerSyncStatus(PartnerId);
        [Display(10, "Sync Percent")]
        public int SyncPercent => Device.GetPartnerSyncPercent(PartnerId);
        [Display(11, "Sync Type")]
        public string SyncType => Device.GetPartnerSyncType(PartnerId);
        [Display(12, "Sync Elapsed Time")]
        public string SyncElapsedTime => Device.GetPartnerSyncElapsedTime(PartnerId);
        [Display(13, "Sync Estimated Time")]
        public string SyncEstimatedTime => Device.GetPartnerSyncEstimatedTime(PartnerId);
        [Display(14, "Tracker Frozen")]
        public bool TrackerFrozen => Device.GetPartnerTrackerFrozen(PartnerId);
        [Display(15, "Tracker Snapshot Storage")]
        public string TrackerSnapshotStorage => Device.GetPartnerTrackerSnapshotStorage(PartnerId);
        [Display(16, "Tracker Mount Time")]
        public DateTime? TrackerMountTime => Device.GetPartnerTrackerMountTime(PartnerId);
        [Display(17, "Tracker Mount Snapshot")]
        public string TrackerMountSnapshot => Device.GetPartnerTrackerMountSnapshot(PartnerId);

        public string GetPropertyValue(string value) => Device.GetPartnerParameter(value, PartnerId);

        public IServerControl RemoveInterface(IHANetworkInterface iface) {
            return Device.RemovePartnerInterface(iface);
        }

        public IServerControl AddInterface(IHANetworkInterface iface, int priority) {
            return Device.AddPartnerInterface(iface, priority);
        }
    }
}
