using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Drawing;
using MadLedFrameworkSDK;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace Gradient
{
	public class DummyControlDevice : ControlDevice
	{

	}
	// Token: 0x02000002 RID: 2
	public class GradientDriver : ISimpleLEDDriverWithConfig
	{

		public static Assembly assembly = Assembly.GetExecutingAssembly();
		//public static Stream imageStream = assembly.GetManifestResourceStream("RainbowWave.rainbowwave.png");

		[JsonIgnore]
		public GradientConfigModel configModel = new GradientConfigModel();


		public bool GetIsDirty()
		{
			return configModel.DataIsDirty;
		}

		public void SetIsDirty(bool val)
		{
			configModel.DataIsDirty = val;
		}

		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public GradientDriver()
		{
				this.leds[0] = new ControlDevice.LedUnit
				{
					LEDName = "LED 1",
					Data = new ControlDevice.LEDData
					{
						LEDNumber = 0,
					}
				};
			this.leds[1] = new ControlDevice.LedUnit
			{
				LEDName = "LED 2",
				Data = new ControlDevice.LEDData
				{
					LEDNumber = 1,
				}
			};
			this.leds[0].Color = new LEDColor(configModel.Color1R, configModel.Color1G, configModel.Color1B);
			this.leds[1].Color = new LEDColor(configModel.Color2R, configModel.Color2G, configModel.Color2B);

			this.timer = new Timer(new TimerCallback(this.TimerCallback), null, 0, 33);
		}

		private void TimerCallback(object state)
		{
			this.leds[0].Color = new LEDColor(configModel.Color1R, configModel.Color1G, configModel.Color1B);
			this.leds[1].Color = new LEDColor(configModel.Color2R, configModel.Color2G, configModel.Color2B);
		}


		// Token: 0x06000003 RID: 3 RVA: 0x0000215E File Offset: 0x0000035E
		public void Configure(DriverDetails driverDetails)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002164 File Offset: 0x00000364
		public List<ControlDevice> GetDevices()
		{
			return new List<ControlDevice>
			{
				new ControlDevice
				{
					Name = "Two Color Gradient",
					Driver = this,
					//ProductImage = (Bitmap) System.Drawing.Image.FromStream(imageStream),
					LEDs = this.leds,
					DeviceType = "Effect"
				}
			};
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021B4 File Offset: 0x000003B4
		public void Push(ControlDevice controlDevice)
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021B7 File Offset: 0x000003B7
		public void Pull(ControlDevice controlDevice)
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021BC File Offset: 0x000003BC
		public DriverProperties GetProperties()
		{
			return new DriverProperties
			{
				SupportsPull = false,
				SupportsPush = false,
				IsSource = true,
				SupportsCustomConfig = true,
				Id = Guid.Parse("08803b6f-ecce-4f11-9981-26a734b0e3a6")
			};
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021EB File Offset: 0x000003EB
		public string Name()
		{
			return "Gradient";
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021F2 File Offset: 0x000003F2
		public void Dispose()
		{

		}

		public UserControl GetCustomConfig(ControlDevice controlDevice)
		{
			var config = new CustomConfig
			{
				DataContext = configModel
			};

			configModel.CurrentControlDevice = controlDevice;

			return config;
		}

		public T GetConfig<T>() where T : SLSConfigData
		{
			GradientConfigModel data = this.configModel;
			SLSConfigData proxy = data;
			return (T)proxy;
		}


		public void PutConfig<T>(T config) where T : SLSConfigData
		{
			GradientConfigModel proxy = config as GradientConfigModel;
		}

		// Token: 0x04000001 RID: 1
		private const int LEDCount = 2;

		private Timer timer;

		// Token: 0x04000003 RID: 3
		private ControlDevice.LedUnit[] leds = new ControlDevice.LedUnit[LEDCount];
	}

	public class ThreeColGradientDriver : ISimpleLEDDriverWithConfig
	{

		public static Assembly assembly = Assembly.GetExecutingAssembly();
		//public static Stream imageStream = assembly.GetManifestResourceStream("RainbowWave.rainbowwave.png");

		[JsonIgnore]
		public ThreeColGradientConfigModel configModel = new ThreeColGradientConfigModel();


		public bool GetIsDirty()
		{
			return configModel.DataIsDirty;
		}

		public void SetIsDirty(bool val)
		{
			configModel.DataIsDirty = val;
		}

		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ThreeColGradientDriver()
		{
			this.leds[0] = new ControlDevice.LedUnit
			{
				LEDName = "LED 1",
				Data = new ControlDevice.LEDData
				{
					LEDNumber = 0,
				}
			};
			this.leds[1] = new ControlDevice.LedUnit
			{
				LEDName = "LED 2",
				Data = new ControlDevice.LEDData
				{
					LEDNumber = 1,
				}
			};
			this.leds[2] = new ControlDevice.LedUnit
			{
				LEDName = "LED 3",
				Data = new ControlDevice.LEDData
				{
					LEDNumber = 2,
				}
			};
			this.leds[0].Color = new LEDColor(configModel.Color1R, configModel.Color1G, configModel.Color1B);
			this.leds[1].Color = new LEDColor(configModel.Color2R, configModel.Color2G, configModel.Color2B);
			this.leds[2].Color = new LEDColor(configModel.Color3R, configModel.Color3G, configModel.Color3B);

			this.timer = new Timer(new TimerCallback(this.TimerCallback), null, 0, 33);
		}

		private void TimerCallback(object state)
		{
			this.leds[0].Color = new LEDColor(configModel.Color1R, configModel.Color1G, configModel.Color1B);
			this.leds[1].Color = new LEDColor(configModel.Color2R, configModel.Color2G, configModel.Color2B);
			this.leds[2].Color = new LEDColor(configModel.Color3R, configModel.Color3G, configModel.Color3B);
		}


		// Token: 0x06000003 RID: 3 RVA: 0x0000215E File Offset: 0x0000035E
		public void Configure(DriverDetails driverDetails)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002164 File Offset: 0x00000364
		public List<ControlDevice> GetDevices()
		{
			return new List<ControlDevice>
			{
				new ControlDevice
				{
					Name = "Three Color Gradient",
					Driver = this,
					//ProductImage = (Bitmap) System.Drawing.Image.FromStream(imageStream),
					LEDs = this.leds,
					DeviceType = "Effect"
				}
			};
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021B4 File Offset: 0x000003B4
		public void Push(ControlDevice controlDevice)
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021B7 File Offset: 0x000003B7
		public void Pull(ControlDevice controlDevice)
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021BC File Offset: 0x000003BC
		public DriverProperties GetProperties()
		{
			return new DriverProperties
			{
				SupportsPull = false,
				SupportsPush = false,
				IsSource = true,
				SupportsCustomConfig = true,
				Id = Guid.Parse("de9b0292-051e-4e5a-8ac1-8bbb6741e90e")
			};
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021EB File Offset: 0x000003EB
		public string Name()
		{
			return "Gradient";
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021F2 File Offset: 0x000003F2
		public void Dispose()
		{

		}

		public UserControl GetCustomConfig(ControlDevice controlDevice)
		{
			var config = new ThreeColConfig
			{
				DataContext = configModel
			};

			configModel.CurrentControlDevice = controlDevice;

			return config;
		}

		public T GetConfig<T>() where T : SLSConfigData
		{
			ThreeColGradientConfigModel data = this.configModel;
			SLSConfigData proxy = data;
			return (T)proxy;
		}


		public void PutConfig<T>(T config) where T : SLSConfigData
		{
			ThreeColGradientConfigModel proxy = config as ThreeColGradientConfigModel;
		}

		// Token: 0x04000001 RID: 1
		private const int LEDCount = 3;

		private Timer timer;

		// Token: 0x04000003 RID: 3
		private ControlDevice.LedUnit[] leds = new ControlDevice.LedUnit[LEDCount];
	}

	public class SolidColorDriver : ISimpleLEDDriverWithConfig
	{

		public static Assembly assembly = Assembly.GetExecutingAssembly();
		//public static Stream imageStream = assembly.GetManifestResourceStream("RainbowWave.rainbowwave.png");

		[JsonIgnore]
		public SolidColorConfigModel configModel = new SolidColorConfigModel();


		public bool GetIsDirty()
		{
			return configModel.DataIsDirty;
		}

		public void SetIsDirty(bool val)
		{
			configModel.DataIsDirty = val;
		}

		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public SolidColorDriver()
		{
			this.leds[0] = new ControlDevice.LedUnit
			{
				LEDName = "LED 1",
				Data = new ControlDevice.LEDData
				{
					LEDNumber = 0,
				}
			};
			this.leds[0].Color = new LEDColor(configModel.Color1R, configModel.Color1G, configModel.Color1B);
			this.timer = new Timer(new TimerCallback(this.TimerCallback), null, 0, 33);
		}

		private void TimerCallback(object state)
		{
			this.leds[0].Color = new LEDColor(configModel.Color1R, configModel.Color1G, configModel.Color1B);
		}


		// Token: 0x06000003 RID: 3 RVA: 0x0000215E File Offset: 0x0000035E
		public void Configure(DriverDetails driverDetails)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002164 File Offset: 0x00000364
		public List<ControlDevice> GetDevices()
		{
			return new List<ControlDevice>
			{
				new ControlDevice
				{
					Name = "Solid Color",
					Driver = this,
					//ProductImage = (Bitmap) System.Drawing.Image.FromStream(imageStream),
					LEDs = this.leds,
					DeviceType = "Effect"
				}
			};
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021B4 File Offset: 0x000003B4
		public void Push(ControlDevice controlDevice)
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021B7 File Offset: 0x000003B7
		public void Pull(ControlDevice controlDevice)
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021BC File Offset: 0x000003BC
		public DriverProperties GetProperties()
		{
			return new DriverProperties
			{
				SupportsPull = false,
				SupportsPush = false,
				IsSource = true,
				SupportsCustomConfig = true,
				Id = Guid.Parse("504f988c-a36c-420a-a83c-105665be18ff")
			};
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021EB File Offset: 0x000003EB
		public string Name()
		{
			return "Solid Color";
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021F2 File Offset: 0x000003F2
		public void Dispose()
		{

		}

		public UserControl GetCustomConfig(ControlDevice controlDevice)
		{
			var config = new SolidColorConfig
			{
				DataContext = configModel
			};

			configModel.CurrentControlDevice = controlDevice;

			return config;
		}

		public T GetConfig<T>() where T : SLSConfigData
		{
			SolidColorConfigModel data = this.configModel;
			SLSConfigData proxy = data;
			return (T)proxy;
		}


		public void PutConfig<T>(T config) where T : SLSConfigData
		{
			SolidColorConfigModel proxy = config as SolidColorConfigModel;
		}

		// Token: 0x04000001 RID: 1
		private const int LEDCount = 1;

		private Timer timer;

		// Token: 0x04000003 RID: 3
		private ControlDevice.LedUnit[] leds = new ControlDevice.LedUnit[LEDCount];
	}
}
