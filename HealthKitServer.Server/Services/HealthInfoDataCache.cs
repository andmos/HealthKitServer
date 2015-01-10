using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace HealthKitServer.Server
{
	public class HealthInfoDataCache : IHealthInfoDataStorage
	{
		private readonly ConcurrentDictionary<int, HealthKitData> m_storedHealthInfo; 

		public HealthInfoDataCache ()
		{
			m_storedHealthInfo = new ConcurrentDictionary<int, HealthKitData> (); 
		}

		public IEnumerable<HealthKitData> GetAllPersons ()
		{
			return m_storedHealthInfo.Values.ToArray (); 
		}

		public void AddOrUpdatePersonHealthInfoToStorage(HealthKitData person)
		{
			if (person.Id == 0) 
			{
				person.Id = m_storedHealthInfo.Count + 1; 	
			}
			m_storedHealthInfo.AddOrUpdate(person.Id, person, (id, oldPersonHealthInfo) => person);
		}

		public HealthKitData GetPatientHealthInfo(int id)
		{
			HealthKitData personFromCache;
			return m_storedHealthInfo.TryGetValue (id, out personFromCache) ? personFromCache : new HealthKitData(); 
		}

	}
}

