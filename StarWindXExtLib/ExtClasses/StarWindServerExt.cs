using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarWindXLib;

namespace StarWindXExtLib
{
    public class StarWindServerExt : IStarWindServerExt
    {
        public static readonly IStarWindX Component = new StarWindX();

        public StarWindServerExt(IStarWindServer server)
        {
            Server = server;
            foreach (var prop in GetType().GetProperties())
            {
                if (!prop.PropertyType.GetInterfaces().Contains(typeof(IAbstractValue))) continue;
                if (!prop.CanRead) continue;
                if (prop.GetValue(this) is IAbstractValue value) value.Server = this;
            }
        }

        public StarWindServerExt(string ip, int port)
            : this(Component.CreateServer(ip, port))
        {
        }

        private IStarWindServer Server { get; }

        public IEnumerable<ITargetExt> Targets => Server.Targets.Cast<ITarget>().Select(target => target.ToExt());

        public IEnumerable<IDeviceExt> Devices => Server.Devices.Cast<IDevice>().Select(device => device.ToExt());

        public IDeviceExt GetDeviceByID(string DeviceId)
        {
            return Server.GetDeviceByID(DeviceId).ToExt();
        }

        public IDeviceExt CreateDevice(string Path, string Name, STARWIND_DEVICE_TYPE fileType, Parameters builder)
        {
            return Server.CreateDevice(Path, Name, fileType, builder).ToExt();
        }

        public ITargetExt CreateTarget(string targetAlias, string TargetName, Parameters builder)
        {
            return Server.CreateTarget(targetAlias, TargetName, builder).ToExt();
        }

        public static STARWIND_DEVICE_TYPE GetDeviceType(string deviceType)
        {
            if (deviceType == "Image file") return STARWIND_DEVICE_TYPE.STARWIND_IMAGE_DEVICE;
            if (deviceType == "RAM disk") return STARWIND_DEVICE_TYPE.STARWIND_RAM_DEVICE;
            if (deviceType == "LSFS Disk") return STARWIND_DEVICE_TYPE.STARWIND_DD_LSFS_DEVICE;

            // ...
            if (deviceType == "DiskBridge") return STARWIND_DEVICE_TYPE.STARWIND_CUSTOM_DEVICE;
            if (deviceType == "Control Device") return STARWIND_DEVICE_TYPE.STARWIND_CUSTOM_DEVICE;
            return STARWIND_DEVICE_TYPE.STARWIND_CUSTOM_DEVICE;
        }

        public static string GetServerName(IStarWindServerExt server)
        {
            return "_" + server.IP.Replace('.', '_') + "_" + server.Port;
        }

        #region IStarWindServer

        public void Connect()
        {
            Server.Connect();
        }

        public void Disconnect()
        {
            Server.Disconnect();
        }

        public void Refresh()
        {
            Server.Refresh();
        }

        public void CreateFile(string Path, string Name, STARWIND_FILE_TYPE fileType, Parameters builder)
        {
            Server.CreateFile(Path, Name, fileType, builder);
        }

        IDevice IStarWindServer.CreateDevice(string Path, string Name, STARWIND_DEVICE_TYPE fileType,
            Parameters builder)
        {
            return Server.CreateDevice(Path, Name, fileType, builder).ToExt();
        }

        ITarget IStarWindServer.CreateTarget(string targetAlias, string TargetName, Parameters builder)
        {
            return Server.CreateTarget(targetAlias, TargetName, builder).ToExt();
        }

        public void ExecuteCommand(STARWIND_COMMAND_TYPE cmdType, string commandName, Parameters builder)
        {
            Server.ExecuteCommand(cmdType, commandName, builder);
        }

        public void RemoveFile(string Path, STARWIND_DEVICE_TYPE DeviceType)
        {
            Server.RemoveFile(Path, DeviceType);
        }

        public void RemoveDevice(string DeviceId, bool forceRemove)
        {
            Server.RemoveDevice(DeviceId, forceRemove);
        }

        public void RemoveTarget(string TargetName, bool forceRemove)
        {
            Server.RemoveTarget(TargetName, forceRemove);
        }

        public ICollection GetInitiators(string TargetId)
        {
            return Server.GetInitiators(TargetId);
        }

        public string GetDeviceID(string deviceName)
        {
            return Server.GetDeviceID(deviceName);
        }

        IDevice IStarWindServer.GetDeviceByID(string DeviceId)
        {
            return Server.GetDeviceByID(DeviceId).ToExt();
        }

        public IPlugin GetPlugin(STARWIND_PLUGIN_ID pluginID)
        {
            return Server.GetPlugin(pluginID);
        }

        public string GetLicensedPlugins(string delimiter)
        {
            return Server.GetLicensedPlugins(delimiter);
        }

        public string ShowFileBrowser(STARWIND_PLUGIN_ID pluginID, IFileBrowserEvents eventsCallback,
            out bool pCancelled)
        {
            return Server.ShowFileBrowser(pluginID, eventsCallback, out pCancelled);
        }

        public void AddCHAP(string TargetName, string LocalName, string LocalSecret, string PeerName, string PeerSecret)
        {
            Server.AddCHAP(TargetName, LocalName, LocalSecret, PeerName, PeerSecret);
        }

        public void RemoveCHAP(string permissionName, string TargetName, bool forceRemove)
        {
            Server.RemoveCHAP(permissionName, TargetName, forceRemove);
        }

        public IACLRule AddACLRule(string ruleName, string Source, string Destination, string Interface, bool Allow)
        {
            return Server.AddACLRule(ruleName, Source, Destination, Interface, Allow);
        }

        public void RemoveACLRule(string ruleID)
        {
            Server.RemoveACLRule(ruleID);
        }

        public void RemoveDeviceEx(string DeviceId, bool forceRemove, Parameters @params)
        {
            Server.RemoveDeviceEx(DeviceId, forceRemove, @params);
        }

        public string GetServerParameter(string paramName)
        {
            return Server.GetServerParameter(paramName);
        }

        public void SetServerParameter(string paramName, string paramValue)
        {
            Server.SetServerParameter(paramName, paramValue);
        }

        public ICollection QueryServerPerformanceData(SW_PERFORMANCE_COUNTER_TYPE counterType,
            SW_PERFORMANCE_TIME_INTERVAL timeInterval)
        {
            return Server.QueryServerPerformanceData(counterType, timeInterval);
        }

        public ICollection QueryTargetPerformanceData(SW_PERFORMANCE_COUNTER_TYPE counterType, string TargetName,
            SW_PERFORMANCE_TIME_INTERVAL timeInterval)
        {
            return Server.QueryTargetPerformanceData(counterType, TargetName, timeInterval);
        }

        public ICollection QueryDevicePerformanceData(SW_PERFORMANCE_COUNTER_TYPE counterType, string deviceName,
            SW_PERFORMANCE_TIME_INTERVAL timeInterval)
        {
            return Server.QueryDevicePerformanceData(counterType, deviceName, timeInterval);
        }

        public void InstallLicense(string licenseFile)
        {
            Server.InstallLicense(licenseFile);
        }

        public void ExecuteCommandEx(STARWIND_COMMAND_TYPE cmdType, string commandName, Parameters builder,
            out ICommandResult pVal)
        {
            Server.ExecuteCommandEx(cmdType, commandName, builder, out pVal);
        }

        public string IP
        {
            get => Server.IP;
            set => Server.IP = value;
        }

        public int Port
        {
            get => Server.Port;
            set => Server.Port = value;
        }

        public IAuthentificationInfo AuthentificationInfo => Server.AuthentificationInfo;

        public bool Connected => Server.Connected;

        ICollection IStarWindServer.Targets => new CollectionExt<ITarget>(Targets);

        ICollection IStarWindServer.Devices => new CollectionExt<IDeviceExt>(Devices);

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

        #region Async

        public async Task ConnectAsync()
        {
            await Task.Run(() => Server.Connect());
        }

        public async Task DisconnectAsync()
        {
            await Task.Run(() => Server.Disconnect());
        }

        public async Task RefreshAsync()
        {
            await Task.Run(() => Server.Refresh());
        }

        #endregion Async

        #region IValues

        public IIntValue LogLevel { get; } = new IntValue();
        public IStringValue LogMask { get; } = new StringValue();
        public IIntValue LogRotateSize { get; } = new IntValue();
        public IIntValue LogRotateKeepLastFiles { get; } = new IntValue();
        public IIntValue UpdatePeriod { get; } = new IntValue();
        public IStringValue UpdateHost { get; } = new StringValue();
        public IStringValue UpdatePage { get; } = new StringValue();
        public IIntValue UpdatePort { get; } = new IntValue();
        public IStringValue UpdateCopyId { get; } = new StringValue();
        public IStringValue UpdateLastRequest { get; } = new StringValue();
        public IBoolValue WUSCEnabled { get; } = new BoolValue();
        public IBoolValue SrvWasDisabled { get; } = new BoolValue();
        public IIntValue SrvRestoreStartType { get; } = new IntValue();
        public IBoolValue VaaiExCopyEnabled { get; } = new BoolValue();
        public IBoolValue VaaiCawEnabled { get; } = new BoolValue();
        public IBoolValue VaaiWriteSameEnabled { get; } = new BoolValue();
        public IBoolValue OdxEnabled { get; } = new BoolValue();
        public IIntValue OdxOptimalRodSizeMB { get; } = new IntValue();
        public IIntValue OdxMaximumRodSizeMB { get; } = new IntValue();
        public IIntValue OdxRodTokenDefaultTimeoutSec { get; } = new IntValue();
        public IIntValue OdxRodTokenMaximumTimeoutSec { get; } = new IntValue();
        public IIntValue PortValue { get; } = new IntValue("Port");
        public IStringValue Interface { get; } = new StringValue();
        public IBoolValue BCastEnable { get; } = new BoolValue();
        public IStringValue BCastInterface { get; } = new StringValue();
        public IIntValue BCastPort { get; } = new IntValue();
        public IStringValue Login { get; } = new StringValue();
        public IStringValue Password { get; } = new StringValue();
        public IIntValue MinBufferSize { get; } = new IntValue();
        public IStringValue AlignmentMask { get; } = new StringValue();
        public IIntValue MaxPendingRequests { get; } = new IntValue();
        public IIntValue iScsiPingPeriod { get; } = new IntValue();
        public IIntValue iScsiDiscoveryListInterfaces { get; } = new IntValue();
        public IIntValue ServerIoWorkersCount { get; } = new IntValue();

        public IIntValue ServerIoWorkersConcurency { get; } = new IntValue();

        // public IIntValue CmdExecTimeWarningLimitInSec { get; } = new IntValue();
        // public IIntValue iScisCmdSendCmdTimeoutInSec { get; } = new IntValue();
        public IStringValue iSerListen { get; } = new StringValue();
        public IStringValue LocalizationDir { get; } = new StringValue();
        public IStringValue DefaultStoragePoolPath { get; } = new StringValue();
        public IBoolValue ExperimentalLSFS { get; } = new BoolValue();
        public IStringValue ClusterName { get; } = new StringValue();
        public IStringValue ClusterGUID { get; } = new StringValue();
        public IIntValue ClusterSettingsVersion { get; } = new IntValue();
        public IStringValue ClusterNodes { get; } = new StringValue();
        public IStringValue ClusterSync { get; } = new StringValue();
        public IStringValue ClusterHeartbeat { get; } = new StringValue();
        public IStringValue DataBaseRoot { get; } = new StringValue();
        public IIntValue DBRotationDays { get; } = new IntValue();
        public IIntValue DBFileSizeDays { get; } = new IntValue();
        public IBoolValue PerformanceMonitorEnabled { get; } = new BoolValue();
        public IStringValue PerformanceRoot { get; } = new StringValue();
        public IIntValue FSMThresholdPercent { get; } = new IntValue();
        public IIntValue FSMCheckPeriodSeconds { get; } = new IntValue();
        public IBoolValue FSMEnabled { get; } = new BoolValue();

        #endregion IValues
    }
}