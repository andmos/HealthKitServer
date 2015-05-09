using System;

namespace HealthKitServer
{
	public interface IDevice
	{
		string Device { get; }
		string PlatformVersion { get; }
	}
}

