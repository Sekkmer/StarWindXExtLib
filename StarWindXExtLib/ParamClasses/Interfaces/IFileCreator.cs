using StarWindXLib;

namespace StarWindXExtLib
{
    public interface IFileCreator : IAppender
    {
        string Path { get; set; }
        string Name { get; set; }
        int Size { get; set; }
        STARWIND_FILE_TYPE FileType { get; }

        /// <summary>
        ///     Default: false on Image, true on LSFS
        /// </summary>
        bool Compressed { get; set; }
    }
}