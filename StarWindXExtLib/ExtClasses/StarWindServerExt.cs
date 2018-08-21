using StarWindXLib;

using System.Linq;

namespace StarWindXExtLib {

    public class StarWindServerExt : IStarWindServerExt {
        private IStarWindServer Server { get; }

        public static IStarWindX Component { get; } = new StarWindX();

        #region IStarWindServer

        public void Connect() {
            Server.Connect();
        }

        public void Disconnect() {
            Server.Disconnect();
        }

        public void Refresh() {
            Server.Refresh();
        }

        public void CreateFile(string Path, string Name, STARWIND_FILE_TYPE fileType, Parameters builder) {
            Server.CreateFile(Path, Name, fileType, builder);
        }

        public IDevice CreateDevice(string Path, string Name, STARWIND_DEVICE_TYPE fileType, Parameters builder) {
            return Server.CreateDevice(Path, Name, fileType, builder);
        }

        public ITarget CreateTarget(string targetAlias, string TargetName, Parameters builder) {
            return Server.CreateTarget(targetAlias, TargetName, builder);
        }

        public void ExecuteCommand(STARWIND_COMMAND_TYPE cmdType, string commandName, Parameters builder) {
            Server.ExecuteCommand(cmdType, commandName, builder);
        }

        public void RemoveFile(string Path, STARWIND_DEVICE_TYPE DeviceType) {
            Server.RemoveFile(Path, DeviceType);
        }

        public void RemoveDevice(string DeviceId, bool forceRemove) {
            Server.RemoveDevice(DeviceId, forceRemove);
        }

        public void RemoveTarget(string TargetName, bool forceRemove) {
            Server.RemoveTarget(TargetName, forceRemove);
        }

        public ICollection GetInitiators(string TargetId) {
            return Server.GetInitiators(TargetId);
        }

        public string GetDeviceID(string deviceName) {
            return Server.GetDeviceID(deviceName);
        }

        public IDevice GetDeviceByID(string DeviceId) {
            return Server.GetDeviceByID(DeviceId);
        }

        public IPlugin GetPlugin(STARWIND_PLUGIN_ID pluginID) {
            return Server.GetPlugin(pluginID);
        }

        public string GetLicensedPlugins(string delimiter) {
            return Server.GetLicensedPlugins(delimiter);
        }

        public string ShowFileBrowser(STARWIND_PLUGIN_ID pluginID, IFileBrowserEvents eventsCallback, out bool pCancelled) {
            return Server.ShowFileBrowser(pluginID, eventsCallback, out pCancelled);
        }

        public void AddCHAP(string TargetName, string LocalName, string LocalSecret, string PeerName, string PeerSecret) {
            Server.AddCHAP(TargetName, LocalName, LocalSecret, PeerName, PeerSecret);
        }

        public void RemoveCHAP(string permissionName, string TargetName, bool forceRemove) {
            Server.RemoveCHAP(permissionName, TargetName, forceRemove);
        }

        public IACLRule AddACLRule(string ruleName, string Source, string Destination, string Interface, bool Allow) {
            return Server.AddACLRule(ruleName, Source, Destination, Interface, Allow);
        }

        public void RemoveACLRule(string ruleID) {
            Server.RemoveACLRule(ruleID);
        }

        public void RemoveDeviceEx(string DeviceId, bool forceRemove, Parameters @params) {
            Server.RemoveDeviceEx(DeviceId, forceRemove, @params);
        }

        public string GetServerParameter(string paramName) {
            return Server.GetServerParameter(paramName);
        }

        public void SetServerParameter(string paramName, string paramValue) {
            Server.SetServerParameter(paramName, paramValue);
        }

        public ICollection QueryServerPerformanceData(SW_PERFORMANCE_COUNTER_TYPE counterType, SW_PERFORMANCE_TIME_INTERVAL timeInterval) {
            return Server.QueryServerPerformanceData(counterType, timeInterval);
        }

        public ICollection QueryTargetPerformanceData(SW_PERFORMANCE_COUNTER_TYPE counterType, string TargetName, SW_PERFORMANCE_TIME_INTERVAL timeInterval) {
            return Server.QueryTargetPerformanceData(counterType, TargetName, timeInterval);
        }

        public ICollection QueryDevicePerformanceData(SW_PERFORMANCE_COUNTER_TYPE counterType, string deviceName, SW_PERFORMANCE_TIME_INTERVAL timeInterval) {
            return Server.QueryDevicePerformanceData(counterType, deviceName, timeInterval);
        }

        public void InstallLicense(string licenseFile) {
            Server.InstallLicense(licenseFile);
        }

        public void ExecuteCommandEx(STARWIND_COMMAND_TYPE cmdType, string commandName, Parameters builder, out ICommandResult pVal) {
            Server.ExecuteCommandEx(cmdType, commandName, builder, out pVal);
        }

        public string IP { get => Server.IP; set => Server.IP = value; }
        public int Port { get => Server.Port; set => Server.Port = value; }

        public IAuthentificationInfo AuthentificationInfo => Server.AuthentificationInfo;

        public bool Connected => Server.Connected;

        public ICollection Targets => Server.Targets;

        public ICollection Devices => Server.Devices;

        public string ServerInformation => Server.ServerInformation;

        public IFileBrowser FileBrowser => Server.FileBrowser;

        public ICollection CHAPPermissions => Server.CHAPPermissions;

        public ICollection ActiveInterfaces => Server.ActiveInterfaces;

        public ICollection ACLRules => Server.ACLRules;

        public IACLRule ACLDefaultRule => Server.ACLDefaultRule;

        public ILicenseInfo LicenseInfo => Server.LicenseInfo;

        public ICollection StoragePools => Server.StoragePools;

        public ICollection NumaNodes => Server.NumaNodes;

        #endregion IStarWindServer

        #region IValues

        public IValue<int> LogLevel { get; } = new IntValue();
        public IValue<string> LogMask { get; } = new StringValue();
        public IValue<int> LogRotateSize { get; } = new IntValue();
        public IValue<int> LogRotateKeepLastFiles { get; } = new IntValue();
        public IValue<int> UpdatePeriod { get; } = new IntValue();
        public IValue<string> UpdateHost { get; } = new StringValue();
        public IValue<string> UpdatePage { get; } = new StringValue();
        public IValue<int> UpdatePort { get; } = new IntValue();
        public IValue<string> UpdateCopyId { get; } = new StringValue();
        public IValue<string> UpdateLastRequest { get; } = new StringValue();
        public IValue<bool> WUSCEnabled { get; } = new BoolValue();
        public IValue<bool> SrvWasDisabled { get; } = new BoolValue();
        public IValue<int> SrvRestoreStartType { get; } = new IntValue();
        public IValue<bool> VaaiExCopyEnabled { get; } = new BoolValue();
        public IValue<bool> VaaiCawEnabled { get; } = new BoolValue();
        public IValue<bool> VaaiWriteSameEnabled { get; } = new BoolValue();
        public IValue<bool> OdxEnabled { get; } = new BoolValue();
        public IValue<int> OdxOptimalRodSizeMB { get; } = new IntValue();
        public IValue<int> OdxMaximumRodSizeMB { get; } = new IntValue();
        public IValue<int> OdxRodTokenDefaultTimeoutSec { get; } = new IntValue();
        public IValue<int> OdxRodTokenMaximumTimeoutSec { get; } = new IntValue();
        public IValue<int> PortValue { get; } = new IntValue("Port");
        public IValue<string> Interface { get; } = new StringValue();
        public IValue<bool> BCastEnable { get; } = new BoolValue();
        public IValue<string> BCastInterface { get; } = new StringValue();
        public IValue<int> BCastPort { get; } = new IntValue();
        public IValue<string> Login { get; } = new StringValue();
        public IValue<string> Password { get; } = new StringValue();
        public IValue<int> MinBufferSize { get; } = new IntValue();
        public IValue<string> AlignmentMask { get; } = new StringValue();
        public IValue<int> MaxPendingRequests { get; } = new IntValue();
        public IValue<int> iScsiPingPeriod { get; } = new IntValue();
        public IValue<int> iScsiDiscoveryListInterfaces { get; } = new IntValue();
        public IValue<int> ServerIoWorkersCount { get; } = new IntValue();
        public IValue<int> ServerIoWorkersConcurency { get; } = new IntValue();
        public IValue<int> CmdExecTimeWarningLimitInSec { get; } = new IntValue();
        public IValue<int> iScisCmdSendCmdTimeoutInSec { get; } = new IntValue();
        public IValue<string> iSerListen { get; } = new StringValue();
        public IValue<string> LocalizationDir { get; } = new StringValue();
        public IValue<string> DefaultStoragePoolPath { get; } = new StringValue();
        public IValue<bool> ExperimentalLSFS { get; } = new BoolValue();
        public IValue<string> ClusterName { get; } = new StringValue();
        public IValue<string> ClusterGUID { get; } = new StringValue();
        public IValue<int> ClusterSettingsVersion { get; } = new IntValue();
        public IValue<string> ClusterNodes { get; } = new StringValue();
        public IValue<string> ClusterSync { get; } = new StringValue();
        public IValue<string> ClusterHeartbeat { get; } = new StringValue();
        public IValue<string> DataBaseRoot { get; } = new StringValue();
        public IValue<int> DBRotationDays { get; } = new IntValue();
        public IValue<int> DBFileSizeDays { get; } = new IntValue();
        public IValue<bool> PerformanceMonitorEnabled { get; } = new BoolValue();
        public IValue<string> PerformanceRoot { get; } = new StringValue();
        public IValue<int> FSMThresholdPercent { get; } = new IntValue();
        public IValue<int> FSMCheckPeriodSeconds { get; } = new IntValue();
        public IValue<bool> FSMEnabled { get; } = new BoolValue();

        #endregion IValues

        public StarWindServerExt(IStarWindServer server) {
            Server = server;
            foreach (var prop in GetType().GetProperties()) {
                if (!prop.PropertyType.GetInterfaces().Contains(typeof(IAbstactValue))) { continue; }
                if (!prop.CanRead) { continue; }
                if (prop.GetValue(this) is IAbstactValue value) {
                    value.Server = this;
                }
            }
        }

        public StarWindServerExt(string ip, int port) {
            Server = Component.CreateServer(ip, port);
            foreach (var prop in GetType().GetProperties()) {
                if (!prop.PropertyType.GetInterfaces().Contains(typeof(IAbstactValue))) { continue; }
                if (!prop.CanRead) { continue; }
                if (prop.GetValue(this) is IAbstactValue value) {
                    value.Server = this;
                }
            }
        }
    }
}