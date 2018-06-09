namespace StarWindXExtLib {
    public interface ICacheParam {
        CacheType CacheType { get; set; }
        int SizeInMB { get; set; }
        int CacheBlockExpiryPeriodMS { get; set; }
        bool CacheEnabled { get; }
        bool EnableBlockExpiry { get; set; }
        void CopyFrom(ICacheParam other);
    }
}
