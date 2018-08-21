using StarWindXLib;

namespace StarWindXExtLib {

    public interface IFileCreator : IAppender {
        string Path { get; set; }
        string Name { get; set; }
        int Size { get; set; }
        STARWIND_FILE_TYPE FileType { get; }
        bool Compressed { get; set; }
    }
}