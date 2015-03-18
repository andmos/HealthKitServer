using System;
using System.Collections.Generic;

namespace HealthKitServer
{
	public interface IHealthKitDataWebService
	{
		bool UploadHealthKitDataToHealthKitServer(string healthKitServerAPIAddress, HealthKitData dataObject);
		IEnumerable<HealthKitData> GetHealtKitDataFromHealthKitServer(string healthKitServerAPIAddress, int id);
	}

}

