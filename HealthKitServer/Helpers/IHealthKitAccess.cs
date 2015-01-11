using System;
using System.Threading.Tasks;

namespace HealthKitServer
{
	public interface IHealthKitAccess
	{
		void SetUpPermissions();
		Task<string> GetTotalSteps();
		Task<string> GetTotalLengthWalked(); 
		Task<string> GetTotalFlights();
		Task<string> GetDateOfBirth();

	}
}

