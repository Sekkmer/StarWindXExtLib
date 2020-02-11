namespace StarWindXExtLib
{

    public interface IHADeviceCreator : IDeviceCreator
    {
        InitializaMethod InitializaMethod { get; set; }
        IHANodeParam FirstNode { get; }
        IHANodeParam SecondNode { get; }
        IHANodeParam ThirdNode { get; }
    }
}