using System;
using Newtonsoft.Json;

namespace HealthKitServer
{

	public class HealthKitData
	{

		public int Id { get; set;}


		public string BloodType { get; set;}


		public DateTime DateOfBirth { get; set;}


		public DistanceReading DistanceReadings { get; set;}
	}
}

