using StarWindXLib;

namespace StarWindXExtLib {

    public class RamDeviceCreator : ParameterAppender, IRamDeviceCreator {
        public string Path => "";
        public string Name => "";
        public STARWIND_DEVICE_TYPE DeviceType => STARWIND_DEVICE_TYPE.STARWIND_RAM_DEVICE;

        [Param("size")]
        public int Size { get; private set; }

        [Param("useawe"), BoolToString("yes", "no")]
        public bool Useawe { get; set; } = false;

        [Param("sectorsize")]
        public int LogicalSectorSize { get; set; } = 512;

        [Param("psectorSize")]
        public int PhysicalSectorSize { get; set; } = 4096;

        [Param("format")]
        public CacheFormat Format { get; set; } = CacheFormat.NONE;

        public void SetSize(int size) {
            Size = size;
        }
    }
}