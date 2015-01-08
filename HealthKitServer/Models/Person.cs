using System;

namespace HealthKitServer
{
	public class Person
	{
		public int Id { get; set;}
		public string BloodType { get; set;}
		public DateTime TimeOfBirth { get; set;}
		public DistanceReading DistanceReadings { get; set;}
	}
}

