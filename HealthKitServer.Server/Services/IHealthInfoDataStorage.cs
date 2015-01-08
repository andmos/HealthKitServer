using System;
using System.Collections.Generic;

namespace HealthKitServer.Server
{
	public interface IHealthInfoDataStorage
	{
		IEnumerable<Person> GetAllPersons(); 
		Person GetPatientHealthInfo(int id);
		void AddOrUpdatePersonHealthInfoToStorage(Person person);

	}
}

