using System;
using MySql.Data.MySqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace HealthKitServer.Server
{
	public class HealthKitDataMysqlConnection : IHealthKitDataStorage
	{
		private readonly string m_connectionString;
		public HealthKitDataMysqlConnection (string connectionString) 
		{
			m_connectionString = connectionString;
		}

		public IEnumerable<HealthKitData> GetAllHealthKitData ()
		{
			using (var connection = new MySqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString;
					connection.Open();
					return  Enumerable.Empty<HealthKitData>(); 
				}
				catch(MySql.Data.MySqlClient.MySqlException e)
				{
					Console.WriteLine (e);
				}
			}
			return  Enumerable.Empty<HealthKitData>();
		}

		public IEnumerable<HealthKitData> GetSpesificHealthKitData (int id)
		{
			throw new NotImplementedException ();
		}

		public void AddOrUpdateHealthKitDataToStorage (HealthKitData person)
		{
			var sql = @"INSERT INTO HealthKitData(PersonId, RecordingId,RecordingTimeStamp,BloodType,DateOfBirth,Sex,Height)
						VALUES (@PersonId, @RecordingId, @RecordingTimeStamp,@BloodType,@DateOfBirth,@Sex,@Height);";

			using (var connection = new MySqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString;
					connection.Open(); 
					connection.Execute(sql,person);

				}
				catch(MySql.Data.MySqlClient.MySqlException e)
				{
					Console.WriteLine (e.ToString ());
				}
				finally
				{
					connection.Close();
				}
			}
		}
			
	}
}

