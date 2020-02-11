namespace StarWindXExtLib
{

    public interface IRamDeviceCreator : IDeviceCreator, ISectorSize
    {

        void SetSize(int size);

        bool Useawe { get; set; }
        FileSystemFormat Format { get; set; }
    }
}