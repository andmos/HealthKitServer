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
					Container.Singleton<IHealthInfoDataStorage>().AddOrUpdateHealthKitDataToStorage(person);
					return Response.AsJson(person);
				}
				catch(Exception e)
				{
					return Response.AsText(e.Message);
				}
			};

			Get["/api/v1/getAllHealthKitData"] = parameters => 
			{
				return Response.AsJson (Container.Singleton<IHealthInfoDataStorage> ().GetAllHealthKitData());
			};

			Get["/api/v1/getHealthKitData"] = parameters => 
			{
				var id = this.Request.Query["id"];
				int number; 
				if(int.TryParse(id, out number))
				{
					return Response.AsJson(Container.Singleton<IHealthInfoDataStorage>().GetSpesificHealthKitData(number));

				}
				return "Invalid query";

			};
		}
	}
}

