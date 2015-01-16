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

		public IEnumerable<HealthKitData> GetAllHealthKitData ()
		{
			return m_storedHealthInfo.Values.SelectMany (d => d).ToList (); 
		}

		public void AddOrUpdateHealthKitDataToStorage(HealthKitData person)
		{
			List<HealthKitData> healthKitData = new List<HealthKitData> ();
			if (person.PersonId == 0) 
			{
				person.PersonId = m_storedHealthInfo.Count + 1; 
				healthKitData.Add (person);
			}
			if (GetSpesificHealthKitData (person.PersonId) != null) 
			{
				healthKitData = GetSpesificHealthKitData (person.PersonId).ToList ();
				person.RecordingId = healthKitData.Count + 1;
				healthKitData.Add (person);
			}
			
			m_storedHealthInfo.AddOrUpdate(person.PersonId, healthKitData, (id, oldPersonHealthInfo) => healthKitData);
		}

		public IEnumerable<HealthKitData> GetSpesificHealthKitData(int id)
		{
			List<HealthKitData> personFromCache;
			return m_storedHealthInfo.TryGetValue (id, out personFromCache) ? personFromCache : Enumerable.Empty<HealthKitData>(); 
		}

	}
}

