using System;
using LightInject.Nancy;
using HealthKitServer.Server;
using LightInject;

namespace TestHealthKitServer.Server
{
	public class TestableLightInjectBootstrapper : LightInjectNancyBootstrapper
	{
		IHealthKitDataStorage m_dataStorage;


		public TestableLightInjectBootstrapper(IHealthKitDataStorage dataStorage = null)
		{
			if (dataStorage != null) 
			{
				m_dataStorage = dataStorage; 
			} else 
			{
				m_dataStorage = new HealthKitDataCache ();
			}
		}

		protected override void ConfigureApplicationContainer (IServiceContainer existingContainer)
		{
			existingContainer.Register<ILogFactory, TestableLogFactory>(new PerContainerLifetime());
			existingContainer.RegisterFrom <ServerCompositionRoot>();
			existingContainer.RegisterInstance<IHealthKitDataStorage>(m_dataStorage);
		}
	}
}

