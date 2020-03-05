namespace StarWindXExtLib
{
    public interface IFlashCache
    {
        /// <summary>
        ///     Name of an IDevice
        /// </summary>
        string FlashStorage { get; }

        string SetFlashCache(IDeviceExt device);
    }
}