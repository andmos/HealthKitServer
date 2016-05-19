using System;

using HealthKitServer.Server;

namespace TestHealthKitServer.Server
{
	public class TestableLogFactory : ILogFactory
	{

		public HealthKitServer.Server.ILog GetLogger(Type type)
		{            
			return new TestLogger(); 
		}
	}
	public class TestLogger : ILog
	{
		
		public void Info (string message)
		{
			Console.WriteLine (message);
		}
		public void Debug (string message)
		{
			Console.WriteLine (message);
		}
		public void Error (string message, Exception exception = null)
		{
			Console.WriteLine (message);
		}

		
	}
}

