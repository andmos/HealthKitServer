using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HealthKitServer.Server
{
	public class HealthKitDataStorageProfiler : IHealthKitDataStorage
	{

		private IHealthKitDataStorage m_dataStorage; 
		private ILog m_logger; 

		public HealthKitDataStorageProfiler (IHealthKitDataStorage dataStorage, ILog logger)
		{
			m_dataStorage = dataStorage; 
			m_logger = logger; 
		}
			

		public IEnumerable<HealthKitData> GetAllHealthKitData ()
		{
			var stopWatch = Stopwatch.StartNew ();
			var allRecords = m_dataStorage.GetAllHealthKitData (); 
			stopWatch.Stop (); 
			m_logger.Info (string.Format ("{0} GetAllHealthKitData: Call took {1} Ms", m_dataStorage.GetType(), stopWatch.ElapsedMilliseconds));
			return allRecords;
		}

		public IEnumerable<HealthKitData> GetSpesificHealthKitData (int personId)
		{
			var stopWatch = Stopwatch.StartNew ();
			var recordFromId = m_dataStorage.GetSpesificHealthKitData (personId);
			stopWatch.Stop (); 
			m_logger.Info (string.Format ("{0} GetSpesificHealthKitData: Call took {1} Ms", m_dataStorage.GetType(), stopWatch.ElapsedMilliseconds));
			return recordFromId;
		}

		public HealthKitData GetSpesificHealthKitDataRecord (int personId, int recordId)
		{
			var stopWatch = Stopwatch.StartNew ();
			var record = m_dataStorage.GetSpesificHealthKitDataRecord (personId, recordId);
			stopWatch.Stop (); 
			m_logger.Info (string.Format ("{0} GetSpesificHealthKitDataRecord: Call took {1} Ms", m_dataStorage.GetType(), stopWatch.ElapsedMilliseconds));
			return record;
		}

		public void AddOrUpdateHealthKitDataToStorage (HealthKitData person)
		{
			var stopWatch = Stopwatch.StartNew ();
			m_dataStorage.AddOrUpdateHealthKitDataToStorage (person);
			stopWatch.Stop ();
			m_logger.Info (string.Format ("{0} AddOrUpdateHealthKitDataToStorage: Call took {1} Ms", m_dataStorage.GetType(), stopWatch.ElapsedMilliseconds));
		}
			
	}
}

