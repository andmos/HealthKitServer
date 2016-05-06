using System;

namespace HealthKitServer.Server
{
	public interface ILogFactory
	{
		ILog GetLogger(Type type);
	}
}

