using StarWindXLib;

namespace StarWindXExtLib {

    public interface IDeviceCreator : IAppender {
        string Path { get; }
        string Name { get; }
        int Size { get; }
        STARWIND_DEVICE_TYPE DeviceType { get; }
    }
}