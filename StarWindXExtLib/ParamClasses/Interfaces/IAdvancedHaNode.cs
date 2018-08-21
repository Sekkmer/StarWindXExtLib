using System.Collections.Generic;

namespace StarWindXExtLib {

    public interface IAdvancedHANode : ICache {
        int NodeId { get; }
        bool IsAutoSynchEnabled { get; set; }
        bool IsALUAOptimized { get; set; }
        int SynchSessionsCount { get; set; }
        int Offset { get; set; }
        FailoverConfType FailoverConfType { get; set; }
        int Priority { get; set; }
        NodeType NodeType { get; set; }
        string TargetName { get; set; }
        string AuthChapLogin { get; set; }
        string AuthChapPassword { get; set; }
        string AuthMChapName { get; set; }
        string AuthMChapSecret { get; set; }
        string AuthChapType { get; set; }
        string JournalStorage { get; set; }
        IHATask Task { get; }
        Dictionary<int, List<string>> PartnerIP { get; }

        string GetValue(string name);
    }
}