using StarWindXLib;

using System;
using System.Collections.Generic;
using System.Linq;

namespace StarWindXExtLib {

    internal class ControlInterface : Displayable, IControlInterface {

        [Display(0, "HostName")]
        public string HostName { get; }

        [Display(1, "Net Interfaces")]
        public List<string> NetInterfaces { get; }

        [Display(2)]
        public int Port { get; }

        public override string UniqueId => HostName;

        public ControlInterface(ICommandResult result, int partnerId) {
            var prefix = partnerId == 0 ? "CurrentNode" : "PartnerNode";
            var sufix = partnerId == 0 ? "" : partnerId.ToString();
            string GetParam(string str) {
                return result.GetParam(prefix + "" + sufix);
            }
            HostName = GetParam("HostName");
            NetInterfaces = GetParam("NetInterfaces").Split(';').ToList();
            try {
                Port = Convert.ToInt32(GetParam("Port"));
            } catch (Exception) { Port = -1; }
        }
    }
}