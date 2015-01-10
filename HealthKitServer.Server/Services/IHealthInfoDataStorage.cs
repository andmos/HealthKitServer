using System;
using System.Collections.Generic;

namespace HealthKitServer.Server
{
	public interface IHealthInfoDataStorage
	{
		IEnumerable<HealthKitData> GetAllPersons(); 
		HealthKitData GetPatientHealthInfo(int id);
		void AddOrUpdatePersonHealthInfoToStorage(HealthKitData person);

	}
}

