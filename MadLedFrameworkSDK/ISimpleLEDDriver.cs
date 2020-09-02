﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MadLedFrameworkSDK
{
    public interface ISimpleLEDDriver : IDisposable
    {
        void Configure(DriverDetails driverDetails);
        List<ControlDevice> GetDevices();
        void Push(ControlDevice controlDevice);
        void Pull(ControlDevice controlDevice);
        DriverProperties GetProperties();

        

        string Name();
    }

    public interface ISimpleLEDDriverWithConfig : ISimpleLEDDriver
    {
        UserControl GetCustomConfig();
    }

    public class DriverProperties
    {
        public bool SupportsPull { get; set; }
        public bool SupportsPush { get; set; }
        public bool IsSource { get; set; }
        public bool SupportsCustomConfig { get; set; }
    }
    
    public  class DriverDetails
    {
        
    public string Name { get; set; }
        public virtual string Platform { get; } = "Unknown";
    }

    public class MadLedDriverDetails: DriverDetails
    {
        public override string Platform { get; } = "MadLed";
        public string ComPort { get; set; }
    }
}
