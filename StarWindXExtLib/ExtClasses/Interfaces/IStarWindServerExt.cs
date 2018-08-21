using StarWindXLib;

namespace StarWindXExtLib {

    public interface IStarWindServerExt : IStarWindServer {
        IValue<int> LogLevel { get; }
        IValue<string> LogMask { get; }
        IValue<int> LogRotateSize { get; }
        IValue<int> LogRotateKeepLastFiles { get; }
        IValue<int> UpdatePeriod { get; }
        IValue<string> UpdateHost { get; }
        IValue<string> UpdatePage { get; }
        IValue<int> UpdatePort { get; }
        IValue<string> UpdateCopyId { get; }
        IValue<string> UpdateLastRequest { get; }
        IValue<bool> WUSCEnabled { get; }
        IValue<bool> SrvWasDisabled { get; }
        IValue<int> SrvRestoreStartType { get; }
        IValue<bool> VaaiExCopyEnabled { get; }
        IValue<bool> VaaiCawEnabled { get; }
        IValue<bool> VaaiWriteSameEnabled { get; }
        IValue<bool> OdxEnabled { get; }
        IValue<int> OdxOptimalRodSizeMB { get; }
        IValue<int> OdxMaximumRodSizeMB { get; }
        IValue<int> OdxRodTokenDefaultTimeoutSec { get; }
        IValue<int> OdxRodTokenMaximumTimeoutSec { get; }
        IValue<int> PortValue { get; }
        IValue<string> Interface { get; }
        IValue<bool> BCastEnable { get; }
        IValue<string> BCastInterface { get; }
        IValue<int> BCastPort { get; }
        IValue<string> Login { get; }
        IValue<string> Password { get; }
        IValue<int> MinBufferSize { get; }
        IValue<string> AlignmentMask { get; }
        IValue<int> MaxPendingRequests { get; }
        IValue<int> iScsiPingPeriod { get; }
        IValue<int> iScsiDiscoveryListInterfaces { get; }
        IValue<int> ServerIoWorkersCount { get; }
        IValue<int> ServerIoWorkersConcurency { get; }
        IValue<int> CmdExecTimeWarningLimitInSec { get; }
        IValue<int> iScisCmdSendCmdTimeoutInSec { get; }
        IValue<string> iSerListen { get; }
        IValue<string> LocalizationDir { get; }
        IValue<string> DefaultStoragePoolPath { get; }
        IValue<bool> ExperimentalLSFS { get; }
        IValue<string> ClusterName { get; }
        IValue<string> ClusterGUID { get; }
        IValue<int> ClusterSettingsVersion { get; }
        IValue<string> ClusterNodes { get; }
        IValue<string> ClusterSync { get; }
        IValue<string> ClusterHeartbeat { get; }
        IValue<string> DataBaseRoot { get; }
        IValue<int> DBRotationDays { get; }
        IValue<int> DBFileSizeDays { get; }
        IValue<bool> PerformanceMonitorEnabled { get; }
        IValue<string> PerformanceRoot { get; }
        IValue<int> FSMThresholdPercent { get; }
        IValue<int> FSMCheckPeriodSeconds { get; }
        IValue<bool> FSMEnabled { get; }
    }
}