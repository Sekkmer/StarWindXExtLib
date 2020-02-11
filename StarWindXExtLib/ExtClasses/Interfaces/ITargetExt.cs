using StarWindXLib;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWindXExtLib
{
    public interface ITargetExt : ITarget
    {
        new IEnumerable<IDeviceExt> Devices { get; }

        Task RefreshAsync();
        Task AttachDeviceAsync(string deviceName);
        Task DetachDeviceAsync(string deviceName);
        Task DisconnectInitiatorAsync(string initiatorIQN);
    }
}