using System;
using LightInject.Nancy;
using Nancy.Diagnostics;
using System.Configuration;
using LightInject;

namespace HealthKitServer.Host
{
	public class BootStrapper : LightInjectNancyBootstrapper
	{
		protected override DiagnosticsConfiguration DiagnosticsConfiguration 
		{
			get 
			{
				return new DiagnosticsConfiguration { Password = ConfigurationManager.AppSettings ["DiagnosticsPassword"] };
			}
		}
		protected override IServiceContainer GetServiceContainer()
		{
			var compositionRoot = new CompositionRoot ();  
			var container = new ServiceContainer();

			compositionRoot.Compose(container);

			return container; 
		}

	}
}

