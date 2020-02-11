namespace StarWindXExtLib
{

    public class CacheParam : ICacheParam
    {

        [Param("CacheMode")]
        public CacheMode CacheMode { get; set; }

        [Param(false, "CacheSize"), IntZero("")]
        public int SizeInMB { get; set; }

        [Param(false), IntZero("")]
        public int CacheBlockExpiryPeriodMS { get; set; }

        [EnableParam("CacheSize")]
        public bool CacheEnabled => CacheMode != CacheMode.None || EnableNoneSize;

        public bool EnableNoneSize { get; set; } = false;

        [EnableParam("CacheBlockExpiryPeriodMS")]
        public bool EnableBlockExpiry { get; set; } = false;

        public void CopyFrom(ICacheParam other)
        {
            CacheMode = other.CacheMode;
            SizeInMB = other.SizeInMB;
            CacheBlockExpiryPeriodMS = other.CacheBlockExpiryPeriodMS;
            EnableBlockExpiry = other.EnableBlockExpiry;
        }
    }
}