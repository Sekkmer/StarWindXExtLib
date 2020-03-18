using System;
using System.Collections.Generic;
using System.Linq;
using StarWindXLib;

namespace StarWindXExtLib
{
    internal class HADeviceExt : DeviceExt, IHADeviceExt
    {
        private readonly IHADevice haDevice;

        private int lastPartnerCount;

        private List<IHAPartnerExt> partnersExt;

        public HADeviceExt(IHADevice ha) : base(ha)
        {
            haDevice = ha;
            Refreshed += PartnerRefreshed;
        }

        public IEnumerable<IHANetworkInterface> AllChannels => GetChannels(NetworkInterfaceType.Synchronization)
            .Concat(GetChannels(NetworkInterfaceType.Heartbeat));

        public int ParnerNodesCount => GetPropertyValueAsInt("ha_partner_nodes_count");

        public bool MaintenanceMode => GetPropertyValueAsBool("ha_maintenance_mode", "1");

        public bool Buffering => GetPropertyValueAsBool("buffering");

        public bool Asyncmode => GetPropertyValueAsBool("asyncmode");

        public bool Readonly => GetPropertyValueAsBool("readonly");

        public bool Highavailability => GetPropertyValueAsBool("highavailability");

        public bool NodeRemovedFromPartners => GetPropertyValueAsBool("ha_is_node_removed_from_partners");

        public bool StorageExtendSupported => GetPropertyValueAsBool("ha_is_storage_extend_supported");

        public bool StorageSnapshotSupported => GetPropertyValueAsBool("ha_is_storage_snapshot_supported");

        public bool StorageDeviceReady => GetPropertyValueAsBool("ha_is_storage_device_ready");

        public bool StorageDeviceReadonly => GetPropertyValueAsBool("ha_is_storage_device_readonly");

        public bool SMISHidden => GetPropertyValueAsBool("ha_is_SMISHidden");

        public bool AutosyncEnabled => GetPropertyValueAsBool("ha_autosynch_enabled");

        public bool Tracker => GetPropertyValueAsBool("ha_tracker");

        public bool TrackerFrozen => GetPropertyValueAsBool("ha_tracker_frozen");

        public string SyncType => GetPropertyValue("ha_synch_type");

        public string SyncElapsedTime => GetPropertyValue("ha_sync_elapsed_time");

        public string SyncEstimatedTime => GetPropertyValue("ha_sync_estimated_time");

        public string Priority => GetPropertyValue("ha_priority");

        public string AutoSyncPriority => GetPropertyValue("ha_auto_sync_priority");

        public string ALUAGroupNodeState => GetPropertyValue("ha_alua_group_node_state");

        public string TrackerSnapshotsStorage => GetPropertyValue("ha_tracker_snapshots_storage");

        public string TrackerMountTime => GetPropertyValue("ha_tracker_mount_time");

        public string TrackerMountSnapshot => GetPropertyValue("ha_tracker_mount_snapshot");

        public string TrackerStatus => GetPropertyValue("ha_tracker_status");

        public string TrackerPending => GetPropertyValue("ha_tracker_pending");

        public DateTime? TrackerReplicated => GetPropertyValueAsDateTime("ha_tracker_replicated");

        public string TrackerReplicating => GetPropertyValue("ha_tracker_replicating");

        public DateTime? TrackerScheduled => GetPropertyValueAsDateTime("ha_tracker_scheduled");

        public FailoverConfType FailoverConfigType =>
            (FailoverConfType) GetPropertyValueAsInt("ha_failover_config_type");

        public IEnumerable<IHAPartnerExt> PartnersExt => partnersExt ?? (partnersExt = partnersExt =
                                                             Enumerable.Range(1, ParnerNodesCount).Select(i =>
                                                                 new HAParnerExt(this, i) as IHAPartnerExt).ToList());

        public IServerControl RemovePartnerInterface(IHANetworkInterface iface)
        {
            return new HAInterfaceEditor(this, iface) {ActionIsAdd = false};
        }

        public IServerControl AddPartnerInterface(IHANetworkInterface iface, int priority)
        {
            return new HAInterfaceEditor(this, iface) {ActionIsAdd = true, Priority = priority};
        }

        public IEnumerable<IControlInterface> GetOwnAndPartnersControlInterfaces(IStarWindServer server)
        {
            var pars = new Parameters();
            pars.AppendParam("sendTo", DeviceId);
            pars.AppendParam("GetOwnAndPartnersControlInterfaces", "");
            server.ExecuteCommandEx(STARWIND_COMMAND_TYPE.STARWIND_CONTROL_REQUEST, "", pars, out var result);
            return Enumerable.Range(0, ParnerNodesCount).Select(i => new ControlInterface(result, i));
        }

        public IServerCommand RestoreCurrentHANode()
        {
            var command = new ServerCommand("restoreCurrentHANode");
            command.AppendParam("deviceID", DeviceId);
            return command;
        }

        public IServerCommand RestoreFromPartnerNode(IHAPartnerExt partner)
        {
            var command = new ServerCommand("restoreCurrentHANode");
            command.AppendParam("deviceID", DeviceId);
            command.AppendParam("partnetTargetName", partner.TargetName);
            return command;
        }

        private void PartnerRefreshed(object sender, EventArgs e)
        {
            if (partnersExt == null) return;
            if (lastPartnerCount > ParnerNodesCount) partnersExt.RemoveAll(p => p.PartnerId > ParnerNodesCount);
            if (lastPartnerCount < ParnerNodesCount)
                partnersExt.AddRange(Enumerable.Range(lastPartnerCount, ParnerNodesCount)
                    .Select(i => new HAParnerExt(this, i) as IHAPartnerExt));
            lastPartnerCount = ParnerNodesCount;
            foreach (var partner in PartnersExt) (partner as HAParnerExt).ParentRefreshed();
        }

        private IEnumerable<IHANetworkInterface> GetChannels(NetworkInterfaceType type)
        {
            var name = type == NetworkInterfaceType.Synchronization ? "sync" : "heartbeat";
            return Enumerable.Range(1, ParnerNodesCount).SelectMany(parnterId =>
                SelectChannels(parnterId, type,
                    GetPropertyValue("ha_partner_node" + parnterId + "_" + name + "_channels")));
        }

        private static IEnumerable<IHANetworkInterface> SelectChannels(int parnterId, NetworkInterfaceType type,
            string channels)
        {
            if (channels == "Empty") yield break;
            if (channels.Contains(";"))
                foreach (var str in channels.Split(';'))
                    yield return new HANetworkInterface(parnterId, type, str);
            else
                yield return new HANetworkInterface(parnterId, type, channels);
        }

        #region HADevice

        public void Synchronize(SW_HA_SYNC_TYPE syncType, string srcTargetName)
        {
            haDevice.Synchronize(syncType, srcTargetName);
        }

        public void MarkAsSynchronized()
        {
            haDevice.MarkAsSynchronized();
        }

        public void RemovePartner(string TargetName)
        {
            haDevice.RemovePartner(TargetName);
        }

        public void SwitchMaintenanceMode(bool enable, bool force)
        {
            haDevice.SwitchMaintenanceMode(enable, force);
        }

        public void AddPartner(Parameters p)
        {
            haDevice.AddPartner(p);
        }

        [Obsolete("Use \"PartnerExt\"")] public ICollection Partners => haDevice.Partners;


        public ICollection SynchronizationChannels =>
            new CollectionExt<IHANetworkInterface>(GetChannels(NetworkInterfaceType.Synchronization));

        public ICollection HeartbeatChannels =>
            new CollectionExt<IHANetworkInterface>(GetChannels(NetworkInterfaceType.Heartbeat));

        public int SyncPercent => haDevice.SyncPercent;

        public SW_HA_SYNC_STATUS SyncStatus => haDevice.SyncStatus;

        public bool IsWaitingAutosync => haDevice.IsWaitingAutosync;

        public SW_HA_NODE_TYPE NodeType => haDevice.NodeType;

        public int SyncTrafficShare
        {
            get => haDevice.SyncTrafficShare;
            set => haDevice.SyncTrafficShare = value;
        }

        public SW_HA_FAILOVER_STRATEGY FailoverStrategy => haDevice.FailoverStrategy;

        #endregion HADevice

        #region Partner Info

        public string GetPartnerTargetName(int node)
        {
            return GetPartnerParameter("target_name", node);
        }

        public int GetPartnerPriority(int node)
        {
            return GetPartnerParameterAsInt("priority", node);
        }

        public SW_HA_NODE_TYPE GetPartnerType(int node)
        {
            return (SW_HA_NODE_TYPE) GetPartnerParameterAsInt("type", node);
        }

        public string GetPartnerStorageDeviceType(int node)
        {
            return GetPartnerParameter("storage_device_type", node);
        }

        public IEnumerable<IHANetworkInterface> GetPartnerSynchronizationChannels(int node)
        {
            return SelectChannels(node, NetworkInterfaceType.Synchronization,
                GetPartnerParameter("sync_channels", node));
        }

        public IEnumerable<IHANetworkInterface> GetPartnerHeartbeatChannels(int node)
        {
            return SelectChannels(node, NetworkInterfaceType.Heartbeat,
                GetPartnerParameter("heartbeat_channels", node));
        }

        public bool GetPartnerSynchronizationValidConnection(int node)
        {
            return GetPartnerParameter("is_exist_sync_valid_connection", node) == "1";
        }

        public bool GetPartnerHeartbeatValidConnection(int node)
        {
            return GetPartnerParameter("is_exist_heartbeat_valid_connection", node) == "1";
        }

        public SW_HA_SYNC_STATUS GetPartnerSyncStatus(int node)
        {
            return (SW_HA_SYNC_STATUS) GetPartnerParameterAsInt("sync_status", node);
        }

        public int GetPartnerSyncPercent(int node)
        {
            return GetPartnerParameterAsInt("sync_percent", node);
        }

        public string GetPartnerSyncType(int node)
        {
            return GetPartnerParameter("sync_type", node);
        }

        public string GetPartnerSyncElapsedTime(int node)
        {
            return GetPartnerParameter("sync_elapsed_time", node);
        }

        public string GetPartnerSyncEstimatedTime(int node)
        {
            return GetPartnerParameter("sync_estimated_time", node);
        }

        public bool GetPartnerTrackerFrozen(int node)
        {
            return GetPartnerParameter("tracker_frozen", node) == "yes";
        }

        public string GetPartnerTrackerSnapshotStorage(int node)
        {
            return GetPartnerParameter("tracker_snapshots_storage", node);
        }

        public DateTime? GetPartnerTrackerMountTime(int node)
        {
            return GetPartnerParameterAsDateTime("tracker_mount_time", node);
        }

        public string GetPartnerTrackerMountSnapshot(int node)
        {
            return GetPartnerParameter("tracker_mount_snapshot", node);
        }

        public string GetPartnerParameter(string value, int node)
        {
            return GetPropertyValue("ha_partner_node" + node + "_" + value);
        }

        public int GetPartnerParameterAsInt(string value, int node)
        {
            return GetPropertyValueAsInt("ha_partner_node" + node + "_" + value);
        }

        public DateTime? GetPartnerParameterAsDateTime(string value, int node)
        {
            return GetPropertyValueAsDateTime("ha_partner_node" + node + "_" + value);
        }

        #endregion Parther Info
    }
}