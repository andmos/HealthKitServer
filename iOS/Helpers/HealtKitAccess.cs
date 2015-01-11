﻿using System;
using MonoTouch.HealthKit;
using MonoTouch.Foundation;
using Newtonsoft.Json;
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

		public async Task<string>  GetTotalSteps()
		{
			var stepsCount = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.StepCount);
			var sumOptions = HKStatisticsOptions.CumulativeSum;
			string resultString = string.Empty;
			var query = new HKStatisticsQuery(stepsCount, new NSPredicate (IntPtr.Zero), sumOptions, (HKStatisticsQuery resultQuery, HKStatistics results, NSError error) => {
				if (results != null) {
					var quantitySample = results;
					var quantity =  quantitySample.SumQuantity();
					resultString = quantity.ToString();
					Console.WriteLine(string.Format("totally walked {0} steps",quantity.ToString()));
				}

			});
			await Task.Factory.StartNew(() =>HealthKitStore.ExecuteQuery (query));
			return resultString;
		}

		public async Task<string> GetTotalLengthWalked()
		{
			var stepsCount = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.DistanceWalkingRunning);
			var sumOptions = HKStatisticsOptions.CumulativeSum;
			string resultString = string.Empty;
			var query = new HKStatisticsQuery(stepsCount, new NSPredicate (IntPtr.Zero), sumOptions,(HKStatisticsQuery resultQuery, HKStatistics results, NSError error) => {
				if (results != null) {
					var quantitySample = results;
					var quantity = quantitySample.SumQuantity();
					resultString = quantity.ToString();;
					Console.WriteLine(string.Format("totally walked {0}",quantity.ToString()));

				}

			});
			await Task.Factory.StartNew(() => HealthKitStore.ExecuteQuery (query));
			return resultString;
		}

		public async Task<string> GetTotalFlights()
		{
			var flightsCount = HKObjectType.GetQuantityType (HKQuantityTypeIdentifierKey.FlightsClimbed);
			var sumOptions = HKStatisticsOptions.CumulativeSum;
			string resultString = string.Empty;
			var query = new HKStatisticsQuery(flightsCount, new NSPredicate (IntPtr.Zero), sumOptions,(HKStatisticsQuery resultQuery, HKStatistics results, NSError error) => {
				if (results != null) {
					var quantitySample = results;
					var quantity = quantitySample.SumQuantity();

					resultString = quantity.ToString();;

					Console.WriteLine(string.Format("totally walked {0} flights",quantity.ToString()));

				}

			});
			await Task.Factory.StartNew(() =>HealthKitStore.ExecuteQuery (query));
			return resultString;
		}

		public async Task<string> GetDateOfBirth()
		{
			NSError error;
			string resultString = string.Empty;
			await Task.Factory.StartNew(() =>resultString = m_healthKitStore.GetDateOfBirth (out error).ToString ());
			return resultString;
			Console.WriteLine(m_healthKitStore.GetDateOfBirth(out error));
		}
	}
		
}

