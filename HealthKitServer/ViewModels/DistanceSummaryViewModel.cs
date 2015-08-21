using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace HealthKitServer.ViewModels
{
	public class DistanceSummaryViewModel : ObservableBase 
	{
		private HealthKitDataDecorator m_healthKitDataDecorator; 
		private HealthKitData m_healthKitdataObject; 
		private IHealthKitDataWebService m_healthKitDataWebService; 
		private ObservableCollection<HealthKitData> m_healthKitDataFromServer; 
		private bool m_isDecorated;
		private string m_healthKitServerPostAPIAddress = "http://apollo.amosti.net:5002/api/v1/addHealthKitData";
		private string m_healthKitServerGetAPIAddress = "http://apollo.amosti.net:5002/api/v1/gethealthkitdata?id=";
		private RelayCommand m_uploadCommand;
		private ICommand m_GetDataCommand; 
		private ICommand m_reloadCommand; 

		public DistanceSummaryViewModel (IHealthKitDataWebService healthKitDataWebService)
		{
			m_healthKitdataObject = new HealthKitData {PersonId = 3, DistanceReadings = new DistanceReading{}, HeartRateReadings = new HeartRateReading{}, Device = Container.Resolve<IDevice>().Device};
			m_healthKitDataDecorator = new HealthKitDataDecorator (Container.Singleton<IHealthKitAccess> (), m_healthKitdataObject);
			StartDecoration ();
			m_healthKitDataWebService = healthKitDataWebService; 
			m_healthKitDataFromServer = new ObservableCollection<HealthKitData> ();
			m_uploadCommand = new RelayCommand (() => UploadDataToHealthKitServer (),() => true); // => CanUploadHealthKitDataToServer());
			m_GetDataCommand = new DelegateCommand (GetDataFromHealthKitServer, () => true);
			m_reloadCommand = new DelegateCommand (StartDecoration, () => true); 
		}

		public RelayCommand UploadCommand 
		{
			get{ return m_uploadCommand; }
			private set{this.SetPropertyValue (ref m_uploadCommand, value); }
		}

		public ICommand GetDataCommand 
		{
			get{ return m_GetDataCommand; }
			private set{this.SetPropertyValue (ref m_GetDataCommand, value); }
		}

		public ICommand ReloadCommand
		{
			get{ return m_reloadCommand; }
			private set{ this.SetPropertyValue (ref m_reloadCommand, value); }
		}

		public ObservableCollection<HealthKitData> HealthKitDataFromServer
		{
			get{ return m_healthKitDataFromServer; }
			private set{ this.SetPropertyValue (ref m_healthKitDataFromServer, value); }
		}

		public string HealthKitServerAddress
		{
			get { return m_healthKitServerPostAPIAddress; }
			set { this.SetPropertyValue (ref m_healthKitServerPostAPIAddress, value); }
		}

		public bool IsDecorating
		{
			get{return m_isDecorated; }
			set{ this.SetPropertyValue (ref m_isDecorated, value); }
		}

		private async void StartDecoration(object o = null)
		{
			IsDecorating = true; 
			await m_healthKitDataDecorator.DecorateHealthKitData().ContinueWith(SetDecoratoting);
		}

		private void SetDecoratoting(Task<bool> obj)
		{
			IsDecorating = false; 
		}

		private void UploadDataToHealthKitServer(object o = null)
		{
			var uploaded = m_healthKitDataWebService.UploadHealthKitDataToHealthKitServer (m_healthKitServerPostAPIAddress, m_healthKitdataObject);
		}

		private bool CanUploadHealthKitDataToServer()
		{
			if (!IsDecorating) 
			{
				return true; 
			} 
			return false; 
		}

		private void GetDataFromHealthKitServer(object o = null)
		{
			var response = m_healthKitDataWebService.GetHealtKitDataFromHealthKitServer (m_healthKitServerGetAPIAddress, 3);
			if (response != null) 
			{
				if(m_healthKitDataFromServer.Any())
				{
					// till next Xamarin Update
					int temp = m_healthKitDataFromServer.Count;
					for (int i = 0; i < temp; i++) {
						m_healthKitDataFromServer.RemoveAt (0);
					};
					//m_healthKitDataFromServer.Clear ();
				}
				foreach (var data in response.ToList()) 
				{
					m_healthKitDataFromServer.Add (data);
				}
			}
		}
			
	
	}
}