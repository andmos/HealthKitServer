using System;
using HealthKitServer.Server;
using System.Configuration;
using LightInject;

namespace HealthKitServer.Host
{
	public class CompositionRoot : ICompositionRoot
	{
		public void Compose (IServiceRegistry serviceRegistry)
		{
			serviceRegistry.RegisterFrom<HealthKitServer.Server.ServerCompositionRoot> (); 

			var dataStorage = ConfigurationManager.AppSettings["DataStorage"];
			var database = ConfigurationManager.AppSettings["Database"];
			switch (dataStorage) 
			{
				case "cache":
				serviceRegistry.Register<IHealthKitDataStorage>(factory => new HealthKitDataCache (), new PerContainerLifetime()); 
					break; 
				case "solr":
					var solrServerAddress = ConfigurationManager.AppSettings["SolrServerAddress"];
				serviceRegistry.Register<IHealthKitDataStorage>(factory => new HealthKidDataSolrConnection (solrServerAddress), new PerContainerLifetime());
					break;
				case "redis":
					var redisServerAddress = ConfigurationManager.AppSettings["RedisServerAddress"];
				serviceRegistry.Register<IHealthKitDataStorage>(factory => new HealthKitDataRedisConnection (redisServerAddress), new PerContainerLifetime());
					break;
				case "database": 
					if (database.ToLower().Equals("mysql")) 
					{
						var mysqlConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
					serviceRegistry.Register<IHealthKitDataStorage>(factory => new HealthKitDataMysqlConnection (mysqlConnectionString), new PerContainerLifetime());
					}
					if (database.ToLower().Equals("postgresql")) 
					{
						var postgresqlConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
					serviceRegistry.Register<IHealthKitDataStorage>(factory => new HealthKitDataPostgresConnection (postgresqlConnectionString), new PerContainerLifetime());
					}
					break;
			
			}
		}
	}
}
