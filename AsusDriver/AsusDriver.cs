using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadLedFrameworkSDK;
using AuraServiceLib;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace AsusDriver
{
    public class AsusDriver : ISimpleLEDDriver
    {
        private IAuraSdk2 _sdk;

        public void Configure(DriverDetails driverDetails)
        {
            _sdk = (IAuraSdk2)new AuraSdk();
            _sdk.SwitchMode();
        }

        public List<ControlDevice> GetDevices()
        {
            List<ControlDevice> devices = new List<ControlDevice>();
            foreach (IAuraSyncDevice device in _sdk.Enumerate(0))
            {
                try
                {
                    ControlDevice ctrlDevice = new ControlDevice
                    {
                        Driver = this,
                        Name = device.Name,
                        DeviceType = null,
                        ProductImage = GetImage(null)
                    };

                    List<ControlDevice.LedUnit> leds = new List<ControlDevice.LedUnit>();

                    int ledIndex = 0;

                    foreach (IAuraRgbLight light in device.Lights)
                    {
                        leds.Add(new ControlDevice.LedUnit()
                        {
                            Data = new ControlDevice.LEDData
                            {
                                LEDNumber = ledIndex,
                            },
                            LEDName = light.Name
                        });
                        ledIndex++;
                    }

                    ctrlDevice.LEDs = leds.ToArray();

                    switch (device.Type)
                    {
                        case 0x00010000: //Motherboard
                            ctrlDevice.DeviceType = DeviceTypes.MotherBoard;
                            ctrlDevice.ProductImage = GetImage("Motherboard");
                            ctrlDevice.Name = "Motherboard";
                            break;

                        case 0x00011000: //Motherboard LED Strip
                            ctrlDevice.DeviceType = DeviceTypes.LedStrip;
                            ctrlDevice.ProductImage = GetImage("AddressableHeader");
                            break;

                        case 0x00020000: //VGA
                            ctrlDevice.DeviceType = DeviceTypes.GPU;
                            ctrlDevice.ProductImage = GetImage("GPU");
                            break;
                    }
                    devices.Add(ctrlDevice);
                }
                catch
                {

                }

            }



            return devices;
        }

        public void Dispose()
        {
            _sdk?.ReleaseControl(0);
            _sdk = null;
        }

        public T GetConfig<T>() where T : SLSConfigData
        {
            //TODO throw new NotImplementedException();
            return null;
        }

        public DriverProperties GetProperties()
        {
            return new DriverProperties
            {
                SupportsPull = false,
                SupportsPush = true,
                IsSource = false,
                Id = Guid.Parse("bcc35bad-d1ee-4303-a74f-5fa2d381e0af"),
                SupportsCustomConfig = false
            };
        }

        public string Name()
        {
            return "Asus";
        }

        public void Pull(ControlDevice controlDevice)
        {
            throw new NotImplementedException();
        }

        public void Push(ControlDevice controlDevice)
        {
            throw new NotImplementedException();
        }

        public void PutConfig<T>(T config) where T : SLSConfigData
        {
            //throw new NotImplementedException();
        }

        public Bitmap GetImage(string image)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();

            try
            {
                Stream imageStream = myAssembly.GetManifestResourceStream("AsusDriver.ProductImages." + image + ".png");
                return (Bitmap)Image.FromStream(imageStream);
            }
            catch
            {
                Stream placeholder = myAssembly.GetManifestResourceStream("AsusDriver.AsusPlaceholder.png");
                return (Bitmap)Image.FromStream(placeholder);
            }
        }
    }
}