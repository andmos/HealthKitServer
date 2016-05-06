using System;

namespace HealthKitServer.Server
{
	public class Log : ILog
	{
		private readonly Action<string> logDebug;
		private readonly Action<string, Exception> logError;
		private readonly Action<string> logInfo;

		public Log(Action<string> logInfo, Action<string> logDebug, Action<string, Exception> logError)
		{
			this.logInfo = logInfo;
			this.logDebug = logDebug;
			this.logError = logError;
		}

		public void Info(string message)
		{
			logInfo(message);
		}

		public void Debug(string message)
		{
			logDebug(message);
		}

		public void Error(string message, Exception exception = null)
		{
			logError(message, exception);
		}
	}
}

