using System;
using System.Linq;
using System.Collections.Generic;
using Npgsql;
using Dapper;


namespace HealthKitServer.Server
{
	public class HealthKitDataPostgresConnection : IHealthKitDataStorage
	{
		private readonly string m_connectionString;

		public HealthKitDataPostgresConnection (string connectionString) 
		{
			m_connectionString = connectionString;

		}
		

		public IEnumerable<HealthKitData> GetAllHealthKitData ()
		{
			var sql = @"Select * FROM HealthKitData;";
			using (var connection = new NpgsqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString;
					connection.Open();
					IEnumerable<HealthKitData> result =  connection.Query(sql);

					return result;
				}
				catch(NpgsqlException e)
				{
					Console.WriteLine (e);
				}
			}
			return  Enumerable.Empty<HealthKitData>();
		}

		public IEnumerable<HealthKitData> GetSpesificHealthKitData (int id)
		{
			var sql = @"Select * FROM HealthKitData WHERE PersonId = @PersonId";

			using (var connection = new NpgsqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString;
					connection.Open(); 
					IEnumerable<HealthKitData> result =  connection.Query(sql, new {PersonId = id});

					return result;
				}
				catch(MySql.Data.MySqlClient.MySqlException e)
				{
					Console.WriteLine (e.ToString ());
					return  Enumerable.Empty<HealthKitData>(); 
				}
				finally
				{
					connection.Close();
				}
			}
		}

		public HealthKitData GetSpesificHealthKitDataRecord(int personId, int recordId)
		{
			throw new NotImplementedException ();
		}

		public void AddOrUpdateHealthKitDataToStorage (HealthKitData person)
		{
			var sql = @"INSERT INTO HealthKitData(PersonId,RecordingTimeStamp,BloodType,DateOfBirth,Sex,Height)
						VALUES (@PersonId, @RecordingTimeStamp,@BloodType,@DateOfBirth,@Sex,@Height);";

			using (var connection = new NpgsqlConnection ()) 
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

