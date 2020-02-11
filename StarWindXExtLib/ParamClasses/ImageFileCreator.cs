using StarWindXLib;

namespace StarWindXExtLib
{

    public class ImageFileCreator : ParameterAppender, IImageFileCreator
    {
        public STARWIND_FILE_TYPE FileType => STARWIND_FILE_TYPE.STARWIND_IMAGE_FILE;

        public string Path { get; set; }

        public string Name { get; set; }

        [Param]
        public int Size { get; set; }

        [Param]
        [BoolToString("True", "False")]
        public bool Flat { get; set; } = true;

        [Param]
        [BoolToString("True", "False")]
        public bool ZeroOut { get; set; } = false;

        [Param]
        [BoolToString("True", "False")]
        public bool Compressed { get; set; } = false;

        [Param]
        [BoolToString("True", "False")]
        public bool DeferredInit { get; set; } = true;

        [Param]
        [BoolToString("True", "False")]
        [EnableParam("Username", "Password")]
        public bool Encrypted { get; set; } = false;

        [Param(false)]
        public string Username { get; set; } = "";

        [Param(false)]
        public string Password { get; set; } = "";
    }
}