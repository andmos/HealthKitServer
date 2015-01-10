using System;

namespace HealthKitServer
{
	public interface IHealthKitAccess
	{
		void SetUpPermissions();
		void GetTotalSteps(HealthKitData dataobject);
		void GetTotalLengthWalked(HealthKitData dataobject); 
		void GetTotalFlights(HealthKitData dataobject);
		void GetDateOfBirth(HealthKitData dataobject);

	}
}

