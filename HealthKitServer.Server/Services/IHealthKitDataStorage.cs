using System;
using System.Collections.Generic;

namespace HealthKitServer.Server
{
	public interface IHealthKitDataStorage
	{
		IEnumerable<HealthKitData> GetAllHealthKitData(); 
		IEnumerable<HealthKitData> GetSpesificHealthKitData(int id);
		void AddOrUpdateHealthKitDataToStorage(HealthKitData person);

	}
}

