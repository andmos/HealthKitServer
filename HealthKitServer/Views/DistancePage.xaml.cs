using System;
using System.Collections.Generic;
using Xamarin.Forms;
using HealthKitServer.ViewModels;

namespace HealthKitServer
{	
	public partial class DistancePage : ContentPage
	{	
		public DistancePage ()
		{ 
			InitializeComponent ();
			BindingContext = new DistanceSummaryViewModel (Container.Resolve<IHealthKitDataWebService>());
		}
	}
}

