namespace StarWindXExtLib {

    public abstract class ServerControl : ParameterAppender, IServerControl {

        [Param(false, "sendTo")]
        public abstract string SendTo { get; }

        [ParamValueAsName(false)]
        public abstract string Request { get; }

        public abstract string RequestValue { get; protected set; }

        [EnableParam("sendTo", "Request")]
        public bool AddSendToAndRequest { get; set; } = true;
    }
}