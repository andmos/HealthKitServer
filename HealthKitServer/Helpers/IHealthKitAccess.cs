using System;

namespace HealthKitServer
{
	public interface IHealthKitAccess
	{
		void SetUpPermissions();
		void GetTotalSteps();
		void GetTotalLengthWalked(); 
	}
}

