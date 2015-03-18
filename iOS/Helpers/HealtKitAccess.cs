using System;
using MonoTouch.HealthKit;
using MonoTouch.Foundation;
using Newtonsoft.Json;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;

namespace HealthKitServer
{
	public class HealtKitAccess : IHealthKitAccess
	{
		private HKHealthStore m_healthKitStore;


		public void SetUpPermissions ()
		{
			var distanceQuantityType = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.DistanceWalkingRunning);
			var stepsQuantityType = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.StepCount);
			var flightsQuantityType = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.FlightsClimbed);
			var heightQuantityType = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.Height);
			var heartRateQuantityType = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.HeartRate);
			var nikeFuelQuantityType = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.NikeFuel);
			var dateOfBirthCharacteristicType = HKObjectType.GetCharacteristicType (HKCharacteristicTypeIdentifierKey.DateOfBirth);
			var sexCharacteristicType = HKObjectType.GetCharacteristicType (HKCharacteristicTypeIdentifierKey.BiologicalSex);
			var bloodTypeCharacteristicType = HKObjectType.GetCharacteristicType (HKCharacteristicTypeIdentifierKey.BloodType);

			if (m_healthKitStore == null)
			{
				HealthKitStore = new HKHealthStore ();
				m_healthKitStore.RequestAuthorizationToShare (new NSSet (new [] { distanceQuantityType , stepsQuantityType , flightsQuantityType  }), new NSSet (new [] {  (NSObject) distanceQuantityType ,(NSObject)  stepsQuantityType , (NSObject) flightsQuantityType , (NSObject)  heightQuantityType , (NSObject)dateOfBirthCharacteristicType, (NSObject) sexCharacteristicType, (NSObject) bloodTypeCharacteristicType, (NSObject)nikeFuelQuantityType, (NSObject)bloodTypeCharacteristicType  }), (success, error) => {
					Console.WriteLine ("Authorized:" + success);
					if (error != null) {
						Console.WriteLine ("Authorization error: " + error);
					}
				});

			}
		}

		public HKHealthStore HealthKitStore
		{
			get
			{
				return m_healthKitStore;
			}
			private set
			{
				m_healthKitStore = value;
			}
		}

		public async Task<string>  QueryTotalSteps()
		{
			var stepsCount = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.StepCount);
			var sumOptions = HKStatisticsOptions.CumulativeSum;
			string resultString = string.Empty;
			var query = new HKStatisticsQuery(stepsCount, new NSPredicate (IntPtr.Zero), sumOptions, (HKStatisticsQuery resultQuery, HKStatistics results, NSError error) => {
				if (results != null) {
					var quantitySample = results;
					var quantity =  quantitySample.SumQuantity();
				//	resultString = quantity.ToString();
					HealthKitDataContext.ActiveHealthKitData.DistanceReadings.TotalSteps = quantity.ToString();
					Console.WriteLine(string.Format("totally walked {0} steps",quantity.ToString()));
				}

			});
			await Task.Factory.StartNew(() =>HealthKitStore.ExecuteQuery (query));
			return resultString;
		}

		public async Task<string>  QueryTotalStepsRecordingFirstRecordingDate()
		{
			var stepsCount = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.StepCount);
			var sumOptions = HKStatisticsOptions.CumulativeSum;
			string resultString = string.Empty;
			var query = new HKStatisticsQuery(stepsCount, new NSPredicate (IntPtr.Zero), sumOptions, (HKStatisticsQuery resultQuery, HKStatistics results, NSError error) => {
				if (results != null) {
					var quantitySample = results;
					var quantity =  quantitySample.StartDate;
					//	resultString = quantity.ToString();
					HealthKitDataContext.ActiveHealthKitData.DistanceReadings.RecordingStarted = quantity.ToString();
					Console.WriteLine(string.Format("Started recording steps: {0} ",quantity.ToString()));
				}

			});
			await Task.Factory.StartNew(() =>HealthKitStore.ExecuteQuery (query));
			return resultString;
		}

		public async Task<string>  QueryTotalStepsRecordingLastRecordingDate()
		{
			var stepsCount = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.StepCount);
			var sumOptions = HKStatisticsOptions.CumulativeSum;
			string resultString = string.Empty;
			var query = new HKStatisticsQuery(stepsCount, new NSPredicate (IntPtr.Zero), sumOptions, (HKStatisticsQuery resultQuery, HKStatistics results, NSError error) => {
				if (results != null) {
					var quantitySample = results;
					var quantity =  quantitySample.EndDate;
					//	resultString = quantity.ToString();
					HealthKitDataContext.ActiveHealthKitData.DistanceReadings.RecordingStoped = quantity.ToString();
					Console.WriteLine(string.Format("Last recording of steps: {0} ",quantity.ToString()));
				}

			});
			await Task.Factory.StartNew(() =>HealthKitStore.ExecuteQuery (query));
			return resultString;
		}

		public async Task<string> QueryTotalLengthWalked()
		{
			var stepsCount = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.DistanceWalkingRunning);
			var sumOptions = HKStatisticsOptions.CumulativeSum;
			string resultString = string.Empty;
			var query = new HKStatisticsQuery(stepsCount, new NSPredicate (IntPtr.Zero), sumOptions,(HKStatisticsQuery resultQuery, HKStatistics results, NSError error) => {
				if (results != null) {
					var quantitySample = results;
					var quantity = quantitySample.SumQuantity();
					// resultString = quantity.ToString();;
					HealthKitDataContext.ActiveHealthKitData.DistanceReadings.TotalDistance = quantity.ToString();
					Console.WriteLine(string.Format("totally walked {0}",quantity.ToString()));

				}

			});
			await Task.Factory.StartNew(() => HealthKitStore.ExecuteQuery (query));
			return resultString;
		}
			
		public async Task<double> QueryTotalHeight()
		{

			var heightType = HKQuantityType.GetQuantityType (HKQuantityTypeIdentifierKey.Height);
			double usersHeight = 0.0;

			var timeSortDescriptor = new NSSortDescriptor (HKSample.SortIdentifierEndDate, false);
			var query = new HKSampleQuery (heightType, new NSPredicate (IntPtr.Zero), 1, new NSSortDescriptor[] { timeSortDescriptor },
				(HKSampleQuery resultQuery, HKSample[] results, NSError error) => {

					HKQuantity quantity = null;
					string resultString = string.Empty;
					if (results.Length != 0) {
						resultString = results [results.Length - 1].ToString();
						HealthKitDataContext.ActiveHealthKitData.Height = ParseStringResultToDouble(resultString);
						Console.WriteLine(string.Format("value of Fetched: {0}", ParseStringResultToDouble(resultString)));
					}

				});
			m_healthKitStore.ExecuteQuery (query);
			Console.WriteLine(string.Format("Total height: ", usersHeight));
			return usersHeight;
		}


		public async Task<double> QueryLastRegistratedWalkingDistance()
		{

			var heightType = HKQuantityType.GetQuantityType (HKQuantityTypeIdentifierKey.DistanceWalkingRunning);
			double lastRegistratedWalkingDistance = 0.0;

			var timeSortDescriptor = new NSSortDescriptor (HKSample.SortIdentifierEndDate, false);
			var query = new HKSampleQuery (heightType, new NSPredicate (IntPtr.Zero), 1, new NSSortDescriptor[] { timeSortDescriptor },
				(HKSampleQuery resultQuery, HKSample[] results, NSError error) => {

					HKQuantity quantity = null;
					string resultString = string.Empty;
					if (results.Length != 0) {
						resultString = results [results.Length - 1].ToString();
						lastRegistratedWalkingDistance = ParseStringResultToDouble(resultString);
						HealthKitDataContext.ActiveHealthKitData.DistanceReadings.TotalDistanceOfLastRecording = lastRegistratedWalkingDistance;
						Console.WriteLine(string.Format("value of Fetched: {0}", ParseStringResultToDouble(resultString)));
					}

				});
			m_healthKitStore.ExecuteQuery (query);
			Console.WriteLine(string.Format("Total walked last recording: ", lastRegistratedWalkingDistance));
			return lastRegistratedWalkingDistance;
		}



		public async Task<int> QueryLastRegistratedSteps()
		{
			var heightType = HKQuantityType.GetQuantityType (HKQuantityTypeIdentifierKey.StepCount);
			int lastRegistratedSteps = 0;

			var timeSortDescriptor = new NSSortDescriptor (HKSample.SortIdentifierEndDate, false);
			var query = new HKSampleQuery (heightType, new NSPredicate (IntPtr.Zero), 1, new NSSortDescriptor[] { timeSortDescriptor },
				(HKSampleQuery resultQuery, HKSample[] results, NSError error) => {

					HKQuantity quantity = null;
					string resultString = string.Empty;
					if (results.Length != 0) {
						resultString = results [results.Length - 1].ToString();
						lastRegistratedSteps = ParseStringResultToInteger(resultString);
						HealthKitDataContext.ActiveHealthKitData.DistanceReadings.TotalStepsOfLastRecording = lastRegistratedSteps;
					}

				});
			m_healthKitStore.ExecuteQuery (query);
			return lastRegistratedSteps;
		}

	

		public async Task<string> QueryTotalFlights()
		{
			var flightsCount = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.FlightsClimbed);
			var sumOptions = HKStatisticsOptions.CumulativeSum;
			string resultString = string.Empty;
			var query = new HKStatisticsQuery(flightsCount, new NSPredicate (IntPtr.Zero), sumOptions,(HKStatisticsQuery resultQuery, HKStatistics results, NSError error) => {
				if (results != null) {
					var quantitySample = results;
					var quantity = quantitySample.SumQuantity();

				    resultString = quantity.ToString();;
					HealthKitDataContext.ActiveHealthKitData.DistanceReadings.TotalFlightsClimed = quantity.ToString();
					Console.WriteLine(string.Format("totally walked {0} flights",quantity.ToString()));

				}

			});
			await Task.Factory.StartNew(() =>HealthKitStore.ExecuteQuery (query));
			return resultString;
		}

		public async Task<string> QueryDateOfBirth()
		{
			NSError error;
			string resultString = string.Empty;
			await Task.Factory.StartNew(() =>resultString = m_healthKitStore.GetDateOfBirth (out error).ToString ());
			HealthKitDataContext.ActiveHealthKitData.DateOfBirth = resultString;
			Console.WriteLine(resultString);
			return resultString;

		}

		public async Task<string> QueryBloodType()
		{
			NSError error;
			string resultString = string.Empty;
			await Task.Factory.StartNew(() =>resultString = m_healthKitStore.GetBloodType (out error).BloodType.ToString());
			HealthKitDataContext.ActiveHealthKitData.BloodType = ParseBloodTypeString(resultString);
			Console.WriteLine(ParseBloodTypeString(resultString));
			return ParseBloodTypeString(resultString);
		}

		private string ParseBloodTypeString(string bloodType)
		{
			switch (bloodType)
			{
			case("APositive"):
				return "A+";
			case("ANegative"):
				return "A-";
			case("BPositive"):
				return "B+";
			case("ABPositive"):
				return "AB+";
			case("ABNegative"):
				return "AB-";
			case("OPositive"):
				return "O+";
			case("ONegative"):
				return "O-";
			}
			return "null";
		}


		public async Task<string> QuerySex()
		{
			NSError error;
			string resultString = string.Empty;
			await Task.Factory.StartNew(() =>resultString = m_healthKitStore.GetBiologicalSex (out error).BiologicalSex.ToString());
			HealthKitDataContext.ActiveHealthKitData.Sex = resultString;
			Console.WriteLine(resultString);
			return resultString;
		}

		private double ParseStringResultToDouble(string result)
		{
			double res = 0;
			var resultAsArray = result.Split (null);
			bool tryParse = double.TryParse(resultAsArray[0], System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out res);

			if (tryParse)
				return res;
			else {
				return res;
			}
		}

		private int ParseStringResultToInteger(string result)
		{
			int res = 0;
			var resultAsArray = result.Split (null);
			bool tryParse = int.TryParse(resultAsArray[0], System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out res);

			if (tryParse)
				return res;
			else {
				return res;
			}
		}
	}

}
