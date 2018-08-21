using StarWindXLib;

using System;

namespace StarWindXExtLib {

    internal class HADeviceExt : DeviceExt, IHADeviceExt {
        private IHADevice haDevice;

        public HADeviceExt(IHADevice ha) : base(ha) {
            haDevice = ha;
        }

#if b12381

        public void Synchronize(SW_HA_SYNC_TYPE syncType, string srcTargetName) {
            haDevice.Synchronize(syncType, srcTargetName);
        }

#else
        public void Synchronize(SW_HA_SYNC_TYPE syncType) {
            haDevice.Synchronize(syncType);
        }
#endif

        public void MarkAsSynchronized() {
            haDevice.MarkAsSynchronized();
        }

        [Obsolete("Use \"PartnerExt\"")]
        public ICollection Partners => haDevice.Partners;

        private ICollection GetChannels(NetworkInterfaceType type) {
            var infs = new CollectionExt();
            var name = type == NetworkInterfaceType.Synchronization ? "sync" : "heartbeat";
            for (var i = 1; i <= ParnerNodesCount; i++) {
                AddChannels(i, type, infs, GetPropertyValue("ha_partner_node" + i.ToString() + "_" + name + "_channels"));
            }
            return infs;
        }

        private void AddChannels(int parnterId, NetworkInterfaceType type, CollectionExt infs, string channels) {
            if (channels == "Empty") { return; }
            IHANetworkInterface ToNetworkInterface(string str) {
                var array = str.Split('$');
                return new HANetworkInterface(parnterId, type, array[0], Convert.ToInt32(array[1]), array[2] == "1");
            }
            try {
                if (channels.Contains(";")) {
                    foreach (var str in channels.Split(';')) {
                        infs.Add(ToNetworkInterface(str));
                    }
                } else {
                    infs.Add(ToNetworkInterface(channels));
                }
            } catch (Exception) { }
        }

        [Display(2000, "Synchronization Channels", CollecionType = typeof(IHANetworkInterface))]
        public ICollection SynchronizationChannels => GetChannels(NetworkInterfaceType.Synchronization);

        [Display(2001, "Heartbeat Channels", CollecionType = typeof(IHANetworkInterface))]
        public ICollection HeartbeatChannels => GetChannels(NetworkInterfaceType.Heartbeat);

        [Display(2002, "Sync Status")]
        public SW_HA_SYNC_STATUS SyncStatus => haDevice.SyncStatus;

        [Display(2003, "Sync Traffic Share")]
        public int SyncTrafficShare { get => haDevice.SyncTrafficShare; set => haDevice.SyncTrafficShare = value; }

        [Display(2004, "Wait On Autosynch")]
        public bool IsWaitingAutosync => GetPropertyValue("ha_wait_on_autosynch") == "1";

        public bool IsWaitOnAutosynch => haDevice.IsWaitingAutosync;

        [Display(2005, "Parner Nodes Count")]
        public int ParnerNodesCount => GetPropertyValueAsInt("ha_partner_nodes_count");

        [Display(2006, "Maintenance Mode")]
        public bool MaintenanceMode => GetPropertyValue("ha_maintenance_mode") == "1";

        public string GetPartnerTargetName(int node) {
            return GetPartnerParameter("target_name", node);
        }

        public int GetPartnerPriority(int node) {
            return GetPartnerParameterAsInt("priority", node);
        }

        public NodeType GetPartnerType(int node) {
            return (NodeType)GetPartnerParameterAsInt("type", node);
        }

        public string GetPartnerStorageDeviceType(int node) {
            return GetPartnerParameter("storage_device_type", node);
        }

        public ICollection GetPartnerSynchronizationChannels(int node) {
            var infs = new CollectionExt();
            AddChannels(node, NetworkInterfaceType.Synchronization, infs, GetPartnerParameter("sync_channels", node));
            return infs;
        }

        public ICollection GetPartnerHeartbeatChannels(int node) {
            var infs = new CollectionExt();
            AddChannels(node, NetworkInterfaceType.Heartbeat, infs, GetPartnerParameter("heartbeat_channels", node));
            return infs;
        }

        public bool GetPartnerSynchronizationValidConnection(int node) {
            return GetPartnerParameter("is_exist_sync_valid_connection", node) == "1";
        }

        public bool GetPartnerHeartbeatValidConnection(int node) {
            return GetPartnerParameter("is_exist_heartbeat_valid_connection", node) == "1";
        }

        public SW_HA_SYNC_STATUS GetPartnerSyncStatus(int node) {
            return (SW_HA_SYNC_STATUS)GetPartnerParameterAsInt("sync_status", node);
        }

        public int GetPartnerSyncPercent(int node) {
            return GetPartnerParameterAsInt("sync_percent", node);
        }

        public string GetPartnerSyncType(int node) {
            return GetPartnerParameter("sync_type", node);
        }

        public string GetPartnerSyncElapsedTime(int node) {
            return GetPartnerParameter("sync_elapsed_time", node);
        }

        public string GetPartnerSyncEstimatedTime(int node) {
            return GetPartnerParameter("sync_estimated_time", node);
        }

        public bool GetPartnerTrackerFrozen(int node) {
            return GetPartnerParameter("tracker_frozen", node) == "yes";
        }

        public string GetPartnerTrackerSnapshotStorage(int node) {
            return GetPartnerParameter("tracker_snapshots_storage", node);
        }

        public DateTime? GetPartnerTrackerMountTime(int node) {
            return GetPartnerParameterAsDateTime("tracker_mount_time", node);
        }

        public string GetPartnerTrackerMountSnapshot(int node) {
            return GetPartnerParameter("tracker_mount_snapshot", node);
        }

        public string GetPartnerParameter(string value, int node) {
            return GetPropertyValue("ha_partner_node" + node.ToString() + "_" + value);
        }

        public int GetPartnerParameterAsInt(string value, int node) {
            return GetPropertyValueAsInt("ha_partner_node" + node.ToString() + "_" + value);
        }

        public DateTime? GetPartnerParameterAsDateTime(string value, int node) {
            return GetPropertyValueAsDateTime("ha_partner_node" + node.ToString() + "_" + value);
        }

        [Display(2007)]
        public bool Buffering => GetPropertyValue("buffering") == "yes";

        [Display(2008)]
        public bool Asyncmode => GetPropertyValue("asyncmode") == "yes";

        [Display(2009)]
        public bool Readonly => GetPropertyValue("readonly") == "yes";

        [Display(2010)]
        public bool Highavailability => GetPropertyValue("highavailability") == "yes";

        [Display(2011, "Node Removed From Partners")]
        public bool NodeRemovedFromPartners => GetPropertyValue("ha_is_node_removed_from_partners") == "yes";

        [Display(2012, "Storage Extend Supported")]
        public bool StorageExtendSupported => GetPropertyValue("ha_is_storage_extend_supported") == "yes";

        [Display(2013, "Storage Snapshot Supported")]
        public bool StorageSnapshotSupported => GetPropertyValue("ha_is_storage_snapshot_supported") == "yes";

        [Display(2014, "Storage Device Ready")]
        public bool StorageDeviceReady => GetPropertyValue("ha_is_storage_device_ready") == "yes";

        [Display(2015, "Storage Device Readonly")]
        public bool StorageDeviceReadonly => GetPropertyValue("ha_is_storage_device_readonly") == "yes";

        [Display(2016, "SMIS Hidden")]
        public bool SMISHidden => GetPropertyValue("ha_is_SMISHidden") == "yes";

        [Display(2017, "Autosync Enabled")]
        public bool AutosyncEnabled => GetPropertyValue("ha_autosynch_enabled") == "yes";

        [Display(2018)]
        public bool Tracker => GetPropertyValue("ha_tracker") == "yes";

        [Display(2019, "Tracker Frozen")]
        public bool TrackerFrozen => GetPropertyValue("ha_tracker_frozen") == "yes";

        [Display(2020, "SyncT ype")]
        public string SyncType => GetPropertyValue("ha_synch_type");

        [Display(2021, "Sync Elapsed Time")]
        public string SyncElapsedTime => GetPropertyValue("ha_sync_elapsed_time");

        [Display(2022, "Sync Estimated Time")]
        public string SyncEstimatedTime => GetPropertyValue("ha_sync_estimated_time");

        public int SyncPercent => haDevice.SyncPercent;

        [Display(2023)]
        public string Priority => GetPropertyValue("ha_priority");

        [Display(2024, "Auto Sync Priority")]
        public string AutoSyncPriority => GetPropertyValue("ha_auto_sync_priority");

        [Display(2025, "ALUA Group Node State")]
        public string ALUAGroupNodeState => GetPropertyValue("ha_alua_group_node_state");

        [Display(2026, "Tracker Snapshots Storage")]
        public string TrackerSnapshotsStorage => GetPropertyValue("ha_tracker_snapshots_storage");

        [Display(2027, "Tracker Mount Time")]
        public string TrackerMountTime => GetPropertyValue("ha_tracker_mount_time");

        [Display(2028, "Tracker Mount Snapshot")]
        public string TrackerMountSnapshot => GetPropertyValue("ha_tracker_mount_snapshot");

        [Display(2029, "Tracker Status")]
        public string TrackerStatus => GetPropertyValue("ha_tracker_status");

        [Display(2030, "Tracker Pending")]
        public string TrackerPending => GetPropertyValue("ha_tracker_pending");

        [Display(2031, "Tracker Replicated")]
        public DateTime? TrackerReplicated => GetPropertyValueAsDateTime("ha_tracker_replicated");

        [Display(2032, "Tracker Replicating")]
        public string TrackerReplicating => GetPropertyValue("ha_tracker_replicating");

        [Display(2033, "Tracker Scheduled")]
        public DateTime? TrackerScheduled => GetPropertyValueAsDateTime("ha_tracker_scheduled");

        [Display(2034, "Node Type")]
        public NodeType NodeType => (NodeType)GetPropertyValueAsInt("ha_node_type");

        [Display(2035, "Failover Config Type")]
        public FailoverConfType FailoverConfigType => (FailoverConfType)GetPropertyValueAsInt("ha_failover_config_type");

        [Display(2036, "Partners", CollecionType = typeof(IHAPartnerExt))]
        public ICollection PartnersExt => GetPartnersExt();

        private ICollection partnersExt;

        private int lastPartnerCount = 0;

        private ICollection GetPartnersExt() {
            if (UpdateNeeded && lastPartnerCount != ParnerNodesCount) {
                var collection = new CollectionExt();
                for (var i = 1; i <= ParnerNodesCount; i++) {
                    collection.Add(new HAParnerExt(this, i));
                }
                partnersExt = collection;
                UpdateNeeded = false;
                lastPartnerCount = ParnerNodesCount;
            }
            return partnersExt;
        }

        public void RemovePartner(string TargetName) {
            haDevice.RemovePartner(TargetName);
        }

        public void SwitchMaintenanceMode(bool enable, bool force) {
            haDevice.SwitchMaintenanceMode(enable, force);
        }

        public IServerControl RemovePartnerInterface(IHANetworkInterface iface) {
            return new HAInterfaceEditor(this, iface) { ActionIsAdd = false };
        }

        public IServerControl AddPartnerInterface(IHANetworkInterface iface, int priority) {
            return new HAInterfaceEditor(this, iface) { ActionIsAdd = true, Priority = priority };
        }

        public ICollection GetOwnAndPartnersControlInterfaces(IStarWindServer server) {
            var pars = new Parameters();
            pars.AppendParam("sendTo", DeviceId);
            pars.AppendParam("GetOwnAndPartnersControlInterfaces", "");
            server.ExecuteCommandEx(STARWIND_COMMAND_TYPE.STARWIND_CONTROL_REQUEST, "", pars, out var result);
            var collection = new CollectionExt();
            for (var i = 0; i <= ParnerNodesCount; i++) {
                collection.Add(new ControlInterface(result, i));
            }
            return collection;
        }

        public IServerCommand RestoreCurrentHANode() {
            var command = new ServerCommand("restoreCurrentHANode");
            command.AppendParam("deviceID", DeviceId);
            return command;
        }

        public IServerCommand RestoreFromPartnerNode(IHAPartnerExt partner) {
            var command = new ServerCommand("restoreCurrentHANode");
            command.AppendParam("deviceID", DeviceId);
            command.AppendParam("partnetTargetName", partner.TargetName);
            return command;
        }
    }
}