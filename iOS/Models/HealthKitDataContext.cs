using System;

namespace HealthKitServer
{
	public class HealthKitDataContext
	{
		//This is SO cheating.
		public static HealthKitData ActiveHealthKitData { get; set; }
	}
}

