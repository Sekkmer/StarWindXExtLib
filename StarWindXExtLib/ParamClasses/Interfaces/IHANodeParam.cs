using StarWindXLib;

namespace StarWindXExtLib
{
    public interface IHANodeParam : ICache
    {
        string HostName { get; set; }
        int PortNumber { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        int Size { get; set; }
        string Path { get; set; }
        string BaseName { get; set; }
        STARWIND_FILE_TYPE StorageType { get; set; }
        string BlockSize { get; set; }
        string TargetName { get; set; }
        string TargetAlias { get; set; }
        int SectorSize { get; set; }
        bool AutoSynchEnabled { get; set; }
        bool ALUAOptimized { get; set; }
        string SyncInterface { get; set; }
        string HBInterface { get; set; }
        int SynchSessionsCount { get; set; }
        string ChapType { get; set; }
        string ChapLogin { get; set; }
        string ChapPassword { get; set; }
        string MChapName { get; set; }
        string MChapSecret { get; set; }
        string PoolName { get; set; }

        void SetServer(IStarWindServerExt server);
    }
}