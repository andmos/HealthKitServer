using System;
using HealthKitServer.Server;

namespace HealthKitServer.Host
{
	public class CompositionRoot
	{
		public CompositionRoot ()
		{
			var container = Container.Instance = new SimpleContainer ();
			container.RegisterSingleton<IHealthInfoDataStorage>(new HealthInfoDataCache());
		}
	}
}

