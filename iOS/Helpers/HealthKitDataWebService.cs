﻿using System;
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

		public bool CheckConnectionToHealthKitServer(string healthKitServerAPIAddress)
		{
			try 
			{
				using (var client = new WebClient ()) 
				{
					return client.DownloadString (new Uri (healthKitServerAPIAddress)).Equals ("pong"); 
				}
			} catch (Exception e) {
				return false; 
			}
		}
	}
}
