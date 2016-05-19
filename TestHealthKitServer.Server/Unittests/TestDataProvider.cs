using System;
using HealthKitServer.Server;
using System.Collections.Generic;
using HealthKitServer;

namespace TestHealthKitServer.Server
{
	public class TestDataProvider
	{
		public static void ProvideTestData(IHealthKitDataStorage dataStorage)
		{
			var records = SetUpMultipleHealthKitObjects ();
			foreach (var record in records) 
			{
				dataStorage.AddOrUpdateHealthKitDataToStorage (record);
			}
		}

		public static IEnumerable<HealthKitData> SetUpMultipleHealthKitObjects()
		{
			IList<HealthKitData> multipleDataRecords = new List<HealthKitData> (); 
			multipleDataRecords.Add(new HealthKitData { PersonId = 11,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+-",  DateOfBirth = "10.01.2015", DistanceReadings = new DistanceReading {
					TotalDistance = 40, TotalSteps = 500, TotalStepsOfLastRecording = 200, TotalFlightsClimed = 30, TotalDistanceOfLastRecording = 10.50, 
				}});
			multipleDataRecords.Add(new HealthKitData { PersonId = 12,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+",  DateOfBirth = "08.01.2015", DistanceReadings = new DistanceReading {
					TotalDistance = 40, TotalSteps = 500, TotalStepsOfLastRecording = 200, TotalFlightsClimed = 30, TotalDistanceOfLastRecording = 10.50, 
				}});
			multipleDataRecords.Add(new HealthKitData { PersonId = 11,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+-",  DateOfBirth = "11.01.2015", DistanceReadings = new DistanceReading {
					TotalDistance = 40, TotalSteps = 500, TotalStepsOfLastRecording = 200, TotalFlightsClimed = 45, TotalDistanceOfLastRecording = 10.50, 
				}});
			return multipleDataRecords;
		}
	}
}

