namespace StarWindXExtLib
{
    public abstract class ServerControl : ParameterAppender, IServerControl
    {
        public abstract string RequestValue { get; protected set; }

        [EnableParam("sendTo", "Request")] public bool AddSendToAndRequest { get; set; } = true;

        [Param(false, "sendTo")] public abstract string SendTo { get; }

        [ParamValueAsName(false)] public abstract string Request { get; }
    }
}