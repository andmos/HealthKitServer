using System;
using HealthKitServer;
using System.Threading.Tasks;

namespace TestHealthKitServer.HealthKitServer
{
	public class TestableHealthKitDataAccess : IHealthKitAccess  
	{
		public TestableHealthKitDataAccess ()
		{
		}

		public void SetUpPermissions ()
		{
			return;
		}

		public Task<string> QueryTotalSteps ()
		{
			return Task<string>.Factory.StartNew(() => "50000");
		}

		public Task<string> QueryTotalStepsRecordingFirstRecordingDate ()
		{
			return Task<string>.Factory.StartNew(() => "01.01.2011");
		}

		public Task<string> QueryTotalStepsRecordingLastRecordingDate ()
		{
			return Task<string>.Factory.StartNew(() => "01.01.2012");
		}

		public Task<string> QueryTotalLengthWalked ()
		{
			return Task<string>.Factory.StartNew(() => "10000");
		}

		public Task<string> QueryTotalFlights ()
		{
			return Task<string>.Factory.StartNew(() => "3000");
		}

		public Task<double> QueryTotalHeight ()
		{
			return Task<double>.Factory.StartNew(() => 1.74);
		}

		public Task<string> QueryDateOfBirth ()
		{
			return Task<string>.Factory.StartNew(() => "22.04.1990");
		}

		public Task<string> QueryBloodType ()
		{
			return Task<string>.Factory.StartNew(() => "A+");
		}

		public Task<string> QuerySex ()
		{
			return Task<string>.Factory.StartNew(() => "Male");
		}

		public Task<double> QueryLastRegistratedWalkingDistance ()
		{
			return Task<double>.Factory.StartNew(() => 5);
		}

		public Task<int> QueryLastRegistratedSteps ()
		{
			return Task<int>.Factory.StartNew(() => 20);
		}

		public Task<int> QueryLastRegistratetHeartRate ()
		{
			return Task<int>.Factory.StartNew(() => 85);
		}
	}
}

