using System;

namespace HealthKitServer
{
	public class DistanceSummaryViewModel : ObservableBase 
	{
		private string m_totalSteps; 
		private string m_totalDistance;
		private IHealthKitAccess m_healthKitAccess; 


		public DistanceSummaryViewModel ()
		{
			m_healthKitAccess = Container.Singleton<IHealthKitAccess> (); 
			m_healthKitAccess.SetUpPermissions (); 
			m_healthKitAccess.GetTotalSteps ();
			m_healthKitAccess.GetTotalLengthWalked ();
		}

		public string TotalSteps
		{
			get { return m_totalSteps; }
			set { this.SetPropertyValue (ref m_totalSteps, value); }
		}

		public string TotalDistance
		{
			get { return m_totalDistance; }
			set { this.SetPropertyValue (ref m_totalDistance, value); }
		}

	}
}

