using System;
using SolrNet;
using System.Collections.Generic;
using System.Linq;

namespace HealthKitServer.Server
{
	public class HealthInfoDataSolrConnection : IHealthInfoDataStorage
	{
		private readonly ISolrOperations<Person> m_solrServer;

		public HealthInfoDataSolrConnection (string connectionString)
		{
			Startup.Init<Person>(connectionString);
		}

		public IEnumerable<Person> GetAllPersons ()
		{
			return m_solrServer.Query (new SolrQuery ("*:*")).ToArray();
		}

		public Person GetPatientHealthInfo (int id)
		{
			return m_solrServer.Query (new SolrQueryByField ("i", id.ToString ())).FirstOrDefault ();
		}

		public void AddOrUpdatePersonHealthInfoToStorage (Person person)
		{
			m_solrServer.Add (person);
		}


	}
}

