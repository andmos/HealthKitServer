﻿using System;
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
					client.DefaultRequestHeaders[HttpRequestHeader.ContentType] = "application/json";
					var response = client.PostAsync(new Uri(healthKitServerAddress), JsonConvert.SerializeObject(dataObject));
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