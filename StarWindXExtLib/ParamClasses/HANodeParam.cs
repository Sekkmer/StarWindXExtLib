using StarWindXLib;

namespace StarWindXExtLib {
    public class HANodeParam : IHANodeParam {
        [Param] public string HostName { get; set; }
        [Param] public int PortNumber { get; set; } = 3261;
        [Param] public string UserName { get; set; } = "root";
        [Param] public string Password { get; set; } = "starwind";
        [Param] public int Size { get; set; }
        [Param] public string Path { get; set; }
        [Param] public string BaseName { get; set; }
        [Param] public int Type => (int)StorageType;

        public STARWIND_FILE_TYPE StorageType { get; set; }

        [Param] public string BlockSize { get; set; }
        [Param] public string TargetName { get; set; } = "";
        [Param] public string TargetAlias { get; set; }
        [Param("sectorSize")] public int SectorSize { get; set; } = 512;
        [Param] [BoolToString("0", "1")] public bool AutoSynchEnabled { get; set; } = true;
        [Param] [BoolToString("1", "0")] public bool ALUAOptimized { get; set; } = true;
        [Param] public string SyncInterface { get; set; }
        [Param] public string HBInterface { get; set; }
        [Param] public int SynchSessionsCount { get; set; } = 1;
        [Param] public string ChapType { get; set; } = "";
        [Param] public string ChapLogin { get; set; } = "";
        [Param] public string ChapPassword { get; set; } = "";
        [Param] public string MChapName { get; set; } = "";
        [Param] public string MChapSecret { get; set; } = "";
        [Param] public string PoolName { get; set; } = "Default";
        [FlatParam] public ICacheParam Cache { get; } = new CacheParam();

        public void SetServer(IStarWindServer server) {
            HostName = server.IP;
            PortNumber = server.Port;
            UserName = server.AuthentificationInfo.Login;
            Password = server.AuthentificationInfo.Password;
        }
    }
}
