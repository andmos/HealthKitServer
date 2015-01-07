using System;
using Xamarin.Forms;

namespace HealthKitServer
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			return new DistancePage (); 
		}
	}
}

