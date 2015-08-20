using System;
using Newtonsoft.Json;

namespace HealthKitServer
{
	public class DistanceReading
	{

		public int TotalSteps { get; set; }
			

		public int TotalStepsOfLastRecording { get; set; }


		public double TotalDistance { get; set; }


		public double TotalDistanceOfLastRecording{ get; set; }


		public int TotalFlightsClimed { get; set; }


		public string RecordingStarted { get; set; }


		public string RecordingStoped {get; set; }


	}
}

