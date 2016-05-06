using System;
using log4net.Config;
using log4net;

namespace HealthKitServer.Server
{
	public class Log4NetLogFactory : ILogFactory
	{
		public Log4NetLogFactory()
		{
			XmlConfigurator.Configure();           
		}

		public ILog GetLogger(Type type)
		{            
			var logger = LogManager.GetLogger(type);            
			return new NancyTraceLog (new Log(logger.Info, logger.Debug, logger.Error));
		}
	}
}

