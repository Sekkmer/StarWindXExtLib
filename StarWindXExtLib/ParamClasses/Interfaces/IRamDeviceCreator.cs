namespace StarWindXExtLib
{
    public interface IRamDeviceCreator : IDeviceCreator, ISectorSize
    {
        bool Useawe { get; set; }
        FileSystemFormat Format { get; set; }
        void SetSize(int size);
    }
}