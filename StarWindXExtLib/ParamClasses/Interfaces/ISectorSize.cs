namespace StarWindXExtLib
{
    public interface ISectorSize
    {
        /// <summary>
        ///     Default 512
        /// </summary>
        int LogicalSectorSize { get; set; }

        /// <summary>
        ///     Default 4096
        /// </summary>
        int PhysicalSectorSize { get; set; }
    }
}