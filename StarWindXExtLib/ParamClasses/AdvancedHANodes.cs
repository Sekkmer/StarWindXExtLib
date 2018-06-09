using System;
using System.Collections.Generic;

namespace StarWindXExtLib {
    public class AdvancedHANodes : ParameterAppender, IAdvancedHANodes {
        [Param] public string Priority { get; private set; }
        [Param("nodeType")]
        public string NodeType { get; private set; }
        [Param("PartnerTargetName")]
        public string TargetName { get; private set; }
        [Param(false, "JournalStorages")]
        public string JournalStorage { get; private set; }

        [Param] public string PartnerIP { get; private set; }
        [Param] public string AuthChapLogin { get; private set; }
        [Param] public string AuthChapPassword { get; private set; }
        [Param] public string AuthMChapName { get; private set; }
        [Param] public string AuthMChapSecret { get; private set; }
        [Param] public string AuthChapType { get; private set; }

        [EnableParam("JournalStorages")]
        public bool EnableJournalStorage { get; private set; }

        public IAdvancedHANode First => Nodes[0];
        private List<IAdvancedHANode> Nodes { get; }

        public int Count => Nodes.Count;

        private static readonly Dictionary<string, int> ConcatFrom = new Dictionary<string, int> {
                { "Priority", 0 },
                { "NodeType", 0 },
                { "JournalStorage", 0 },
                { "TargetName", 1 },
                { "AuthChapLogin", 1 },
                { "AuthChapPassword", 1 },
                { "AuthMChapName", 1 },
                { "AuthMChapSecret", 1 },
                { "AuthChapType", 1 }
            };

        public AdvancedHANodes(List<IAdvancedHANode> nodes) {
            Nodes = nodes;
            foreach (var prop in GetType().GetProperties()) {
                if (prop.CanWrite) {
                    if (prop.Name == "PartnerIP") {
                        continue;
                    }
                    prop.SetValue(this, ConcatValues(prop.Name, nodes));
                }
            }
            EnableJournalStorage = nodes.Exists(node => node.JournalStorage != "");
            PartnerIP = "";
            for (int i = 1; i < Count; i++) {
                if (i > 2 && i < Count - 1) { PartnerIP += ";"; }
                PartnerIP += "#p" + i.ToString() + "=" + String.Join(",", First.PartnerIP[nodes[i].NodeId]);
            }
        }

        private static string ConcatValues(string name, List<IAdvancedHANode> nodes) {
            string value = "";
            int index = 0;
            int from = ConcatFrom[name];
            foreach (IAdvancedHANode node in nodes) {
                if (index < from) { index++; continue; }
                if (index > from + 1 && index < nodes.Count - 1) { value += ";"; }
                value += "#p" + index.ToString() + "=" + node.GetValue(name);
                index++;
            }
            return value;
        }

        public List<T> Transform<T>(Converter<IAdvancedHANode, T> convert) {
            return Nodes.ConvertAll(convert);
        }
    }
}
