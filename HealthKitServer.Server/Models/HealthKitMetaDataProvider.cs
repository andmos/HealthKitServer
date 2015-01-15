using System;
using Nancy.Swagger.Services;
using Nancy.Swagger;

namespace HealthKitServer.Server
{
	public class HealthKitMetaDataProvider : ISwaggerModelDataProvider
	{
		public SwaggerModelData GetModelData()
		{
			return SwaggerModelData.ForType<HealthKitData>(with =>
				{
					with.Description("Recorded data from HealthKit");
					with.Property(x => x.BloodType)
						.Description("Registrated bloodtype.")
						.Required(false)
						.UniqueItems(true);
						
					with.Property(x => x.DateOfBirth)
						.Description("Registrated date of birth.")
						.Required(false)
						.UniqueItems(true);
				});
		}
	}
}

