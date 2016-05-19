using System;
using NUnit.Framework;
using HealthKitServer;
using Newtonsoft.Json;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestHealthKitServer.Server
{

	/// <summary>
	/// Integrationtests to run against local testserver.  
	/// </summary>


	[TestFixture()]
	public class IntegrationTestHealthKitDataServerModule
	{
		private IHealthKitDataWebService m_healthKitDataWebClient; 
		private const string HealthKitServerUploadUrl = "http://localhost:5000/api/v1/addHealthKitData";
		private const string HealthKitServerGetUsersRecordsUrl = "http://localhost:5000/api/v1/gethealthkitdata";
		private const string HealthKitServerGetSpesificHealthKitRecord = "http://localhost:5000/api/v1/getHealthKitDataRecord";
		private const string HealthKitServerPingEndpoint = "http://localhost:5000/api/v1/ping"; 

		[SetUp()]
		public void Init()
		{
			m_healthKitDataWebClient = new IntegrationTestableHealthKitDataWebService ();
		}

		[Test()]
		[Category("integration")]
		public void Ping_GivenCallToPingEndopoint_ReturnsPong()
		{
			var response = m_healthKitDataWebClient.CheckConnectionToHealthKitServer (HealthKitServerPingEndpoint);

			Assert.IsTrue (response); 
		}

		[Test()]
		[Category("integration")]
		public void AddHealthKitData_GivenSingleHealthKitDataRecord_ResponseContainsSameData()
		{
			var testData = SetUpSingleHealthKitDataObject (); 

			var uploadResponse = m_healthKitDataWebClient.UploadHealthKitDataToHealthKitServer (HealthKitServerUploadUrl, testData);
			var deserializedObject = JsonConvert.DeserializeObject<HealthKitData> (uploadResponse);

			Assert.AreEqual (testData.PersonId, deserializedObject.PersonId);
			Assert.AreEqual (testData.Height, deserializedObject.Height);
			Assert.AreEqual (testData.DistanceReadings.TotalDistance, deserializedObject.DistanceReadings.TotalDistance);
		}
			
		[Test()]
		[Category("integration")]
		public void GetHealthKitData_RequestsHealthKitDataForUserId_ReturnsCorrectRecordsFromServer()
		{
			var testData = SetUpSingleHealthKitDataObject (); 

			var healthKitDataFromServer = m_healthKitDataWebClient.GetHealtKitDataFromHealthKitServer (HealthKitServerGetUsersRecordsUrl, testData.PersonId);

			Assert.IsNotNull (healthKitDataFromServer);
			Assert.IsTrue (healthKitDataFromServer.ToList ().Any (x => x.PersonId == testData.PersonId));
		}

		[Test()]
		[Category("integration")]
		public void GetHealthKitData_ChecksHealthKitDataForRecordId_ReturnsRecordIdsWithValue()
		{
			var testData = SetUpSingleHealthKitDataObject (); 

			var healthKitDataFromServer = m_healthKitDataWebClient.GetHealtKitDataFromHealthKitServer (HealthKitServerGetUsersRecordsUrl, testData.PersonId);

			Assert.IsNotNull (healthKitDataFromServer);
			Assert.IsTrue (healthKitDataFromServer.ToList ().Any (x => x.RecordId != 0));
		}

		[Test()]
		[Category("integration")]
		public void GetHealthKitData_GivenMultipleRecordsWithSamePersonId_ReturnsTrueIfAllRecordIdsAreUnique()
		{
			var testData = SetUpSingleHealthKitDataObject (); 
			AddMultipleHealthKitDataRecordsToHealthKitServer ();

			var healthKitDataFromServer = m_healthKitDataWebClient.GetHealtKitDataFromHealthKitServer (HealthKitServerGetUsersRecordsUrl, testData.PersonId);

			Assert.IsNotNull (healthKitDataFromServer);
			Assert.IsTrue (CheckResponseForUniqueRecordIds(healthKitDataFromServer));
		}

		[Test()]
		[Category("integration")]
		public void GetHealthKitData_GivenSingleRecordWithHealthKitData_ReturnsTrueIfAllHealthKitDataIsCorrect()
		{
			var testData = SetUpSingleHealthKitDataObject (); 
			m_healthKitDataWebClient.UploadHealthKitDataToHealthKitServer (HealthKitServerUploadUrl, testData);

			var healthKitDataFromServer = m_healthKitDataWebClient.GetHealtKitDataFromHealthKitServer (HealthKitServerGetUsersRecordsUrl, testData.PersonId);

			Assert.IsNotNull (healthKitDataFromServer);
			Assert.AreEqual (testData.BloodType, healthKitDataFromServer.FirstOrDefault(t => t.BloodType == testData.BloodType).BloodType);
			Assert.AreEqual (testData.DateOfBirth, healthKitDataFromServer.FirstOrDefault(t => t.DateOfBirth == testData.DateOfBirth).DateOfBirth);
			Assert.AreEqual (testData.Sex, healthKitDataFromServer.FirstOrDefault(t => t.Sex == testData.Sex).Sex);
			Assert.AreEqual (testData.HeartRateReadings.LastRegisteredHeartRate, healthKitDataFromServer.FirstOrDefault (t => t.HeartRateReadings.LastRegisteredHeartRate == testData.HeartRateReadings.LastRegisteredHeartRate).HeartRateReadings.LastRegisteredHeartRate);

		}

		[Test()]
		[Category("integration")]
		public void GetHealthKitData_GivenSingleRecordWithDistanceReadings_ReturnsTrueIfDistanceReadingIsCorrect()
		{
			var testData = SetUpSingleHealthKitDataObject (); 
			m_healthKitDataWebClient.UploadHealthKitDataToHealthKitServer (HealthKitServerUploadUrl, testData);

			var healthKitDataFromServer = m_healthKitDataWebClient.GetHealtKitDataFromHealthKitServer (HealthKitServerGetUsersRecordsUrl, testData.PersonId);

			Assert.IsNotNull (healthKitDataFromServer);
			Assert.AreEqual (testData.DistanceReadings.TotalDistance, healthKitDataFromServer.FirstOrDefault(t => t.DistanceReadings.TotalDistance == testData.DistanceReadings.TotalDistance).DistanceReadings.TotalDistance);
			Assert.AreEqual (testData.DistanceReadings.TotalStepsOfLastRecording, healthKitDataFromServer.FirstOrDefault(t => t.DistanceReadings.TotalStepsOfLastRecording == testData.DistanceReadings.TotalStepsOfLastRecording).DistanceReadings.TotalStepsOfLastRecording);

		}

		[Test()]
		[Category("integration")]
		public void GetHealthKitDataRecord_GivenSingleHealthKitRecords_ReturnsCorrectHealthKitRecord()
		{
			var testData = SetUpSingleHealthKitDataObject (); 
			m_healthKitDataWebClient.UploadHealthKitDataToHealthKitServer (HealthKitServerUploadUrl, testData);
			var healthKitRecordsFromPerson = m_healthKitDataWebClient.GetHealtKitDataFromHealthKitServer (HealthKitServerGetUsersRecordsUrl, testData.PersonId).Count ();

			var healthKitRecordFromServer = m_healthKitDataWebClient.GetHealthKitDataRecordFromHealthKitServer (HealthKitServerGetSpesificHealthKitRecord, testData.PersonId, healthKitRecordsFromPerson );

			Assert.IsNotNull (healthKitRecordFromServer);
			Assert.IsFalse (healthKitRecordFromServer.RecordId == 0); 

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

		private void AddMultipleHealthKitDataRecordsToHealthKitServer()
		{
			var records = SetUpMultipleHealthKitObjects ();

			foreach (var record in records) 
			{
				m_healthKitDataWebClient.UploadHealthKitDataToHealthKitServer (HealthKitServerUploadUrl, record);
			}
		}
			
		private HealthKitData SetUpSingleHealthKitDataObject()
		{
			return new HealthKitData { PersonId = 11,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+",  DateOfBirth = "08.01.2015", HeartRateReadings = new HeartRateReading { LastRegisteredHeartRate = 50, Source = "Testdata" }, DistanceReadings = new DistanceReading {
					TotalDistance = 40, TotalSteps = 500, TotalStepsOfLastRecording = 200, TotalFlightsClimed = 30, TotalDistanceOfLastRecording = 10.50, 
				}};
		}
			
		private IEnumerable<HealthKitData> SetUpMultipleHealthKitObjects()
		{
			IList<HealthKitData> multipleDataRecords = new List<HealthKitData> (); 
			multipleDataRecords.Add(new HealthKitData { PersonId = 11,  HeartRateReadings = new HeartRateReading { LastRegisteredHeartRate = 50, Source = "Testdata" }, RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+",  DateOfBirth = "08.01.2015", DistanceReadings = new DistanceReading {
					TotalDistance = 40, TotalSteps = 500, TotalStepsOfLastRecording = 200, TotalFlightsClimed = 30, TotalDistanceOfLastRecording = 10.50, 
				}});
			multipleDataRecords.Add(new HealthKitData { PersonId = 12,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+",  DateOfBirth = "08.01.2015", HeartRateReadings = new HeartRateReading { LastRegisteredHeartRate = 50, Source = "Testdata" }, DistanceReadings = new DistanceReading {
					TotalDistance = 40, TotalSteps = 520, TotalStepsOfLastRecording = 200, TotalFlightsClimed = 30, TotalDistanceOfLastRecording = 10.50, 
				}});
			multipleDataRecords.Add(new HealthKitData { PersonId = 11,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+",  DateOfBirth = "08.01.2015", HeartRateReadings = new HeartRateReading { LastRegisteredHeartRate = 50, Source = "Testdata" }, DistanceReadings = new DistanceReading {
					TotalDistance = 40, TotalSteps = 550, TotalStepsOfLastRecording = 200, TotalFlightsClimed = 30, TotalDistanceOfLastRecording = 10.50, 
				}});
			return multipleDataRecords;
		}
	}
}

