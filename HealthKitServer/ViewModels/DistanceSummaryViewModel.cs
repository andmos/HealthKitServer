﻿using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace HealthKitServer
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
		private ICommand m_uploadCommand;
		private ICommand m_GetDataCommand; 

		public DistanceSummaryViewModel ()
		{
			m_healthKitdataObject = new HealthKitData {PersonId = 3, DistanceReadings = new DistanceReading{}, Device = Container.Resolve<IDevice>().Device};
			m_healthKitDataDecorator = new HealthKitDataDecorator (Container.Singleton<IHealthKitAccess> (), m_healthKitdataObject);
			StartDecoration ();
			m_healthKitDataWebService = Container.Resolve<IHealthKitDataWebService> ();
			m_healthKitDataFromServer = new ObservableCollection<HealthKitData> ();
			m_uploadCommand = new DelegateCommand (UploadDataToHealthKitServer, () => true);
			m_GetDataCommand = new DelegateCommand (GetDataFromHealthKitServer, () => true);
		}

		public ICommand UploadCommand 
		{
			get{ return m_uploadCommand; }
			private set{this.SetPropertyValue (ref m_uploadCommand, value); }
		}

		public ICommand GetDataCommand 
		{
			get{ return m_GetDataCommand; }
			private set{this.SetPropertyValue (ref m_GetDataCommand, value); }
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
			var uploaded = m_healthKitDataWebService.UploadHealthKitDataToHealthKitServer (m_healthKitServerPostAPIAddress, m_healthKitdataObject);
		}

		private void GetDataFromHealthKitServer(object o = null)
		{
			var response = m_healthKitDataWebService.GetHealtKitDataFromHealthKitServer (m_healthKitServerGetAPIAddress, 3);
			if (response != null) 
			{
				
				//too lazy to RisePropertyChanged for now.
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

