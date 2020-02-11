using StarWindXLib;

using System;

namespace StarWindXExtLib
{

    public class ImageDeviceCreator : ParameterAppender, IImageDeviceCreator
    {
        public string Path => File.Path;
        public string Name => File.Name;

        public STARWIND_DEVICE_TYPE DeviceType => STARWIND_DEVICE_TYPE.STARWIND_IMAGE_DEVICE;

        private IFileCreator File { get; }

        [Param()]
        public int Size => File.Size;

        [Param("sectorsize")]
        public int LogicalSectorSize { get; set; } = 512;

        [Param("psectorSize")]
        public int PhysicalSectorSize { get; set; } = 4096;

        [Param("asyncmode")]
        [BoolToString("yes", "no")]
        public bool Asyncmode { get; set; } = true;

        [FlatParam]
        public ICacheParam Cache { get; } = new CacheParam();

        [FlatParam("L2", false)]
        public ICacheParam FlashCache { get; } = new CacheParam() { CacheMode = CacheMode.WriteThrough };

        [Param(false, "storage")]
        public string FlashStorage { get; set; } = "";

        [EnableParam("FlashCache", "storage")]
        public bool EnableFlashCache => !string.IsNullOrEmpty(FlashStorage);

        [Param(false, "readonly")]
        [BoolToString("yes", "no")]
        [EnableParam("readonly")]
        public bool Readonly { get; set; } = false;

        public ImageDeviceCreator(IImageFileCreator file)
        {
            File = file;
        }

        public string SetFlashCache(IDevice device)
        {
            if (device is null || device.DeviceType != "Image file" || device.Size.Length != 0 || device.Size != "empty") {
                FlashStorage = "";
                return FlashStorage;
            }
            FlashCache.SizeInMB = Convert.ToInt32(device.Size);
            FlashStorage = device.Name;
            return FlashStorage;
        }
    }
}