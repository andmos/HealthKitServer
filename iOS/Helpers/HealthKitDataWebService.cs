using System;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace HealthKitServer.iOS
{
	public class HealthKitDataWebService : IHealthKitDataWebService 
	{
		public bool UploadHealthKitDataToHealthKitServer(string healthKitServerAPIAddress, HealthKitData dataObject)
		{
			try
			{

				using (var client = new WebClient())
				{
					client.Headers[HttpRequestHeader.ContentType] = "application/json";  
					var result = client.UploadString(new Uri(healthKitServerAPIAddress), JsonConvert.SerializeObject(HealthKitDataContext.ActiveHealthKitData));
					return true; 
				}
			}
			catch(Exception e)
			{
				return false; 
			}
		}

		public HealthKitData GetHealtKitDataFromHealthKitServer (string healthKitServerAPIAddress, int id)
		{
			try
			{
				using (var client = new WebClient())
				{
					var result = client.DownloadString(new Uri(healthKitServerAddress + id));
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
