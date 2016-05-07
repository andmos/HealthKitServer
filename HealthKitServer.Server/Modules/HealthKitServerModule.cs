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
		private ILog m_logger;
		private IHealthKitDataStorage m_dataStorage;

		public HealthKitServerModule(IHealthKitDataStorage dataStorage, ILogFactory logger) : base("/api/v1")
		{
			m_logger = logger.GetLogger (GetType());
			m_dataStorage = dataStorage;

			Get ["/ping"] = parameters => 
			{
				var response = (Response) "pong"; 
				response.StatusCode = HttpStatusCode.OK;

				return response; 
			};

			Post["/addHealthKitData"] = parameters =>
			{
				try
				{
					var person = this.Bind<HealthKitData>();	 
					m_dataStorage.AddOrUpdateHealthKitDataToStorage(person);
					var response = Response.AsJson(person);
					response.StatusCode = HttpStatusCode.Created;
					return response; 
				}
				catch(Exception e)
				{
					m_logger.Error(e.Message, e); 
					var response = (Response) e.ToString(); 
					response.StatusCode = HttpStatusCode.BadRequest; 

					return response;
				}
			};
				
			Get["/getAllHealthKitData"] = parameters => 
			{
				var response = Response.AsJson (m_dataStorage.GetAllHealthKitData());
				response.StatusCode = HttpStatusCode.OK;
				return response;
			};

			Get["/getHealthKitData"] = parameters => 
			{
				var id = this.Request.Query["id"];
				int number; 
				Nancy.Response response;
				if(int.TryParse(id, out number))
				{
					response = Response.AsJson(m_dataStorage.GetSpesificHealthKitData(number));
					response.StatusCode = HttpStatusCode.OK;
					return response;

				}
				response = Response.AsText("Bad Request");
				response.StatusCode = HttpStatusCode.BadRequest;
				return response;
			};

			Get["/getHealthKitData"] = parameters => 
			{
				var personId = this.Request.Query["id"];
				int number; 
				Nancy.Response response;
				if(int.TryParse(personId, out number))
				{
					response = Response.AsJson(m_dataStorage.GetSpesificHealthKitData(number));
					response.StatusCode = HttpStatusCode.OK;
					return response;

				}
				response = Response.AsText("Bad Request");
				response.StatusCode = HttpStatusCode.BadRequest;
				return response;
			};

			Get["/getHealthKitDataRecord"] = parameters => 
			{
				var personId = this.Request.Query["id"];
				var recordId = this.Request.Query["recordId"]; 
				int idNumber = new int();
				int recordIdNumber = new int(); 
				Nancy.Response response;
				if(int.TryParse(personId, out idNumber) && int.TryParse(recordId, out recordIdNumber))
				{
					response = Response.AsJson(m_dataStorage.GetSpesificHealthKitDataRecord(idNumber, recordIdNumber));
					response.StatusCode = HttpStatusCode.OK;
					return response;

				}
				response = Response.AsText("Bad Request");
				response.StatusCode = HttpStatusCode.BadRequest;
				return response;
			};
		}
	}
}

