using System;
using HealthKitServer;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TestHealthKitServer.Server
{
	public class IntegrationTestableHealthKitDataWebService  : IHealthKitDataWebService
	{
			public string UploadHealthKitDataToHealthKitServer(string healthKitServerAPIAddress, HealthKitData dataObject)
			{
				try
				{

					using (var client = new WebClient())
					{
						client.Headers[HttpRequestHeader.ContentType] = "application/json";  
						var response = client.UploadString(new Uri(healthKitServerAPIAddress), JsonConvert.SerializeObject(dataObject));
					return response; 
					}
				}
				catch(Exception e)
				{
				return e.ToString(); 
				}
			}
			
			public IEnumerable<HealthKitData> GetHealtKitDataFromHealthKitServer (string healthKitServerAPIAddress, int id)
			{
				try
				{
					using (var client = new WebClient())
					{
					var query = string.Format("?id={0}", id);	
					var result = client.DownloadString(new Uri(healthKitServerAPIAddress + query));
						return JsonConvert.DeserializeObject<IEnumerable<HealthKitData>>(result);
					}
				}
				catch(Exception e)
				{
					return new [] { new HealthKitData ()}; 
				}
			}

		public HealthKitData GetHealthKitDataRecordFromHealthKitServer(string healthKitServerAPIAddress, int personId, int recordId)
		{
			try
			{
				using (var client = new WebClient())
				{
					var query = string.Format("?id={0}&recordId={1}", personId, recordId);
					var result = client.DownloadString(new Uri(healthKitServerAPIAddress + query));
					return JsonConvert.DeserializeObject<HealthKitData>(result);
				}
			}
			catch(Exception e)
			{
				return new HealthKitData (); 
			}
		}

	}
}

