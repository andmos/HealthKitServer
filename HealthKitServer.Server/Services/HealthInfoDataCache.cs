using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace HealthKitServer.Server
{
	public class HealthInfoDataCache : IHealthInfoDataStorage
	{
		private readonly ConcurrentDictionary<int, List<HealthKitData>> m_storedHealthInfo; 

		public HealthInfoDataCache ()
		{
			m_storedHealthInfo = new ConcurrentDictionary<int, List<HealthKitData>> (); 
		}

		public IEnumerable<HealthKitData> GetAllPersons ()
		{
			return m_storedHealthInfo.Values.SelectMany (d => d).ToList (); 
		}

		public void AddOrUpdatePersonHealthInfoToStorage(HealthKitData person)
		{
			List<HealthKitData> healthKitData = new List<HealthKitData> ();
			if (person.PersonId == 0) 
			{
				person.PersonId = m_storedHealthInfo.Count + 1; 
				healthKitData.Add (person);
			}
			if (GetPatientHealthInfo (person.PersonId) != null) 
			{
				healthKitData = GetPatientHealthInfo (person.PersonId).ToList ();
				person.RecordingId = healthKitData.Count + 1;
				healthKitData.Add (person);
			}
			
			m_storedHealthInfo.AddOrUpdate(person.PersonId, healthKitData, (id, oldPersonHealthInfo) => healthKitData);
		}

		public IEnumerable<HealthKitData> GetPatientHealthInfo(int id)
		{
			List<HealthKitData> personFromCache;
			return m_storedHealthInfo.TryGetValue (id, out personFromCache) ? personFromCache : null; 
		}

	}
}

