using System;
using StackExchange.Redis;
using System.Collections.Generic;

namespace HealthKitServer.Server
{
	public class HealthKitDataRedisConnection : IHealthKitDataStorage 
	{

		private ConnectionMultiplexer m_redisServer;
		private IDatabase m_redisDatabase;

		public HealthKitDataRedisConnection (string redisServer)
		{
			m_redisServer = ConnectionMultiplexer.Connect(redisServer);	
			m_redisDatabase = m_redisServer.GetDatabase ();
			var ping =  m_redisDatabase.Ping(); 
		}


		public IEnumerable<HealthKitData> GetAllHealthKitData ()
		{
			throw new NotImplementedException ();
		}

		public IEnumerable<HealthKitData> GetSpesificHealthKitData (int id)
		{
			throw new NotImplementedException ();
		}

		public void AddOrUpdateHealthKitDataToStorage (HealthKitData person)
		{
		//	m_redisDatabase.SetAdd (person.Id, person);
		}


	}
}

