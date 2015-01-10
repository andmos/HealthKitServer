using System;

namespace HealthKitServer
{
	public class DistanceSummaryViewModel : ObservableBase 
	{
		private HealthKitDataDecorator m_healthKitDataDecorator; 
		private HealthKitData m_healthKitdataObject; 
		private IHealthKitDataUploader m_healthKitDataUploader; 
		private string m_healthKitServerAddress;

		public DistanceSummaryViewModel ()
		{
			m_healthKitdataObject = new HealthKitData ();
			m_healthKitDataDecorator = new HealthKitDataDecorator (Container.Singleton<IHealthKitAccess> (), m_healthKitdataObject);
			m_healthKitDataUploader = Container.Resolve<IHealthKitDataUploader> ();

			var uploaded = m_healthKitDataUploader.UploadHealthKitDataToHealthKitServer(m_healthKitServerAddress, m_healthKitdataObject);
		}

		public string HealthKitServerAddress
		{
			get { return m_healthKitServerAddress; }
			set { this.SetPropertyValue (ref m_healthKitServerAddress, value); }
		}

	}
}

