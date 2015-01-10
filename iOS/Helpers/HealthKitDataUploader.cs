using System;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace HealthKitServer.iOS
{
	public class HealthKitDataUploader : IHealthKitDataUploader 
	{
		public bool UploadHealthKitDataToHealthKitServer(string healthKitServerAddress, HealthKitData dataObject)
		{
			try
			{
				using (var client = new HttpClient(new ModernHttpClient.NativeMessageHandler()))
				{
					var response = client.PostAsync(new Uri(healthKitServerAddress), new StringContent(JsonConvert.SerializeObject(dataObject)));
					return true; 
				}
			}
			catch(Exception e)
			{
				return false; 
			}
		}
	}
}
