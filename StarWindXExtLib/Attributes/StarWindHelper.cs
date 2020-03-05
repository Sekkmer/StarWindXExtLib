using StarWindXExtLib;

namespace StarWindXView
{
    internal class StarWindHelper
    {
        internal static string GetServerName(IStarWindServerExt server)
        {
            return server.IP + ":" + server.Port;
        }
    }
}