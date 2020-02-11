namespace StarWindXExtLib
{

    public interface ICacheParam
    {
        CacheMode CacheMode { get; set; }
        int SizeInMB { get; set; }
        int CacheBlockExpiryPeriodMS { get; set; }
        bool CacheEnabled { get; }
        bool EnableBlockExpiry { get; set; }
        bool EnableNoneSize { get; set; }
        void CopyFrom(ICacheParam other);
    }
}