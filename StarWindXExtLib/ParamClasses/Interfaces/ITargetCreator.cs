using StarWindXLib;

using System;
using System.Collections.Generic;

namespace StarWindXExtLib {

    public interface ITargetCreator : IAppender {
        string Alias { get; set; }
        string Name { get; set; }
        bool Clustered { get; set; }
        bool VVols { get; set; }
        List<IDevice> DevicesToAttach { get; }
        Func<string> GetServerName { get; set; }
    }
}