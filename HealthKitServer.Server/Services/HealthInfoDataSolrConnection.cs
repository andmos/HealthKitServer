using System;
using SolrNet;
using System.Collections.Generic;
using System.Linq;

namespace HealthKitServer.Server
{
	public class HealthInfoDataSolrConnection : IHealthInfoDataStorage
	{
		private readonly ISolrOperations<HealthKitData> m_solrServer;

		public HealthInfoDataSolrConnection (string connectionString)
		{
			Startup.Init<HealthKitData>(connectionString);
		}

		public IEnumerable<HealthKitData> GetAllPersons ()
		{
			return m_solrServer.Query (new SolrQuery ("*:*")).ToArray();
		}

		public IEnumerable<HealthKitData> GetPatientHealthInfo (int id)
		{
			throw new NotImplementedException ();
		}

		public void AddOrUpdatePersonHealthInfoToStorage (HealthKitData person)
		{
			m_solrServer.Add (person);
		}


	}
}

