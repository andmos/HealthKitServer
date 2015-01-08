using System;
using System.Diagnostics;
using Nancy.Hosting.Self;
namespace HealthKitServer.Server
{
	public class HealthKitServerSelfHost
	{

		private NancyHost m_nancyHost;


			public void Start()
			{
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

