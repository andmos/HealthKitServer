using System;
using System.Windows.Input;

namespace HealthKitServer
{
	public class DistanceSummaryViewModel : ObservableBase 
	{
		private HealthKitDataDecorator m_healthKitDataDecorator; 
		private HealthKitData m_healthKitdataObject; 
		private IHealthKitDataWebService m_healthKitDataUploader; 
		private bool m_isDecorated;
		private string m_healthKitServerPostAPIAddress = "http://apollo.amosti.net:5002/api/v1/addPatient";
		private string m_healthKitServerGetAPIAddress = "http://apollo.amosti.net:5002/api/v1/getpatient?id=";
		private ICommand m_uploadCommand;

		public DistanceSummaryViewModel ()
		{
			m_healthKitdataObject = new HealthKitData {Id = 3, DistanceReadings = new DistanceReading{}};
			m_healthKitDataDecorator = new HealthKitDataDecorator (Container.Singleton<IHealthKitAccess> (), m_healthKitdataObject);
			StartDecoration ();
			m_healthKitDataUploader = Container.Resolve<IHealthKitDataWebService> ();
			m_uploadCommand = new DelegateCommand (UploadDataToHealthKitServer, () => true);

		}

		public ICommand UploadCommand 
		{
			get{ return m_uploadCommand; }
			private set{this.SetPropertyValue (ref m_uploadCommand, value); }
		}

		public string HealthKitServerAddress
		{
			get { return m_healthKitServerPostAPIAddress; }
			set { this.SetPropertyValue (ref m_healthKitServerPostAPIAddress, value); }
		}

		public bool IsDecorated
		{
			get{return m_isDecorated; }
			set{ this.SetPropertyValue (ref m_isDecorated, value);
				NotifyPropertyChanged ("IsDecorated");}
		}

		private async void StartDecoration()
		{
			IsDecorated = await m_healthKitDataDecorator.DecorateHealthKitData();
			NotifyPropertyChanged ("IsDecorated");
		}

		private void UploadDataToHealthKitServer(object o = null)
		{
			var uploaded = m_healthKitDataUploader.UploadHealthKitDataToHealthKitServer (m_healthKitServerPostAPIAddress, m_healthKitdataObject);
		}
	}
}

