using System;
using HealthKitServer.Server;
using System.Configuration;

namespace HealthKitServer.Host
{
	public class CompositionRoot
	{
		public CompositionRoot ()
		{

			var container = Container.Instance = new SimpleContainer ();

			var dataStorage = ConfigurationManager.AppSettings["DataStorage"];
			if(dataStorage.ToLower().Equals("cache"))
			{
				container.RegisterSingleton<IHealthInfoDataStorage>(new HealthInfoDataCache());
			}
		}
	}
}

