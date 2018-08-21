namespace StarWindXExtLib {

    public interface IDDFileCreator : IFileCreator {
        int BlockSize { get; set; }
        bool DeferredCreation { get; set; }
    }
}