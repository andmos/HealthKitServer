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
		public HealthKitServerModule(IRouteCacheProvider routeCacheProvider) 
		{

			Get["/api/v1"] = parameters =>
			{
				return Negotiate
					.WithModel(routeCacheProvider.GetCache().RetrieveMetadata<HealthKitServerMetadataModule>());

			};

			Post["/api/v1/addHealthKitData"] = parameters =>
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

			Get["/api/v1/getAllHealthKitData"] = parameters => 
			{
				return Response.AsJson (Container.Singleton<IHealthKitDataStorage> ().GetAllHealthKitData());
			};

			Get["/api/v1/getHealthKitData"] = parameters => 
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

