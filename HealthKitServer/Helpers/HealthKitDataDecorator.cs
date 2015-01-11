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
			m_healthKitData.DateOfBirth = await m_healthKitAccess.GetDateOfBirth ();
			m_healthKitData.DistanceReadings.TotalSteps = await m_healthKitAccess.GetTotalSteps ();
			m_healthKitData.DistanceReadings.TotalFlightsClimed = await m_healthKitAccess.GetTotalFlights ();
			m_healthKitData.DistanceReadings.TotalDistance = await m_healthKitAccess.GetTotalLengthWalked ();
			return true;
		}
	}
}

