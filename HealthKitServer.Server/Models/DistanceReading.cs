using System;

namespace HealthKitServer.Server
{
	public class DistanceReading
	{
		public string TotalSteps { get; set; }
		public string TotalStepsToday { get; set; }
		public string TotalDistance { get; set; }
		public string TotalDistanceToday{ get; set; }
		public string TotalFlightsClimed { get; set; }
		public string RecordingStarted { get; set; }
		public string RecordingStoped {get; set; }
		public string SecondsSinceLatestRecord { get; set; }
	}
}

