namespace StarWindXExtLib
{

    public interface ISectorSize
    {
        int LogicalSectorSize { get; set; }
        int PhysicalSectorSize { get; set; }
    }
}