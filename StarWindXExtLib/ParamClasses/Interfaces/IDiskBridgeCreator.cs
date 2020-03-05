namespace StarWindXExtLib
{
    public interface IDiskBridgeCreator : IServerAdd, ICache
    {
        string File { get; set; }
        bool Readonly { get; set; }
        bool Asyncmode { get; set; }
    }
}