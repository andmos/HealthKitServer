using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HealthKitServer
{	
	public partial class DistancePage : ContentPage
	{	
		public DistancePage ()
		{ 
			InitializeComponent ();
			BindingContext = new DistanceSummaryViewModel ();
		}
	}
}

