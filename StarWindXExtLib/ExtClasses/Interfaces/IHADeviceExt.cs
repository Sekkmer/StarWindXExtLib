using System;
using System.Collections.Generic;
using StarWindXLib;

namespace StarWindXExtLib
{
    public interface IHADeviceExt : IDeviceExt, IHADevice
    {
        int ParnerNodesCount { get; }
        bool MaintenanceMode { get; }
        bool Buffering { get; }
        bool Asyncmode { get; }
        bool Readonly { get; }
        bool Highavailability { get; }
        bool NodeRemovedFromPartners { get; }
        bool StorageExtendSupported { get; }
        bool StorageSnapshotSupported { get; }
        bool StorageDeviceReady { get; }
        bool StorageDeviceReadonly { get; }
        bool SMISHidden { get; }
        bool AutosyncEnabled { get; }
        bool Tracker { get; }
        bool TrackerFrozen { get; }
        string SyncType { get; }
        string SyncElapsedTime { get; }
        string SyncEstimatedTime { get; }
        string Priority { get; }
        string AutoSyncPriority { get; }
        string ALUAGroupNodeState { get; }
        string TrackerSnapshotsStorage { get; }
        string TrackerMountTime { get; }
        string TrackerMountSnapshot { get; }
        string TrackerStatus { get; }
        string TrackerPending { get; }
        DateTime? TrackerReplicated { get; }
        string TrackerReplicating { get; }
        DateTime? TrackerScheduled { get; }
        NodeType NodeType { get; }
        FailoverConfType FailoverConfigType { get; }

        IEnumerable<IHAPartnerExt> PartnersExt { get; }

        IServerControl AddPartnerInterface(IHANetworkInterface iface, int priority);

        IServerControl RemovePartnerInterface(IHANetworkInterface iface);

        IEnumerable<IControlInterface> GetOwnAndPartnersControlInterfaces(IStarWindServer server);

        IServerCommand RestoreCurrentHANode();

        IServerCommand RestoreFromPartnerNode(IHAPartnerExt partner);

        #region Partner Info

        string GetPartnerTargetName(int node);

        int GetPartnerPriority(int node);

        NodeType GetPartnerType(int node);

        string GetPartnerStorageDeviceType(int node);

        IEnumerable<IHANetworkInterface> GetPartnerSynchronizationChannels(int node);

        IEnumerable<IHANetworkInterface> GetPartnerHeartbeatChannels(int node);

        IEnumerable<IHANetworkInterface> AllChannels { get; }

        bool GetPartnerSynchronizationValidConnection(int node);

        bool GetPartnerHeartbeatValidConnection(int node);

        SW_HA_SYNC_STATUS GetPartnerSyncStatus(int node);

        int GetPartnerSyncPercent(int node);

        string GetPartnerSyncType(int node);

        string GetPartnerSyncElapsedTime(int node);

        string GetPartnerSyncEstimatedTime(int node);

        bool GetPartnerTrackerFrozen(int node);

        string GetPartnerTrackerSnapshotStorage(int node);

        DateTime? GetPartnerTrackerMountTime(int node);

        string GetPartnerTrackerMountSnapshot(int node);

        string GetPartnerParameter(string value, int node);

        #endregion Partner Info
    }
}