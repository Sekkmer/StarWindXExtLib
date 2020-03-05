namespace StarWindXExtLib
{
    public class ServerCommand : ParameterAppender, IServerCommand
    {
        public ServerCommand(string command)
        {
            Command = command;
        }

        public string Command { get; }
    }
}