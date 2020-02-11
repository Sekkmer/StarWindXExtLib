using StarWindXLib;

namespace StarWindXExtLib
{

    public class HADeviceCreator : ParameterAppender, IHADeviceCreator
    {
        public string Path => FirstNode.Path;

        public string Name => FirstNode.BaseName;

        public int Size => FirstNode.Size;

        public STARWIND_DEVICE_TYPE DeviceType => STARWIND_DEVICE_TYPE.STARWIND_HA_DEVICE;

        [Param] public InitializaMethod InitializaMethod { get; set; } = InitializaMethod.Clear;

        [FlatParam] public IHANodeParam FirstNode { get; } = new HANodeParam();
        [SubParam("partner1")] public IHANodeParam SecondNode { get; } = new HANodeParam();
        [SubParam(false, "partner2")] public IHANodeParam ThirdNode { get; private set; } = null;

        private bool enableThirdNode = false;

        [EnableParam("partner2")]
        public bool EnableThirdNode {
            get => enableThirdNode; set {
                enableThirdNode = value;
                if (value && ThirdNode is null) {
                    ThirdNode = new HANodeParam();
                }
            }
        }
    }
}