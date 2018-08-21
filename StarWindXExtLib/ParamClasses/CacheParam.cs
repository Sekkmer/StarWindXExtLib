namespace StarWindXExtLib {

    public class CacheParam : ICacheParam {

        [Param("CacheType")]
        public CacheType CacheType { get; set; }

        [Param(false, "CacheSize")]
        public int SizeInMB { get; set; }

        [Param(false)]
        [IntZero("")]
        public int CacheBlockExpiryPeriodMS { get; set; }

        [EnableParam("CacheSize")]
        public bool CacheEnabled => CacheType != CacheType.None;

        [EnableParam("CacheBlockExpiryPeriodMS")]
        public bool EnableBlockExpiry { get; set; } = false;

        public void CopyFrom(ICacheParam other) {
            CacheType = other.CacheType;
            SizeInMB = other.SizeInMB;
            CacheBlockExpiryPeriodMS = other.CacheBlockExpiryPeriodMS;
            EnableBlockExpiry = other.EnableBlockExpiry;
        }
    }
}