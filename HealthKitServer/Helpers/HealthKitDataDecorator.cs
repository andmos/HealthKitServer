using System;
using System.Threading.Tasks;

namespace HealthKitServer
{
	public class HealthKitDataDecorator
	{
		IHealthKitAccess m_healthKitAccess;
		HealthKitData m_healthKitData;

		public HealthKitDataDecorator (IHealthKitAccess healthKitAccess, HealthKitData dataObject)
		{
			m_healthKitAccess = healthKitAccess;
			m_healthKitData = dataObject;
			m_healthKitAccess.SetUpPermissions ();
		}

		public async Task<bool> DecorateHealthKitData()
		{
			m_healthKitData.DateOfBirth = await m_healthKitAccess.QueryDateOfBirth ();
			m_healthKitData.DistanceReadings.TotalSteps = await m_healthKitAccess.QueryTotalSteps ();
			m_healthKitData.DistanceReadings.TotalFlightsClimed = await m_healthKitAccess.QueryTotalFlights ();
			m_healthKitData.DistanceReadings.TotalDistance = await m_healthKitAccess.QueryTotalLengthWalked ();
			m_healthKitData.DistanceReadings.RecordingStarted = await m_healthKitAccess.QueryTotalStepsRecordingFirstRecordingDate ();
			m_healthKitData.DistanceReadings.RecordingStoped = await m_healthKitAccess.QueryTotalStepsRecordingLastRecordingDate ();
			m_healthKitData.BloodType = await m_healthKitAccess.QueryBloodType ();
			m_healthKitData.Sex = await m_healthKitAccess.QuerySex ();
			m_healthKitData.Height = await m_healthKitAccess.QueryTotalHeight (); 
			m_healthKitData.DistanceReadings.TotalDistanceOfLastRecording = await m_healthKitAccess.QueryLastRegistratedWalkingDistance ();
			m_healthKitData.DistanceReadings.TotalStepsOfLastRecording = await m_healthKitAccess.QueryLastRegistratedSteps ();
			m_healthKitData.RecordingTimeStamp = DateTime.UtcNow;
			//m_healthKitData.LastRegisteredHeartRate = await m_healthKitAccess.QueryLastRegistratetHeartRate ();
			return true;
		}
	}
}

