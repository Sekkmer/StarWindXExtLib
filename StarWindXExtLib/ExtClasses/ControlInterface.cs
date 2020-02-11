using StarWindXLib;

using System;
using System.Collections.Generic;
using System.Linq;

namespace StarWindXExtLib
{

    internal class ControlInterface : IControlInterface
    {

        public string HostName { get; }

        public List<string> NetInterfaces { get; }

        public int Port { get; }

        public ControlInterface(ICommandResult result, int partnerId)
        {
            var prefix = partnerId == 0 ? "CurrentNode" : "PartnerNode";
            var sufix = partnerId == 0 ? "" : partnerId.ToString();
            string GetParam(string str)
            {
                return result.GetParam(prefix + str + sufix);
            }
            HostName = GetParam("HostName");
            NetInterfaces = GetParam("NetInterfaces").Split(';').ToList();
            try {
                Port = Convert.ToInt32(GetParam("Port"));
            } catch (Exception) { Port = -1; }
        }
    }
}