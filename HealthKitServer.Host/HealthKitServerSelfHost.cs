using System;
using System.Diagnostics;
using Nancy.Hosting.Self;
using System.Configuration;

namespace HealthKitServer.Host
{
	public class HealthKitServerSelfHost
	{

		private NancyHost m_nancyHost;

		private string m_listeningAddress;

		public void Start()
		{
			var compositionRoot = new CompositionRoot (); 
			m_listeningAddress = string.Format ("http://localhost:{0}", ConfigurationManager.AppSettings ["HttpPort"]);

			m_nancyHost = new NancyHost(new System.Uri(m_listeningAddress));
			m_nancyHost.Start();

			Console.WriteLine (string.Format ("HealthKitServer listening on {0}", m_listeningAddress));
			Console.WriteLine (string.Format ("Datasource is {0}", ConfigurationManager.AppSettings ["DataStorage"]));
		}
			
		public void Stop()
		{
			m_nancyHost.Stop();
			Console.WriteLine("Stopped. Good bye!");
		}
	}
}

