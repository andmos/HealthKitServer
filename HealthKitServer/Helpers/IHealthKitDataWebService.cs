using System;

namespace HealthKitServer
{
	public interface IHealthKitDataUploader
	{
		bool UploadHealthKitDataToHealthKitServer(string healthKitServerAPIAddress, HealthKitData dataObject);
		HealthKitData GetHealtKitDataFromHealthKitServer(string healthKitServerAPIAddress, int id);
	}

}

