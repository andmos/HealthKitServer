using System;

namespace HealthKitServer
{
	public interface IHealthKitDataWebService
	{
		bool UploadHealthKitDataToHealthKitServer(string healthKitServerAPIAddress, HealthKitData dataObject);
		HealthKitData GetHealtKitDataFromHealthKitServer(string healthKitServerAPIAddress, int id);
	}

}

