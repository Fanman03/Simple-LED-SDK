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

        public IAuraSyncDeviceCollection _collection;

        public void Configure(DriverDetails driverDetails)
        {
            _sdk = (IAuraSdk2)new AuraSdk();
            _sdk.SwitchMode();
            _collection = _sdk.Enumerate(0);
        }

        public List<ControlDevice> GetDevices()
        {
            List<ControlDevice> devices = new List<ControlDevice>();
            foreach (IAuraSyncDevice device in _collection)
            {
                try
                {
                    AsusControlDevice ctrlDevice = new AsusControlDevice
                    {
                        Driver = this,
                        Name = device.Name,
                        InternalName = device.Name,
                        DeviceType = null,
                        ProductImage = GetImage(null)
                    };

                    IEnumerable<IAuraSyncDevice> e = _collection.Cast<IAuraSyncDevice>();
                    var query = e.Where(x => x.Name == ctrlDevice.InternalName);
                    ctrlDevice.device = query.First();

                    List<ControlDevice.LedUnit> leds = new List<ControlDevice.LedUnit>();

                    int ledIndex = 0;

                    foreach (IAuraRgbLight light in device.Lights)
                    {
                        leds.Add(new ControlDevice.LedUnit()
                        {
                            Data = new AsusLedData
                            {
                                LEDNumber = ledIndex,
                                AsusLedName = light.Name
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

                        //For now, skip other device types.
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

        DateTime lastRun = DateTime.MinValue;
        public void Push(ControlDevice controlDevice)
        {
            AsusControlDevice asusDevice = (AsusControlDevice)controlDevice;

            if ((DateTime.Now - lastRun).TotalMilliseconds > 200)
            {
                int inc = 0;
                foreach (var led in controlDevice.LEDs)
                {
                    asusDevice.device.Lights[inc].Red = (byte)led.Color.Red;
                    asusDevice.device.Lights[inc].Green = (byte)led.Color.Green;
                    asusDevice.device.Lights[inc].Blue = (byte)led.Color.Blue;
                    inc++;
                }
                Task.Run(() => { asusDevice.device.Apply(); });
                
            }
        }

        public void PutConfig<T>(T config) where T : SLSConfigData
        {
            //throw new NotImplementedException();
        }
        public class AsusLedData : ControlDevice.LEDData
        {
            public string AsusLedName { get; set; }
        }

        public class AsusControlDevice : ControlDevice
        {
            public string InternalName { get; set; }

            public IAuraSyncDevice device { get; set; }
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