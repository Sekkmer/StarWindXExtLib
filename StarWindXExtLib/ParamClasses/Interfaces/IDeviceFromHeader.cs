namespace StarWindXExtLib {

    public interface IDeviceFromHeader : IServerAdd {
        string HeaderPath { get; set; }
        bool Readonly { get; set; }
    }
}