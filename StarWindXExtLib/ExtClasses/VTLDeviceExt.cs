using StarWindXLib;

namespace StarWindXExtLib
{
    internal class VTLDeviceExt : DeviceExt, IVTLDeviceExt
    {
        private readonly IVTLDevice vtlDevice;

        public VTLDeviceExt(IVTLDevice vtl) : base(vtl)
        {
            vtlDevice = vtl;
        }

        #region IVTLDevice

        public IVirtualTape CreateTape(STARWIND_TAPE_TYPE type, ulong Size, ulong partSize, Parameters builder)
        {
            return vtlDevice.CreateTape(type, Size, partSize, builder);
        }

        public void RemoveTape(int slotAddress)
        {
            vtlDevice.RemoveTape(slotAddress);
        }

        public void InsertTape(string tapeBarcode, int slotAddress)
        {
            vtlDevice.InsertTape(tapeBarcode, slotAddress);
        }

        public int CheckReplicationCredentials(VTLReplicationSettings pVal)
        {
            return vtlDevice.CheckReplicationCredentials(pVal);
        }

        public int ApplyReplicationSettings(VTLReplicationSettings pVal)
        {
            return vtlDevice.ApplyReplicationSettings(pVal);
        }

        public void UploadTape(string tapeBarcode)
        {
            vtlDevice.UploadTape(tapeBarcode);
        }

        public void DownloadTape(string tapeBarcode)
        {
            vtlDevice.DownloadTape(tapeBarcode);
        }

        public ICollection Tapes => vtlDevice.Tapes;

        public int AvailableSlots => vtlDevice.AvailableSlots;

        public int TransportSlots => vtlDevice.TransportSlots;

        public int DriveSlots => vtlDevice.DriveSlots;

        public int ImportExportSlots => vtlDevice.ImportExportSlots;

        public int StorageSlots => vtlDevice.StorageSlots;

        public ICollection Slots => vtlDevice.Slots;

        public VTLReplicationSettings ReplicationSettings => vtlDevice.ReplicationSettings;

        public int DriveType => vtlDevice.DriveType;

        #endregion IVTLDevice
    }
}