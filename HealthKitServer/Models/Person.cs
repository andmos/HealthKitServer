using System;
using Newtonsoft.Json;

namespace HealthKitServer
{

	public class Person
	{
		[JsonProperty("id")] 
		public int Id { get; set;}

		[JsonProperty("bloodType")] 
		public string BloodType { get; set;}

		[JsonProperty("timeOfBirth")]
		public DateTime TimeOfBirth { get; set;}

		[JsonProperty("distanceReadings")]
		public DistanceReading DistanceReadings { get; set;}
	}
}

