using System;

namespace HealthKitServer
{
	public interface IHealthKitDataUploader
	{
		bool UploadHealthKitDataToHealthKitServer(string healthKitServerAddress, HealthKitData dataObject);
	}

}

