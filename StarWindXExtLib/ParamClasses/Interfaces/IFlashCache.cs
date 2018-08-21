using StarWindXLib;

namespace StarWindXExtLib {

    public interface IFlashCache {
        string FlashStorage { get; }

        string SetFlashCache(IDevice device);
    }
}