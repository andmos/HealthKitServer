using System;
using Newtonsoft.Json;

namespace HealthKitServer
{
	public class DistanceReading
	{

		public string TotalSteps { get; set; }


		public int TotalStepsOfLastRecording { get; set; }


		public string TotalDistance { get; set; }


		public double TotalDistanceOfLastRecording{ get; set; }


		public string TotalFlightsClimed { get; set; }


		public string RecordingStarted { get; set; }


		public string RecordingStoped {get; set; }


	}
}

