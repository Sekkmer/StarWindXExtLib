using System.Collections.Generic;

namespace StarWindXExtLib
{

    public interface IControlInterface
    {
        string HostName { get; }
        List<string> NetInterfaces { get; }
        int Port { get; }
    }
}