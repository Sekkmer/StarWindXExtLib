using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarWindXLib;

namespace StarWindXExtLib
{
    internal class TargetExt : ITargetExt
    {
        private readonly ITarget target;

        public TargetExt(ITarget target)
        {
            this.target = target;
        }

        #region ITarget

        public void Refresh()
        {
            target.Refresh();
        }

        public void AttachDevice(string deviceName)
        {
            target.AttachDevice(deviceName);
        }

        public void DetachDevice(string deviceName)
        {
            target.DetachDevice(deviceName);
        }

        public void DisconnectInitiator(string initiatorIQN)
        {
            target.DisconnectInitiator(initiatorIQN);
        }

        public string GetPropertyValue(string propertyName)
        {
            return target.GetPropertyValue(propertyName);
        }

        public string Name => target.Name;

        public string Id => target.Id;

        public string Alias => target.Alias;

        public bool IsClustered => target.IsClustered;

        public IEnumerable<IDeviceExt> Devices => target.Devices.Cast<IDevice>().Select(device => device.ToExt());

        ICollection ITarget.Devices => new CollectionExt<IDeviceExt>(Devices);

        public ICollection Permissions => target.Permissions;

        public string type => target.type;

        #endregion ITarget

        #region Async

        public async Task RefreshAsync()
        {
            await Task.Run(() => target.Refresh());
        }

        public async Task AttachDeviceAsync(string deviceName)
        {
            await Task.Run(() => target.AttachDevice(deviceName));
        }

        public async Task DetachDeviceAsync(string deviceName)
        {
            await Task.Run(() => target.DetachDevice(deviceName));
        }

        public async Task DisconnectInitiatorAsync(string initiatorIQN)
        {
            await Task.Run(() => target.DisconnectInitiator(initiatorIQN));
        }

        #endregion Async
    }
}