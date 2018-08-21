namespace StarWindXExtLib {

    public interface IServerCommand : IAppender {
        string Command { get; }
    }
}