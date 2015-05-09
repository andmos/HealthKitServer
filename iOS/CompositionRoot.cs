using System;

namespace HealthKitServer.iOS
{
	public class CompositionRoot
	{
		public CompositionRoot ()
		{
			var container = Container.Instance = new SimpleContainer (); 
			container.RegisterSingleton<IHealthKitAccess> (new HealtKitAccess());
			container.Register<IHealthKitDataWebService, HealthKitDataWebService> ();
			container.Register<IDevice, iOSDevice> (); 
			HealthKitDataContext.ActiveHealthKitData = new HealthKitData{PersonId=3, DistanceReadings = new DistanceReading{}, Device = container.Resolve<IDevice>().Device};
		}
	}
}

