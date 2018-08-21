namespace StarWindXExtLib {

    public interface IServerControl : IAppender {
        string SendTo { get; }
        string Request { get; }
    }
}