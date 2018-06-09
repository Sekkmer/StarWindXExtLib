using StarWindXLib;

namespace StarWindXExtLib {
    public class DDFileCreator : ParameterAppender, IDDFileCreator {
        public string Path { get; set; }
        public string Name { get; set; }

        public STARWIND_FILE_TYPE FileType => STARWIND_FILE_TYPE.STARWIND_DD_FILE;


        [Param]
        public int Size { get; set; }
        [Param]
        public int BlockSize { get; set; } = 0;
        [Param]
        [BoolToString("yes", "no")]
        public bool DeferredCreation { get; set; } = true;
        [Param]
        [BoolToString("yes", "no")]
        public bool Compressed { get; set; } = true;
    }
}
