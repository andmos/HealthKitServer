using System;
using MonoTouch.UIKit;

namespace HealthKitServer.iOS
{
	public class iOSDevice : IDevice
	{
		public iOSDevice ()
		{
		}



		public string Device {
			get 
			{
				return UIDevice.CurrentDevice.Model;
			}
		
		}

		public string PlatformVersion {
			get 
			{
				return UIDevice.CurrentDevice.SystemName;
			}
		}
			
	}
}

