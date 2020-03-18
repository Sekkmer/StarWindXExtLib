using StarWindXLib;
using System;

namespace StarWindXExtLib
{
    public class HATask : IHATask
    {
        public string Value
        {
            get
            {
                var utc = (DateTimeOffset) DateTime.SpecifyKind(Time, DateTimeKind.Utc);
                try
                {
                    return
                        NodeTypeInt + ":" +
                        utc.ToUnixTimeSeconds() + ":" +
                        Unknown1 + ":" +
                        RepeatInterval + ":" +
                        Unknown2 +
                        (Preserve == 0 ? "" : ":" + Preserve);
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }


        public SW_HA_NODE_TYPE NodeType { get; set; } = SW_HA_NODE_TYPE.SW_HA_NODE_TYPE_SYNC;

        public int NodeTypeInt
        {
            get
            {
                // TODO test this
                return NodeType switch
                {
                    SW_HA_NODE_TYPE.SW_HA_NODE_TYPE_SYNC => 5,
                    SW_HA_NODE_TYPE.SW_HA_NODE_TYPE_ASYNC => 4,
                    SW_HA_NODE_TYPE.SW_HA_NODE_TYPE_WITNESS => 0,
                    _ => -1
                };
            }
        }

        public DateTime Time { get; set; }
        public int Unknown1 { get; set; } = 0;
        public int RepeatInterval { get; set; } = 0;
        public int Unknown2 { get; set; } = 0;
        public int Preserve { get; set; } = 0;
        public bool Enabled { get; set; } = false;
    }
}