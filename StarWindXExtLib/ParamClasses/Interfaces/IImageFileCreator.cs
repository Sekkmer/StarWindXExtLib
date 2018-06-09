namespace StarWindXExtLib {
    public interface IImageFileCreator : IFileCreator {
        bool Flat { get; set; }
        bool ZeroOut { get; set; }
        bool DeferredInit { get; set; }
        bool Encrypted { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
