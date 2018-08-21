namespace StarWindXExtLib {

    public class ServerCommand : ParameterAppender, IServerCommand {
        public string Command { get; }

        public ServerCommand(string command) {
            Command = command;
        }
    }
}