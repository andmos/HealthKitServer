using System;
using SolrNet;
using System.Collections.Generic;
using System.Linq;

namespace HealthKitServer.Server
{
	public class HealthKidDataSolrConnection : IHealthKitDataStorage
	{
		private readonly ISolrOperations<HealthKitData> m_solrServer;

		public HealthKidDataSolrConnection (string connectionString)
		{
			Startup.Init<HealthKitData>(connectionString);
		}

		public IEnumerable<HealthKitData> GetAllHealthKitData ()
		{
			return m_solrServer.Query (new SolrQuery ("*:*")).ToArray();
		}

		public IEnumerable<HealthKitData> GetSpesificHealthKitData (int id)
		{
			throw new NotImplementedException ();
		}

		public HealthKitData GetSpesificHealthKitDataRecord(int personId, int recordId)
		{
			throw new NotImplementedException ();
		}

		public void AddOrUpdateHealthKitDataToStorage (HealthKitData person)
		{
			m_solrServer.Add (person);
		}


	}
}

