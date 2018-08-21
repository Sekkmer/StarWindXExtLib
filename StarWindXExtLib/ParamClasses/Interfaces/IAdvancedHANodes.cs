using System;
using System.Collections.Generic;

namespace StarWindXExtLib {

    public interface IAdvancedHANodes : IAppender {
        string Priority { get; }
        string NodeType { get; }
        string JournalStorage { get; }
        string TargetName { get; }
        string PartnerIP { get; }
        string AuthChapLogin { get; }
        string AuthChapPassword { get; }
        string AuthMChapName { get; }
        string AuthMChapSecret { get; }
        string AuthChapType { get; }

        bool EnableJournalStorage { get; }

        IAdvancedHANode First { get; }

        int Count { get; }

        List<T> Transform<T>(Converter<IAdvancedHANode, T> convert);
    }
}