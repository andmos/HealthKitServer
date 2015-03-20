using System;
using NUnit.Framework;
using HealthKitServer;
using HealthKitServer.Server;
using System.Linq;
using System.Collections.Generic;

namespace TestHealthKitServer.Server
{
	[TestFixture ()]
	public class TestHealthKitDataCahche
	{
		private IHealthKitDataStorage m_dataStorage;

		[SetUp()]
		public void Init()
		{
			m_dataStorage = new HealthKitDataCache ();	
		}

		[Test()]
		public void AddOrUpdateHealthKitDataToStorage_GivenHealthKitDataGetsStoredInCache()
		{
			var testData = SetUpSingleHealthKitDataObject();

			m_dataStorage.AddOrUpdateHealthKitDataToStorage (testData);
			var count = m_dataStorage.GetAllHealthKitData ().Count ();

			Assert.IsTrue (count == 1);
		}

		[Test()]
		public void GetSpesificHealthKitData_GivenMultipleHealthKitDataWithDifferentIdsReturnsCorrectNumberofRecords()
		{
			var testData = SetUpMultipleHealthKitObjects ();

			foreach (var record in testData) 
			{
				m_dataStorage.AddOrUpdateHealthKitDataToStorage (record);	
			}

			var countId1 = m_dataStorage.GetSpesificHealthKitData (11).Count();

			Assert.IsTrue (countId1 == 2);
		}

		[Test()]
		public void GetAllHealthKitData_GivenMultipleHealthKitRecordsCorrectNumberOfRecordsGetsAdded()
		{
			var testData = SetUpMultipleHealthKitObjects ();

			foreach (var record in testData)
			{
				m_dataStorage.AddOrUpdateHealthKitDataToStorage (record);
			}

			var totalCount = m_dataStorage.GetAllHealthKitData ().Count();

			Assert.IsTrue (totalCount == 3);
		}


		private HealthKitData SetUpSingleHealthKitDataObject()
		{
			return new HealthKitData { PersonId = 11,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+",  DateOfBirth = "08.01.2015", DistanceReadings = new DistanceReading {
					TotalDistance = "40", TotalSteps = "500", TotalStepsOfLastRecording = 200, TotalFlightsClimed = "30", TotalDistanceOfLastRecording = 10.50, 
				}};
		}

		private IEnumerable<HealthKitData> SetUpMultipleHealthKitObjects()
		{
			IList<HealthKitData> multipleDataRecords = new List<HealthKitData> (); 
			multipleDataRecords.Add(new HealthKitData { PersonId = 11,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+",  DateOfBirth = "08.01.2015", DistanceReadings = new DistanceReading {
					TotalDistance = "40", TotalSteps = "500", TotalStepsOfLastRecording = 200, TotalFlightsClimed = "30", TotalDistanceOfLastRecording = 10.50, 
				}});
			multipleDataRecords.Add(new HealthKitData { PersonId = 12,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+",  DateOfBirth = "08.01.2015", DistanceReadings = new DistanceReading {
					TotalDistance = "40", TotalSteps = "500", TotalStepsOfLastRecording = 200, TotalFlightsClimed = "30", TotalDistanceOfLastRecording = 10.50, 
				}});
			multipleDataRecords.Add(new HealthKitData { PersonId = 11,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+",  DateOfBirth = "08.01.2015", DistanceReadings = new DistanceReading {
					TotalDistance = "40", TotalSteps = "500", TotalStepsOfLastRecording = 200, TotalFlightsClimed = "30", TotalDistanceOfLastRecording = 10.50, 
				}});
			return multipleDataRecords;
		}
	
	}
}

