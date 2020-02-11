namespace StarWindXExtLib
{
    public class DiskBridgeCreator : ParameterAppender, IDiskBridgeCreator
    {

        public string Name { get; set; }

        [Param("file")]
        public string File { get; set; }

        [Param(false, "readonly"), BoolToString("Yes", "No")]
        public bool Readonly { get; set; } = false;

        [Param(false, "asyncmode"), BoolToString("Yes", "No")]
        public bool Asyncmode { get; set; } = false;

        [FlatParam]
        public ICacheParam Cache { get; } = new CacheParam();
    }
}
