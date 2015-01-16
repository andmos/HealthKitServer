using System;
using System.Collections.Generic;

namespace HealthKitServer.Server
{
	public interface IHealthInfoDataStorage
	{
		IEnumerable<HealthKitData> GetAllHealthKitData(); 
		IEnumerable<HealthKitData> GetSpesificHealthKitData(int id);
		void AddOrUpdateHealthKitDataToStorage(HealthKitData person);

	}
}

