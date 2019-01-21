using StarWindXLib;

using System;

namespace StarWindXExtLib {

    public class LsfsDeviceCreator : ParameterAppender, ILsfsDeviceCreator {
        public string Path => File.Path;
        public string Name => File.Name;

        public STARWIND_DEVICE_TYPE DeviceType => STARWIND_DEVICE_TYPE.STARWIND_DD_LSFS_DEVICE;

        private IFileCreator File { get; }

        [Param]
        [BoolToString("yes", "no")]
        public bool SupportDeletion { get; set; } = true;

        [Param]
        [EnableParam("PMCacheSize")]
        [BoolToString("yes", "no")]
        public bool DeduplicationEnabled { get; set; } = false;

        [Param]
        [BoolToString("yes", "no")]
        public bool AutoDefrag { get; set; } = false;

        [Param("sectorsize")]
        public int LogicalSectorSize { get; set; } = 512;

        [Param("psectorSize")]
        public int PhysicalSectorSize { get; set; } = 4096;

        [Param]
        public int VLun => 0;

        [Param]
        [BoolToString("1", "0")]
        public bool AsyncAdd => true;

        [Param]
        public int Size => File.Size;

        [FlatParam]
        public ICacheParam Cache { get; } = new CacheParam();

        [FlatParam("L2", false)]
        public ICacheParam FlashCache { get; } = new CacheParam() { CacheMode = CacheMode.WriteThrough };

        [Param(false, "storage")]
        public string FlashStorage { get; set; } = "";

        [EnableParam("FlashCache", "storage")]
        public bool EnableFlashCache => FlashStorage != "";

        [Param(false, "PMCacheSize")]
        public int StarPackCache { get; set; } = 16;

        [Param(false, "readonly")]
        [BoolToString("yes", "no")]
        [EnableParam("readonly")]
        public bool Readonly { get; set; } = false;

        [Param(false)]
        [BoolToString("yes", "no")]
        public bool EsxComatibleMode { get; set; } = true;

        [EnableParam("EsxComatibleMode")]
        public bool EnableEsxComatibleMode { get; set; } = false;

        public LsfsDeviceCreator(IDDFileCreator file) {
            File = file;
        }

        public LsfsDeviceCreator(IDDFileCreator file, IDevice flashDevice) {
            File = file;
            FlashStorage = flashDevice.File;
            FlashCache.SizeInMB = Convert.ToInt32(flashDevice.Size);
        }

        public string SetFlashCache(IDevice device) {
            if (device is null && device.DeviceType == "Image file" && device.Size != "" && device.Size != "empty") {
                FlashStorage = "";
                return FlashStorage;
            }
            FlashCache.SizeInMB = Convert.ToInt32(device.Size);
            FlashStorage = device.Name;
            return FlashStorage;
        }
    }
}