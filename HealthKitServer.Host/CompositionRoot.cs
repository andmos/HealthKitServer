using System;
using HealthKitServer.Server;
using System.Configuration;

namespace HealthKitServer.Host
{
	public class CompositionRoot
	{
		public CompositionRoot ()
		{
			var container = Container.Instance = new SimpleContainer ();

			var dataStorage = ConfigurationManager.AppSettings["DataStorage"];
			var database = ConfigurationManager.AppSettings["Database"];

			switch (dataStorage) 
			{
			case "cache":
				container.RegisterSingleton<IHealthKitDataStorage>(new HealthKitDataCache ()); 
				break; 
			case "solr":
				var solrServerAddress = ConfigurationManager.AppSettings["SolrServerAddress"];
				container.RegisterSingleton<IHealthKitDataStorage> (new HealthKidDataSolrConnection (solrServerAddress));
				break;
			case "redis":
				var redisServerAddress = ConfigurationManager.AppSettings["RedisServerAddress"];
				container.RegisterSingleton<IHealthKitDataStorage> (new HealthKitDataRedisConnection (redisServerAddress));
				break;
			case "database": 
				if (database.ToLower().Equals("mysql")) 
				{
					var mysqlConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
					container.RegisterSingleton<IHealthKitDataStorage> (new HealthKitDataMysqlConnection (mysqlConnectionString));
				}
				if (database.ToLower().Equals("postgresql")) 
				{
					var postgresqlConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
					container.RegisterSingleton<IHealthKitDataStorage> (new HealthKitDataPostgresConnection (postgresqlConnectionString));
				}
				break;
			
			}
		}
	}
}
