﻿using System;
using StarWindXLib;

namespace StarWindXExtLib {
    public interface IHAPartnerExt {
        int PartnerId { get; }
        string TargetName { get; }
        int Priority { get; }
        NodeType Type { get; }
        string StorageDeviceType { get; }
        ICollection SynchronizationChannels { get; }
        ICollection HeartbeatChannels { get; }
        bool SynchronizationValidConnection { get; }
        bool HeartbeatValidConnection { get; }
        SW_HA_SYNC_STATUS SyncStatus { get; }
        int SyncPercent { get; }
        string SyncType { get; }
        string SyncElapsedTime { get; }
        string SyncEstimatedTime { get; }
        bool TrackerFrozen { get; }
        string TrackerSnapshotStorage { get; }
        DateTime? TrackerMountTime { get; }
        string TrackerMountSnapshot { get; }
        string GetPropertyValue(string value);
        IServerControl RemoveInterface(IHANetworkInterface iface);        
        IServerControl AddInterface(IHANetworkInterface iface, int priority);
    }
}