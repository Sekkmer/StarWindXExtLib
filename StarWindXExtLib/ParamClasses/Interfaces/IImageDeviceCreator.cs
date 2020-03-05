namespace StarWindXExtLib
{
    public interface IImageDeviceCreator : ISimpleDeviceCreator
    {
        bool Asyncmode { get; set; }
        bool Readonly { get; set; }
    }
}