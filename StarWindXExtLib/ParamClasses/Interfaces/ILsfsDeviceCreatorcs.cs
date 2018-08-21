namespace StarWindXExtLib {

    public interface ILsfsDeviceCreator : ISimpleDeviceCreator {
        bool SupportDeletion { get; set; }
        bool DeduplicationEnabled { get; set; }
        bool AutoDefrag { get; set; }
        int StarPackCache { get; set; }
        bool Readonly { get; set; }
    }
}