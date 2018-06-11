﻿using System;

namespace StarWindXExtLib {
    public class AdvancedHADeviceCreator : ParameterAppender, IAdvancedHADeviceCreator {
        public string Name => DeviceName;

        public string DeviceName { get; set; }

        [Param]
        public string OwnTargetName => Image.OwnTargetName;
        [Param("file")]
        public string HeaderFile => Image.DeviceHeaderPath;
        [Param("asyncmode")]
        [BoolToString("yes", "no")]
        public bool Asyncmode { get; set; } = true;
        [Param("highavailability")]
        [BoolToString("yes", "no")]
        public bool Highavailability { get; set; } = true;
        [Param("readonly")]
        [BoolToString("yes", "no")]
        public bool Readonly { get; set; } = false;
        [Param("buffering")]
        [BoolToString("yes", "no")]
        public bool Buffering { get; set; } = false;
        [Param("header")]
        public int Header { get; set; } = 65536;
        [FlatParam]
        public ICacheParam Cache { get; } = new CacheParam() { EnableBlockExpiry = true };
        [Param]
        public string AluaNodeGroupStates {
            get => String.Join(",", image.Nodes.Transform(node => node.IsALUAOptimized ? "0" : "1"));
        }
        [Param]
        public string Storage => Image.Device.Name;

        private IHAImageHeaderCreator image;

        public IHAImageHeaderCreator Image {
            get => image;
            set {
                image = value;
                Cache.CopyFrom(image.Cache);
                Cache.EnableBlockExpiry = true;
                if (Cache.CacheType == CacheType.None) {
                    Cache.CacheBlockExpiryPeriodMS = 0;
                } else if (Cache.CacheBlockExpiryPeriodMS == 0) {
                    Cache.CacheBlockExpiryPeriodMS = 5000;
                }
            }
        }
    }
}
