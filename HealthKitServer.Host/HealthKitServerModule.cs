using System;
using Nancy;

namespace HealthKitServer.Server
{
	public class HealthKitServerModule : NancyModule
	{
		public HealthKitServerModule() 
		{

			Get["/api/V1/totalsteps"] = parameters =>
			{
				var feeds = new string[] {"foo", "bar"};
				return Response.AsJson(feeds);
			};
		}
	}
}

