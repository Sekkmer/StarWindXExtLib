using System;
using System.Collections.Generic;

namespace StarWindXExtLib
{
    public class AdvancedHANode : IAdvancedHANode
    {
        private NodeType nodeType = NodeType.Synchronous;

        public AdvancedHANode(int id)
        {
            NodeId = id;
        }

        public string TaskValue => Task.Value;
        public int NodeId { get; }

        public bool IsAutoSynchEnabled { get; set; } = true;
        public bool IsALUAOptimized { get; set; } = true;
        public int SynchSessionsCount { get; set; } = 1;
        public int Offset { get; set; } = 0;
        public FailoverConfType FailoverConfType { get; set; } = FailoverConfType.Heartbeat;
        public int Priority { get; set; }

        public NodeType NodeType
        {
            get => nodeType;
            set
            {
                nodeType = value;
                Task.NodeType = value;
            }
        }

        public string TargetName { get; set; }
        public string AuthChapLogin { get; set; } = "0b";
        public string AuthChapPassword { get; set; } = "0b";
        public string AuthMChapName { get; set; } = "0b";
        public string AuthMChapSecret { get; set; } = "0b";
        public string AuthChapType { get; set; } = "none";
        public ICacheParam Cache { get; } = new CacheParam();

        public string JournalStorage { get; set; } = "";

        public IHATask Task { get; } = new HATask();

        public Dictionary<int, List<string>> PartnerIP { get; } = new Dictionary<int, List<string>>();

        public string GetValue(string name)
        {
            var obj = GetType().GetProperty(name).GetValue(this);
            if (obj is string str)
                return str;
            if (obj is NodeType type)
                return EnumFormat.EnumToString(type);
            try
            {
                return obj.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(name + "\n\n" + e.Message);
                throw;
            }
        }
    }
}