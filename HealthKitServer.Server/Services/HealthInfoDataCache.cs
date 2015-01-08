using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace HealthKitServer.Server
{
	public class HealthInfoDataCache : IHealthInfoDataStorage
	{
		private readonly ConcurrentDictionary<int, Person> m_storedHealthInfo; 

		public HealthInfoDataCache ()
		{
			m_storedHealthInfo = new ConcurrentDictionary<int, Person> (); 
		}

		public IEnumerable<Person> GetAllPersons ()
		{
			return m_storedHealthInfo.Values.ToArray (); 
		}

		public void AddOrUpdatePersonHealthInfoToStorage(Person person)
		{
			if (person.Id == null) 
			{
				person.Id = m_storedHealthInfo.Count + 1; 	
			}
			m_storedHealthInfo.AddOrUpdate(person.Id, person, (id, oldPersonHealthInfo) => person);
		}

		public Person GetPatientHealthInfo(int id)
		{
			Person personFromCache;
			return m_storedHealthInfo.TryGetValue (id, out personFromCache) ? personFromCache : new Person(); 
		}

	}
}

