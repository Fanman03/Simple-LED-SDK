using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadLedFrameworkSDK;
using Newtonsoft.Json;

namespace Gradient
{
    public class ThreeColGradientConfigModel : SLSConfigData
    {
        private int color1R = 255;
        public int Color1R
        {
            get => color1R;
            set
            {
                SetProperty(ref color1R, value);
                DataIsDirty = true;
            }
        }

        private int color1G = 0;
        public int Color1G
        {
            get => color1G;
            set
            {
                SetProperty(ref color1G, value);
                DataIsDirty = true;
            }
        }

        private int color1B = 0;
        public int Color1B
        {
            get => color1B;
            set
            {
                SetProperty(ref color1B, value);
                DataIsDirty = true;
            }
        }

        private int color2R = 0;
        public int Color2R
        {
            get => color2R;
            set
            {
                SetProperty(ref color2R, value);
                DataIsDirty = true;
            }
        }

        private int color2G = 255;
        public int Color2G
        {
            get => color2G;
            set
            {
                SetProperty(ref color2G, value);
                DataIsDirty = true;
            }
        }

        private int color2B = 255;
        public int Color2B
        {
            get => color2B;
            set
            {
                SetProperty(ref color2B, value);
                DataIsDirty = true;
            }
        }

            private int color3R = 0;
        public int Color3R
        {
            get => color3R;
            set
            {
                SetProperty(ref color3R, value);
                DataIsDirty = true;
            }
        }

        private int color3G = 0;
        public int Color3G
        {
            get => color3G;
            set
            {
                SetProperty(ref color3G, value);
                DataIsDirty = true;
            }
        }

        private int color3B = 255;
        public int Color3B
        {
            get => color3B;
            set
            {
                SetProperty(ref color3B, value);
                DataIsDirty = true;
            }
        }


        private ControlDevice controlDevice;

        [JsonIgnore]
        public ControlDevice CurrentControlDevice
        {
            get => controlDevice;
            set => SetProperty(ref controlDevice, value);
        }
    }
}
