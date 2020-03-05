namespace StarWindXExtLib
{
    public class DeviceFromHeader : ParameterAppender, IDeviceFromHeader
    {
        public string Name
        {
            get => "";
            set { }
        }

        [Param("file")] public string HeaderPath { get; set; }

        [Param("readonly")]
        [BoolToString("Yes", "No")]
        public bool Readonly { get; set; } = false;
    }
}