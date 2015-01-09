using System;
using Newtonsoft.Json;

namespace HealthKitServer
{
	public class DistanceReading
	{
		[JsonProperty("totalSteps")] 
		public string TotalSteps { get; set; }

		[JsonProperty("totalStepsToday")] 
		public string TotalStepsToday { get; set; }

		[JsonProperty("totalDistance")] 
		public string TotalDistance { get; set; }

		[JsonProperty("totalDistanceToday")] 
		public string TotalDistanceToday{ get; set; }

		[JsonProperty("totalFlightsClimed")] 
		public string TotalFlightsClimed { get; set; }

		[JsonProperty("recordingStarted")] 
		public string RecordingStarted { get; set; }

		[JsonProperty("recordingStoped")] 
		public string RecordingStoped {get; set; }

		[JsonProperty("secondsSinceLatestRecord")] 
		public string SecondsSinceLatestRecord { get; set; }
	}
}

