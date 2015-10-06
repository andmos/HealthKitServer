using System;
using System.Collections.Generic;

namespace HealthKitServer
{
	public interface IHealthKitDataWebService
	{
		string UploadHealthKitDataToHealthKitServer(string healthKitServerAPIAddress, HealthKitData dataObject);
		IEnumerable<HealthKitData> GetHealtKitDataFromHealthKitServer(string healthKitServerAPIAddress, int personId);
		HealthKitData GetHealthKitDataRecordFromHealthKitServer(string healthKitServerAPIAddress, int personId, int recordId);
	}

}

