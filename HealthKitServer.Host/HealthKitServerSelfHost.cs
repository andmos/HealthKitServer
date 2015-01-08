using System;
using System.Diagnostics;
using Nancy.Hosting.Self;

namespace HealthKitServer.Host
{
	public class HealthKitServerSelfHost
	{

		private NancyHost m_nancyHost;


			public void Start()
			{
			var compositionRoot = new CompositionRoot (); 
				m_nancyHost = new NancyHost(new System.Uri("http://localhost:5000"));
				m_nancyHost.Start();

			}
			
			public void Stop()
			{
				m_nancyHost.Stop();
				Console.WriteLine("Stopped. Good bye!");
			}
	}
}

