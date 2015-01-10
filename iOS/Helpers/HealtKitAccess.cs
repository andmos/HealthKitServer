using System;
using MonoTouch.HealthKit;
using MonoTouch.Foundation;
using Newtonsoft.Json;

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
			var dateOfBirthCharacteristicType = HKObjectType.GetCharacteristicType (HKCharacteristicTypeIdentifierKey.DateOfBirth);
			var sexCharacteristicType = HKObjectType.GetCharacteristicType (HKCharacteristicTypeIdentifierKey.BiologicalSex);
			var bloodTypeCharacteristicType = HKObjectType.GetCharacteristicType (HKCharacteristicTypeIdentifierKey.BloodType);

			if (m_healthKitStore == null) 
			{
				HealthKitStore = new HKHealthStore (); 
				m_healthKitStore.RequestAuthorizationToShare (new NSSet (new [] { distanceQuantityType , stepsQuantityType , flightsQuantityType  }), new NSSet (new [] {  (NSObject) distanceQuantityType ,(NSObject)  stepsQuantityType , (NSObject) flightsQuantityType , (NSObject)  heightQuantityType , (NSObject)dateOfBirthCharacteristicType, (NSObject) sexCharacteristicType, (NSObject) bloodTypeCharacteristicType }), (success, error) => {
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

		public void GetTotalSteps(HealthKitData dataobject)
		{
			var stepsCount = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.StepCount);
			var sumOptions = HKStatisticsOptions.CumulativeSum;
			var query = new HKStatisticsQuery(stepsCount, new NSPredicate (IntPtr.Zero), sumOptions,(HKStatisticsQuery resultQuery, HKStatistics results, NSError error) => {
				if (results != null) {
					var quantitySample = results;
					var quantity = quantitySample.SumQuantity();
					dataobject.DistanceReadings.TotalSteps = quantity;
					Console.WriteLine(string.Format("totally walked {0} steps",quantity.ToString()));
				}

			});
			HealthKitStore.ExecuteQuery (query);
		}

		public void GetTotalLengthWalked(HealthKitData dataobject)
		{
			var stepsCount = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.DistanceWalkingRunning);
			var sumOptions = HKStatisticsOptions.CumulativeSum;
			var query = new HKStatisticsQuery(stepsCount, new NSPredicate (IntPtr.Zero), sumOptions,(HKStatisticsQuery resultQuery, HKStatistics results, NSError error) => {
				if (results != null) {
					var quantitySample = results;
					var quantity = quantitySample.SumQuantity();
					dataobject.DistanceReadings.TotalDistance = quantity;
					Console.WriteLine(string.Format("totally walked {0}",quantity.ToString()));

				}

			});
			 HealthKitStore.ExecuteQuery (query);
		}

		public void GetTotalFlights(HealthKitData dataobject)
		{
			var flightsCount = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.FlightsClimbed);
			var sumOptions = HKStatisticsOptions.CumulativeSum;
			var query = new HKStatisticsQuery(flightsCount, new NSPredicate (IntPtr.Zero), sumOptions,(HKStatisticsQuery resultQuery, HKStatistics results, NSError error) => {
				if (results != null) {
					var quantitySample = results;
					var quantity = quantitySample.SumQuantity();

					dataobject.DistanceReadings.TotalFlightsClimed = quantity;

					Console.WriteLine(string.Format("totally walked {0} flights",quantity.ToString()));
					Console.WriteLine(m_healthKitStore.GetDateOfBirth(out error));
				}

			});
			HealthKitStore.ExecuteQuery (query);
		}

		public void GetDateOfBirth(HealthKitData dataobject)
		{
			NSError error;
			dataobject.DateOfBirth = m_healthKitStore.GetDateOfBirth (out error);
		}
	}
		
}

