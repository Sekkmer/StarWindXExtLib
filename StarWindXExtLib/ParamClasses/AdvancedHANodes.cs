using System;
using System.Collections.Generic;

namespace StarWindXExtLib
{
    public class AdvancedHANodes : ParameterAppender, IAdvancedHANodes
    {
        private static readonly Dictionary<string, int> ConcatFrom = new Dictionary<string, int>
        {
            {nameof(Priority), 0},
            {nameof(NodeType), 0},
            {nameof(JournalStorage), 0},
            {nameof(TargetName), 1},
            {nameof(AuthChapLogin), 1},
            {nameof(AuthChapPassword), 1},
            {nameof(AuthMChapName), 1},
            {nameof(AuthMChapSecret), 1},
            {nameof(AuthChapType), 1}
        };

        public AdvancedHANodes()
        {
        }

        public AdvancedHANodes(List<IAdvancedHANode> nodes)
        {
            LoadNodes(nodes);
        }

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

        [EnableParam("JournalStorages")] public bool EnableJournalStorage { get; private set; }

        public IAdvancedHANode First => Nodes[0];

        public int Count => Nodes.Count;

        public void LoadNodes(List<IAdvancedHANode> nodes)
        {
            Nodes = nodes;
            foreach (var prop in GetType().GetProperties()) {
                if (!prop.CanWrite) continue;
                if (prop.Name == nameof(PartnerIP) || prop.Name == nameof(EnableJournalStorage)) continue;
                prop.SetValue(this, ConcatValues(prop.Name, nodes));
            }

            EnableJournalStorage = nodes.Exists(node => !string.IsNullOrEmpty(node.JournalStorage));
            PartnerIP = "";
            for (var i = 1; i < Count; i++) {
                if (i > 2 && i < Count - 1) PartnerIP += ";";
                PartnerIP += "#p" + i + "=" + string.Join(",", First.PartnerIP[nodes[i].NodeId]);
            }
        }

        public List<T> Transform<T>(Converter<IAdvancedHANode, T> convert)
        {
            return Nodes.ConvertAll(convert);
        }

        private static string ConcatValues(string name, List<IAdvancedHANode> nodes)
        {
            var value = "";
            var index = 0;
            var from = ConcatFrom[name];
            foreach (var node in nodes) {
                if (index < from) {
                    index++;
                    continue;
                }

                if (index > from) value += ";";
                value += "#p" + index + "=" + node.GetValue(name);
                index++;
            }

            return value;
        }
    }
}