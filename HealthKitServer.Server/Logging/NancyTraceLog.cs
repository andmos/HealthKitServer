using System;

namespace HealthKitServer.Server
{
	public class NancyTraceLog :ILog
	{
		private readonly ILog log;

		public NancyTraceLog(ILog log)
		{
			this.log = log;
		}

		public void Info(string message)
		{
			CurrentNancyContext.GetCurrentContext().Trace.TraceLog.WriteLog((sb) => sb.AppendLine(message));
			log.Info(message);
		}

		public void Debug(string message)
		{
			CurrentNancyContext.GetCurrentContext().Trace.TraceLog.WriteLog((sb) => sb.AppendLine(message));
			log.Debug(message);
		}

		public void Error(string message, Exception exception = null)
		{
			CurrentNancyContext.GetCurrentContext().Trace.TraceLog.WriteLog((sb) => sb.AppendLine(message));
			log.Error(message, exception);
		}
	}
}

