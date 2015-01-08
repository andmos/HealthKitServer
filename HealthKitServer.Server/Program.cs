using System;
using Topshelf;

namespace HealthKitServer.Server
{
	public class Program
	{
		public static void Main()
		{
			HostFactory.Run(x => 
				{
					x.UseLinuxIfAvailable();
					x.Service<HealthKitServerSelfHost>(s => 
						{
							s.ConstructUsing(name => new HealthKitServerSelfHost()); 
							s.WhenStarted(tc => tc.Start()); 
							s.WhenStopped(tc => tc.Stop()); 
						});

					x.RunAsLocalSystem(); 
					x.SetDescription("HealthKitServer"); 
					x.SetDisplayName("HealthKitServer Service"); 
					x.SetServiceName("HealthKitServer"); 
				}); 
		}
	}
}

