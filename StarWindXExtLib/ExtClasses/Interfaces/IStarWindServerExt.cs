using System.Collections.Generic;
using System.Threading.Tasks;
using StarWindXLib;

namespace StarWindXExtLib
{
    public interface IStarWindServerExt : IStarWindServer
    {
        new IEnumerable<IDeviceExt> Devices { get; }
        new IEnumerable<ITargetExt> Targets { get; }

        IIntValue LogLevel { get; }
        IStringValue LogMask { get; }
        IIntValue LogRotateSize { get; }
        IIntValue LogRotateKeepLastFiles { get; }
        IIntValue UpdatePeriod { get; }
        IStringValue UpdateHost { get; }
        IStringValue UpdatePage { get; }
        IIntValue UpdatePort { get; }
        IStringValue UpdateCopyId { get; }
        IStringValue UpdateLastRequest { get; }
        IBoolValue WUSCEnabled { get; }
        IBoolValue SrvWasDisabled { get; }
        IIntValue SrvRestoreStartType { get; }
        IBoolValue VaaiExCopyEnabled { get; }
        IBoolValue VaaiCawEnabled { get; }
        IBoolValue VaaiWriteSameEnabled { get; }
        IBoolValue OdxEnabled { get; }
        IIntValue OdxOptimalRodSizeMB { get; }
        IIntValue OdxMaximumRodSizeMB { get; }
        IIntValue OdxRodTokenDefaultTimeoutSec { get; }
        IIntValue OdxRodTokenMaximumTimeoutSec { get; }
        IIntValue PortValue { get; }
        IStringValue Interface { get; }
        IBoolValue BCastEnable { get; }
        IStringValue BCastInterface { get; }
        IIntValue BCastPort { get; }
        IStringValue Login { get; }
        IStringValue Password { get; }
        IIntValue MinBufferSize { get; }
        IStringValue AlignmentMask { get; }
        IIntValue MaxPendingRequests { get; }
        IIntValue iScsiPingPeriod { get; }
        IIntValue iScsiDiscoveryListInterfaces { get; }
        IIntValue ServerIoWorkersCount { get; }

        IIntValue ServerIoWorkersConcurency { get; }

        // IIntValue CmdExecTimeWarningLimitInSec { get; }
        // IIntValue iScisCmdSendCmdTimeoutInSec { get; }
        IStringValue iSerListen { get; }
        IStringValue LocalizationDir { get; }
        IStringValue DefaultStoragePoolPath { get; }
        IBoolValue ExperimentalLSFS { get; }
        IStringValue ClusterName { get; }
        IStringValue ClusterGUID { get; }
        IIntValue ClusterSettingsVersion { get; }
        IStringValue ClusterNodes { get; }
        IStringValue ClusterSync { get; }
        IStringValue ClusterHeartbeat { get; }
        IStringValue DataBaseRoot { get; }
        IIntValue DBRotationDays { get; }
        IIntValue DBFileSizeDays { get; }
        IBoolValue PerformanceMonitorEnabled { get; }
        IStringValue PerformanceRoot { get; }
        IIntValue FSMThresholdPercent { get; }
        IIntValue FSMCheckPeriodSeconds { get; }
        IBoolValue FSMEnabled { get; }

        new IDeviceExt GetDeviceByID(string DeviceId);
        new IDeviceExt CreateDevice(string Path, string Name, STARWIND_DEVICE_TYPE fileType, Parameters builder);
        new ITargetExt CreateTarget(string targetAlias, string TargetName, Parameters builder);

        Task ConnectAsync();
        Task DisconnectAsync();
        Task RefreshAsync();
    }
}