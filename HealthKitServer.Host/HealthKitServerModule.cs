using System;
using Nancy;
using Nancy.ModelBinding;
using HealthKitServer.Server;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HealthKitServer.Host
{
	public class HealthKitServerModule : NancyModule
	{
		public HealthKitServerModule() 
		{

			Post["/api/v1/addPatient?"] = parameters =>
			{
				var person = this.Bind<Person>();
				Container.Singleton<IHealthInfoDataStorage>().AddOrUpdatePersonHealthInfoToStorage(person);
				return Response.AsJson(person);
			};

			Get["/api/v1/getAllPatients"] = parameters => 
			{
				return Response.AsJson (Container.Singleton<IHealthInfoDataStorage> ().GetAllPersons());
			};

			Get["/api/v1/getPatient"] = parameters => 
			{

				Container.Singleton<IHealthInfoDataStorage>().AddOrUpdatePersonHealthInfoToStorage(person);
				var id = this.Request.Query["id"];
				int number; 
				if(int.TryParse(id, out number))
				{
					return Response.AsJson(Container.Singleton<IHealthInfoDataStorage>().GetPatientHealthInfo(number));
				}
				return "Invalid query";

			};
		}
	}
}

