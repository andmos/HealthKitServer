using System;
using MonoTouch.HealthKit;
using MonoTouch.Foundation;

namespace HealthKitServer
{
	public class HealtKitAccess
	{
		private HKHealthStore m_healthKitStore;


		public HealtKitAccess ()
		{
			var distanceWalked = HKQuantityTypeIdentifierKey.DistanceWalkingRunning;
			var tempQuantityType = HKObjectType.GetQuantityType (distanceWalked);

			if (m_healthKitStore == null) 
			{
				HealthKitStore = new HKHealthStore (); 
				m_healthKitStore.RequestAuthorizationToShare (new NSSet (new [] { tempQuantityType }), new NSSet (), (success, error) => {
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
	}
		
}

