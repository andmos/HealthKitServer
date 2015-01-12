using System;
using System.Threading.Tasks;

namespace HealthKitServer
{
	public interface IHealthKitAccess
	{
		void SetUpPermissions();
		Task<string> QueryTotalSteps();
		Task<string> QueryTotalStepsRecordingFirstRecordingDate ();
		Task<string> QueryTotalStepsRecordingLastRecordingDate ();
		Task<string> QueryTotalLengthWalked(); 
		Task<string> QueryTotalFlights();
		Task<double> QueryTotalHeight ();
		Task<string> QueryDateOfBirth();
		Task<string> QueryBloodType ();
		Task<string> QuerySex ();

		Task<double> QueryLastRegistratedWalkingDistance ();
		Task<double> QueryLastRegistratedSteps ();

	}
}

