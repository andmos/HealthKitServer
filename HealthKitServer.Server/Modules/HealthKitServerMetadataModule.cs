using System;
using Nancy.Swagger.Modules;
using Nancy.Swagger;
using Nancy.Metadata.Module;

namespace HealthKitServer.Server
{
	public class HealthKitServerMetadataModule : MetadataModule<SwaggerRouteData>  
	{
		public HealthKitServerMetadataModule ()
		{
		
			this.Describe ["addPatient"] = desc => {
				return desc.AsSwagger (with => 
					{
					with.ResourcePath ("/api/v1/addPatient");
					with.Summary ("Add HealthKitData to the server.");
					with.Notes ("Adds HealthKitData to the servers datastore.");
				});
			};
			
			this.Describe ["getAllPatients"] = desc => {
				return desc.AsSwagger (with => {
					with.ResourcePath ("/api/v1/getAllPatients");
					with.Summary ("Returns all registrated HealthKitData from the server.");
				});
			};
	
			this.Describe ["getPatient"] = desc => {
				return desc.AsSwagger (with => {
					with.ResourcePath ("/api/v1/getPatient");
					with.Summary ("Get spesific HealthKitData from the server.");
					with.Notes ("Given HealthKitData-owners Id, returns all HealthKitData records for this user.");
					with.QueryParam<int> ("id", "Id for user to search for.");
				});
			};
		
		}
}
}
