using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace HealthKitServer.Server
{
	public class HealthKitDataCache : IHealthKitDataStorage
	{
		private readonly ConcurrentDictionary<int, List<HealthKitData>> m_storedHealthInfo; 

		public HealthKitDataCache ()
		{
			m_storedHealthInfo = new ConcurrentDictionary<int, List<HealthKitData>> (); 
		}

		public IEnumerable<HealthKitData> GetAllHealthKitData ()
		{
			return m_storedHealthInfo.Values.SelectMany (d => d).ToList (); 
		}

		public void AddOrUpdateHealthKitDataToStorage(HealthKitData record)
		{
			List<HealthKitData> healthKitDataRecords = new List<HealthKitData> ();
			if (record.PersonId == 0) 
			{
				record.PersonId = m_storedHealthInfo.Count + 1; 
				healthKitDataRecords.Add (record);
			}
			if (GetSpesificHealthKitData (record.PersonId) != null) 
			{
				healthKitDataRecords = GetSpesificHealthKitData (record.PersonId).ToList ();
				record.RecordId = healthKitDataRecords.Count + 1;
				healthKitDataRecords.Add (record);
			}
			
			m_storedHealthInfo.AddOrUpdate(record.PersonId, healthKitDataRecords, (id, oldPersonHealthInfo) => healthKitDataRecords);
		}

		public IEnumerable<HealthKitData> GetSpesificHealthKitData(int id)
		{
			List<HealthKitData> personFromCache;
			return m_storedHealthInfo.TryGetValue (id, out personFromCache) ? personFromCache : Enumerable.Empty<HealthKitData>(); 
		}

	}
}