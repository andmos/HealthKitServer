using System;
using NUnit.Framework;
using HealthKitServer;
using HealthKitServer.Server;
using System.Linq;
using System.Collections.Generic;

namespace TestHealthKitServer.Server
{
	[TestFixture ()]
	public class TestHealthKitDataCache
	{
		private IHealthKitDataStorage m_dataStorage;

		[SetUp()]
		public void Init()
		{
			m_dataStorage = new HealthKitDataCache ();	
		}

		[Test()]
		[Category("Unit")]
		public void AddOrUpdateHealthKitDataToStorage_GivenHealthKitDataGetsStoredInCache()
		{
			var testData = SetUpSingleHealthKitDataObject();

			m_dataStorage.AddOrUpdateHealthKitDataToStorage (testData);
			var count = m_dataStorage.GetAllHealthKitData ().Count ();

			Assert.IsTrue (count == 1);
		}

		[Test()]
		[Category("Unit")]
		public void AddOrUpdateHealthKitDataToStorage_GivenHealthKitDataWithZeroAsPersonId_GetsStoredInCacheWithIncrementedId()
		{
			var testData = SetUpSingleHealthKitDataObject();
			testData.PersonId = 0;

			m_dataStorage.AddOrUpdateHealthKitDataToStorage (testData);

			Assert.IsTrue (testData.PersonId == 1);
		}

		[Test()]
		[Category("Unit")]
		public void GetSpesificHealthKitData_GivenMultipleHealthKitDataWithDifferentIds_ReturnsCorrectNumberofRecords()
		{
			var testData = SetUpMultipleHealthKitObjects ();
			PutMultipleHealthKitRecordsInCache (testData);

			var countId1 = m_dataStorage.GetSpesificHealthKitData (11).Count();

			Assert.IsTrue (countId1 == 2);
		}

		[Test()]
		[Category("Unit")]
		public void GetSpesificHealthKitData_GivenMultipleRecordsWithSamePersonId_ReturnsTrueIfAllRecordIdsAreUnique()
		{
			var singleTestData = SetUpSingleHealthKitDataObject ();
			var testData = SetUpMultipleHealthKitObjects ();
			PutMultipleHealthKitRecordsInCache (testData);
			m_dataStorage.AddOrUpdateHealthKitDataToStorage (singleTestData);

			var dataFromCache = m_dataStorage.GetSpesificHealthKitData (singleTestData.PersonId);

			Assert.IsTrue (CheckResponseForUniqueRecordIds (dataFromCache));

		}
			
		[Test()]
		[Category("Unit")]
		public void GetAllHealthKitData_GivenMultipleHealthKitRecordsCorrectNumberOfRecordsGetsAdded()
		{
			var testData = SetUpMultipleHealthKitObjects ();
			PutMultipleHealthKitRecordsInCache (testData);

			var totalCount = m_dataStorage.GetAllHealthKitData ().Count();

			Assert.IsTrue (totalCount == 3);
		}

		[Test()]
		[Category("Unit")]
		public void GetSpesificHealthKitDataRecord_GivenMultipleHealthKitRecords_ReturnsHealthKitRecordWithCorrectId()
		{
			var singleTestData = SetUpSingleHealthKitDataObject ();
			var testData = SetUpMultipleHealthKitObjects ();
			m_dataStorage.AddOrUpdateHealthKitDataToStorage (singleTestData);
			PutMultipleHealthKitRecordsInCache (testData);

			var dataFromCache = m_dataStorage.GetSpesificHealthKitDataRecord (singleTestData.PersonId, 1);

			Assert.IsTrue (dataFromCache.RecordId != 0);
			Assert.AreEqual (singleTestData.PersonId, dataFromCache.PersonId);
		}

		[Test()]
		[Category("Unit")]
		public void GetSpesificHealthKitDataRecord_GivenMultipleHealthKitRecords_ReturnsEmptyHealthKitRecordWhenAskedForNonExistingId()
		{
			var singleTestData = SetUpSingleHealthKitDataObject ();
			var testData = SetUpMultipleHealthKitObjects ();
			m_dataStorage.AddOrUpdateHealthKitDataToStorage (singleTestData);
			PutMultipleHealthKitRecordsInCache (testData);

			var dataFromCache = m_dataStorage.GetSpesificHealthKitDataRecord (singleTestData.PersonId, 20);

			Assert.IsTrue (dataFromCache.RecordId == 0);
		}

		private bool CheckResponseForUniqueRecordIds(IEnumerable<HealthKitData> healthKitData)
		{
			List<int> response = new List<int> (healthKitData.Select (r => r.RecordId)); 
			var gr = response.GroupBy (r => r);

			foreach (var number in gr) 
			{
				if (number.Count() > 1) 
				{
					return false; 
				}
			}
			return true; 
		}

		private void PutMultipleHealthKitRecordsInCache(IEnumerable<HealthKitData> healthKitData)
		{
			foreach (var record in healthKitData)
			{
				m_dataStorage.AddOrUpdateHealthKitDataToStorage (record);
			}
		}

		private HealthKitData SetUpSingleHealthKitDataObject()
		{
			return new HealthKitData { PersonId = 11,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+",  DateOfBirth = "08.01.2015", DistanceReadings = new DistanceReading {
					TotalDistance = 40, TotalSteps = 500, TotalStepsOfLastRecording = 200, TotalFlightsClimed = 30, TotalDistanceOfLastRecording = 10.50, 
				}};
		}

		private IEnumerable<HealthKitData> SetUpMultipleHealthKitObjects()
		{
			IList<HealthKitData> multipleDataRecords = new List<HealthKitData> (); 
			multipleDataRecords.Add(new HealthKitData { PersonId = 11,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+",  DateOfBirth = "08.01.2015", DistanceReadings = new DistanceReading {
					TotalDistance = 40, TotalSteps = 500, TotalStepsOfLastRecording = 200, TotalFlightsClimed = 30, TotalDistanceOfLastRecording = 10.50, 
				}});
			multipleDataRecords.Add(new HealthKitData { PersonId = 12,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+",  DateOfBirth = "08.01.2015", DistanceReadings = new DistanceReading {
					TotalDistance = 40, TotalSteps = 500, TotalStepsOfLastRecording = 200, TotalFlightsClimed = 30, TotalDistanceOfLastRecording = 10.50, 
				}});
			multipleDataRecords.Add(new HealthKitData { PersonId = 11,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+",  DateOfBirth = "08.01.2015", DistanceReadings = new DistanceReading {
					TotalDistance = 40, TotalSteps = 500, TotalStepsOfLastRecording = 200, TotalFlightsClimed = 30, TotalDistanceOfLastRecording = 10.50, 
				}});
			return multipleDataRecords;
		}
	
	}
}

