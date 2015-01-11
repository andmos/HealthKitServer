using System;
using StackExchange.Redis;
using System.Collections.Generic;

namespace HealthKitServer.Server
{
	public class HealthKitInfoRedisConnection : IHealthInfoDataStorage 
	{

		private ConnectionMultiplexer m_redisServer;
		private IDatabase m_redisDatabase;

		public HealthKitInfoRedisConnection (string redisServer)
		{
			m_redisServer = ConnectionMultiplexer.Connect(redisServer);	
			m_redisDatabase = m_redisServer.GetDatabase ();
			var ping =  m_redisDatabase.Ping(); 
		}


		public IEnumerable<HealthKitData> GetAllPersons ()
		{
			throw new NotImplementedException ();
		}

		public HealthKitData GetPatientHealthInfo (int id)
		{
			throw new NotImplementedException ();
		}

		public void AddOrUpdatePersonHealthInfoToStorage (HealthKitData person)
		{
		//	m_redisDatabase.SetAdd (person.Id, person);
		}


	}
}

