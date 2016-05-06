using System;
using log4net.Config;
using log4net;
using HealthKitServer.Server;

namespace HealthKitServer.Host
{
	public class Log4NetLogFactory : ILogFactory
	{
		public Log4NetLogFactory()
		{
			XmlConfigurator.Configure();           
		}

		public HealthKitServer.Server.ILog GetLogger(Type type)
		{            
			var logger = LogManager.GetLogger(type);            
			return new NancyTraceLog (new Log(logger.Info, logger.Debug, logger.Error));
		}
	}
}

