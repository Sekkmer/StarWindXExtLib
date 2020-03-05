using System;
using System.Collections.Generic;

namespace StarWindXExtLib
{
    public class HAPartnerAdder : ParameterAppender, IAdvancedHANodes, IServerControl
    {
        private static readonly List<string> ConcatAll = new List<string>
        {
            "Priority",
            "NodeType",
            "JournalStorage",
            "TargetName",
            "AuthChapLogin",
            "AuthChapPassword",
            "AuthMChapName",
            "AuthMChapSecret",
            "AuthChapType"
        };

        public HAPartnerAdder(IHADeviceExt device, List<IAdvancedHANode> nodes)
        {
            Device = device;
            Nodes = nodes;
            ConcatAllValues();
            PartnerIP = "";
            for (var i = 1; i < Count; i++)
            {
                if (i > 2 && i < Count - 1) PartnerIP += ";";
                PartnerIP += "#p" + i + "=" + string.Join(",", First.PartnerIP[nodes[i].NodeId]);
            }
        }

        public HAPartnerAdder(IHADeviceExt device)
        {
            Device = device;
        }

        [Param("Replicator")]
        public string ReplicatorString
        {
            get
            {
                var str = "";
                for (var i = 1; i <= Nodes.Count; i++)
                {
                    if (i > 1) str += ";";
                    str += "#p" + i + "=" + Replicator;
                }

                return str;
            }
        }

        public int Replicator { get; set; } = 0;

        private IHADeviceExt Device { get; }
        private List<IAdvancedHANode> Nodes { get; set; }

        [Param] public string Priority { get; private set; }

        [Param("nodeType")] public string NodeType { get; private set; }

        [Param("PartnerTargetName")] public string TargetName { get; private set; }

        [Param(false, "JournalStorages")] public string JournalStorage { get; private set; }

        [Param] public string PartnerIP { get; private set; }
        [Param] public string AuthChapLogin { get; private set; }
        [Param] public string AuthChapPassword { get; private set; }
        [Param] public string AuthMChapName { get; private set; }
        [Param] public string AuthMChapSecret { get; private set; }
        [Param] public string AuthChapType { get; private set; }

        public bool EnableJournalStorage => !string.IsNullOrEmpty(First.JournalStorage);

        public IAdvancedHANode First => Nodes[0];

        public int Count => Nodes.Count;

        public void LoadNodes(List<IAdvancedHANode> nodes)
        {
            Nodes = nodes;
            ConcatAllValues();
            PartnerIP = "";
            for (var i = 1; i < Count; i++)
            {
                if (i > 2 && i < Count - 1) PartnerIP += ";";
                PartnerIP += "#p" + i + "=" + string.Join(",", First.PartnerIP[nodes[i].NodeId]);
            }
        }

        public List<T> Transform<T>(Converter<IAdvancedHANode, T> convert)
        {
            return Nodes.ConvertAll(convert);
        }

        public string SendTo => Device.DeviceId;

        public string Request => "AddPartner";

        private void ConcatAllValues()
        {
            foreach (var name in ConcatAll)
            {
                var value = "";
                for (var index = 1; index < Count; index++)
                {
                    if (index > 2 && index < Count - 1) value += ";";
                    value += "#p" + index + "=" + First.GetValue(name);
                    index++;
                }

                var prop = GetType().GetProperty(name);
                if (prop.CanWrite) prop.SetValue(this, value);
            }
        }
    }
}