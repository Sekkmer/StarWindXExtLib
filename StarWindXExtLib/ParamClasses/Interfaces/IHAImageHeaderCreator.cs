using System.Collections.Generic;

namespace StarWindXExtLib
{
    public interface IHAImageHeaderCreator : IServerControl, ICache
    {
        IAdvancedHANodes Nodes { get; set; }
        string SerialId { get; set; }

        /// <summary>
        ///     Default: "STARWIND"
        /// </summary>
        string Product { get; set; }

        /// <summary>
        ///     Default: "STARWIND"
        /// </summary>
        string Vendor { get; set; }

        /// <summary>
        ///     Default: 0
        /// </summary>
        int Replicator { get; set; }

        IDeviceExt Device { get; set; }

        string DeviceName { get; }
        string OwnTargetName { get; }
        string DeviceHeaderPath { get; }

        void LoadNodes(List<IAdvancedHANode> list);
    }
}