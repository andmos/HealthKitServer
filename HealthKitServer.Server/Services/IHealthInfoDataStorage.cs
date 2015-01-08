using System;
using System.Collections.Generic;

namespace HealthKitServer.Server
{
	public interface IHealthInfoDataStorage
	{
		IEnumerable<Person> GetAllPersons(); 
	}
}

