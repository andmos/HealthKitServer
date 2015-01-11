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

			if(dataStorage.ToLower().Equals("cache"))
			{
				container.RegisterSingleton<IHealthInfoDataStorage>(new HealthInfoDataCache());
			}
			if (dataStorage.ToLower ().Equals ("solr")) 
			{
				var solrServerAddress = ConfigurationManager.AppSettings["SolrServerAddress"];
				container.RegisterSingleton<IHealthInfoDataStorage> (new HealthInfoDataSolrConnection (solrServerAddress));	
			}
			if (dataStorage.ToLower ().Equals ("redis")) 
			{
				var redisServerAddress = ConfigurationManager.AppSettings["RedisServerAddress"];
				container.RegisterSingleton<IHealthInfoDataStorage> (new HealthKitInfoRedisConnection (redisServerAddress));
			}
		}
	}
}

