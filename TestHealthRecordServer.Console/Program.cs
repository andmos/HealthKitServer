using System;
using System.Net;
using Newtonsoft.Json;
using HealthKitServer;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace TestHealthKitServer.Console
{
	public class Program
	{
		public static void Main(string[] args)
		{
			string serverUrl; 

			if (args.IsAny()) 
			{
				serverUrl = args [0]; 
			} 
			else 
			{
				serverUrl = ConfigurationManager.AppSettings ["ServerURL"];
			}

			using(var wc = new WebClient())
			{
				wc.Headers[HttpRequestHeader.ContentType] = "application/json"; 

				var jsonString = JsonConvert.SerializeObject(new HealthKitData { PersonId = 11,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74, HeartRateReadings = new HeartRateReading{ LastRegisteredHeartRate = 85, Source = "TestConsole"},
					BloodType = "A+",  DateOfBirth = "08.01.2015", DistanceReadings = new DistanceReading {
						TotalDistance = 40, TotalSteps = 500, TotalStepsOfLastRecording = 200, TotalFlightsClimed = 30, TotalDistanceOfLastRecording = 10.50, 
					}
				});

				JObject response = JObject.Parse(wc.UploadString(string.Format("{0}/api/v1/addHealthKitData", serverUrl), jsonString));

				System.Console.WriteLine(string.Format("Response from {0}: {1}",serverUrl, response));  
			}
		}
	}
}
