using StarWindXLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWindXExtLib {
    internal class TargetExt : ITarget {
        ITarget target;

        public TargetExt(ITarget target) {
            this.target = target;
        }

        public void Refresh() {
            target.Refresh();
        }

        public void AttachDevice(string deviceName) {
            target.AttachDevice(deviceName);
        }

        public void DetachDevice(string deviceName) {
            target.DetachDevice(deviceName);
        }

        public void DisconnectInitiator(string initiatorIQN) {
            target.DisconnectInitiator(initiatorIQN);
        }

        public string GetPropertyValue(string propertyName) {
            return target.GetPropertyValue(propertyName);
        }

        public string Name => target.Name;

        public string Id => target.Id;

        public string Alias => target.Alias;

        public bool IsClustered => target.IsClustered;

        public ICollection Devices => target.Devices.Transform((IDevice device) => device.ToExt());

        public ICollection Permissions => target.Permissions;

        public string type => target.type;
    }
}
