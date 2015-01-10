using System;

namespace HealthKitServer
{
	public class HealthKitDataDecorator
	{

		IHealthKitAccess m_healthKitAccess;
		HealthKitData m_healthKitData;

		public HealthKitDataDecorator (IHealthKitAccess healthKitAccess, HealthKitData dataObject)
		{
			healthKitAccess = m_healthKitAccess;
			m_healthKitData = dataObject;
			m_healthKitAccess.SetUpPermissions ();
		}

		public void DecorateHealthKitData()
		{
			m_healthKitAccess.GetDateOfBirth (m_healthKitData);
			m_healthKitAccess.GetTotalSteps (m_healthKitData);
			m_healthKitAccess.GetTotalFlights (m_healthKitData);
			m_healthKitAccess.GetTotalLengthWalked (m_healthKitData);
		}
	}
}

