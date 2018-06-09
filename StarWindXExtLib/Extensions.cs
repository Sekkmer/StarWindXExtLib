using System;
using System.Collections.Generic;
using System.Net;
using StarWindXLib;

namespace StarWindXExtLib {
    public static class Extensions {
        public static ICollection GetDevicesExt(this IStarWindServer server) {
            return server.Devices.Transform<IDevice, IDeviceExt>(device => device.ToExt()); ;
        }

        public static ICollection GetDevicesExt(this ITarget target) {
            return target.Devices.Transform<IDevice, IDeviceExt>(device => device.ToExt());
        }

        public static dynamic ToExt(this IDevice device) {
            if (device is IHADevice ha) {
                return new HADeviceExt(ha);
            } else if (device is ILSFSDevice lsfs) {
                return new LSFSDeviceExt(lsfs);
            } else {
                return new DeviceExt(device);
            }
        }

        public static bool Contains(this ICollection collection, IDevice other) {
            foreach (IDevice item in collection) {
                if (item.IsEquals(other)) {
                    return true;
                }
            }
            return false;
        }

        public static bool Contains(this ICollection collection, ITarget other) {
            foreach (ITarget item in collection) {
                if (item.IsEquals(other)) {
                    return true;
                }
            }
            return false;
        }

        public static T Find<T>(this ICollection collection, Predicate<T> pred) {
            foreach (T item in collection) {
                if (pred(item)) {
                    return item;
                }
            }
            return default;
        }

        public static IDevice MountSnapshot(this IStarWindServer server, ILSFSDevice device, ISnapshot snapshot) {
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
            return server.CreateDevice(path, name, STARWIND_DEVICE_TYPE.STARWIND_DD_LSFS_DEVICE, pars);
        }

        [Obsolete("Curently not working: StarWindX issue with ParametersClass", true)]
        public static void MountSnapshot(this IStarWindServer server, IHADevice device, ISnapshot snapshot, ITarget target = null) {
            var pars = new Parameters();
            pars.AppendParam("MountSnapshot", snapshot.Id.ToString());
            if (target != null) {
                pars.AppendParam("TargetName", target.Name);
            }
            pars.AppendParam("Async", "yes");
            pars.AppendParam("sendTo", device.DeviceId);
            server.ExecuteCommand(STARWIND_COMMAND_TYPE.STARWIND_CONTROL_REQUEST, "", pars);
        }

        public static IDevice CreateDevice(this IStarWindServer server, IDeviceCreator creator) {               
            return server.CreateDevice(creator.Path, creator.Name, creator.DeviceType, creator.GenerateParams());
        }

        public static void CreateFile(this IStarWindServer server, IFileCreator creator) {
            server.CreateFile(creator.Path, creator.Name, creator.FileType, creator.GenerateParams());
        }   

        public static ITarget CreateTarget(this IStarWindServer server, ITargetCreator creator) {
            creator.GetServerName = () => server.GetHostName();
            return server.CreateTarget(creator.Alias, creator.Name, creator.GenerateParams());
        }

        public static string GetHostName(this IStarWindServer server) {
            return Dns.GetHostEntry(server.IP).HostName;
        }

        public static void Control(this IStarWindServer server, IServerControl control) {
            server.ExecuteCommand(STARWIND_COMMAND_TYPE.STARWIND_CONTROL_REQUEST, "", control.GenerateParams());
        }

        public static ICommandResult ControlEx(this IStarWindServer server, IServerControl control) {
            server.ExecuteCommandEx(STARWIND_COMMAND_TYPE.STARWIND_CONTROL_REQUEST, "", control.GenerateParams(), out ICommandResult result);
            return result;
        }

        public static bool IsEquals(this IDevice lhs, IDevice rhs) {
            return lhs.DeviceId == rhs.DeviceId;
        }

        public static bool IsEquals(this ITarget lhs, ITarget rhs) {
            return lhs.Id == rhs.Id;
        }

        public static bool IsElement<T>(this ICollection collection, Predicate<T> pred) {
            foreach (T element in collection) {
                if (pred(element)) { return true; }
            }
            return false;
        }

        public static List<T> AsList<T>(this ICollection collection) {
            var list = new List<T>();
            foreach (T snapshot in collection) {
                list.Add(snapshot);
            }
            return list;
        }

        public static List<T> AsList<T>(this ICollection collection, Predicate<T> pred) {
            var list = new List<T>();
            foreach (T element in collection) {
                if (pred(element)) { list.Add(element); }
            }
            return list;
        }

        public static ICollection Transform<T, U>(this ICollection collection, Func<T, U> trans) {
            var outCollection = new CollectionExt();
            foreach (T element in collection) {
                outCollection.Add(trans(element));
            }
            return outCollection;
        }
    }
}