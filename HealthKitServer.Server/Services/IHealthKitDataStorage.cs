using System;
using System.Collections.Generic;

namespace HealthKitServer.Server
{
	public interface IHealthKitDataStorage
	{
		IEnumerable<HealthKitData> GetAllHealthKitData(); 
		IEnumerable<HealthKitData> GetSpesificHealthKitData(int personId);
		HealthKitData GetSpesificHealthKitDataRecord(int personId, int recordId);
		void AddOrUpdateHealthKitDataToStorage(HealthKitData person);
	}
}

