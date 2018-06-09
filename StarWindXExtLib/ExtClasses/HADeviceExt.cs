using System;
using StarWindXLib;

namespace StarWindXExtLib {
    internal class HADeviceExt : DeviceExt, IHADeviceExt {
        IHADevice haDevice;

        public HADeviceExt(IHADevice ha) : base(ha) {
            haDevice = ha;
        }
        public void Synchronize(SW_HA_SYNC_TYPE syncType) {
            haDevice.Synchronize(syncType);
        }

        public void MarkAsSynchronized() {
            haDevice.MarkAsSynchronized();
        }
        public ICollection Partners => haDevice.Partners;
        private ICollection GetChannels(NetworkInterfaceType type) {
            var infs = new CollectionExt();
            var name = type == NetworkInterfaceType.Synchronization ? "sync" : "heartbeat";
            for (int i = 1; i <= ParnerNodesCount; i++) {
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

        public ICollection SynchronizationChannels => GetChannels(NetworkInterfaceType.Synchronization);

        public ICollection HeartbeatChannels => GetChannels(NetworkInterfaceType.Heartbeat);


        public SW_HA_SYNC_STATUS SyncStatus => haDevice.SyncStatus;

        public int SyncTrafficShare { get => haDevice.SyncTrafficShare; set => haDevice.SyncTrafficShare = value; }

        public bool WaitOnAutosynch => GetPropertyValue("ha_wait_on_autosynch") == "1";

        public int ParnerNodesCount => GetPropertyValueAsInt("ha_partner_nodes_count");

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

        public string GetPartnerTrackerSnpshotStorage(int node) {
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

        public bool Buffering => GetPropertyValue("buffering") == "yes";
        public bool Asyncmode => GetPropertyValue("asyncmode") == "yes";
        public bool Readonly => GetPropertyValue("readonly") == "yes";
        public bool Highavailability => GetPropertyValue("highavailability") == "yes";

        public bool NodeRemovedFromPartners => GetPropertyValue("ha_is_node_removed_from_partners") == "yes";
        public bool StorageExtendSupported => GetPropertyValue("ha_is_storage_extend_supported") == "yes";
        public bool StorageSnapshotSupported => GetPropertyValue("ha_is_storage_snapshot_supported") == "yes";
        public bool StorageDeviceReady => GetPropertyValue("ha_is_storage_device_ready") == "yes";
        public bool StorageDeviceReadonly => GetPropertyValue("ha_is_storage_device_readonly") == "yes";
        public bool SMISHidden => GetPropertyValue("ha_is_SMISHidden") == "yes";
        public bool AutosyncEnabled => GetPropertyValue("ha_autosynch_enabled") == "yes";
        public bool Tracker => GetPropertyValue("ha_tracker") == "yes";
        public bool TrackerFrozen => GetPropertyValue("ha_tracker_frozen") == "yes";

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

        public NodeType NodeType => (NodeType)GetPropertyValueAsInt("ha_node_type");
        public FailoverConfType FailoverConfigType => (FailoverConfType)GetPropertyValueAsInt("ha_failover_config_type");

        public void RemovePartner(string TargetName) {
            haDevice.RemovePartner(TargetName);
        }

        public void SwitchMaintenanceMode(bool enable, bool force) {
            haDevice.SwitchMaintenanceMode(enable, force);
        }

        public IServerControl RemovePartnerInterface(IHANetworkInterface iface) {
            return new HAInterfaceEditor(this, iface) { ActionIsAdd = false };
            // pars.AppendParam("Priority", priority.ToString());
        }

        public IServerControl AddPartnerInterface(IHANetworkInterface iface, int priority) {
            return new HAInterfaceEditor(this, iface) { ActionIsAdd = true, Priority = priority };
        }

        public ICollection GetOwnAndPartnersControlInterfaces(IStarWindServer server) {
            var pars = new Parameters();
            pars.AppendParam("sendTo", DeviceId);
            pars.AppendParam("GetOwnAndPartnersControlInterfaces", "");
            server.ExecuteCommandEx(STARWIND_COMMAND_TYPE.STARWIND_CONTROL_REQUEST, "", pars, out ICommandResult result);
            var collection = new CollectionExt();
            for (int i = 0; i < ParnerNodesCount; i++) {
                collection.Add(new ControlInterface(result, i));
            }      
            return collection;
        }
    }
}
