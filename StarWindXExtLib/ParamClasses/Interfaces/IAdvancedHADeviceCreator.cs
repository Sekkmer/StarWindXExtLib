namespace StarWindXExtLib {

    public interface IAdvancedHADeviceCreator : IServerAdd, ICache {
        string DeviceName { get; set; }
        string OwnTargetName { get; }
        string HeaderFile { get; }
        bool Asyncmode { get; set; }
        bool Highavailability { get; set; }
        bool Readonly { get; set; }
        bool Buffering { get; set; }
        int Header { get; set; }
        string AluaNodeGroupStates { get; }
        string Storage { get; }
        IHAImageHeaderCreator Image { get; set; }
    }
}