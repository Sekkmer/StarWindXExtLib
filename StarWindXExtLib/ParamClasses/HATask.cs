using System;

namespace StarWindXExtLib {
    public class HATask : IHATask {
        public string Value =>
            NodeTypeInt.ToString() + ":" +
           ((DateTimeOffset)Time).ToUnixTimeSeconds().ToString() + ":" +
            Unknown1.ToString() + ":" +
            RepeatInterval.ToString() + ":" +
            Unknown2.ToString()
            + (Preserve == 0 ? "" : ":" + Preserve.ToString());

        public NodeType NodeType { get; set; } = NodeType.Synchronous;

        public int NodeTypeInt {
            get {
                switch (NodeType) {
                case NodeType.Synchronous:
                    return 5;
                case NodeType.Asynchronous:
                    return 4;
                case NodeType.Witness:
                    return 0;
                }
                return 0;
            }
        }
        public DateTime Time { get; set; }
        public int Unknown1 { get; set; }
        public int RepeatInterval { get; set; }
        public int Unknown2 { get; set; }
        public int Preserve { get; set; }
        public bool Enabled { get; set; } = false;
    }
}
