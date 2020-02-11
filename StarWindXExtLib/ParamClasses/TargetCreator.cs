using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace StarWindXExtLib
{

    public class TargetCreator : ParameterAppender, ITargetCreator
    {
        public string DirectCommand => "addtarget " + Name;

        public static string TargetNamePrefix { get; set; } = "iqn.2008-08.com.starwindsoftware";

        public Func<string> GetServerName { get; set; }

        [Param(false, "alias")]
        public string Alias { get; set; }

        private string name;
        private bool AutoName => string.IsNullOrEmpty(name);

        public string Name {
            get {
                if (AutoName && (OvverrideAutoNaming || AsServerCommand)) {
                    return TargetNamePrefix + ":" + (GetServerName?.Invoke() ?? "") + "-" + Alias;
                }
                return name;
            }
            set => name = value;
        }

        [Param("clustered")]
        [BoolToString("Yes", "No")]
        public bool Clustered { get; set; } = true;

        [Param("targetType")]
        [BoolToString("vvols", "")]
        [EnableParam("targetType")]
        public bool VVols { get; set; } = false;

        [Param(false, "devices")]
        public string Devices => string.Join(",", DevicesToAttach.ConvertAll(device => device.Name));

        [EnableParam("devices")]
        public bool EnableDevices => DevicesToAttach.Count > 0;

        public List<IDeviceExt> DevicesToAttach { get; } = new List<IDeviceExt>();

        [Description("For bypassing IStarWindServer.CreateTarget() function")]
        [EnableParam("alias")]
        public bool AsServerCommand { get; set; } = false;

        public bool OvverrideAutoNaming { get; set; } = false;
    }
}