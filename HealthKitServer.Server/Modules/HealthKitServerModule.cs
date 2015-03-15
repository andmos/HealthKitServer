using System;
using Nancy;
using Nancy.ModelBinding;
using HealthKitServer.Server;
using System.Collections.Generic;
using Nancy.Routing;

namespace HealthKitServer.Server
{
	public class HealthKitServerModule : NancyModule
	{
		public HealthKitServerModule(IRouteCacheProvider routeCacheProvider) : base("/api/v1")
		{

			Get["/"] = parameters =>
			{
				return Negotiate
					.WithModel(routeCacheProvider.GetCache().RetrieveMetadata<HealthKitServerMetadataModule>());

			};

			Get ["/ping"] = parameters => 
			{
				return "pong";
			};

			Post["/addHealthKitData"] = parameters =>
			{
				try
				{
					var person = this.Bind<HealthKitData>();	 
					Container.Singleton<IHealthKitDataStorage>().AddOrUpdateHealthKitDataToStorage(person);
					return Response.AsJson(person);
				}
				catch(Exception e)
				{
					return Response.AsText(e.Message);
				}
			};

			Get["/getAllHealthKitData"] = parameters => 
			{
				return Response.AsJson (Container.Singleton<IHealthKitDataStorage> ().GetAllHealthKitData());
			};

			Get["/getHealthKitData"] = parameters => 
			{
				var id = this.Request.Query["id"];
				int number; 
				if(int.TryParse(id, out number))
				{
					return Response.AsJson(Container.Singleton<IHealthKitDataStorage>().GetSpesificHealthKitData(number));

				}
				return "Invalid query";
			};
		}
	}
}

