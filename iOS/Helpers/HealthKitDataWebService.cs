using System;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HealthKitServer.iOS
{
	public class HealthKitDataWebService : IHealthKitDataWebService 
	{
		public string UploadHealthKitDataToHealthKitServer(string healthKitServerAPIAddress, HealthKitData dataObject)
		{
			try
			{

				using (var client = new WebClient())
				{
					client.Headers[HttpRequestHeader.ContentType] = "application/json";  
					HealthKitDataContext.ActiveHealthKitData.RecordingTimeStamp = DateTime.UtcNow;
					var response = client.UploadString(new Uri(healthKitServerAPIAddress), JsonConvert.SerializeObject(HealthKitDataContext.ActiveHealthKitData));
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
					var result = client.DownloadString(new Uri(healthKitServerAPIAddress + id));
					return JsonConvert.DeserializeObject<IEnumerable<HealthKitData>>(result);
				}
			}
			catch(Exception e)
			{
				return new [] { new HealthKitData ()}; 
			}
		}
	}
}
