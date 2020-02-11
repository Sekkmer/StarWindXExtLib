using StarWindXLib;

using System;
using System.Net;

namespace StarWindXExtLib
{
    public static class Extensions
    {
        internal static IDeviceExt ToExt(this IDevice device)
        {
            if (device is IDeviceExt ext) {
                return ext;
            } else if (device is IHADevice ha) {
                return new HADeviceExt(ha);
            } else if (device is ILSFSDevice lsfs) {
                return new LSFSDeviceExt(lsfs);
            } else {
                return new DeviceExt(device);
            }
        }

        internal static ITargetExt ToExt(this ITarget target)
        {
            if (target is TargetExt ext) {
                return ext;
            }
            return new TargetExt(target);
        }

        public static IDeviceExt MountSnapshot(this IStarWindServer server, ILSFSDevice device, ISnapshot snapshot)
        {
            var dataPath = device.File;
            var pars = new Parameters();
            pars.AppendParam("DataPath", dataPath);
            pars.AppendParam("CacheMode", device.CacheMode);
            pars.AppendParam("CacheSizeMB", device.CacheSize);
            pars.AppendParam("SnapshotID", snapshot.Offset);
            pars.AppendParam("EsxComatibleMode", "yes");
            pars.AppendParam("pSecorSize", device.GetPropertyValue("PhySectorSize"));
            pars.AppendParam("secorSize", device.SectorSize.ToString());
            pars.AppendParam("SnapshotDisabled", "yes");
            pars.AppendParam("size", device.Size);
            var path = dataPath.Substring(0, dataPath.LastIndexOf(@"\"));
            var name = dataPath.Substring(dataPath.LastIndexOf(@"\") + 1);
            name = name.Substring(0, name.LastIndexOf('.'));
            return server.CreateDevice(path, name, STARWIND_DEVICE_TYPE.STARWIND_DD_LSFS_DEVICE, pars).ToExt();
        }

        [Obsolete("Curently not working: StarWindX issue with ParametersClass", false)]
        public static void MountSnapshot(this IStarWindServer server, IHADevice device, ISnapshot snapshot, ITarget target = null)
        {
            var pars = new Parameters();
            pars.AppendParam("MountSnapshot", snapshot.Id.ToString());
            if (target != null) {
                pars.AppendParam("TargetName", target.Name);
            }
            pars.AppendParam("Async", "yes");
            pars.AppendParam("sendTo", device.DeviceId);
            server.ExecuteCommand(STARWIND_COMMAND_TYPE.STARWIND_CONTROL_REQUEST, "", pars);
        }

        public static IDeviceExt CreateDevice(this IStarWindServer server, IDeviceCreator creator)
        {
            return server.CreateDevice(creator.Path, creator.Name, creator.DeviceType, creator.GenerateParams()).ToExt();
        }

        public static void CreateFile(this IStarWindServer server, IFileCreator creator)
        {
            server.CreateFile(creator.Path, creator.Name, creator.FileType, creator.GenerateParams());
        }

        public static ITargetExt CreateTarget(this IStarWindServer server, ITargetCreator creator)
        {
            creator.GetServerName = () => server.GetHostName();
            return server.CreateTarget(creator.Alias, creator.Name, creator.GenerateParams()).ToExt();
        }

        public static string GetHostName(this IStarWindServer server)
        {
            return Dns.GetHostEntry(server.IP).HostName;
        }

        public static void Command(this IStarWindServer server, IServerCommand command)
        {
            server.ExecuteCommand(STARWIND_COMMAND_TYPE.STARWIND_COMMAND, command.Command, command.GenerateParams());
        }

        public static ICommandResult CommandEx(this IStarWindServer server, IServerCommand command)
        {
            server.ExecuteCommandEx(STARWIND_COMMAND_TYPE.STARWIND_COMMAND, command.Command, command.GenerateParams(), out var result);
            return result;
        }

        public static void Control(this IStarWindServer server, IServerControl control)
        {
            server.ExecuteCommand(STARWIND_COMMAND_TYPE.STARWIND_CONTROL_REQUEST, "", control.GenerateParams());
        }

        public static ICommandResult ControlEx(this IStarWindServer server, IServerControl control)
        {
            server.ExecuteCommandEx(STARWIND_COMMAND_TYPE.STARWIND_CONTROL_REQUEST, "", control.GenerateParams(), out var result);
            return result;
        }

        public static bool IsEquals(this IDevice lhs, IDevice rhs)
        {
            return lhs.DeviceId == rhs.DeviceId;
        }

        public static bool IsEquals(this ITarget lhs, ITarget rhs)
        {
            return lhs.Id == rhs.Id;
        }
    }
}