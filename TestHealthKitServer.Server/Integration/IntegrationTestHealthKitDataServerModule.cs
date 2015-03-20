using System;
using NUnit.Framework;
using HealthKitServer;
using Newtonsoft.Json;

namespace TestHealthKitServer.Server
{

	/// <summary>
	/// Integrationtests to run against local testserver.  
	/// </summary>


	[TestFixture()]
	public class IntegrationTestHealthKitDataServerModule
	{
		private IHealthKitDataWebService m_healthKitDataWebClient; 
		private const string HealthKitServerUploadUrl = "http://localhost:5002/api/v1/addHealthKitData";
		private const string HealthKitServerGetUsersRecordsUrl = "http://localhost:5002/api/v1/gethealthkitdata?id=";

		[SetUp()]
		public void Init()
		{
			m_healthKitDataWebClient = new IntegrationTestableHealthKitDataWebService ();
		}

		[Test()]
		public void UploadHealthKitObjectToServer_ResponseIsSameObject()
		{
			var testData = SetUpSingleHealthKitDataObject (); 

			var uploadResponse = m_healthKitDataWebClient.UploadHealthKitDataToHealthKitServer (HealthKitServerUploadUrl, testData);
			var deserializedObject = JsonConvert.DeserializeObject<HealthKitData> (uploadResponse);

			Assert.AreEqual (testData.PersonId, deserializedObject.PersonId);
			Assert.AreEqual (testData.Height, deserializedObject.Height);
			Assert.AreEqual (testData.DistanceReadings.TotalDistance, deserializedObject.DistanceReadings.TotalDistance);
		}

		private HealthKitData SetUpSingleHealthKitDataObject()
		{
			return new HealthKitData { PersonId = 11,  RecordingTimeStamp = DateTime.UtcNow, Sex = "male", Height = 1.74,
				BloodType = "A+",  DateOfBirth = "08.01.2015", DistanceReadings = new DistanceReading {
					TotalDistance = "40", TotalSteps = "500", TotalStepsOfLastRecording = 200, TotalFlightsClimed = "30", TotalDistanceOfLastRecording = 10.50, 
				}};
		}
	}
}

