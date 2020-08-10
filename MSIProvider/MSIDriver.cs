﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadLedFrameworkSDK;

namespace MSIProvider
{
    public class MSIDriver : ISimpleLEDDriver
    {
        public MSIDriver()
        {
            _MsiSDK.Reload();

            var errorCode = _MsiSDK.Initialize();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Configure(DriverDetails driverDetails)
        {
            throw new NotImplementedException();
        }

        public List<ControlDevice> GetDevices()
        {
            var tmp = _MsiSDK.GetDeviceInfo(out string[] deviceTypes, out int[] ledCounts);

            List < ControlDevice > returnValue = new List<ControlDevice>();
            for (int i = 0; i < deviceTypes.Length; i++)
            {
                try
                {
                    string deviceType = deviceTypes[i];
                    int ledCount = ledCounts[i];

                    //Hex3l: MSI_MB provide access to the motherboard "leds" where a led must be intended as a led header (JRGB, JRAINBOW etc..) (Tested on MSI X570 Unify)
                    if (deviceType.Equals("MSI_MB"))
                    {
                        var mbdeivce = new MSIControlDevice
                        {
                            Driver = this,
                            Name = "MSI Motherboard",
                            LEDs = new ControlDevice.LedUnit[ledCount],
                            MSIDeviceType = deviceType
                        };

                        for (int l = 0; l < ledCount; l++)
                        {
                            mbdeivce.LEDs[l] = new ControlDevice.LedUnit
                            {
                                Data = l,
                                Color = new ControlDevice.LEDColor(0, 0, 0),
                                LEDName = "Motherboard LED " + l
                            };
                        }

                        returnValue.Add(mbdeivce);
                    }
                    else if (deviceType.Equals("MSI_VGA"))
                    {

                        var gpuDevice = new MSIControlDevice
                        {
                            Driver = this,
                            Name = "MSI GPU",
                            LEDs = new ControlDevice.LedUnit[ledCount],
                            MSIDeviceType = deviceType
                        };

                        for (int l = 0; l < ledCount; l++)
                        {
                            gpuDevice.LEDs[l] = new ControlDevice.LedUnit
                            {
                                Data = l,
                                Color = new ControlDevice.LEDColor(0, 0, 0),
                                LEDName = "GPU LED " + l
                            };
                        }

                        returnValue.Add(gpuDevice);
                    }

                    //TODO DarthAffe 22.02.2020: Add other devices
                }
                catch
                {

                }
            }

            return returnValue;
        }

        public void Push(ControlDevice controlDevice)
        {
            MSIControlDevice msiDevice = controlDevice as MSIControlDevice;
            
            for (int i = 0; i < controlDevice.LEDs.Length; i++)
            {
                _MsiSDK.SetLedColor(msiDevice.MSIDeviceType, i, controlDevice.LEDs[i].Color.Red,
                    controlDevice.LEDs[i].Color.Green, controlDevice.LEDs[i].Color.Blue);
            }
        }

        public void Pull(ControlDevice controlDevice)
        {
            throw new NotImplementedException();
        }

        public DriverProperties GetProperties()
        {
            return new DriverProperties
            {
                SupportsPull = false
            };
        }
    }
}