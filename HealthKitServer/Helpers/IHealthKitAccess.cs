using System;
using System.Threading.Tasks;

namespace HealthKitServer
{
	public interface IHealthKitAccess
	{
		void SetUpPermissions();
		Task<int> QueryTotalSteps();
		Task<string> QueryTotalStepsRecordingFirstRecordingDate ();
		Task<string> QueryTotalStepsRecordingLastRecordingDate ();
		Task<double> QueryTotalLengthWalked(); 
		Task<int> QueryTotalFlights();
		Task<double> QueryTotalHeight ();
		Task<string> QueryDateOfBirth();
		Task<string> QueryBloodType ();
		Task<string> QuerySex ();

		Task<double> QueryLastRegistratedWalkingDistance ();
		Task<int> QueryLastRegistratedSteps ();
		Task<int> QueryLastRegistratetHeartRate(); 

	}
}

